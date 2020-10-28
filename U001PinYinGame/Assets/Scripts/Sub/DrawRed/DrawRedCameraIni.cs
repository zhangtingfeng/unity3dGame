using Assets.Script.PunPinYin;
using Assets.Scripts.Pub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DrawRedCameraIni : MonoBehaviour
{
    public Text myText;
    

    void Start()
    {



        string strPath = StaticGlobal.getOneLetterPath();


        String strTextPath = strPath + "/01.txt";
        string StrContent = Assets.Scripts.Pub.ReadFileTxtContent.ReadText(strTextPath);
        myText.text = StrContent;

      
        //myPlayLocalFileSound.PlayLocalFile(audioSource, straudioPath);


       

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