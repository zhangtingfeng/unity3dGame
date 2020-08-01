using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeCamera : MonoBehaviour
{

    // Use this for initialization
    void Awake111()
    {
        //Screen.fullScreen = true;
        //if (Display.displays.Length > 1)
        //{
        //    Display.displays[1].Activate();
        //    Display.displays[1].SetRenderingResolution(Display.displays[1].systemWidth, Display.displays[1].systemHeight);
        //}
        if (Display.displays.Length > 2)
        {
            Display.displays[2].Activate();
            Display.displays[2].SetRenderingResolution(Display.displays[2].systemWidth, Display.displays[2].systemHeight);
        }
    }


}
