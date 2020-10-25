using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{
    public GUISkin mySkin;
    public AudioClip menuSound;
   
    private Rect newGamePosition = new Rect(800, 205, 208, 60);
    private Rect howToPlayPosition = new Rect(800, 305, 208, 60);
    private Rect moreGamePosition = new Rect(800, 405, 208, 60);
    // Use this for initialization

    const float devHeight = 9.6f;
    const float devWidth = 6.4f;

    Assets.Script.PunPinYin.StaticGlobal StaticGlobalService = new Assets.Script.PunPinYin.StaticGlobal();

    void Start()
    {
      /*  float screenHeight = Screen.height;
        float screenWidth = Screen.width;
        float orthpgraphicSize = this.GetComponent<Camera>().orthographicSize;
        float aspectRatio = screenWidth * 1.0f / screenHeight;
        float cameraWidth = orthpgraphicSize * 2 * aspectRatio;

        if (cameraWidth < devWidth) {
            orthpgraphicSize = devWidth / (2 * aspectRatio);
            this.GetComponent<Camera>().orthographicSize = orthpgraphicSize;
        }
      */
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        GUI.skin = mySkin;

        if (GUI.Button(newGamePosition, "", GUI.skin.GetStyle("author")))
        {
            UnityAction action = new UnityAction(goMainSelect);
            PlayAudio(menuSound, action);

            
        }

        if (GUI.Button(howToPlayPosition, "", GUI.skin.GetStyle("begin")))
        {
            UnityAction action = new UnityAction(goMainSelect);
            PlayAudio(menuSound);

        }

        if (GUI.Button(moreGamePosition, "", GUI.skin.GetStyle("exit"))) {
            UnityAction action = new UnityAction(goMainSelect);
            PlayAudio(menuSound);
        }

    }


    void goMainSelect() {
        //StaticGlobalService.getTargetItemListPath();


        SceneManager.LoadScene("Select");
    }









    #region  播放按钮声音
    public void PlayAudio(AudioClip clip, UnityAction callback = null, bool isLoop = false)
    {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
        //执行协成获取音频文件的时间
        StartCoroutine(AudioPlayFinished(clip.length, callback));
    }



    IEnumerator AudioPlayFinished(float time, UnityAction callback = null)
    {
        Debug.Log("检测，继续向下执行");
        yield return new WaitForSeconds(time);
        Debug.Log("声音播放完毕，继续向下执行");
        callback();
        //yield WaitForSeconds(menuSound.length);
        //SceneManager.LoadScene("MainSelect");
        //SceneManager.LoadScene(1);
        // Application.LoadLevel(1);

    }
    #endregion  播放按钮声音
}
