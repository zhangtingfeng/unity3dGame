using UnityEngine;
using System.Collections;


public class SelectTrainDifficulty : MonoBehaviour {

    public static int TrainDifficulty = -1;

    public void setTrainDifficulty(int selected)
    {
        string strpersistentDataPath = Application.persistentDataPath + "/oliverData.ini";

        TrainDifficulty = selected;
        Assets.Script.MyIni.WritePrivateProfileString("GameContent", "Speed", TrainDifficulty.toString(), strpersistentDataPath);

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
