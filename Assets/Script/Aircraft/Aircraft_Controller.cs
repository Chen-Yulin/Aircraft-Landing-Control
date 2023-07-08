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
    private Rigidbody rb;

    public float PitchAngle = 0;// push down positive
    public float RowAngle = 0;// anti-clockwise positive

    public bool keyboard = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.Find("MassCenter").localPosition;
        rb.velocity = InitialSpeed*(transform.forward + 0f* transform.right);
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
                RowAngle -= Time.deltaTime * 5;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                RowAngle += Time.deltaTime * 5;
            }
            else
            {
                if (RowAngle < 0.1f && RowAngle > -0.1f)
                {
                    RowAngle = 0;
                }
                else
                {
                    RowAngle -= Time.deltaTime * 20 * (RowAngle > 0 ? 1 : -1);
                }
            }
            RowAngle = Mathf.Clamp(RowAngle, -5, 5);
        }

        Left_H_Tail.transform.localRotation = Quaternion.Euler(PitchAngle + RowAngle,0,0);
        Right_H_Tail.transform.localRotation = Quaternion.Euler(PitchAngle - RowAngle, 0, 0);

    }

    private void FixedUpdate()
    {
        rb.angularDrag = 0.0001f * rb.velocity.sqrMagnitude;
    }

    private void OnDrawGizmos()
    {
        if (rb)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + rb.velocity.normalized * 5000f);
        }

    }
}
