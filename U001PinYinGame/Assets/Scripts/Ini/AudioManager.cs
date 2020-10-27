using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    //将声音管理器写成单例模式
    public static AudioManager Am;
    //音乐播放器
    public AudioSource MusicPlayer;
    //音效播放器
    public AudioSource SoundPlayer;
    void Start()
    {

        StartCoroutine( IELoadExternalAudioWebRequest("C:/tmp/333.wav", AudioType.WAV));

        //Am = this;
        //PlaySound("333");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //播放音乐
    public void PlayMusic(string name)
    {
        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }

    }

    //播放音效
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.clip = clip;
        SoundPlayer.PlayOneShot(clip);
    }



    private IEnumerator IELoadExternalAudioWebRequest(string _url, AudioType _audioType)
    {
        UnityWebRequest _unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(_url, _audioType);
        yield return _unityWebRequest.SendWebRequest();
        if (_unityWebRequest.isHttpError || _unityWebRequest.isNetworkError)
        {
            Debug.Log(_unityWebRequest.error.ToString());
        }
        else
        {
            AudioClip _audioClip = DownloadHandlerAudioClip.GetContent(_unityWebRequest);
            SoundPlayer.clip = _audioClip;
            SoundPlayer.Play();
        }
    }
    private IEnumerator IELoadExternalAudioWWW(string _url, AudioType _audioType)
    {
        WWW _www = new WWW(_url);
        yield return _www;
        if (_www.error == null)
        {
            AudioClip _audioClip = _www.GetAudioClip(true, true, _audioType);
            SoundPlayer.clip = _audioClip;
            SoundPlayer.Play();
        }
        else
        {
            Debug.Log(_www.error);
        }
    }
}