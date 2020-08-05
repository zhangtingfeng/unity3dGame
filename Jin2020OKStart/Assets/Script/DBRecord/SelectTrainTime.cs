using UnityEngine;
using System.Collections;


public class SelectTrainTime : MonoBehaviour
{

    public static int TrainTime = -1;

    public void setTrainTime(int selected)
    {
        TrainTime = selected;
        Assets.Script.MyIni ini = new Assets.Script.MyIni();
        ini.WriteIniContent("GameContent", "LengthofTime", TrainTime.toString());

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
