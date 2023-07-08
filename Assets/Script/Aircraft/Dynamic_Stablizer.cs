using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Dynamic_Stablizer: Dynamic_MainLift
{
    public override float CalculateLift()
    {
        float CL = CalculateCL(0);
        float Lift = 0.5f * AirDensity * rb.velocity.sqrMagnitude * WingArea * CL;
        return Lift;
    }
}
