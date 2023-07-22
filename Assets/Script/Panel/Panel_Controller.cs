using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Panel_Controller : MonoBehaviour
{
    public Aircraft_Controller AC;

    public TextMeshProUGUI AirSpeedText;
    public TextMeshProUGUI AoAText;
    public TextMeshProUGUI RowText;
    public TextMeshProUGUI RowVelText;
    public TextMeshProUGUI PitchText;
    public TextMeshProUGUI PitchVelText;
    public TextMeshProUGUI YawText;
    public TextMeshProUGUI YawVelText;

    // Start is called before the first frame update
    void Start()
    {
        AC = GameObject.Find("F-14").gameObject.GetComponent<Aircraft_Controller>();
        AirSpeedText = transform.Find("AirSpeed").Find("value").GetComponent<TextMeshProUGUI>();
        AoAText = transform.Find("AoA").Find("value").GetComponent<TextMeshProUGUI>();
        RowText = transform.Find("Row").Find("value").GetComponent<TextMeshProUGUI>();
        RowVelText = transform.Find("Row Vel").Find("value").GetComponent<TextMeshProUGUI>();
        PitchText = transform.Find("Pitch").Find("value").GetComponent<TextMeshProUGUI>();
        PitchVelText = transform.Find("Pitch Vel").Find("value").GetComponent<TextMeshProUGUI>();
        YawText = transform.Find("Yaw").Find("value").GetComponent<TextMeshProUGUI>();
        YawVelText = transform.Find("Yaw Vel").Find("value").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        AirSpeedText.text = AC.AirSpeed.ToString("F2");
        AoAText.text = AC.AoA.ToString("F2");
        RowText.text = AC.Roll.ToString("F2");
        RowVelText.text = AC.RollSpeed.ToString("F2");
        PitchText.text = AC.Pitch.ToString("F2");
        PitchVelText.text = AC.PitchSpeed.ToString("F2");
        YawText.text = AC.Yaw.ToString("F2");
        YawVelText.text = AC.YawSpeed.ToString("F2");
    }
}
