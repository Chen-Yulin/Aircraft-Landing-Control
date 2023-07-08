using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Dynamic_MainLift: MonoBehaviour
{
    public float AoA = 0;
    public float WingArea = 50;
    public float AirDensity = 1f;

    protected Vector3 lift_direction;
    protected Vector3 drag_direction;

    protected Transform Aircraft;
    protected Rigidbody rb;

    public void UpdatePara()
    {
        Vector3 velocity_yz = Vector3.ProjectOnPlane(rb.velocity, transform.right);
        AoA = Vector3.Angle(velocity_yz, transform.forward);
        if (Vector3.Dot(velocity_yz, transform.up) > 0)
        {
            AoA = -AoA;
        }
        lift_direction = Vector3.Cross(rb.velocity, transform.right).normalized;
        drag_direction = -rb.velocity.normalized;
    }

    public virtual float CalculateLift()
    {
        float CL = CalculateCL();
        float Lift = 0.5f * AirDensity * rb.velocity.sqrMagnitude * WingArea * CL;
        return Lift;
    }
    public float CalculateDrag()
    {
        float CD = CalculateCD();
        float Drag = 0.5f * AirDensity * rb.velocity.sqrMagnitude * WingArea * CD;
        return Drag;
    }

    public float CalculateCL(int WingConst = 5)
    {
        return (-0.7f * Mathf.Atan(0.1f * Mathf.Abs(AoA)-2)+1)
                * 6f * Mathf.Sin(0.01f * (AoA + WingConst));
    }
    public float CalculateCD()
    {
        return 1f / 3000f * AoA * AoA + 0.008f;
    }


    // Start is called before the first frame update
    void Start()
    {
        Aircraft = transform.parent.parent.transform;
        rb = Aircraft.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        UpdatePara();
        rb.AddForceAtPosition(CalculateLift() * lift_direction + CalculateDrag() * drag_direction, transform.position, ForceMode.Force);
    }

    private void OnDrawGizmos()
    {
        if (rb)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + CalculateLift() * lift_direction * 0.0006f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + CalculateDrag() * drag_direction * 0.0006f);
        }

    }
}
