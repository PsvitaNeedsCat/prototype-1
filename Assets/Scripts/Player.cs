using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CarController))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image chargeBar;

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

    private float ChargeAmount
    {
        get { return chargeAmount; }
        set
        {
            chargeAmount = value;
            chargeBar.fillAmount = chargeAmount;
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

    //private void ChargeInput()
    //{
    //    bool canCharge = false;

    //    if (rigidBody.velocity.magnitude < maxVelocityToStartCharging)
    //    {
    //        canCharge = true;
    //    }

    //    if (isCharging)
    //    {
    //        StopCharging();
    //    }
    //    else if (!isCharging && canCharge)
    //    {
    //        StartCharging();
    //    }
    //}

    private void StartCharging()
    {
        if (rigidBody.velocity.magnitude > maxVelocityToStartCharging) { return; }

        Debug.Log("Starting charging");

        carController.StopAllWheels();
        rigidBody.velocity = Vector3.zero;
        isCharging = true;
        chargingUp = true;
        ChargeAmount = 0.0f;
    }

    private void StopCharging()
    {
        if (!isCharging) { return; }

        Debug.Log("Stopping Charging");

        isCharging = false;

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
}
