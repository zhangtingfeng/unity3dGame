using Assets.Script.PunPinYin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.PunPinYin
{
   

    public class pubButton : MonoBehaviour
    {
        public GUISkin mySkin;
        public AudioClip menuSound;
        private const int ButtonWidth = 144; private int ButtonHeight = 51;


        public ButtonSelect[] myButtonSelctList;

        void OnGUI()
        {
            GUI.skin = mySkin;
            if (myButtonSelctList==null ||  myButtonSelctList.Length < 1) return;
            for (int i = 0; i < myButtonSelctList.Length; i++)
            {
                ButtonSelect myButtonSelct = myButtonSelctList[i];
                Rect curRect = CountAdjustScreen.GetThisSideRect(new Rect(myButtonSelct.left, myButtonSelct.top, ButtonWidth, ButtonHeight));///;// new Rect(835, 469, ButtonWidth, ButtonHeight);
                if (GUI.Button(curRect, "", GUI.skin.GetStyle(myButtonSelct.buttonName)))
                {
                    UnityAction<string> action = new UnityAction<string>(MainSelect);
                    PlayAudio(menuSound, action, myButtonSelct.buttonName);
                }
            }

        }

        virtual
       public void MainSelect(string strWhich)
        {
            //StaticGlobalService.getTargetItemListPath();
            Debug.Log(1);

           // UnityEngine.SceneManagement.SceneManager.LoadScene(strWhich);
        }

        #region  播放按钮声音
        public void PlayAudio(AudioClip clip, UnityAction<string> callback = null, string strWhich = null)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
            //执行协成获取音频文件的时间
            StartCoroutine(AudioPlayFinished(clip.length, callback,  strWhich));
        }

        private IEnumerator AudioPlayFinished(float timelength, UnityAction<string> callback = null, string strWhich = null)
        {
            yield return new WaitForSeconds(timelength);
            callback(strWhich);
        }

        #endregion  播放按钮声音
    }
}
