using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private Rigidbody TargetRigid;
    private GameObject TargetHook;

    private float HookPosition;

    private LineRenderer[] LRs = new LineRenderer[2];

    private void OnTriggerEnter(Collider other)
    {
        if (TargetHook)
        {
            return;
        }
        if (other.gameObject.name == "hook")
        {
            
            TargetHook = other.gameObject;
            TargetRigid = other.gameObject.transform.parent.parent.GetComponent<Rigidbody>();
            HookPosition = transform.InverseTransformPoint(other.transform.position).z * transform.localScale.z;
            Debug.Log("hook aircraft on board on: "+ HookPosition.ToString());
            LRs[0].enabled = true;
            LRs[1].enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LRs[0] = transform.parent.Find("Line0").gameObject.GetComponent<LineRenderer>();
        LRs[1] = transform.parent.Find("Line1").gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetHook)
        {
            LRs[0].SetPosition(0, TargetHook.transform.position);
            LRs[0].SetPosition(1, transform.position + transform.right * 17 + transform.forward * HookPosition);
            LRs[1].SetPosition(0, TargetHook.transform.position);
            LRs[1].SetPosition(1, transform.position - transform.right * 17 + transform.forward * HookPosition);
        }
    }
    void FixedUpdate()
    {
        if (TargetHook)
        {
            if (TargetRigid.velocity.z < 15f)
            {
                TargetHook = null;
                TargetRigid = null;
                LRs[0].enabled = false;
                LRs[1].enabled = false;
            }

            float coeff = Mathf.Pow(TargetRigid.velocity.magnitude, 1.5f) * 20;
            TargetRigid.AddForceAtPosition((transform.position + transform.right * 17 + transform.forward * HookPosition - TargetHook.transform.position) * coeff,
                                            TargetHook.transform.position);
            TargetRigid.AddForceAtPosition((transform.position - transform.right * 17 + transform.forward * HookPosition - TargetHook.transform.position) * coeff,
                                            TargetHook.transform.position);
        }
        
    }

    void OnDrawGizmos()
    {
    }
}
