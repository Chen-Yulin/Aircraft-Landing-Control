using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_Controller : MonoBehaviour
{
    public float InitialSpeed = 100;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.Find("MassCenter").localPosition;
        rb.velocity = InitialSpeed*transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
