
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Script.DB
{
    public class ClassWriteUser
    {


        private static List<String> GetInputFieldNameList()
        {
            List<String> listFieldName = new List<string>();
            listFieldName.Add("InputFieldName");
            listFieldName.Add("InputFieldHospitalNum");
            listFieldName.Add("InputFieldAge");
            //listFieldName.Add("InputFieldBirthday");
            listFieldName.Add("InputFieldDoctor");
            listFieldName.Add("InputFieldAddress");
            listFieldName.Add("InputFieldTel");
            listFieldName.Add("InputFieldTeacher");
            listFieldName.Add("InputFieldResult");

            return listFieldName;
        }

        public static void resetInputFieldName()
        {
            List<String> myGetInputFieldNameList = GetInputFieldNameList();
            for (int i = 0; i < myGetInputFieldNameList.Count; i++)
            {
                GameObject mGameObjectAddUser = GameObject.Find("AddUser");
                GameObject objnamethis = mGameObjectAddUser.transform.Find(myGetInputFieldNameList[i]).gameObject;
                GameObject GameObjectText = objnamethis.transform.Find("Text").gameObject;

                (GameObjectText.GetComponent<UnityEngine.UI.Text>().text) = "";

            }
        }



        public static string writeDBUser()
        {
            List<String> listDBFieldName = new List<string>();
            listDBFieldName.Add("Name");
            listDBFieldName.Add("MedicalRecordNo");

            listDBFieldName.Add("Age");
            listDBFieldName.Add("Doctor");
            listDBFieldName.Add("Address");
            listDBFieldName.Add("Tel");
            listDBFieldName.Add("Teacher");
            listDBFieldName.Add("Result");
            listDBFieldName.Add("Sex");
            listDBFieldName.Add("CreateTime");
            listDBFieldName.Add("CreateBy");


            List<String> myGetInputFieldNameList = GetInputFieldNameList();

            List<String> listValues = new List<string>();
            for (int i = 0; i < myGetInputFieldNameList.Count; i++)
            {
                listValues.Add(getInputFieldName(myGetInputFieldNameList[i]));
            }


            listValues.Add(SelectBoyGirl.boolboy.toInt32().ToString());///性别
            listValues.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            listValues.Add("创建");


            MySQLManager dddMySQLManager = new MySQLManager();
            dddMySQLManager.InsertInto("medicaluser", listDBFieldName.ToArray(), listValues.ToArray());
            return getInputFieldName("Name");


            //System.Data.DataSet myDataSet =
        }

        private static string getInputFieldName(string strText)
        {
            string strReadText = "";
            try
            {
                GameObject mGameObjectAddUser = GameObject.Find("AddUser");
                GameObject objnamethis = mGameObjectAddUser.transform.Find(strText).gameObject;
                GameObject GameObjectText = objnamethis.transform.Find("Text").gameObject;

                strReadText = (GameObjectText.GetComponent<UnityEngine.UI.Text>().text);
                objnamethis.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "1111";
            }
            catch (Exception eee)
            {
                Debug_Log.Call_WriteLog(eee, "getInputFieldNameClassWriteUser", "Unity");
                Debug_Log.Call_WriteLog(strText, "getInputFieldNameClassWriteUser", "Unity");
            }

            return strReadText;
        }

    }
}
