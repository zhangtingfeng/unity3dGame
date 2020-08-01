using UnityEngine;
using System.Collections;


public class SelectContent : MonoBehaviour {

    public static ArrayList ContentList = new ArrayList() ;

    public void setContent(string selected)
    {
        // TrainTime = selected;
        if (ContentList.Contains(selected))
        {
            ContentList.Remove(selected);
        }
        else {
            ContentList.Add(selected);
        }
        string str = string.Join(",", (string[])ContentList.ToArray(typeof(string)));

        string strpersistentDataPath = Application.persistentDataPath + "/oliverData.ini";
        Assets.Script.MyIni.WritePrivateProfileString("GameContent", "Content", str, strpersistentDataPath);

        Debug_Log.Call_WriteLog("SelectContent=" + selected, selected.toString(), "Unity");
        Debug.Log(selected);



        //if (selected)
        //{

        //}
        //else {
        //}
    }
    
   
    // Use this for initialization

}
