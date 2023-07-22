using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_Controller : MonoBehaviour
{
    public GameObject Left_H_Tail;
    public GameObject Right_H_Tail;
    public GameObject Left_V_Tail;
    public GameObject Right_V_Tail;
    public float InitialSpeed = 100;

    public ParticleSystem AB1;
    public ParticleSystem AB2;

    public Rigidbody rb;

    private float pitchAngle = 0;
    public float PitchAngle
    {
        get { return pitchAngle; } 
        set { pitchAngle = value; pitchAngle = Mathf.Clamp(value, -15, 15); }
    }// push down positive
    private float rowAngle = 0;
    public float RollAngle
    {
        get { return rowAngle; }
        set { rowAngle = value; rowAngle = Mathf.Clamp(value, -5, 5); }
    }// anti-clockwise positive
    private float yawAngle = 0;
    public float YawAngle
    {
        get { return yawAngle; }
        set { yawAngle = value; yawAngle = Mathf.Clamp(value, -20, 20); }
    }// head left positive
    private float thrust = 0f;
    public float Thrust
    {
        get { return thrust; }
        set { thrust = value; thrust = Mathf.Clamp(value, 0, 1); }
    }

    public bool keyboard = true;


    //state
    public float AirSpeed
    {
        get
        {
            return rb.velocity.magnitude;  
        }
    }
    public float AoA
    {
        get {
            float AoA;
            if (rb.velocity != Vector3.zero)
            {
                Vector3 velocity_yz = Vector3.ProjectOnPlane(rb.velocity, transform.right);
                AoA = Vector3.Angle(velocity_yz, transform.forward);
                if (Vector3.Dot(velocity_yz, transform.up) > 0)
                {
                    AoA = -AoA;
                }
            }
            else
            {
                AoA = 0;
            }
            

            return AoA; }
    }
    public float Roll // anti-clock wise positive
    {
        get
        {
            if (transform.eulerAngles.z > 180)
            {
                return transform.eulerAngles.z - 360;
            }
            else
            {
                return transform.eulerAngles.z;
            }
        }
    }
    public float RollSpeed
    {
        
        get { return rb.angularVelocity.z; }
    }
    public float Pitch // head up positive
    {
        get
        {
            if (transform.eulerAngles.x > 180)
            {
                return -transform.eulerAngles.x + 360;
            }
            else
            {
                return -transform.eulerAngles.x;
            }
        }
    }
    public float PitchSpeed
    {
        get { return -rb.angularVelocity.x; }
    }
    public float Yaw // head left positive
    {
        get
        {
            if (transform.eulerAngles.y > 180)
            {
                return transform.eulerAngles.y - 360;
            }
            else
            {
                return transform.eulerAngles.y;
            }
        }
    }
    public float YawSpeed
    {
        get { return rb.angularVelocity.y; }
    }



    public void UpdatePara()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.Find("MassCenter").localPosition;
        rb.velocity = InitialSpeed*(transform.forward + 0f* transform.right);
        Thrust = 0;
        AB1 = transform.Find("Afterburner 1").GetComponent<ParticleSystem>();
        AB2 = transform.Find("Afterburner 2").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboard)
        {
            if (Input.GetKey(KeyCode.W))
            {
                PitchAngle -= Time.deltaTime * 10;
            }else if (Input.GetKey(KeyCode.S))
            {
                PitchAngle += Time.deltaTime * 20f;
            }
            else
            {
                if (PitchAngle < 0.1f && PitchAngle > -0.1f)
                {
                    PitchAngle = 0;
                }
                else
                {
                    PitchAngle -= Time.deltaTime * 30 * (PitchAngle > 0 ? 1 : -1);
                }
            }
            PitchAngle = Mathf.Clamp(PitchAngle, -15,15);

            if (Input.GetKey(KeyCode.D))
            {
                RollAngle -= Time.deltaTime * 10;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                RollAngle += Time.deltaTime * 10;
            }
            else
            {
                if (RollAngle < 0.1f && RollAngle > -0.1f)
                {
                    RollAngle = 0;
                }
                else
                {
                    RollAngle -= Time.deltaTime * 20 * (RollAngle > 0 ? 1 : -1);
                }
            }
            RollAngle = Mathf.Clamp(RollAngle, -5, 5);
        }



    }

    private void FixedUpdate()
    {
        rb.angularDrag = 0.0001f * rb.velocity.sqrMagnitude;
        Left_H_Tail.transform.localRotation = Quaternion.Euler(PitchAngle + RollAngle, 0, 0);
        Right_H_Tail.transform.localRotation = Quaternion.Euler(PitchAngle - RollAngle, 0, 0);
        Left_V_Tail.transform.localRotation = Quaternion.Euler(0, YawAngle, 90);
        Right_V_Tail.transform.localRotation = Quaternion.Euler(0, YawAngle, 90);
        rb.AddForce(transform.forward * Thrust * 700000f);
        AB1.startLifetime = 0.03f + Thrust * 0.2f;
        AB2.startLifetime = 0.03f + Thrust * 0.2f;
    }

    private void OnDrawGizmos()
    {
        if (rb)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity.normalized * 5000f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5000f);
        }

    }
}
