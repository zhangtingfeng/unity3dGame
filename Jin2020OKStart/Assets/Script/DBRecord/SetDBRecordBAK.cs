using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.DB;
using Assets.Script;
using System.IO;
using System;
using UnityEngine.UI;

public class SetDBRecordBAK : MonoBehaviour
{
    //private ArrayList ContentList = new ArrayList();
    //private GameObject gamebakObject[10];//有10个元素的学生类对象数组
    // Use this for initialization

    void Awake()
    {
        GameObject.Find("InputFieldSearch").GetComponent<UnityEngine.UI.InputField>().text = "编号：" + StaticGlobal.UserID + "，姓名：" + StaticGlobal.UserName;//有效
        ClassDBRecord.SelectUserID = StaticGlobal.UserID;

        ClassDBRecord.readAllDBRcord();

        #region ini文件
        try
        {

            ReadIniPar ReadIniPargetParme = getPar.getParme();
            //ContentList = new ArrayList(strContent.Split(','));

            //GameObject mCamera = GameObject.Find("Main Camera");
            //Camera mainCamera = gameObject.GetComponent<Camera>();
            //Common.CursorPos ddCursorPosd = Common.getCursorPos(mainCamera, GameObject.Find("LabelMid"));

            //Common.MouseClickSimulate(ddCursorPosd);
            #region 初始化时间长短
            Toggle ToggleTime0 = GameObject.Find("ToggleTime0").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleTime1 = GameObject.Find("ToggleTime1").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleTime2 = GameObject.Find("ToggleTime2").transform.GetComponentsInChildren<Toggle>()[0];
            if (ReadIniPargetParme.LengthofTime == 0)
            {
                ToggleTime0.isOn = true;
            }
            else if (ReadIniPargetParme.LengthofTime == 1)
            {
                ToggleTime1.isOn = true;
            }
            else if (ReadIniPargetParme.LengthofTime == 2)
            {
                ToggleTime2.isOn = true;
            }
            #endregion 初始化时间长短

            #region 初始化速度
            Toggle ToggleSpeed0 = GameObject.Find("ToggleSpeed0").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleSpeed1 = GameObject.Find("ToggleSpeed1").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleSpeed2 = GameObject.Find("ToggleSpeed2").transform.GetComponentsInChildren<Toggle>()[0];
            if (ReadIniPargetParme.speed == 0)
            {
                ToggleSpeed0.isOn = true;
            }
            else if (ReadIniPargetParme.speed == 1)
            {
                ToggleSpeed1.isOn = true;
            }
            else if (ReadIniPargetParme.speed == 2)
            {
                ToggleSpeed2.isOn = true;
            }
            #endregion 初始化速度


            #region 初始化内容选择

            ArrayList bContent = new ArrayList(ReadIniPargetParme.Content.Split(','));
            //if (!String.IsNullOrEmpty(ReadIniPargetParme.Content)) {
            //    SelectContent.ContentList = bContent;
            //}
            Toggle Content_A = GameObject.Find("Content_A").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle Content_FiveShape = GameObject.Find("Content_FiveShape").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle Content_Star = GameObject.Find("Content_Star").transform.GetComponentsInChildren<Toggle>()[0];
            if (bContent.Contains("A")) Content_A.isOn = true;
            if (bContent.Contains("FiveShape")) Content_FiveShape.isOn = true;
            if (bContent.Contains("Star")) Content_Star.isOn = true;
            #endregion 初始化内容选择
        }
        catch (Exception ex)
        {
            ////LogController.writeErrorLog(ex, "ObjectExtended toInt32");
            Debug_Log.Call_WriteLog(ex, "初始化时间长短.ini");

        }

        #endregion
    }

    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

    }
}
