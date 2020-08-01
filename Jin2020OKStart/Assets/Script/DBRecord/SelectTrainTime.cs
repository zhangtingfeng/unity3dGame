using UnityEngine;
using System.Collections;


public class SelectTrainTime : MonoBehaviour {

    public static int TrainTime = -1;

    public void setTrainTime(int selected)
    {
        TrainTime = selected;
        string strpersistentDataPath = Application.persistentDataPath + "/oliverData.ini";
        Assets.Script.MyIni.WritePrivateProfileString("GameContent", "LengthofTime", TrainTime.toString(), strpersistentDataPath);

        Debug_Log.Call_WriteLog("TrainTime=" + TrainTime, selected.toString(), "Unity");
        Debug.Log(selected);



        //if (selected)
        //{

        //}
        //else {
        //}
    }
    
   
    // Use this for initialization

}
