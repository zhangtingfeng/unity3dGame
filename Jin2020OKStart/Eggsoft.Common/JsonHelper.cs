using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using Newtonsoft.Json;

//Insure.Common.XmlHelper
namespace Eggsoft.Common
{


    public class JsonHelper
    {
        public class typeMedia
        {//{"type":"image","media_id":"fUswgG1Wl48Fam3nKVuEVgWlM2Y2UTfEtvcaSozvGd8kasF7kp0fOHtdFGIF1f8q","created_at":1388304068}

            public string type { get; set; }
            public string media_id { get; set; }
            public string thumb_media_id { get; set; }
            public string created_at { get; set; }
        }

        //

            
        #region 新一代的

        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 新一代的解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 新一代的解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        ///<summary>
        ///新一代的反序列化JSON到给定的匿名对象.
        ///<para>var tempEntity = new { ID = 0, Name1 = string.Empty };</para>
        //<para>string json5 = JsonHelper.SerializeObject(tempEntity);</para>
        ////<para>json5 : {"ID":0,"Name":""}</para>
        //<para>tempEntity = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempEntity);</para>
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }
        #endregion 新一代的



       
    }
    
}