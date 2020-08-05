using Assets.Script.GamePlayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class move : MonoBehaviour
{
    public Camera myCamera;//摄像机

    public AudioClip AC;
    //Vector2 first = Vector2.zero;//记录鼠标点击的初始位置

    //Vector2 second = Vector2.zero;//记录鼠标移动时的位置

    bool directorToLeft = false;

    bool directorToRight = false;

    bool directorToUp = false;

    bool directorToDown = false;

    bool boolaltDowm = false;
    ///屏幕1记录时间
    DateTime DateTimeStart = new DateTime();
    //bool dragging = false;//标记是否鼠标在滑动

    float floatMoveMi = 30f;


    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return ("X:" + X + ", Y:" + Y);
        }
    }


    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT pt);
    // Use this for initialization
    void Start()
    {
        try
        {
            Cursor.visible = false;

            Assets.Script.ReadIniPar ReadIniPargetParme = Assets.Script.getPar.getParme();

            int intAllNum = 200;
            switch (ReadIniPargetParme.LengthofTime)
            {
                case 0:
                    intAllNum = 50;
                    break;
                case 1:
                    intAllNum = 100;
                    break;
                case 2:
                    intAllNum = 200;
                    break;
                /* 您可以有任意数量的 case 语句 */
                default: /* 可选的 */
                    intAllNum = 200;
                    break;
            }
            switch (ReadIniPargetParme.speed)
            {
                case 0:
                    floatMoveMi = 60;
                    break;
                case 1:
                    floatMoveMi = 30;
                    break;
                case 2:
                    floatMoveMi = 15;
                    break;
                /* 您可以有任意数量的 case 语句 */
                default: /* 可选的 */
                    floatMoveMi = 200;
                    break;
            }
            string strContent = ReadIniPargetParme.Content;
            if (String.IsNullOrEmpty(strContent)) strContent = "A";
            ArrayList bContentArrayList = new ArrayList(strContent.Split(','));

            GameObject canvas = GameObject.Find("GameEnemyObject");
            ArrayList cShiftArrayList = Common.getShiftArrayList(intAllNum, bContentArrayList);
            Debug_Log.Call_WriteLog(string.Join(",", (string[])cShiftArrayList.ToArray(typeof(string))), "随机图形表示");

            for (int i = 0; i < intAllNum; i++)
            {
                GameObject gameObjectEnemyGame = (GameObject)Resources.Load("Pref/EnemyGame");
                gameObjectEnemyGame = Instantiate(gameObjectEnemyGame);
                gameObjectEnemyGame.name = "myCube" + i.toString();

                /// 查找子物体，并且将得到的物体转换成gameobject
                GameObject objnameFindChild = gameObjectEnemyGame.transform.Find("Cube").gameObject;
                #region 动态材质球
                //System.Random ran = new System.Random();
                //int RandKey = ran.Next(0, bContentArrayList.Count);
                string strLoad = "OuterDATA/GameEnemy_" + cShiftArrayList[i];


                //Material mddddMaterial = Instantiate(lastMat) as Material;

                Texture mat = Resources.Load(strLoad) as Texture;
                objnameFindChild.GetComponent<Renderer>().material.mainTexture = mat;

                // mddddMaterial.mainTexture = mat;
                #endregion




                gameObjectEnemyGame.transform.parent = canvas.transform;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                gameObjectEnemyGame.transform.rotation = rotation;

                #region 对左右位置进行随机 
                int intddd = UnityEngine.Random.Range(-10, 10);///左右随机摆动的
                Vector3 unitVector = new Vector3(56.72f + intddd, 2, 10.15f + i * 3);
                gameObjectEnemyGame.transform.position = unitVector;
                #endregion 

                //  用 slerp 进行插值平滑的旋转
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetAngels, rotateSpeed * Time.deltaTime);
                //transform.rotation = targetAngels;




            }





            Score.iniSucessPercent(intAllNum);
            Debug_Log.Call_WriteLog(intAllNum.toString(), "class move");
            ///屏幕1记录时间
            DateTimeStart = DateTime.Now;
        }
        catch (Exception ex)
        {
            ////LogController.writeErrorLog(ex, "ObjectExtended toInt32");
            Debug_Log.Call_WriteLog(ex, "move初始化推球界面");

        }
    }
    /// <summary>
    /// Awake()是在脚本对象实例化时被调用的，而Start()是在对象的第一帧时被调用的，而且是在Update()之前。
    /// </summary>
    //private void Awake()
    //{
    //    //Unity 获取屏幕当前宽度和高度
    //    //SetMouseToCenter();
    //}
    void setboolaltDown()
    {

    }



    // Update is called once per frame
    void Update()
    {

        //ONGUI里面检测不到shift ctrl的按下，虽然不知道为什么，但是可以在update里面单独检测其输出
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            Cursor.visible = true;
            boolaltDowm = true;
            //Debug_Log.Call_WriteLog("监听LeftAlt RightAlt", "ALt_Down true");
            return;
        }
        else
        {
            if (Cursor.visible == true)
            {
                Cursor.visible = false;
            }
            boolaltDowm = false;
            //Debug_Log.Call_WriteLog("监听LeftAlt RightAlt", "ALt_Down false");
        }
        if (directorToUp || directorToDown || directorToLeft || directorToRight)
        {
            if (boolaltDowm == false)
            {
                Invoke("SetMouseToCenter", 5);//5秒后调用LaunchProjectile () 函数 
            }
            else
            {
                return;
            }
        }

        if (directorToUp)
        {
            directorToUp = false;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, floatMoveMi));
        }

        if (directorToDown)
        {
            directorToDown = false;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -floatMoveMi));
        }

        if (directorToLeft)
        {
            directorToLeft = false;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-floatMoveMi, 0, 0));
        }

        if (directorToRight)
        {
            directorToRight = false;
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(floatMoveMi, 0, 0));
        }


        ///屏幕1记录时间
        DateTime myDateTime = DateTime.Now;
        string strCountDownTime = Common.DateDiff(myDateTime, DateTimeStart);
        UnityEngine.UI.Text mFinisheText1_TimerCount = GameObject.Find("FinisheText1_TimerCount").GetComponent<UnityEngine.UI.Text>();
        mFinisheText1_TimerCount.text = strCountDownTime;

    }


    void StopMove()
    {
        //2D
        //   gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //3D
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    void SetMouseToCenter()
    {
        if (boolaltDowm) return;
        //return;
        SetCursorPos(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2);
        //  first = Event.current.mousePosition;
        //first = new Vector2(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2);
    }

    void FixedUpdate()
    {
        if (boolaltDowm) return;

        POINT currentPosition = new POINT();
        GetCursorPos(out currentPosition);

        //second = Event.current.mousePosition;

        if (currentPosition.X != UnityEngine.Screen.width / 2 || currentPosition.Y != UnityEngine.Screen.height / 2)
        {//记录鼠标拖动的位置

            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            //Vector2 slideDirection = second - first;

            //SetMouseToCenter();
            float x = currentPosition.X - UnityEngine.Screen.width / 2, y = currentPosition.Y - UnityEngine.Screen.height / 2;


            directorToLeft = false;
            directorToRight = false;
            directorToUp = false;
            directorToDown = false;

            if (x < 0)
            {
                directorToLeft = true;
            }
            else
            {
                directorToRight = true;
            }

            if (y < 0)
            {
                directorToUp = true;
            }
            else
            {
                directorToDown = true;
            }

        }
        else
        {
            //调用一次  
            //Invoke("StopMove", 5);//5秒后调用LaunchProjectile () 函数 
            //StopMove();
        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        //print(collision.collider.tag);
        if (collision.collider.tag == "EnemyGame")
        {
            GameObject objname = collision.collider.gameObject;



            //            Unity 3D 粒子播放
            //原创EW_DUST 最后发布于2018 - 12 - 03 21:18:33 阅读数 1346  收藏
            //    展开
            //查找子物体，并且将得到的物体转换成gameobject
            GameObject objnameLizi = objname.transform.parent.transform.Find("Falsh/ParticleSystemBlcok").gameObject;

            Debug_Log.Call_WriteLog("得到最终子物体的名字是：" + objname.name);

            objnameLizi.GetComponent<ParticleSystem>().Play(); //播放
                                                               //gameObject.GetCompoment<ParticleSystem>().Pause(); //暂停
                                                               //gameObject.GetCompoment<ParticleSystem>().Stop(); //停止

            //被撞得物体原点发出声音（第二个参数用来设置发出声音的世界坐标，不要离AudioListener太远）
            AudioSource.PlayClipAtPoint(AC, transform.localPosition);
            Destroy(objname);



            UnityEngine.UI.Text mTextFinisheText1 = GameObject.Find("FinisheText1").GetComponent<UnityEngine.UI.Text>();
            //UnityEngine.UI.Text mTextFinisheText2 = GameObject.Find("FinisheText2").GetComponent<UnityEngine.UI.Text>();

            Score.setSucessPercent(1);
            string strOK = Score.getSucessPercent();
            mTextFinisheText1.text = strOK;
            //mTextFinisheText2.text = strOK;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pick up")
        {
            Destroy(other.gameObject);
        }
    }

    //调用一次  


    //void LaunchProjectile()
    //{
    //    print("hello");
    //}

}
