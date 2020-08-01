using Assets.Script.DB;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSet : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //testDB();
        Screen.SetResolution(1920, 1080, false);
        // Use this for initialization

        // Set screen resolution to 640x960, non-fullscreen
        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

 
    void testDB()
    {
        try
        {

            SQLiteHelper_Example.Start();
        }

        catch (Exception ex)
        {
            Debug_Log.Call_WriteLog(ex, "ExecuteQuery", "Unity");
           // Debug_Log.Call_WriteLog(ex, "testDB");

        }
    }


}

