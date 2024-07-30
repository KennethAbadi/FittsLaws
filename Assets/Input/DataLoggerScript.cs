using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class DataLoggerScript : MonoBehaviour
{
    public string filename = "";
    public string participant_id = "";
    public enum Device { mouse, touchpad };
    public Device device;
    public enum Width { one, two, three };
    public Width width;
    public enum Distance { one, two, three };
    public Distance distance;
    public float time = 0.0f;
    public float finaltime = 0.0f;
    public int wrong = 0;

    private void Start()
    {
        filename = Application.dataPath + "/results" + participant_id + ".csv";
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // New Input System check
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            // Touch input detected
            device = Device.touchpad;
            Debug.Log("Touchscreen input detected.");
        }
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            // Mouse input detected
            device = Device.mouse;
            Debug.Log("Mouse input detected.");
        }

        // Legacy Input System check
        if (Input.touchCount > 0)
        {
            // Touch input detected
            device = Device.touchpad;
            Debug.Log("Touch input detected.");
        }
        else if (Input.GetMouseButtonDown(0))
        {
            // Mouse input detected
            device = Device.mouse;
            Debug.Log("Mouse or touchpad input detected.");
        }
    }

    public void WriteCSV()
    {
        TextWriter tw = new StreamWriter(filename, true);
        tw.WriteLine("" + participant_id + ", " + device + ", " + width + ", " + distance + ", " + finaltime + ", " + wrong);
        tw.Close();
        time = 0;
        wrong = 0;
        finaltime = 0;
    }
}
