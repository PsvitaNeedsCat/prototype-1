using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

internal enum CarDriveType
{
    frontWheelDrive,
    rearWheelDrive,
    fourWheelDrive
}

internal struct WheelColliderFrictionInfo
{
    public float forwardExtremumValue;
    public float forwardAsymptoteValue;
    public float sidewaysExtremumValue;
    public float sidewaysAsymptoteValue;

    public WheelColliderFrictionInfo(WheelCollider collider)
    {
        WheelFrictionCurve forwardCurve = collider.forwardFriction;
        WheelFrictionCurve sidewaysCurve = collider.sidewaysFriction;

        forwardExtremumValue = forwardCurve.extremumValue;
        forwardAsymptoteValue = forwardCurve.asymptoteValue;

        sidewaysExtremumValue = sidewaysCurve.extremumValue;
        sidewaysAsymptoteValue = sidewaysCurve.asymptoteValue;
    }
}

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WheelCollider[] wheelColliders = new WheelCollider[4];
    [SerializeField] private GameObject[] wheelMeshes = new GameObject[4];
    [SerializeField] private GameObject carMesh;

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

    [SerializeField] private float fallRespawnTime = 2.0f;
    [SerializeField] private float meshTurnAngle = 25.0f;
    [SerializeField] private float notGroundedRespawnTime = 3.0f;

    private bool isRespawning = false;
    private float steerAngle = 0.0f;
    private float oldRotation;
    private float currentTorque;
    private Quaternion[] wheelMeshLocalRotations;
    private Rigidbody rigidBody;
    private List<Vector3> lastGroundedFrames = new List<Vector3>();
    private int numGroundedFrames = 30;
    private bool isGrounded = false;
    private bool canSteer = true;
    private float notGroundedTime = 0.0f;

    private WheelColliderFrictionInfo[] wheelColliderFrictionInfos;

    public bool IsRespawning
    {
        get { return isRespawning; }
    }

    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    public bool CanSteer
    {
        get { return canSteer; }
        set { canSteer = value; }
    }

    private void Start()
    {
        wheelMeshLocalRotations = new Quaternion[4];
        wheelColliderFrictionInfos = new WheelColliderFrictionInfo[4];

        for (int i = 0; i < 4; i++)
        {
            wheelMeshLocalRotations[i] = wheelMeshes[i].transform.localRotation;
            wheelColliderFrictionInfos[i] = new WheelColliderFrictionInfo(wheelColliders[i]);
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

        if (!canSteer) { steering = 0.0f; }

        carMesh.transform.localRotation = Quaternion.Euler(0.0f, steering * meshTurnAngle, 0.0f);

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

    public void ApplyImpulse(Vector3 impulse)
    {
        rigidBody.AddForce(impulse, ForceMode.Impulse);
    }

    public void ApplyForce(Vector3 force)
    {
        rigidBody.AddForce(force, ForceMode.Force);
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

            if (wheelHit.normal == Vector3.zero)
            {
                isGrounded = false;
                notGroundedTime += Time.fixedDeltaTime;

                if (notGroundedTime > notGroundedRespawnTime)
                {
                    RespawnCar();
                    Debug.Log("Respawning car");
                }
                return;
            } // If wheels aren't on the ground, don't help with steering
        }

        isGrounded = true;
        notGroundedTime = 0.0f;

        // If all wheels are on ground, update last grounded transform
        lastGroundedFrames.Add(this.transform.position);
        if (lastGroundedFrames.Count > numGroundedFrames)
        {
            lastGroundedFrames.RemoveAt(0);
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

    public float RespawnCar()
    {
        StartCoroutine(RespawnSetKinematic());
        StartCoroutine(RespawnSetIsRespawning());

        RespawnCheckpoint respawnCheckpoint = GetComponent<Player>().lastRespawnCheckpoint;
        float yaw = respawnCheckpoint.transform.rotation.eulerAngles.y;

        transform.DOMove(respawnCheckpoint.transform.position, fallRespawnTime);
        // transform.DOLocalRotate(new Vector3(0.0f, yaw, 0.0f), fallRespawnTime);
        transform.DOLocalRotateQuaternion(Quaternion.Euler(0.0f, yaw, 0.0f), fallRespawnTime);

        return fallRespawnTime;
    }

    private IEnumerator RespawnSetKinematic()
    {
        rigidBody.isKinematic = true;

        yield return new WaitForSeconds(fallRespawnTime);

        rigidBody.isKinematic = false;
    }

    private IEnumerator RespawnSetIsRespawning()
    {
        isRespawning = true;

        yield return new WaitForSeconds(fallRespawnTime);

        isRespawning = false;
    }

    // Either set all wheel colliders to zero friction, or reset them to defaults
    public void WheelCollidersFriction(bool wantFriction)
    {
        for (int i = 0; i < 4; i++)
        {
            WheelCollider wheelCollider = wheelColliders[i];

            WheelFrictionCurve forwardCurve = wheelCollider.forwardFriction;
            WheelFrictionCurve sidewaysCurve = wheelCollider.sidewaysFriction;

            // Return to original values
            if (wantFriction)
            {
                WheelColliderFrictionInfo originalInfo = wheelColliderFrictionInfos[i];

                //forwardCurve.extremumValue = originalInfo.forwardExtremumValue;
                //forwardCurve.asymptoteValue = originalInfo.forwardAsymptoteValue;

                sidewaysCurve.extremumValue = originalInfo.sidewaysExtremumValue;
                sidewaysCurve.asymptoteValue = originalInfo.sidewaysAsymptoteValue;

                // tractionControl = 0.25f;
                steerHelper = 0.5f;
            }
            // Set frictions to minimum
            else
            {
                //forwardCurve.extremumValue = 0.0f;
                //forwardCurve.asymptoteValue = 0.0f;

                sidewaysCurve.extremumValue = 0.0f;
                sidewaysCurve.asymptoteValue = 0.0f;

                tractionControl = 0.0f;
                steerHelper = 0.0f;
            }

            wheelCollider.forwardFriction = forwardCurve;
            wheelCollider.sidewaysFriction = sidewaysCurve;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position +  transform.rotation * centerOfMassOffset, 0.1f);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) { return; }

        Gizmos.color = Color.red;

        for (int i = 0; i < lastGroundedFrames.Count; i++)
        {
            Gizmos.DrawWireSphere(lastGroundedFrames[i], 1.0f);
        }
    }
}
