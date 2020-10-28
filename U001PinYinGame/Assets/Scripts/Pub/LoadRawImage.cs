using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Pub
{
    public class LoadRawImage : MonoBehaviour
    {

        public void showLocalFile(RawImage argRawImage, String FilePath)
        {
            String url = "file:///" + FilePath;

            var tex = Download(url).texture;//使用www加载外部图片（这个方法是自己写的， 不会的话自己上网百度下）

            argRawImage.texture = tex;
        }



        public WWW Download(string path)
        {
            WWW www = new WWW(path);

            YieldToStop(www);

            return www;
        }

        private void YieldToStop(WWW www)
        {
            var @enum = DownloadEnumerator(www);
            while (@enum.MoveNext()) ;
        }

        private IEnumerator DownloadEnumerator(WWW www)
        {
            while (!www.isDone) ;

            yield return www;
        }
    }
}
