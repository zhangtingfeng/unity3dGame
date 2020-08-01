using UnityEngine;
using System.Collections;


public class SelectBoyGirl : MonoBehaviour {

    public static bool boolboy = true;

    public void setBoy(bool selected)
    {
        boolboy = selected;

        Debug_Log.Call_WriteLog("boolboy="+ boolboy, selected.toString(), "Unity");
        Debug.Log(selected);
        if (selected)
        {

        }
        else {
        }
    }
    
   
    // Use this for initialization

}
