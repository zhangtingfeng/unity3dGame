using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class MicroPhoneRecord : MonoBehaviour
{
    private int DeviceLength;
    private const int HEADER_SIZE = 44;


    private delegate void AudioRecordHandle(AudioClip audioClip);
    //public AudioSource audioSource;
    private AudioClip micClip;


    private bool isMicRecordFinished = true;

    private List<float> micDataList = new List<float>();
    private float[] micDataTemp;

    private string micName;
    private string RecordFileName;

    /// <summary>
    /// 是否正在录音
    /// </summary>
    private bool boolRecording = false;///
    private bool boolresetMicrophone = false;///

    private DateTime StartMicrophoneframeTime =DateTime.Now;//记录接受动作的时间，小于0.5秒的放弃/  
                                            //private AudioSource aud;

    public void StartMicrophone(String argRecordFileName)
    {
        if (DateTime.Now.Ticks - StartMicrophoneframeTime.Ticks < 500) return;
        RecordFileName = argRecordFileName;
        Debug.Log(RecordFileName);
        //new AndoridSD().WriteSD();
        // newRecordAudioClip = null;////清楚可以录音后播放的声音
        boolRecording = true;
        boolresetMicrophone = false;
        StopCoroutine(StartMicrophone(Microphone.devices[0], saveAudioRecord));
        StartCoroutine(StartMicrophone(Microphone.devices[0], saveAudioRecord));
        StartMicrophoneframeTime = DateTime.Now;
    }
    public void StopMicrophone()
    {
        boolRecording = false;
        Debug.Log("Stop mic");
        isMicRecordFinished = true;
    }
    public void resetMicrophone() {
        if (boolRecording) {
            boolRecording = false;
            boolresetMicrophone = true;
            StopMicrophone();
        }
    }

    IEnumerator StartMicrophone(string microphoneName, AudioRecordHandle audioRecordFinishedEvent)
    {
        Debug.Log("Start Mic");
        micDataList = new List<float>();
        micName = microphoneName;
        micClip = Microphone.Start(micName, true, 2, 16000);
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



   

    void saveAudioRecord(AudioClip newAudioClip)
    {
        // GetComponent<AudioSource>().clip = newAudioClip;
        // GetComponent<AudioSource>().Play();
        //newRecordAudioClip = newAudioClip;////可以录音后播放使用

        if (boolresetMicrophone) {
            boolresetMicrophone = false;
            return;////不保存了
        }
        SaveRecordedWav(RecordFileName, newAudioClip);
    }





    //保存wav 模式
    public static bool SaveRecordedWav(string filename, AudioClip clip)
    {


        ///cd C:\Users\Administrator\AppData\Local\Android\Sdk\platform-tools
        ////adb forward tcp:34999 localabstract:Unity-com.shiyi.U001PinYinGame
        //Debug.Log(Application.persistentDataPath);
        //if (!filename.ToLower().EndsWith(".wav"))
        //{
        //   filename += ".wav";
        //}

#if UNITY_IPHONE
 Debug.Log("这里是苹果设备>_<");
//		path_1 = Application.persistentDataPath;
		string filepath = Path.Combine(Application.persistentDataPath, filename);
#endif

#if UNITY_STANDALONE_WIN
        try
        {

            //if (!Directory.Exists(Directory.GetDirectories(filename)[0]))
            //{
            //    Directory.CreateDirectory(filename);
            // }
            // }
            string path_save = filename;
            // string filepath = filename;
#endif
#if UNITY_ANDROID

        // Make sure directory exists if user is saving to sub dir.
        //Directory.CreateDirectory(Path.GetDirectoryName(filepath));
        try
        {
            /*
            //存储路径
            string destination = Application.persistentDataPath;
            //若没路径 创建
            if (!Directory.Exists((destination)))
            {
                Directory.CreateDirectory(destination);
            }
            // IO crib sheet..此电脑\vivo S1\内部存储设备\Android\data\com.shiyi.U001PinYinGame\files
            string filePath = destination + "/" + "ddd.txt";
            string sssContent = File.ReadAllText(filePath);




            File.WriteAllText(filePath, "rrr");
            // check if file exists System.IO.File.Exists(f)
            // write to file File.WriteAllText(f,t)
            // delete the file if needed File.Delete(f)
            // read from a file File.ReadAllText(f)









            Debug.Log("这里是安卓设备^_^");
           */
            //存储路径
            string destination = Application.persistentDataPath;
            Debug_Log.Call_WriteLog(destination, "这里是安卓设备persistentDataPath");
            //若没路径 创建
            if (!Directory.Exists((destination)))
            {
                Directory.CreateDirectory(destination);
            }

            string strFolderTest = "FolderTest";
            string strAllFolderTest = Path.Combine(destination, strFolderTest);
            if (!Directory.Exists((strAllFolderTest)))
            {
                Directory.CreateDirectory(strAllFolderTest);
            }

            //文件完整路径
            string path_save = Path.Combine(strAllFolderTest, filename);
            /*
            //string filepath = Path.Combine((new AndoridSD()).getStoragePath(), filename);
            Debug_Log.Call_WriteLog(path_save, "这里是安卓设备");

            AndroidJavaClass jc = new AndroidJavaClass("com.pico.Integration.ThirdActivity");
            //AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            string path_savemysdcard2 = Path.Combine(destination, "mysdcard2.txt");
            //            string s1 = jc.CallStatic<string>("WriteSD", "/storage/0000-E33A/Download/mysdcard2.txt");
            string s1 = jc.CallStatic<string>("WriteSD", path_savemysdcard2);


            ////storage/emulated/0/Android/data/com.shiyi.U001PinYinGame/files

            //		string filepath = Path.Combine(Application.persistentDataPath, filename);
            Debug.Log(path_save);
            */


           
#endif
            using (FileStream fileStream = CreateEmpty(path_save))
            {

                ConvertAndWrite(fileStream, clip);

                WriteHeader(fileStream, clip);
            }
            if (File.Exists((path_save)))
            {
                Debug_Log.Call_WriteLog(path_save, "录音路径成功", "录音成功");
            }
            else
            {
                Debug_Log.Call_WriteLog(path_save, "录音路径失败", "录音成功");
            }
        }
        catch (Exception eee)
        {
            Debug_Log.Call_WriteLog(eee, "写报错", "这里是设备");
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


}