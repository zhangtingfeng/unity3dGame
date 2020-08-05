using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectContent : MonoBehaviour
{

    //public static ArrayList ContentList = new ArrayList() ;

    public void setContent(string selected)
    {

        Toggle Content_Ele = GameObject.Find("Content_" + selected).transform.GetComponentsInChildren<Toggle>()[0];

        Debug_Log.Call_WriteLog(Content_Ele.isOn, "点击" + selected.toString(), "Unity");

        ArrayList ContentList = new ArrayList();
        Assets.Script.ReadIniPar ReadIniPargetParme = Assets.Script.getPar.getParme();
        ArrayList bContent = new ArrayList(ReadIniPargetParme.Content.Split(','));
        if (!System.String.IsNullOrEmpty(ReadIniPargetParme.Content))///如果读取到值了就赋值
        {
            ContentList = bContent;
        }

        // TrainTime = selected;
        if (ContentList.Contains(selected) && !Content_Ele.isOn)
        {
            ContentList.Remove(selected);
        }
        else if (!ContentList.Contains(selected) && Content_Ele.isOn)
        {
            ContentList.Add(selected);
        }
        string str = string.Join(",", (string[])ContentList.ToArray(typeof(string)));

        Assets.Script.MyIni ini = new Assets.Script.MyIni();
        ini.WriteIniContent("GameContent", "Content", str);


        //转换成数组

        //string strdddd = string.Join(",", (string[])ContentList.ToArray(typeof(string)));
        Debug_Log.Call_WriteLog("setContent=" + str, selected.toString(), "Unity");
        Debug.Log(selected);



        //if (selected)
        //{

        //}
        //else {
        //}
    }


    // Use this for initialization

}
