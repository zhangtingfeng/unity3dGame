using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using Assets.Script.DB;

[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class LineOnMouseOver : MonoBehaviour
{
    /// <summary>
    /// 要设置的图集
    /// </summary>
    public Sprite MyOnMouseEntersprit;
    public Sprite MyOnMouseNormalsprit;
    public Sprite MyOnMousePresssprit;
    private int yyyyy = 0;
    Image img;
    public Texture2D cursorTexture;

    Text mytext;
    // Use this for initialization
    void Start()
    {
        img = this.GetComponent<Image>();
        EventTrigger trigger = img.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        EventTrigger.Entry entryLeave = new EventTrigger.Entry();


        // 鼠标点击事件
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener(OnClick);
        trigger.triggers.Add(entryClick);

        // 鼠标进入事件
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener(OnMouseEnter);
        trigger.triggers.Add(entryEnter);

        //鼠标滑出事件 
        entryLeave.eventID = EventTriggerType.PointerExit;
        entryLeave.callback.AddListener(OnMouseLeave);
        trigger.triggers.Add(entryLeave);


    }

    private void OnClick(BaseEventData pointData)
    {
        Transform objnamethisTransform = img.transform;
        string strOneNum = objnamethisTransform.Find("OneNum").gameObject.GetComponent<UnityEngine.UI.Text>().text;
        string strTwoName = objnamethisTransform.Find("TwoName").gameObject.GetComponent<UnityEngine.UI.Text>().text;

        ClassDB.SelectUserID = strOneNum.toInt32();
        ClassDB.SelectUserIDName = strTwoName;
        GameObject GameObjectInputFieldSearch = GameObject.Find("InputFieldSearch");
        if (GameObjectInputFieldSearch != null)
        {
            GameObject.Find("InputFieldSearch").GetComponent<InputField>().text = strOneNum + "  " + strTwoName;//有效
            mytext = GameObject.Find("TextsearchSQL").GetComponent<Text>(); ;
            if (mytext != null)
            {
                // 获取当前时间

                DateTime dateTime = DateTime.Now;

                // 将当前时间显示在 Text 控件上
                mytext.text = "1";
                mytext.text = dateTime.ToString();

                // mytext.text = "slider.value.ToString()";

            }


            string sss = GameObject.Find("TextsearchSQL").gameObject.GetComponent<UnityEngine.UI.Text>().text;
            GameObject.Find("TextsearchSQL").gameObject.GetComponent<UnityEngine.UI.Text>().text = strOneNum;

        }
        //sss = GameObject.Find("TextsearchSQL").gameObject.GetComponent<UnityEngine.UI.Text>().text;

        //Debug_Log.Call_WriteLog(strOneNum, "dddd");
        //transform.GetComponent<Image>().sprite = MyOnMousePresssprit;
        //yyyyy++; InputFieldSearch
        //Debug_Log.Call_WriteLog("Button Clicked. EventTrigger..=" + yyyyy);

        //SceneManager.LoadSceneAsync("DB");
    }

    void Update()
    {

    }


    private void OnMouseEnter(BaseEventData pointData)
    {

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        ///更改按钮图片
        transform.GetComponent<Image>().sprite = MyOnMouseEntersprit;

        yyyyy++;
        //Debug_Log.Call_WriteLog("Button Enter. EventTrigger..=" + yyyyy);
    }

    private void OnMouseLeave(BaseEventData pointData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        transform.GetComponent<Image>().sprite = MyOnMouseNormalsprit;
        yyyyy++;
        //Debug_Log.Call_WriteLog("Button OnMouseLeave. EventTrigger..=" + yyyyy);
    }
}
