using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CarController))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image chargeBar;
    [SerializeField] private TextMeshProUGUI currentLapText;
    [SerializeField] private TextMeshProUGUI totalLapsText;

    [Header("Player Settings")]

    [Tooltip("Amount of time it takes to charge from empty to full")]
    [SerializeField] private float chargeTime = 1.0f;

    [Tooltip("Amount of force to apply when the player releases their charge (scaled by charge amount)")]
    [SerializeField] private float releaseImpulseAmount = 1.0f;

    [Tooltip("How low the player's velocity must be before they can start a new charge")]
    [SerializeField] private float maxVelocityToStartCharging = 1.0f;

    [Tooltip("How long the player's 'wind-back' acceleration will decay over")]
    [SerializeField] private float accelDecayTime = 2.0f;

    [SerializeField] [Range(0.1f, 10.0f)] private float turningSensitivity = 1.0f;

    private float chargeAmount = 0.0f;
    private bool isCharging = false;
    private bool chargingUp = true; // Indicates direction of charging - after being fully charged, the bar will deplete
    private float accelAmount = 0.0f;
    private float steeringInput = 0.0f;

    private InputMaster controls;
    private Rigidbody rigidBody;
    private CarController carController;
    private int lapNum = 1;
    private int lastCheckpointPassed = 0;
    private int numCheckpoints;

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
        ChargeAmount = 0.0f;
        controls = new InputMaster();

        controls.Player1.Enable();

        controls.Player1.ChargePress.performed += _ => StartCharging();
        controls.Player1.ChargeRelease.performed += _ => StopCharging();

        controls.Player1.Turning.performed += ctx => Steer(ctx.ReadValue<float>());
    }

    private void Start()
    {
        totalLapsText.text = GameManager.Instance.numLaps.ToString();
        numCheckpoints = GameManager.Instance.numCheckpoints;
    }

    private void Update()
    {
        AccelerationUpdate();
        ChargingUpdate();
    }

    private void FixedUpdate()
    {
        if (isCharging)
        {
            transform.Rotate(Vector3.up, steeringInput * turningSensitivity);
            carController.Move(0.0f, accelAmount);
        }
        else
        {
            carController.Move(steeringInput, accelAmount);
        }

    }

    private void StartCharging()
    {
        if (rigidBody.velocity.magnitude > maxVelocityToStartCharging) { return; }

        carController.StopAllWheels();
        rigidBody.velocity = Vector3.zero;
        rigidBody.isKinematic = true;
        isCharging = true;
        chargingUp = true;
        ChargeAmount = 0.0f;
    }

    private void StopCharging()
    {
        if (!isCharging) { return; }

        isCharging = false;
        rigidBody.isKinematic = false;
        accelAmount = chargeAmount;
        carController.ApplyForwardImpulse(releaseImpulseAmount * chargeAmount);

        ChargeAmount = 0.0f;
    }

    private void ChargingUpdate()
    {
        if (!isCharging) { return; }

        float deltaCharge = Time.deltaTime / chargeTime;

        if (chargingUp)
        {
            ChargeAmount = Mathf.Clamp(chargeAmount + deltaCharge, 0.0f, 1.0f);

            // If fully charged, start uncharging
            if (chargeAmount > 0.999f)
            {
                chargingUp = false;
            }
        }
        else
        {
            ChargeAmount = Mathf.Clamp(chargeAmount - deltaCharge, 0.0f, 1.0f);

            // If empty charge, start charging up again
            if (chargeAmount < 0.001f)
            {
                chargingUp = true;
            }
        }
    }

    private void AccelerationUpdate()
    {
        accelAmount = Mathf.Clamp(accelAmount - (Time.deltaTime / accelDecayTime), 0.0f, 1.0f);
    }

    private void Steer(float horInput)
    {
        steeringInput = horInput;
    }

    public void Respawn()
    {
        carController.RespawnCar();
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
        }
    }
}
