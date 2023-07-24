using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public List<float> x_ErrRecord = new List<float>();
    public List<float> y_ErrRecord = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        x_ErrRecord.Clear();
        y_ErrRecord.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        string path = Application.dataPath;
        string x_data = string.Join("\n", x_ErrRecord);
        string y_data = string.Join("\n", y_ErrRecord);
        System.IO.File.WriteAllText(Path.Combine(path, "record", "x_err.txt"), x_data);
        System.IO.File.WriteAllText(Path.Combine(path, "record", "y_err.txt"), y_data);
    }

}
