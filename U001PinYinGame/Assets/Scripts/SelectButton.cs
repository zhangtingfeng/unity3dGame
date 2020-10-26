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

        int intLeft1 = 534;
        int intLeft2 = 752;

        myButtonSelctList = new ButtonSelect[10];
        myButtonSelctList[0] = new ButtonSelect { buttonName = "DrawRed", left = intLeft1, top = 165 };
        myButtonSelctList[1] = new ButtonSelect { buttonName = "PlateDraw", left = intLeft1, top = 232 };
        myButtonSelctList[2] = new ButtonSelect { buttonName = "VertiDraw", left = intLeft1, top = 299 };
        myButtonSelctList[3] = new ButtonSelect { buttonName = "SoundModel", left = intLeft1, top = 366 };
        myButtonSelctList[4] = new ButtonSelect { buttonName = "OneWord", left = intLeft2, top = 136 };
        myButtonSelctList[5] = new ButtonSelect { buttonName = "TwoWord", left = intLeft2, top = 202 };
        myButtonSelctList[6] = new ButtonSelect { buttonName = "ThreeWord", left = intLeft2, top = 276 };
        myButtonSelctList[7] = new ButtonSelect { buttonName = "Story", left = intLeft2, top = 350 };
        myButtonSelctList[8] = new ButtonSelect { buttonName = "Scan", left = intLeft2, top = 424 };
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
