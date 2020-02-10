using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleCarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
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
    }

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !isCharging)
        {
            StartCharging();
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            StopCharging();
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            honkAudioSource.Play();
        }
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
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

        ChargingUpdate();
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        AddDownForce();
        UpdateWheelPoses();
    }

    private void StartCharging()
    {
        isCharging = true;
        chargingUp = true;
        ChargeAmount = 0.0f;
    }

    private void StopCharging()
    {
        isCharging = false;

        accelAmount = chargeAmount;

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
}
