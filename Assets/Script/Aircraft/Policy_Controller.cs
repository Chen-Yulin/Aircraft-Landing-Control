using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Policy_Controller : MonoBehaviour
{
    public class Target_Route
    {
        public Vector3 Destination;
        public Vector3 direction;
        public float speed = 80;
    }

    public Aircraft_Controller aircraft;
    public GameObject CV;
    public Target_Route route = new Target_Route();

    private Vector3 preAircraftVel = Vector3.zero;

    bool firstFrame = true;

    public void Calibrate_Stablizer()
    {
        aircraft.PitchAngle = aircraft.AoA;
        aircraft.RowAngle = 0;
        aircraft.YawAngle = 0;
    }


    public void KeepPitch(float p)
    {
        aircraft.PitchAngle += 5 * (p - aircraft.Pitch) / Mathf.Exp(10 * aircraft.PitchSpeed * Mathf.Sign(p - aircraft.Pitch));
    }
    public void KeepRow(float r)
    {
        aircraft.RowAngle = 5 * (r - aircraft.Row) / Mathf.Exp(10 * aircraft.RowSpeed * Mathf.Sign(r - aircraft.Row));
    }
    public void KeepYaw(float y)
    {
        aircraft.YawAngle = -5 * (y - aircraft.Yaw) / Mathf.Exp(10 * aircraft.YawSpeed * Mathf.Sign(y - aircraft.Yaw));
    }

    public void ThrustKeepGliding(float p)// need the pitch to be positve
    {
        float velAngle = 90 - Vector3.Angle(aircraft.rb.velocity, Vector3.up);
        aircraft.Thrust += (p - velAngle)/500f;
    }

    public void ThrustApproachRoute()
    {
        Vector3 approachDirection = Vector3.ProjectOnPlane(route.Destination - transform.position, route.direction);
        //Debug.Log(approachDirection.y);
        ThrustKeepGliding(-3 + Mathf.Clamp(approachDirection.y / 10f, -5,5));
    }
    public void YawApproachRoute()
    {
        Vector3 approachDirection = Vector3.ProjectOnPlane(route.Destination - transform.position, route.direction);

        float distance = 0;

        if (Vector3.Angle(approachDirection, transform.right) < 90)
        {
            distance = Vector3.ProjectOnPlane(approachDirection, Vector3.up).magnitude;
        }
        else
        {
            distance = -Vector3.ProjectOnPlane(approachDirection, Vector3.up).magnitude;
        }


        KeepYaw(-8 + Mathf.Clamp(distance / 10f, -50, 50));
    }

    public void Update_Route()
    {
        route.Destination = CV.transform.position;
        route.direction = new Vector3(-0.14f, -0.05f, 0.99f).normalized;
        float axialVel = Vector3.Dot(aircraft.rb.velocity, route.direction);

        // calculate precise destination
        float FlightTime = 0;
        for (int i = 0; i < 10; i++)
        {
            FlightTime = Vector3.Dot(route.Destination - aircraft.transform.position,route.direction) / axialVel;
            route.Destination = CV.transform.position + 15 * FlightTime * Vector3.forward;
        }

        
    }



    // Start is called before the first frame update
    void Start()
    {
        aircraft = GetComponent<Aircraft_Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (firstFrame) { firstFrame = false; return; }
        Update_Route();




        Calibrate_Stablizer();
        KeepPitch(5f);
        KeepRow(-0f);
        YawApproachRoute();
        ThrustApproachRoute();
    }
    private void OnDrawGizmos()
    {
        if (aircraft)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(route.Destination, route.Destination - route.direction * 50000f);
        }
    }
}
