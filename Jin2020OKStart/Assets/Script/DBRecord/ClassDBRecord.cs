
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Script.DB
{
    public class ClassDBRecord
    {
        private static List<GameObject> dtMenuLineBak = new List<GameObject>();

        /// <summary>
        /// 选择的用户ID
        /// </summary>

        public static int SelectUserID = 0;

        /// <summary>
        /// 选择的记录ID
        /// </summary>
        public static int SelectUserRecordID = 0;

        /// <summary>
        /// 取最大的UserID
        /// </summary>
        public static int maxtUserRecordID = 0;




        public static void readAllDBRcord()
        {
            try
            {
                //MySQLManager myDatabaseManager = new MySQLManager();
                //bool boolContest = myDatabaseManager.TestConnection();
                //GameObject mTextsearch = GameObject.Find("TextsearchSQL");
                //mTextsearch.GetComponent<UnityEngine.UI.Text>().text = boolContest;
                //Debug_Log.Call_WriteLog(boolContest);
                MySQLManager dddMySQLManager = new MySQLManager();
                ///int intK = dddMySQLManager.SqlRecordCount("select ID from medicaluser");
                System.Data.DataTable ddddmedicaluser = dddMySQLManager.QuerySet("select ID,BeginTime,(case Duration when 0 then '一般' when 1 then '中等' when 2 then '难' end)Duration,(case difficulty when 0 then '一般' when 1 then '中等' when 2 then '难' end)difficulty,Content,Result,NeedSucessAll,SucessNum from train where IsDeleted=0 and userID=" + SelectUserID + " order by id desc").Tables[0];
                int intK = ddddmedicaluser.Rows.Count;

                GameObject mUICanvas = GameObject.Find("TableGameObjectAllText");
                int childCount = mUICanvas.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = mUICanvas.transform.GetChild(i);
                    UnityEngine.Object.Destroy(child.gameObject);
                }


                //Debug_Log.Call_WriteLog("child setClickButton..="+ childCount);
                //查找子物体，并且将得到的物体转换成gameobject
                //GameObject objname = mUICanvas.transform.FindChild("ImageOne/TextDB").gameObject;
                //objname.GetComponent<UnityEngine.UI.Text>().text = "dddd";
                //return;

                //var tmpSize = GameObject.Find("TableGameObjectAllText").GetComponent<RectTransform>().rect.height;
                ////GameObject.Find("TableGameObjectAllText").GetComponent<RectTransform>().sizeDelta = new Vector2(445,3000);
                //Debug_Log.Call_WriteLog(tmpSize);
                dtMenuLineBak.Clear();

                for (int i = 0; i < intK; i++)
                {
                    dtMenuLineBak.Add(null);
                }
                string LineImagebak = "Prefabs/ImageOne2TrainRecord";


                for (int i = 0; i < dtMenuLineBak.Count; i++)
                {
                    dtMenuLineBak[i] = (GameObject)UnityEngine.Object.Instantiate(Resources.Load(LineImagebak), new Vector3(0f, 0f, 0), Quaternion.identity);

                    dtMenuLineBak[i].transform.SetParent(mUICanvas.transform);
                    RectTransform rtr = dtMenuLineBak[i].GetComponent<RectTransform>();
                    //定义控件定位点相对基准位置的偏移

                    int intoff = 300 - i * 80 - 20;
                    if (dtMenuLineBak.Count > 7) intoff = intoff + (dtMenuLineBak.Count - 8) * 40;

                    rtr.anchoredPosition = new Vector2(0, intoff);


                    Transform objnamethisTransform = dtMenuLineBak[i].transform;
                    objnamethisTransform.Find("OneNum").gameObject.GetComponent<UnityEngine.UI.Text>().text = ddddmedicaluser.Rows[i]["ID"].ToString();
                    objnamethisTransform.Find("TwoName").gameObject.GetComponent<UnityEngine.UI.Text>().text = DateTime.Parse(ddddmedicaluser.Rows[i]["BeginTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    objnamethisTransform.Find("ThreeSex").gameObject.GetComponent<UnityEngine.UI.Text>().text = ddddmedicaluser.Rows[i]["Duration"].ToString();
                    objnamethisTransform.Find("FourAge").gameObject.GetComponent<UnityEngine.UI.Text>().text = ddddmedicaluser.Rows[i]["difficulty"].ToString();
                    objnamethisTransform.Find("FiveHospitalNum").gameObject.GetComponent<UnityEngine.UI.Text>().text = ddddmedicaluser.Rows[i]["Content"].ToString();
                    objnamethisTransform.Find("SixCreateTime").gameObject.GetComponent<UnityEngine.UI.Text>().text = ddddmedicaluser.Rows[i]["Result"].ToString();
                }
                if (dtMenuLineBak.Count > 0)
                {
                    maxtUserRecordID = ddddmedicaluser.Rows[dtMenuLineBak.Count - 1]["ID"].toInt32();
                }
                //定义控件大小
                RectTransform rtrtable = mUICanvas.GetComponent<RectTransform>();
                rtrtable.sizeDelta = new Vector2(1617, dtMenuLineBak.Count * 80);
            }
            catch (Exception ex)
            {
                ////LogController.writeErrorLog(ex, "ObjectExtended toInt32");
                Debug_Log.Call_WriteLog(ex, "readAllDB");

            }
        }

        public static void deleteAllDBRecord()
        {
            if (SelectUserRecordID != 0)
            {
                MySQLManager dddMySQLManager = new MySQLManager();
                bool boolddd = dddMySQLManager.ExecuteSqlCmd("update train set   IsDeleted=1,ModifyTime=now(),ModifyBy='delete' where id=" + SelectUserRecordID);
            }
        }
    }
}
