using Assets.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

class AllButtonClickGameObject : MonoBehaviour
{
    // private bool boolnewUser = false;

    private static System.Object myLockuser = new System.Object();

    public void onclickFuncton(String strMarker)
    {

        try
        {
            GameObject mUICanvas = GameObject.Find("NewUserAnimation");
            switch (strMarker)
            {
                case "ReturnToDBRecord":
                    Camera CameramyCamera2 = GameObject.Find("myCamera2").GetComponent<UnityEngine.Camera>();
                    CameramyCamera2.enabled = false;

                    Destroy(CameramyCamera2);
                    if (Cursor.visible == false)
                    {
                        Cursor.visible = true;
                    }
                    SceneManager.UnloadSceneAsync("RunGlobal");

                    SceneManager.LoadScene("DBRecord", LoadSceneMode.Single);
                    break;
                case "ReturnDB":
                    if (Cursor.visible == false)
                    {
                        Cursor.visible = true;
                    }

                    SceneManager.LoadScene("DB");
                    break;
                case "userOpen":

                    if (Assets.Script.DB.ClassDB.SelectUserID == 0)
                    {
                        MessageBOX.MessageBox(IntPtr.Zero, "必须先选择一个用户", "确认", 0);
                        break;
                    }
                    // 摘要: 
                    //     Closes all current loaded scenes and loads a scene.
                    //            Single = 0,
                    ////
                    //// 摘要: 
                    ////     Adds the scene to the current loaded scenes.
                    //Additive = 1,
                    StaticGlobal.UserID = Assets.Script.DB.ClassDB.SelectUserID;
                    StaticGlobal.UserName = Assets.Script.DB.ClassDB.SelectUserIDName;
                    SceneManager.LoadScene("DBRecord");
                    break;

                case "JumpStart":
                    SceneManager.LoadScene("Start");
                    break;


                case "NewUser":
                    //new Assets.Script.DB.ClassWriteUser().resetInputFieldName();

                    mUICanvas.SendMessage("showanimator");
                    break;
                
                case "NewUserOK":
                    //SceneManager.LoadSceneAsync("NewUserOK");
                    lock (myLockuser)
                    {
                        StaticGlobal.UserName = Assets.Script.DB.ClassWriteUser.writeDBUser();
                        StaticGlobal.UserID = Assets.Script.DB.ClassDB.maxtUserID + 1;
                        SceneManager.LoadScene("RunGlobal");
                    }
                    // break;
                    //Assets.Script.DB.ClassDB.readAllDB();

                    //mUICanvas.SendMessage("showanimator");
                    break;
                case "DelUser":
                    if (Assets.Script.DB.ClassDB.SelectUserID == 0) {
                         MessageBOX.MessageBox(IntPtr.Zero, "必须先选择一个用户再删除", "确认", 0);
                        break;
                    }
                    int returnNumber = MessageBOX.MessageBox(IntPtr.Zero, "确认删除吗", "确认|取消", 1);
                    if (returnNumber == 1)
                    {
                        Assets.Script.DB.ClassDB.deleteAllDB();
                        //重载当前场景
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        //Assets.Script.DB.ClassDB.readAllDB();
                    }
                    break;
                case "thisRollNum":
                    Debug_Log.Call_WriteLog("ddddd", "thisRollNum");
                    break;

                case "Quit":
                    Debug_Log.Call_WriteLog("Application.Quit();", "Application.Quit();", "Unity");
                    Application.Quit();
                    break;
                case "NewDBRecordOK":
                    //SceneManager.LoadSceneAsync("NewUserOK");
                    lock (myLockuser)
                    {
                        Assets.Script.DB.ClassWriteRecord.writeDBRecord();
                        //StaticGlobal.TrainID = Assets.Script.DB.ClassWriteUser.writeDBUser();
                        //StaticGlobal.UserID = Assets.Script.DB.ClassDB.maxtUserID + 1;
                        SceneManager.LoadScene("RunGlobal");
                    }
                    // break;
                    //Assets.Script.DB.ClassDB.readAllDB();

                    //mUICanvas.SendMessage("showanimator");
                    break;

                case "DelTrainRecord":
                    if (Assets.Script.DB.ClassDBRecord.SelectUserRecordID == 0)
                    {
                        MessageBOX.MessageBox(IntPtr.Zero, "必须先选择一个用户再删除", "确认", 0);
                        break;
                    }
                    int returnDelTrainRecordNumber = MessageBOX.MessageBox(IntPtr.Zero, "确认删除吗", "确认|取消", 1);
                    if (returnDelTrainRecordNumber == 1)
                    {
                        Assets.Script.DB.ClassDBRecord.deleteAllDBRecord();
                        //重载当前场景
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        //Assets.Script.DB.ClassDB.readAllDB();
                    }
                    break;
                    
            }
        }
        catch (Exception eee)
        {
            Debug_Log.Call_WriteLog(eee, "onclickFuncton", "Unity");
            Debug_Log.Call_WriteLog(eee.Message);
        }



    }

}

