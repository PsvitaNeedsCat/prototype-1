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
    [SerializeField] private GameObject chargeBarObj;
    [SerializeField] private Image chargeBarFilled;
    [SerializeField] private Image chargeBarBG;
    [SerializeField] private TextMeshProUGUI currentLapText;
    [SerializeField] private TextMeshProUGUI totalLapsText;
    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject collisionEffect;
    [SerializeField] private GameObject squashObject; // Object to be squashed and stretched during charging
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip chargeUp;
    [SerializeField] private AudioClip chargeDown;
    [SerializeField] private AudioClip chargeRelease;
    [SerializeField] private AudioClip bonusBoost;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource sfxSource2;
    [SerializeField] private AudioSource engineSource;
    [SerializeField] private AudioSource driftSource;
    [SerializeField] private AudioSource chargeSource;
    public GameObject[] wheelSparks;


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
    public AnimationCurve driftVolumeCurve; // Curve representing drift noise volume change based on drift angle

    [HideInInspector] public uint strokes = 0;
    [HideInInspector] public float timer = 0.0f;

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
    private bool finished = false;
    private bool started = false;
    [HideInInspector] public RespawnCheckpoint lastRespawnCheckpoint;
    public bool boostingAllowed = false;

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
            chargeBarFilled.fillAmount = chargeAmount;
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
        
        if (playerNumber == 1)
        {
            controls.Player1.Enable();

            controls.Player1.ChargePress.performed += _ => StartCharging();
            controls.Player1.ChargePress.canceled += _ => StopCharging();

            controls.Player1.HornPress.performed += _ => PressHorn();
            controls.Player1.HornPress.canceled += _ => ReleaseHorn();

            controls.Player1.Turning.performed += ctx => Steer(ctx.ReadValue<float>());
            controls.Player1.Turning.canceled += ctx => Steer(ctx.ReadValue<float>());
        }
        else // 2
        {
            controls.Player2.Enable();

            controls.Player2.ChargePress.performed += _ => StartCharging();
            controls.Player2.ChargePress.canceled += _ => StopCharging();

            controls.Player2.HornPress.performed += _ => PressHorn();
            controls.Player2.HornPress.canceled += _ => ReleaseHorn();

            controls.Player2.Turning.performed += ctx => Steer(ctx.ReadValue<float>());
            controls.Player2.Turning.canceled += ctx => Steer(ctx.ReadValue<float>());
        }

        // Set controller
        if (Gamepad.all.Count >= playerNumber)
        {
            controls.devices = new[] { Gamepad.all[playerNumber - 1], Keyboard.all[0] };
        }
        else
        {
            controls.devices = new[] { Keyboard.all[0] };
        }

        // Set horn
        DontDestroyScript dontDestroy = FindObjectOfType<DontDestroyScript>();
        if (dontDestroy)
        {
            hornScript.ChangeHorn(dontDestroy.horns[playerNumber - 1]);
        }        
    }

    private void Start()
    {
        totalLapsText.text = GameManager.Instance.numLaps.ToString();
        numCheckpoints = GameManager.Instance.numCheckpoints;
    }

    private void Update()
    {
        ChargeBarUpdate();
        AccelerationUpdate();
        ChargingUpdate();
    }

    private void FixedUpdate()
    {
        if (!finished && started) timer += Time.fixedDeltaTime;

        // Debug
        //if (!finished && Input.GetKey(KeyCode.J) && playerNumber == 1) { finished = true; Debug.Log("Player 1 finished"); GameManager.Instance.PlayerFinished(1); }
        //else if (!finished && Input.GetKey(KeyCode.K) && playerNumber == 2) { finished = true; Debug.Log("Player 2 finished"); GameManager.Instance.PlayerFinished(2); }

        UpdateCurves();
        CheckCrash();
        CheckDrift();

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

    private void UpdateCurves()
    {
        float playerSpeed = rigidBody.velocity.magnitude;
        float clampedSpeed = Mathf.Clamp(playerSpeed / 20.0f, 0.0f, 1.0f);
        playerAnimator.SetFloat("Speed", clampedSpeed);
        targetFOV = speedFOVCurve.Evaluate(clampedSpeed);

        float deltaFOV = targetFOV - followCam.m_Lens.FieldOfView;
        followCam.m_Lens.FieldOfView += deltaFOV / 6.5f;

        targetVolume = engineVolumeCurve.Evaluate(clampedSpeed);
        targetPitch = enginePitchCurve.Evaluate(clampedSpeed);

        float deltaVolume = targetVolume - engineSource.volume;
        engineSource.volume += deltaVolume / 5.0f;

        float deltaPitch = targetPitch - engineSource.pitch;
        engineSource.pitch += deltaPitch / 5.0f;
    }

    private void CheckCrash()
    {
        float deltaSpeed = lastFrameSpeed - rigidBody.velocity.magnitude;

        if (deltaSpeed > 10.0f)
        {
            if (!(stunImmuneTimer > 0.01f))
            {
                stunnedTimer = StunDuration(deltaSpeed);
                if (isCharging) { StopCharging(); }

                GameObject effect = Instantiate(collisionEffect, this.transform); //, Quaternion.identity, null);
                GameObject.Destroy(effect, 5.0f);

                sfxSource.PlayOneShot(crashSound, stunnedTimer * 2.0f);
            }
        }
    }

    private void CheckDrift()
    {
        if (rigidBody.velocity.magnitude < 5.0f) 
        {
            driftSource.volume = 0.0f;

            for (int i = 0; i < wheelSparks.Length; i++)
            {
                wheelSparks[i].transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }

            return;
        }

        Vector3 velocityDir = rigidBody.velocity.normalized;
        velocityDir.y = 0.0f;

        Vector3 facingDir = transform.forward.normalized;
        facingDir.y = 0.0f;

        float dotProduct = Mathf.Abs(Vector3.Dot(velocityDir, facingDir));
        driftSource.volume = driftVolumeCurve.Evaluate(Mathf.Clamp(dotProduct, 0.0f, 1.0f));

        for (int i = 0; i < wheelSparks.Length; i++)
        {
            wheelSparks[i].transform.localScale = new Vector3((1.0f - dotProduct) * 2.0f, (1.0f - dotProduct) * 2.0f, (1.0f - dotProduct) * 2.0f);
        }
    }

    private void StartCharging()
    {
        if (isCharging) { return; }
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
        squashObject.transform.DOScaleZ(1.25f, chargeTime).SetEase(Ease.InOutSine);
        squashObject.transform.DOScaleX(0.85f, chargeTime).SetEase(Ease.InOutSine);

        chargeBarObj.transform.DOKill();
        chargeBarObj.transform.DOScale(1.1f, chargeTime);

        chargeSource.Stop();
        chargeSource.PlayOneShot(chargeUp);
    }

    private void StopCharging()
    {
        if (!isCharging) { return; }
        if (!boostingAllowed) 
        {
            isCharging = false;
            ChargeAmount = 0.0f;
            return;
        }

        isCharging = false;
        rigidBody.isKinematic = false;
        rigidBody.velocity = Vector3.zero;
        accelAmount = chargeAmount;
        carController.CanSteer = true;
        carController.WheelCollidersFriction(true);
        cooldownTimer = chargeCooldown * chargeAmount;

        carController.OnReleaseCharge(chargeAmount);

        stunImmuneTimer = 0.1f;

        float longChargeBonus = Mathf.Clamp(bonusChargeCurve.Evaluate(normalisedTimeCharged), 0.0f, 999.0f);
        carController.ApplyForwardImpulse(releaseImpulseAmount * (chargeAmount + longChargeBonus));
        
        ChargeAmount = 0.0f;

        strokes += 1;

        squashObject.transform.DOKill();
        squashObject.transform.DOScaleZ(1.0f, 0.75f).SetEase(Ease.InOutSine);
        squashObject.transform.DOScaleX(1.0f, 0.75f).SetEase(Ease.InOutSine);

        chargeBarObj.transform.DOKill();
        chargeBarObj.transform.DOScale(1.0f, 0.25f);

        if (normalisedTimeCharged > 0.7f)
        {
            BonusBoost();
        }

        chargeSource.Stop();
        sfxSource.PlayOneShot(chargeRelease, 1.0f);
    }

    private void BonusBoost()
    {
        sfxSource2.Stop();
        sfxSource2.PlayOneShot(bonusBoost, 0.6f);

        chargeBarObj.transform.DOKill();
        chargeBarObj.transform.DOScale(1.25f, 0.2f).SetEase(Ease.InOutElastic).OnComplete(() => chargeBarObj.transform.DOScale(1.0f, 0.2f).SetEase(Ease.InOutElastic));
        //chargeBarObj.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 5.0f), 0.1f).SetEase(Ease.InOutElastic).OnComplete(() => chargeBarObj.transform.DOLocalRotate(new Vector3(0.0f, 0.0f, 0.0f), 0.1f).SetEase(Ease.InOutElastic));
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
                chargeSource.Stop();
                chargeSource.PlayOneShot(chargeDown);

                chargeBarObj.transform.DOKill();
                chargeBarObj.transform.DOScale(1.0f, chargeTime);

                squashObject.transform.DOKill();
                squashObject.transform.DOScaleZ(1.0f, chargeTime).SetEase(Ease.InOutSine);
                squashObject.transform.DOScaleX(1.0f, chargeTime).SetEase(Ease.InOutSine);
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
                chargeSource.Stop();
                chargeSource.PlayOneShot(chargeUp);

                chargeBarObj.transform.DOKill();
                chargeBarObj.transform.DOScale(1.1f, chargeTime);

                squashObject.transform.DOKill();
                squashObject.transform.DOScaleZ(1.25f, chargeTime).SetEase(Ease.InOutSine);
                squashObject.transform.DOScaleX(0.85f, chargeTime).SetEase(Ease.InOutSine);
            }
        }
    }

    private void ChargeBarUpdate()
    {
        if (cooldownTimer > 0.001f)
        {
            chargeBarFilled.transform.localScale = Vector3.zero;
            chargeBarBG.transform.localScale = Vector3.zero;
        }
        else
        {
            chargeBarFilled.transform.localScale = Vector3.one;
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

    //public float Respawn()
    //{
    //    if (IsRespawning) { return ; }

    //    float respawnTime = carController.RespawnCar();
    //    stunImmuneTimer = respawnTime;
    //    return respawnTime;
    //}

    public void PassedCheckpoint(int checkpointNum)
    {
        if (finished) return;

        // Check if in order
        if (checkpointNum == lastCheckpointPassed + 1)
        {
            // Check if lap complete
            if (checkpointNum == numCheckpoints)
            {
                if (CurrentLapNumber == GameManager.Instance.numLaps)
                {
                    GameManager.Instance.PlayerFinished(playerNumber);
                    // SetInputControl(false);
                    boostingAllowed = false;
                    finished = true;
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
            if (playerNumber == 1)
            {
                controls.Player1.Enable();
            }
            else
            {
                controls.Player2.Enable();
            }
        }
        else
        {
            if (playerNumber == 1)
            {
                controls.Player1.Disable();
            }
            else
            {
                controls.Player2.Disable();
            }

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

    public void StartTiming()
    {
        started = true;
    }
}
