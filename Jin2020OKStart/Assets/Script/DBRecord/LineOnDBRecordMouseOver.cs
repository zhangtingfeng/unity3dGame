using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using Assets.Script.DB;

[RequireComponent(typeof(UnityEngine.EventSystems.EventTrigger))]
public class LineOnDBRecordMouseOver : MonoBehaviour
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

    //Text mytext;
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

        ClassDBRecord.SelectUserRecordID = strOneNum.toInt32();
        ClassDB.SelectUserIDName = strTwoName;
        GameObject GameObjectInputFieldRecordSearch = GameObject.Find("InputFieldRecordSearch");
        if (GameObjectInputFieldRecordSearch != null)
        {
            GameObject.Find("InputFieldRecordSearch").GetComponent<InputField>().text = "序号："+strOneNum + "，训练时间：" + strTwoName;//有效
            
        }
       
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
