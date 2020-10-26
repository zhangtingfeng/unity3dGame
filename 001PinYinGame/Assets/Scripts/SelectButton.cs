using Assets.Script.PunPinYin;
using Assets.Scripts.PunPinYin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectButton : Assets.Scripts.PunPinYin.pubButton
{





    void Start()
    {

        myButtonSelctList = new ButtonSelect[10];
        myButtonSelctList[0] = new ButtonSelect { buttonName = "DrawRed", left = 524, top = 165 };
        myButtonSelctList[1] = new ButtonSelect { buttonName = "PlateDraw", left = 524, top = 228 };
        myButtonSelctList[2] = new ButtonSelect { buttonName = "VertiDraw", left = 524, top = 299 };
        myButtonSelctList[3] = new ButtonSelect { buttonName = "SoundModel", left = 524, top = 366 };
        myButtonSelctList[4] = new ButtonSelect { buttonName = "OneWord", left = 752, top = 136 };
        myButtonSelctList[5] = new ButtonSelect { buttonName = "TwoWord", left = 752, top = 199 };
        myButtonSelctList[6] = new ButtonSelect { buttonName = "ThreeWord", left = 752, top = 275 };
        myButtonSelctList[7] = new ButtonSelect { buttonName = "Story", left = 752, top = 338 };
        myButtonSelctList[8] = new ButtonSelect { buttonName = "Scan", left = 752, top = 402 };
        myButtonSelctList[9] = new ButtonSelect { buttonName = "Exit", left = 18, top = 479 };
    }

    override
    public void MainSelect(string strWhich)
    {
        //StaticGlobalService.getTargetItemListPath();
        switch (strWhich)
        {
            case "Exit":
                UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
                break;
        }
        // UnityEngine.SceneManagement.SceneManager.LoadScene(strWhich);
    }

    // Update is called once per frame
    void Update()
    {

    }









}
