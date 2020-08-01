
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Script.DB
{
    public class ClassWriteRecord
    {



        public static string writeDBRecord()
        {


            string strInputFieldName = "";
            try
            {
                ReadIniPar ReadIniPargetParme = getPar.getParme();
                //ContentList = new ArrayList(strContent.Split(','));


                List<String> listDBFieldName = new List<string>();
                listDBFieldName.Add("UserID");
                listDBFieldName.Add("BeginTime");

                listDBFieldName.Add("Duration");
                listDBFieldName.Add("difficulty");
                listDBFieldName.Add("CreateTime");



                List<String> listValues = new List<string>();
                listValues.Add(StaticGlobal.UserID.toString());
                listValues.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                listValues.Add(ReadIniPargetParme.LengthofTime.toString());
                listValues.Add(ReadIniPargetParme.speed.toString());
                listValues.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));


                MySQLManager dddMySQLManager = new MySQLManager();
                dddMySQLManager.InsertInto("train", listDBFieldName.ToArray(), listValues.ToArray());
                System.Data.DataTable ddddtrain = dddMySQLManager.QuerySet("SELECT max(ID) from train where UserID=" + StaticGlobal.UserID.toString()).Tables[0];
                StaticGlobal.TrainID = ddddtrain.Rows[0][0].toInt32();
                Debug_Log.Call_WriteLog(StaticGlobal.TrainID.toString(), "执行Sql语句dsStaticGlobal  ", "Unity");
            }
            catch (Exception ex)
            {
                ////LogController.writeErrorLog(ex, "ObjectExtended toInt32");
                Debug_Log.Call_WriteLog(ex, "readAllDB");

            }
            return strInputFieldName;
            //System.Data.DataSet myDataSet =
        }


    }
}
