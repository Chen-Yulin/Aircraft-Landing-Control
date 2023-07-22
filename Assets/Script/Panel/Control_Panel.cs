using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control_Panel : MonoBehaviour
{
    public GameObject Aircraft;

    public GameObject StickAnchor;
    public GameObject YawAnchor;
    public GameObject ThrottleAnchor;

    private Aircraft_Controller ac;

    // Start is called before the first frame update
    void Start()
    {
        ac = Aircraft.GetComponent<Aircraft_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        StickAnchor.transform.localPosition = new Vector3(-ac.RollAngle * 11, -ac.PitchAngle * 3.6f, 0);
        YawAnchor.transform.localPosition = new Vector3(-ac.YawAngle * 2.75f, 0, 0);
        ThrottleAnchor.transform.localPosition = new Vector3(0, ac.Thrust * 110f-55, 0);
    }
}
