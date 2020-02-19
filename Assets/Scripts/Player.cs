using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using DG.Tweening;

[RequireComponent(typeof(CarController))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image chargeBar;
    [SerializeField] private Image chargeBarBG;
    [SerializeField] private TextMeshProUGUI currentLapText;
    [SerializeField] private TextMeshProUGUI totalLapsText;
    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject collisionEffect;
    [SerializeField] private GameObject squashObject; // Object to be squashed and stretched during charging
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource engineSource;

    // public GameObject stunnedIndicator; // Temp

    [Header("Player Settings")]

    [Tooltip("Amount of time it takes to charge from empty to full")]
    [SerializeField] private float chargeTime = 1.0f;

    [Tooltip("Amount of force to apply when the player releases their charge (scaled by charge amount)")]
    [SerializeField] private float releaseImpulseAmount = 1.0f;

    [Tooltip("How long the player's 'wind-back' acceleration will decay over")]
    [SerializeField] private float accelDecayTime = 2.0f;

    [Tooltip("Cooldown for a fullycharged release - scales down based on how much you charged up")]
    [SerializeField] private float chargeCooldown = 2.0f;

    [SerializeField] [Range(0.1f, 10.0f)] private float turningSensitivity = 1.0f;

    [SerializeField] int playerNumber = 1;

    public AnimationCurve chargeUpCurve; // Base charge up curve
    public AnimationCurve bonusChargeCurve; // Curve representing how much bonus power you get from charging longer
    public AnimationCurve speedFOVCurve; // Curve representing camera FOV change based on speed
    public AnimationCurve enginePitchCurve; // Curve representing engine noise pitch change based on speed
    public AnimationCurve engineVolumeCurve; // Curve representing engine noise pitch change based on speed

    private float chargeAmount = 0.0f; // How full the charge bar is
    private float normalisedTimeCharged = 0.0f; // Amount of time spent charging
    private bool isCharging = false;
    private bool chargingUp = true; // Indicates direction of charging - after being fully charged, the bar will deplete
    private float accelAmount = 0.0f;
    private float steeringInput = 0.0f;
    private float targetFOV = 60.0f;
    private float targetVolume = 0.0f;
    private float targetPitch = 1.0f;

    private float cooldownTimer = 0.0f;
    private float stunnedTimer = 0.0f;
    private float stunImmuneTimer = 0.0f;
    private float lastFrameSpeed;

    private InputMaster controls;
    private Rigidbody rigidBody;
    private CarController carController;
    private HornScript hornScript;
    private int lapNum = 1;
    private int lastCheckpointPassed = 0;
    private int numCheckpoints;
    [HideInInspector] public RespawnCheckpoint lastRespawnCheckpoint;

    public bool IsRespawning
    {
        get { return carController.IsRespawning; }
    }

    private float ChargeAmount
    {
        get { return chargeAmount; }
        set
        {
            chargeAmount = value;
            chargeBar.fillAmount = chargeAmount;
        }
    }

    private int CurrentLapNumber
    {
        get { return lapNum; }
        set
        {
            lapNum = value;
            currentLapText.text = lapNum.ToString();
        }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
        hornScript = GetComponent<HornScript>();

        ChargeAmount = 0.0f;
        controls = new InputMaster();

        // Made a switch for future proofing
        switch (playerNumber)
        {
            case 1:
            {
                    controls.Player1.Enable();

                    controls.Player1.ChargePress.performed += _ => StartCharging();
                    controls.Player1.ChargeRelease.performed += _ => StopCharging();

                    controls.Player1.HornPress.performed += _ => PressHorn();
                    controls.Player1.HornRelease.performed += _ => ReleaseHorn();

                    controls.Player1.Turning.performed += ctx => Steer(ctx.ReadValue<float>());

                    // Set up devices (Gamepad and keyboard if gamepad is plugged in, else just a keyboard
                    controls.devices = (Gamepad.all.Count >= 1) ? new[] { Gamepad.all[0], Keyboard.all[0] } : controls.devices = new[] { Keyboard.all[0] };

                    // Give correct horn
                    if (GameObject.Find("DontDestroyObj"))
                        hornScript.ChangeHorn(GameObject.Find("DontDestroyObj").GetComponent<DontDestroyScript>().p1SelectedHorn);

                    break;
            }

            case 2:
            {
                    controls.Player2.Enable();

                    controls.Player2.ChargePress.performed += _ => StartCharging();
                    controls.Player2.ChargeRelease.performed += _ => StopCharging();

                    controls.Player2.HornPress.performed += _ => PressHorn();
                    controls.Player2.HornRelease.performed += _ => ReleaseHorn();

                    controls.Player2.Turning.performed += ctx => Steer(ctx.ReadValue<float>());

                    // Set up devices (Gamepad and keyboard if gamepad is plugged in, else just a keyboard
                    controls.devices = (Gamepad.all.Count >= 2) ? new[] { Gamepad.all[1], Keyboard.all[0] } : controls.devices = new[] { Keyboard.all[0] };

                    // Give player the correct horn sound
                    if (GameObject.Find("DontDestroyObj"))
                        hornScript.ChangeHorn(GameObject.Find("DontDestroyObj").GetComponent<DontDestroyScript>().p2SelectedHorn);

                    break;
            }

            default:
                break;
        }
        
    }

    private void Start()
    {
        totalLapsText.text = GameManager.Instance.numLaps.ToString();
        numCheckpoints = GameManager.Instance.numCheckpoints;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }

        ChargeBarUpdate();
        AccelerationUpdate();
        ChargingUpdate();
    }

    private void FixedUpdate()
    {
        float playerSpeed = rigidBody.velocity.magnitude;
        float clampedSpeed = Mathf.Clamp(playerSpeed / 20.0f, 0.0f, 1.0f);
        playerAnimator.SetFloat("Speed", clampedSpeed);
        targetFOV = speedFOVCurve.Evaluate(clampedSpeed);

        float deltaFOV = targetFOV - followCam.m_Lens.FieldOfView;
        followCam.m_Lens.FieldOfView += deltaFOV / 7.5f;

        targetVolume = engineVolumeCurve.Evaluate(clampedSpeed);
        targetPitch = enginePitchCurve.Evaluate(clampedSpeed);
        
        float deltaVolume = targetVolume - engineSource.volume;
        engineSource.volume += deltaVolume / 5.0f;

        float deltaPitch = targetPitch - engineSource.pitch;
        engineSource.pitch += deltaPitch / 5.0f;

        float deltaSpeed = lastFrameSpeed - rigidBody.velocity.magnitude;

        if (deltaSpeed > 10.0f)
        {
            

            if (!(stunImmuneTimer > 0.01f))
            {
                stunnedTimer = StunDuration(deltaSpeed);
                if (isCharging) { StopCharging(); }

                GameObject effect = Instantiate(collisionEffect, this.transform); //, Quaternion.identity, null);
                GameObject.Destroy(effect, 5.0f);

                sfxSource.PlayOneShot(crashSound);
            }
        }

        if (isCharging)
        {
            transform.Rotate(Vector3.up, steeringInput * turningSensitivity);
            carController.Move(0.0f, accelAmount);
        }
        else
        {
            carController.Move(steeringInput, accelAmount);
        }

        lastFrameSpeed = rigidBody.velocity.magnitude;

    }

    private void StartCharging()
    {
        if (cooldownTimer > 0.001f) { return; }
        if (!carController.IsGrounded) { return; }
        if (stunnedTimer > 0.01f && stunImmuneTimer < 0.01f) { return; }

        carController.StopAllWheels();
        carController.WheelCollidersFriction(false);
        carController.CanSteer = false;

        isCharging = true;
        chargingUp = true;
        ChargeAmount = 0.0f;
        normalisedTimeCharged = 0.0f;

        squashObject.transform.DOKill();
        squashObject.transform.DOScaleZ(1.25f, chargeTime).SetEase(Ease.OutElastic);
        squashObject.transform.DOScaleX(0.85f, chargeTime).SetEase(Ease.OutElastic);
    }

    private void StopCharging()
    {
        if (!isCharging) { return; }

        isCharging = false;
        rigidBody.isKinematic = false;
        rigidBody.velocity = Vector3.zero;
        accelAmount = chargeAmount;
        carController.CanSteer = true;
        carController.WheelCollidersFriction(true);
        cooldownTimer = chargeCooldown * chargeAmount;

        stunImmuneTimer = 0.1f;

        float longChargeBonus = Mathf.Clamp(bonusChargeCurve.Evaluate(normalisedTimeCharged), 0.0f, 999.0f);
        carController.ApplyForwardImpulse(releaseImpulseAmount * (chargeAmount + longChargeBonus));

        ChargeAmount = 0.0f;

        squashObject.transform.DOKill();
        squashObject.transform.DOScaleZ(1.0f, 0.75f).SetEase(Ease.OutElastic);
        squashObject.transform.DOScaleX(1.0f, 0.75f).SetEase(Ease.OutElastic);
    }

    void PressHorn()
    {
        hornScript.PlayHorn();
    }

    void ReleaseHorn()
    {
        hornScript.StopHorn();
    }

    private void ChargingUpdate()
    {
        stunnedTimer = Mathf.Clamp(stunnedTimer - Time.deltaTime, 0.0f, 5.0f);
        stunImmuneTimer = Mathf.Clamp(stunImmuneTimer - Time.deltaTime, 0.0f, 5.0f);

        //if (stunnedTimer > 0.01f && stunImmuneTimer < 0.01f) { stunnedIndicator.SetActive(true); }
        //else { stunnedIndicator.SetActive(false); }

        cooldownTimer = Mathf.Clamp(cooldownTimer - Time.deltaTime, 0.0f, chargeCooldown);

        if (!isCharging) { return; }

        float deltaCharge = Time.deltaTime / chargeTime;

        if (chargingUp)
        {
            normalisedTimeCharged = Mathf.Clamp(normalisedTimeCharged + deltaCharge, 0.0f, 1.0f);
            ChargeAmount = chargeUpCurve.Evaluate(normalisedTimeCharged);

            // If fully charged, start uncharging
            if (normalisedTimeCharged > 0.999f)
            {
                chargingUp = false;
            }
        }
        else
        {
            normalisedTimeCharged = Mathf.Clamp(normalisedTimeCharged - deltaCharge, 0.0f, 1.0f);
            ChargeAmount = chargeUpCurve.Evaluate(normalisedTimeCharged);

            // If empty charge, start charging up again
            if (normalisedTimeCharged < 0.001f)
            {
                chargingUp = true;
            }
        }
    }

    private void ChargeBarUpdate()
    {
        if (cooldownTimer > 0.001f)
        {
            chargeBar.transform.localScale = Vector3.zero;
            chargeBarBG.transform.localScale = Vector3.zero;
        }
        else
        {
            chargeBar.transform.localScale = Vector3.one;
            chargeBarBG.transform.localScale = Vector3.one;
        }
    }

    private void AccelerationUpdate()
    {
        accelAmount = Mathf.Clamp(accelAmount - (Time.deltaTime / accelDecayTime), 0.0f, 1.0f);
    }

    private void Steer(float horInput)
    {
        if (stunnedTimer > 0.01f && stunImmuneTimer < 0.01f) { horInput = 0.0f; return; }

        // Inside the deadzone
        steeringInput = (Mathf.Abs(horInput) <= 0.5f) ? 0.0f : steeringInput = horInput;
    }

    public float Respawn()
    {
        float respawnTime = carController.RespawnCar();
        stunImmuneTimer = respawnTime;
        return respawnTime;
    }

    public void PassedCheckpoint(int checkpointNum)
    {
        // Check if in order
        if (checkpointNum == lastCheckpointPassed + 1)
        {
            // Check if lap complete
            if (checkpointNum == numCheckpoints)
            {
                if (CurrentLapNumber == GameManager.Instance.numLaps)
                {
                    GameManager.Instance.raceComplete = true;
                    GameManager.Instance.winner = playerNumber;
                    return;
                }

                CurrentLapNumber++;
                lastCheckpointPassed = 0;
            }
            else
            {
                lastCheckpointPassed = checkpointNum;
            }
        }
    }


    public void SetInputControl(bool canInput)
    {
        if (canInput)
        {
            controls.Enable();
        }
        else
        {
            controls.Disable();

            if (isCharging)
            {
                StopCharging();
            }
        }
    }

    public void ApplyImpulse(Vector3 impulse)
    {
        carController.ApplyImpulse(impulse);
    }

    public void ApplyForce(Vector3 force)
    {
        carController.ApplyForce(force);
    }

    float StunDuration(float deltaSpeed)
    {
        if (deltaSpeed < 10.0f)
        {
            return 0.0f;
        }

        if (deltaSpeed > 30.0f)
        {
            return 30.0f;
        }

        float quotient = (deltaSpeed - 10.0f) / 20.0f;

        return (0.5f + (1.5f * quotient));
    }

    public void PassedRespawnCheckpoint(RespawnCheckpoint checkpoint)
    {
        lastRespawnCheckpoint = checkpoint;
    }
}
