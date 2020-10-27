using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class MicroPhoneInput : MonoBehaviour
{
    public int DeviceLength;
    const int HEADER_SIZE = 44;
    /*
    

    private static MicroPhoneInput m_instance;
    private AudioClip clip;

    private string path_1;

    public float sensitivity = 100;
    public float loudness = 0;

    private static string[] micArray = null;

    

    const int RECORD_TIME = 3;
    */



    public delegate void AudioRecordHandle(AudioClip audioClip);
    public AudioSource audioSource;

    AudioClip micClip;


    bool isMicRecordFinished = true;

    List<float> micDataList = new List<float>();
    float[] micDataTemp;

    string micName;



    private AudioSource aud;

    public IEnumerator LoadAudio(string recordPath)
    {
        // www 加载音频
        WWW www = new WWW(recordPath);
        yield return www;
        var clipTemp = www.GetAudioClip();
        aud.clip = clipTemp;

        //yield return new WaitForSeconds(5);
        //aud.loop = true;

        //播放音频
        aud.Play();
    }

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android 测试路径
        string filePath = "file:///sdcard/msc/test_13.wav";
        Debug.LogError(filePath);
#endif

#if UNITY_EDITOR
        // Unity Editor 测试路径
        string filePath = "E:/WorksUnityGame/U001PinYinGame/externalResources/001GameResource/b/01OneSyllable/01/01.wav";
#endif

        //StartCoroutine(LoadAudio(filePath));

    }
    public void StartMicrophone()
    {
        StopCoroutine(StartMicrophone(null, PlayAudioRecord));
        StartCoroutine(StartMicrophone(null, PlayAudioRecord));
    }


    IEnumerator StartMicrophone(string microphoneName, AudioRecordHandle audioRecordFinishedEvent)
    {
        Debug.Log("Start Mic");
        micDataList = new List<float>();
        micName = microphoneName;
        micClip = Microphone.Start(micName, true, 1, 16000);
        isMicRecordFinished = false;
        int length = micClip.channels * micClip.samples;
        bool isSaveFirstHalf = true;//将音频从中间分生两段，然后分段保存
        int micPosition;
        while (!isMicRecordFinished || isSaveFirstHalf)////true不能结束
        {
            if (isSaveFirstHalf)
            {
                yield return new WaitUntil(() => { micPosition = Microphone.GetPosition(micName); return micPosition > length * 6 / 10 && micPosition < length; });//保存前半段
                micDataTemp = new float[length / 2];
                micClip.GetData(micDataTemp, 0);
                micDataList.AddRange(micDataTemp);
                isSaveFirstHalf = !isSaveFirstHalf;
            }
            else
            {
                yield return new WaitUntil(() => { micPosition = Microphone.GetPosition(micName); return micPosition > length / 10 && micPosition < length / 2; });//保存后半段
                micDataTemp = new float[length / 2];
                micClip.GetData(micDataTemp, length / 2);
                micDataList.AddRange(micDataTemp);
                isSaveFirstHalf = !isSaveFirstHalf;
            }

        }
        micPosition = Microphone.GetPosition(micName);
        if (micPosition <= length)//前半段
        {
            micDataTemp = new float[micPosition / 2];
            micClip.GetData(micDataTemp, 0);
        }
        else
        {
            micDataTemp = new float[micPosition - length / 2];
            micClip.GetData(micDataTemp, length / 2);
        }
        micDataList.AddRange(micDataTemp);
        Microphone.End(micName);
        AudioClip newAudioClip = AudioClip.Create("RecordClip", micDataList.Count, 1, 16000, false);
        newAudioClip.SetData(micDataList.ToArray(), 0);
        audioRecordFinishedEvent(newAudioClip);
        Debug.Log("RecordEnd");
    }



    public void StopMicrophone()
    {
        Debug.Log("Stop mic");
        isMicRecordFinished = true;
    }

    void PlayAudioRecord(AudioClip newAudioClip)
    {
       // GetComponent<AudioSource>().clip = newAudioClip;
       // GetComponent<AudioSource>().Play();

        Save("C:\\tmp\\333", newAudioClip);
    }






    /*


    // Use this for initialization
    void Start()
    {
        getInstance();
    }
    //--------------------------------

    public static MicroPhoneInput getInstance()
    {
        if (m_instance == null)
        {
            micArray = Microphone.devices;
            if (micArray.Length == 0)
            {
                Debug.LogError("Microphone.devices is null");
            }
            foreach (string deviceStr in Microphone.devices)
            {
                Debug.Log("device name = " + deviceStr);
            }
            if (micArray.Length == 0)
            {
                Debug.LogError("no mic device");
            }

            GameObject MicObj = new GameObject("MicObj");
            m_instance = MicObj.AddComponent<MicroPhoneInput>();
        }
        return m_instance;
    }

    public void StartRecord()
    {
        GetComponent<AudioSource>().Stop();
        if (micArray.Length == 0)
        {
            Debug.Log("No Record Device!");
            return;
        }
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().mute = true;
        GetComponent<AudioSource>().clip = Microphone.Start(null, true, 2, 44100); //循环录制
        clip = GetComponent<AudioSource>().clip;
        while (!(Microphone.GetPosition(null) > 0))
        {
        }
        GetComponent<AudioSource>().Play();
        Debug.Log("StartRecord");
        //倒计时
        StartCoroutine(TimeDown());

    }

    public void StopRecord()
    {
        if (micArray.Length == 0)
        {
            Debug.Log("No Record Device!");
            return;
        }
        if (!Microphone.IsRecording(null))
        {
            return;
        }
        Microphone.End(null);
        GetComponent<AudioSource>().Stop();

        Debug.Log("StopRecord");

    }

    public Byte[] GetClipData()
    {
        if (GetComponent<AudioSource>().clip == null)
        {
            Debug.Log("GetClipData audio.clip is null");
            return null;
        }

        float[] samples = new float[GetComponent<AudioSource>().clip.samples];

        GetComponent<AudioSource>().clip.GetData(samples, 0);


        Byte[] outData = new byte[samples.Length * 2];
        //Int16[] intData = new Int16[samples.Length];
        //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

        int rescaleFactor = 32767; //to convert float to Int16

        for (int i = 0; i < samples.Length; i++)
        {
            short temshort = (short)(samples[i] * rescaleFactor);

            Byte[] temdata = System.BitConverter.GetBytes(temshort);

            outData[i * 2] = temdata[0];
            outData[i * 2 + 1] = temdata[1];


        }
        if (outData == null || outData.Length <= 0)
        {
            Debug.Log("GetClipData intData is null");
            return null;
        }
        //return intData;

        return outData;
    }
    public void PlayClipData(Int16[] intArr)
    {

        string aaastr = intArr.ToString();
        long aaalength = aaastr.Length;
        Debug.LogError("aaalength=" + aaalength);

        string aaastr1 = Convert.ToString(intArr);
        aaalength = aaastr1.Length;
        Debug.LogError("aaalength=" + aaalength);

        if (intArr.Length == 0)
        {
            Debug.Log("get intarr clipdata is null");
            return;
        }
        //从Int16[]到float[]
        float[] samples = new float[intArr.Length];
        int rescaleFactor = 32767;
        for (int i = 0; i < intArr.Length; i++)
        {
            samples[i] = (float)intArr[i] / rescaleFactor;
        }

        //从float[]到Clip
        AudioSource audioSource = this.GetComponent<AudioSource>();
        if (audioSource.clip == null)
        {
            audioSource.clip = AudioClip.Create("playRecordClip", intArr.Length, 1, 44100, false, false);
        }
        audioSource.clip.SetData(samples, 0);
        audioSource.mute = false;
        audioSource.Play();
    }
    public void PlayRecord()
    {
        if (GetComponent<AudioSource>().clip == null)
        {
            Debug.Log("audio.clip=null");
            return;
        }
        GetComponent<AudioSource>().mute = false;
        GetComponent<AudioSource>().loop = false;
        GetComponent<AudioSource>().Play();
        Debug.Log("PlayRecord");

    }



    public float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        GetComponent<AudioSource>().GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }

    // Update is called once per frame
    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
        if (loudness > 1)
        {
            Debug.Log("loudness = " + loudness);
        }
    }

    private IEnumerator TimeDown()
    {
        Debug.Log(" IEnumerator TimeDown()");

        int time = 0;
        while (time < RECORD_TIME)
        {
            if (!Microphone.IsRecording(null))
            { //如果没有录制
                Debug.Log("IsRecording false");
                yield break;
            }
            Debug.Log("yield return new WaitForSeconds " + time);
            yield return new WaitForSeconds(1);
            time++;
        }
        if (time >= 10)
        {
            Debug.Log("RECORD_TIME is out! stop record!");
            StopRecord();
        }
        yield return 0;
    }

   



    public void SaveMusic()
    {
        Save("C:\\tmp\\333", clip);
    } */
    //	public void UnSaveMusic()
    //	{
    //		clip = null;
    //	}
    //保存wav 模式
    public static bool Save(string filename, AudioClip clip)
    {

        Debug.Log(Application.persistentDataPath);
        if (!filename.ToLower().EndsWith(".wav"))
        {
            filename += ".wav";
        }

#if UNITY_IPHONE
//		path_1 = Application.persistentDataPath;
		string filepath = Path.Combine(Application.persistentDataPath, filename);
#endif

#if UNITY_STANDALONE_WIN
        string filepath = filename;
#endif
#if UNITY_ANDROID
 
		string filepath = Path.Combine(Application.persistentDataPath, filename);
#endif

        //		string filepath = Path.Combine(Application.persistentDataPath, filename);
        Debug.Log(filepath);

        // Make sure directory exists if user is saving to sub dir.
        //Directory.CreateDirectory(Path.GetDirectoryName(filepath));

        using (FileStream fileStream = CreateEmpty(filepath))
        {

            ConvertAndWrite(fileStream, clip);

            WriteHeader(fileStream, clip);
        }

        return true; // TODO: return false if there's a failure saving the file
    }
    static FileStream CreateEmpty(string filepath)
     {
         FileStream fileStream = new FileStream(filepath, FileMode.Create);
         byte emptyByte = new byte();

         for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
         {
             fileStream.WriteByte(emptyByte);
         }

         return fileStream;
     }
     static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
     {

         float[] samples = new float[clip.samples];

         clip.GetData(samples, 0);

         Int16[] intData = new Int16[samples.Length];
         //converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

         Byte[] bytesData = new Byte[samples.Length * 2];
         //bytesData array is twice the size of
         //dataSource array because a float converted in Int16 is 2 bytes.

         int rescaleFactor = 32767; //to convert float to Int16

         for (int i = 0; i < samples.Length; i++)
         {
             intData[i] = (short)(samples[i] * rescaleFactor);
             Byte[] byteArr = new Byte[2];
             byteArr = BitConverter.GetBytes(intData[i]);
             byteArr.CopyTo(bytesData, i * 2);
         }

         fileStream.Write(bytesData, 0, bytesData.Length);
     }
     static void WriteHeader(FileStream fileStream, AudioClip clip)
     {

         int hz = clip.frequency;
         int channels = clip.channels;
         int samples = clip.samples;

         fileStream.Seek(0, SeekOrigin.Begin);

         Byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
         fileStream.Write(riff, 0, 4);

         Byte[] chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
         fileStream.Write(chunkSize, 0, 4);

         Byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
         fileStream.Write(wave, 0, 4);

         Byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
         fileStream.Write(fmt, 0, 4);

         Byte[] subChunk1 = BitConverter.GetBytes(16);
         fileStream.Write(subChunk1, 0, 4);

         UInt16 two = 2;
         UInt16 one = 1;

         Byte[] audioFormat = BitConverter.GetBytes(one);
         fileStream.Write(audioFormat, 0, 2);

         Byte[] numChannels = BitConverter.GetBytes(channels);
         fileStream.Write(numChannels, 0, 2);

         Byte[] sampleRate = BitConverter.GetBytes(hz);
         fileStream.Write(sampleRate, 0, 4);

         Byte[] byteRate = BitConverter.GetBytes(hz * channels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
         fileStream.Write(byteRate, 0, 4);

         UInt16 blockAlign = (ushort)(channels * 2);
         fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

         UInt16 bps = 16;
         Byte[] bitsPerSample = BitConverter.GetBytes(bps);
         fileStream.Write(bitsPerSample, 0, 2);

         Byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
         fileStream.Write(datastring, 0, 4);

         Byte[] subChunk2 = BitConverter.GetBytes(samples * channels * 2);
         fileStream.Write(subChunk2, 0, 4);

         //		fileStream.Close();
     }
    /* */

    /// <summary>
    /// 获取麦克风设备
    /// </summary>
    public void GetMicrophoneDevice()
    {
        string[] mDevice = Microphone.devices;
        DeviceLength = mDevice.Length;
        if (DeviceLength == 0)
            Debug.Log("找不到麦克风设备！");
    }
    void OnGUI()
    {

        if (DeviceLength == 0)
        {
            if (ShowGUIButton("获取麦克风设备"))
            {
                GetMicrophoneDevice();
            }
        }
        else if (DeviceLength > 0)
        {
            GUILayout.Label("录音频率:");
            //Frequency = GUILayout.TextField(Frequency, GUILayout.Width(Screen.width / 5), GUILayout.Height(Screen.height / 20));
            GUILayout.BeginVertical();

            if (ShowGUIButton("开始录音"))
            {
                StartMicrophone();
            }
            if (ShowGUIButton("结束录音"))
            {
                StopMicrophone();
            }
            if (ShowGUIButton("回放录音"))
            {
               // PlayRecord();
            }
            if (ShowGUIButton("获取录音数据"))
            {
               // SaveMusic();
                //PrintRecordData();
            }

            // GUILayout.EndVertical();
        }
        // GUILayout.Label(infoLog);

    }

    //#region [Private Way]

    /// <summary>
    /// 显示GUI 按钮
    /// </summary>
    /// <returns><c>true</c>, if GUI button was shown, <c>false</c> otherwise.</returns>
    /// <param name="buttonName">Button name.</param>
    bool ShowGUIButton(string buttonName)
    {
        return GUILayout.Button(buttonName, GUILayout.Height(Screen.height / 10), GUILayout.Width(Screen.width / 2));
    }

   
}