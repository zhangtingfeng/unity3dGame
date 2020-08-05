using UnityEngine;
using System.Collections;


public class SelectTrainDifficulty : MonoBehaviour
{

    public static int TrainDifficulty = -1;

    public void setTrainDifficulty(int selected)
    {

        TrainDifficulty = selected;
        Assets.Script.MyIni ini = new Assets.Script.MyIni();
        ini.WriteIniContent("GameContent", "Speed", TrainDifficulty.toString());

        Debug_Log.Call_WriteLog("TrainDifficulty=" + TrainDifficulty, selected.toString(), "Unity");
        Debug.Log(selected);
        //if (selected)
        //{

        //}
        //else {
        //}
    }


    // Use this for initialization

}
