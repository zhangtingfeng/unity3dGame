using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOnMouseOver : BaseOnMouse
{

    void Awake()
    {

       


    }

    public override void setClickButton()
    {
        SceneManager.LoadSceneAsync("DB");
    }





}