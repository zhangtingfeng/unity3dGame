﻿using Assets.Script.PunPinYin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CameraIni : MonoBehaviour
{
    public Text myText;


    void Start()
    {
        myText.text = StaticGlobal.SelectDestinationTargetWord;
    }
        /*
        private int intwidth;
        private int intheight;
        public Text TextWH;
        void Start()
        {
            intwidth = Screen.width;
            intheight = Screen.height;
            print("intwidth="+ intwidth + "  intheight="+ intheight);
            Debug.Log("intwidth=" + intwidth + "  intheight=" + intheight);

            TextWH.text = "intwidth=" + intwidth + "  intheight=" + intheight;

        }*/
        // Use this for initialization
        void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);////跨场景播放不销毁
    }


}