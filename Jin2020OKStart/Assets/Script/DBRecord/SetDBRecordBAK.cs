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
            /**
            Toggle ToggleContentnormal = GameObject.Find("ToggleContentnormal").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleNumber = GameObject.Find("ToggleNumber").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleShape = GameObject.Find("ToggleShape").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleFood = GameObject.Find("ToggleFood").transform.GetComponentsInChildren<Toggle>()[0];
            Toggle ToggleFruit = GameObject.Find("ToggleFruit").transform.GetComponentsInChildren<Toggle>()[0];
            if (ContentList.Contains("normal")) ToggleContentnormal.isOn = true;
            if (ContentList.Contains("number")) ToggleNumber.isOn = true;
            if (ContentList.Contains("shape")) ToggleShape.isOn = true;
            if (ContentList.Contains("food")) ToggleFood.isOn = true;
            if (ContentList.Contains("fruit")) ToggleFruit.isOn = true;
            **/
            #endregion 初始化内容选择
            //写入Count类型count值对应的数值（如果存在了相同的key会覆盖原来key的内容）
            //MyIni.WritePrivateProfileString("GameContent", "LengthofTime", LengthofTime.toString(), strpersistentDataPath);
            //MyIni.WritePrivateProfileString("GameContent", "Speed", speed.toString(), strpersistentDataPath);
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
