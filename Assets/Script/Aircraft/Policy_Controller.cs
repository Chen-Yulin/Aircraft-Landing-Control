using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policy_Controller : MonoBehaviour
{
    public Aircraft_Controller aircraft;

    public void Calibrate_H_Tail()
    {
        aircraft.PitchAngle = aircraft.AoA;
    }


    public void KeepPitch(float p)
    {
        aircraft.PitchAngle += 5 * (p - aircraft.Pitch) / Mathf.Exp(10 * aircraft.PitchSpeed * Mathf.Sign(p - aircraft.Pitch));
    }
    public void KeepRow(float r)
    {
        aircraft.RowAngle =5 * (r - aircraft.Row) / Mathf.Exp(10 * aircraft.RowSpeed * Mathf.Sign(r - aircraft.Row));
    }
    public void KeepYaw(float y)
    {
        aircraft.YawAngle = 5 * (y - aircraft.Yaw) / Mathf.Exp(10 * aircraft.YawSpeed * Mathf.Sign(y - aircraft.Yaw));
    }


    // Start is called before the first frame update
    void Start()
    {
        aircraft = GetComponent<Aircraft_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        Calibrate_H_Tail();
        KeepPitch(0f);
        KeepRow(0f);
    }
    private void FixedUpdate()
    {
        
    }
}
