


using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;
using Assets.Script.DB;

public class SearchButton : BaseOnMouse
{
   

    public override void setClickButton()
    {
        ClassDB.readAllDB();
        //Awake111();
        //Debug_Log.Call_WriteLog("child setClickButton..=");
        // return System.Math.PI * Radius * Radius;
    }


   

}
