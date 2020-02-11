using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SimpleCarController : MonoBehaviour
{
    private float steeringAngle;

    public WheelCollider frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;
    public Transform frontLeftTransform, frontRightTransform, backLeftTransform, backRightTransform;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public float downForce = 100.0f;

    public Image chargeBar;
    public float chargeTime = 1.0f;
    private float chargeAmount = 0.0f;
    public bool isCharging = false;
    private bool chargingUp = true;

    public AudioSource honkAudioSource;
    private InputMaster controls;

    public float impulseAmount = 1.0f;

    public float maxVelocityToStartCharge = 1.0f;

    [Range(0, 1)] [SerializeField] private float steerHelper; // 0 is raw physics, 1 the car will grip in the direction it is facing
    private float oldRotation;

    private float ChargeAmount
    {
        get { return chargeAmount; }
        set
        {
            chargeAmount = value;
            chargeBar.fillAmount = chargeAmount;
        }
    }

    // Value to control decaying acceleration
    private float accelAmount = 0.0f;
    public float accelDecayTime = 2.0f;

    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        ChargeAmount = 0.0f;
        controls = new InputMaster();

        controls.Player1.Enable();
        controls.Player1.Charge.performed += _ => ChargeInput();

        controls.Player1.Turning.performed += ctx => Steer(ctx.ReadValue<float>());
    }

    public void GetInput()
    {

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            honkAudioSource.Play();
        }
    }

    private void Steer(float horInput)
    {
        steeringAngle = maxSteerAngle * horInput;
        
    }

    private void Accelerate()
    {
        MoveCar(accelAmount * motorForce);
    }

    private void MoveCar(float amount)
    {
        backLeftWheel.motorTorque = amount;
        backRightWheel.motorTorque = amount;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(backLeftWheel, backLeftTransform);
        UpdateWheelPose(backRightWheel, backRightTransform);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 pos = _transform.position;
        Quaternion quat = _transform.rotation;

        _collider.GetWorldPose(out pos, out quat);

        _transform.position = pos;
        _transform.rotation = quat;
    }

    private void AddDownForce()
    {
        rigidBody.AddForce(-transform.up * downForce * rigidBody.velocity.magnitude);
    }

    private void Update()
    {
        accelAmount = Mathf.Clamp(accelAmount - (Time.deltaTime / accelDecayTime), 0.0f, 1.0f);
        GetInput();

        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;

        ChargingUpdate();
    }

    private void FixedUpdate()
    {
        Accelerate();
        AddDownForce();
        SteerHelper();
        UpdateWheelPoses();
    }

    private void ChargeInput()
    {
        bool canCharge = false;

        if (rigidBody.velocity.magnitude < maxVelocityToStartCharge)
        {
            canCharge = true;
        }

        if (isCharging)
        {
            StopCharging();
        }
        else if (!isCharging && canCharge)
        {
            StartCharging();
        }
    }

    private void StartCharging()
    {
        rigidBody.velocity = Vector3.zero;
        isCharging = true;
        chargingUp = true;
        ChargeAmount = 0.0f;
    }

    private void StopCharging()
    {
        isCharging = false;

        accelAmount = chargeAmount;
        rigidBody.AddForce(transform.forward * chargeAmount * impulseAmount, ForceMode.Impulse);

        ChargeAmount = 0.0f; 
    }

    private void ChargingUpdate()
    {
        if (isCharging)
        {
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
    }

    private void SteerHelper()
    {
        WheelHit wheelHit;

        frontLeftWheel.GetGroundHit(out wheelHit);

        // Wheels aren't on the ground so don't realign rigidbody velocity
        if (wheelHit.normal == Vector3.zero) { return; }

        frontRightWheel.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero) { return; }

        backLeftWheel.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero) { return; }

        backRightWheel.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero) { return; }

        if (Mathf.Abs(oldRotation - transform.eulerAngles.y) < 10.0f)
        {
            var turnAdjust = (transform.eulerAngles.y - oldRotation) * steerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnAdjust, Vector3.up);
            rigidBody.velocity = velRotation * rigidBody.velocity;
        }

        oldRotation = transform.eulerAngles.y;
    }

}
