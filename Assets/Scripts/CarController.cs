using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum CarDriveType
{
    frontWheelDrive,
    rearWheelDrive,
    fourWheelDrive
}

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[4];
    [SerializeField] private GameObject[] wheelMeshes = new GameObject[4];

    [Header("Car Movement Settings")]
    [SerializeField] private CarDriveType driveType;
    [SerializeField] private Vector3 centerOfMassOffset;
    [SerializeField] private float maxSteerAngle = 30.0f;

    [Tooltip("0 is raw physics, 1 means tyres will grip in the direction the car is facing")]
    [SerializeField] [Range(0, 1)] private float steerHelper = 0.0f;

    [Tooltip("0 is no traction control, 1 is full traction control")]
    [SerializeField] [Range(0, 1)] private float tractionControl = 0.0f;

    [SerializeField] private float downForce = 100.0f; // Force to keep the car on the ground
    [SerializeField] private float maxSpeed = 200.0f;
    [SerializeField] private float totalTorque = 2500.0f; // Controls acceleration of car
    [SerializeField] private float slipLimit = 0.3f;

    private float steerAngle = 0.0f;
    private float oldRotation;
    private float currentTorque;
    private Quaternion[] wheelMeshLocalRotations;
    private Rigidbody rigidBody;

    private void Start()
    {
        wheelMeshLocalRotations = new Quaternion[4];

        for (int i = 0; i < 4; i++)
        {
            wheelMeshLocalRotations[i] = wheelMeshes[i].transform.localRotation;
        }

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.centerOfMass = centerOfMassOffset;

        currentTorque = totalTorque - (tractionControl * totalTorque);
    }

    public void Move(float steering, float accel)
    {
        UpdateWheelMeshes();

        // Clamp input values
        steering = Mathf.Clamp(steering, -1.0f, 1.0f);
        accel = Mathf.Clamp(accel, 0.0f, 1.0f);

        // Set steering on the front wheels (wheels 0 and 1 must be front wheels)
        steerAngle = steering * maxSteerAngle;
        wheelColliders[0].steerAngle = steerAngle;
        wheelColliders[1].steerAngle = steerAngle;

        SteerHelper();
        ApplyTorque(accel);
        CapSpeed();
        AddDownForce();
        TractionControl();

    }

    public void ApplyForwardImpulse(float forceSize)
    {
        rigidBody.AddForce(transform.forward * forceSize, ForceMode.Impulse);
    }

    private void UpdateWheelMeshes()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;

            wheelColliders[i].GetWorldPose(out pos, out quat);
            wheelMeshes[i].transform.position = pos;
            wheelMeshes[i].transform.rotation = quat;
        }
    }

    private void CapSpeed()
    {
        float speed = rigidBody.velocity.magnitude;

        if (speed > maxSpeed)
        {
            rigidBody.velocity = maxSpeed * rigidBody.velocity.normalized;
        }
    }

    private void ApplyTorque(float accel)
    {
        float thrustTorque;

        // Apply torque based on drive type
        switch (driveType)
        {
            case CarDriveType.fourWheelDrive:
            {
                thrustTorque = accel * (currentTorque / 4.0f);

                for (int i = 0; i < 4; i++)
                {
                    wheelColliders[i].motorTorque = thrustTorque;
                }

                break;
            }

            case CarDriveType.frontWheelDrive:
            {
                thrustTorque = accel * (currentTorque / 2.0f);
                wheelColliders[0].motorTorque = wheelColliders[1].motorTorque = thrustTorque;
                break;
            }

            case CarDriveType.rearWheelDrive:
            {
                thrustTorque = accel * (currentTorque / 2.0f);
                wheelColliders[2].motorTorque = wheelColliders[3].motorTorque = thrustTorque;
                break;
            }
        }
    }

    private void SteerHelper()
    {
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelHit;
            wheelColliders[i].GetGroundHit(out wheelHit);

            if (wheelHit.normal == Vector3.zero) { return; } // If wheels aren't on the ground, don't help with steering
        }

        // Avoid gimbal lock problems that will cause a sudden shift in direction
        if (Mathf.Abs(oldRotation - transform.eulerAngles.y) < 10.0f)
        {
            float turnAdjust = (transform.eulerAngles.y - oldRotation) * steerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnAdjust, Vector3.up);
            rigidBody.velocity = velRotation * rigidBody.velocity;
        }

        oldRotation = transform.eulerAngles.y;
    }

    private void AddDownForce()
    {
        rigidBody.AddForce(-transform.up * downForce * rigidBody.velocity.magnitude);
    }

    private void TractionControl()
    {
        WheelHit wheelHit;

        switch (driveType)
        {
            case CarDriveType.fourWheelDrive:
                {
                    for (int i = 0; i < 4; i++)
                    {
                        wheelColliders[i].GetGroundHit(out wheelHit);
                        AdjustTorque(wheelHit.forwardSlip);
                    }
                    break;
                }

            case CarDriveType.rearWheelDrive:
                {
                    wheelColliders[2].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    wheelColliders[3].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    break;
                }

            case CarDriveType.frontWheelDrive:
                {
                    wheelColliders[0].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    wheelColliders[1].GetGroundHit(out wheelHit);
                    AdjustTorque(wheelHit.forwardSlip);

                    break;
                }
        }
    }

    private void AdjustTorque(float forwardSlip)
    {
        if (forwardSlip >= slipLimit && currentTorque >= 0.0f)
        {
            currentTorque -= 10.0f * tractionControl;
        }
        else
        {
            currentTorque += 10.0f * tractionControl;

            if (currentTorque > totalTorque)
            {
                currentTorque = totalTorque;
            }
        }
    }

    public void StopAllWheels()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = 0.0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position +  transform.rotation * centerOfMassOffset, 0.1f);
    }
}
