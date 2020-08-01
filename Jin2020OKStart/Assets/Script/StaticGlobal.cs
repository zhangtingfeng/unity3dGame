using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    /// <summary>
    /// 全局通用变量
    /// </summary>
    public class StaticGlobal

    {
        /// <summary>
        /// 训练的用户名ID  80是缺省值  这样做是是为了方便记录页面进行测试
        /// </summary>
        public static int UserID = 80;
        /// <summary>
        /// 训练的用户名
        /// </summary>
        public static string UserName = "";
        /// <summary>
        /// 训练的用户的当前训练ID，
        /// </summary>
        public static int TrainID = 0;

        /// <summary>
        /// 训练开始时间，
        /// </summary>
        public static DateTime startTime = DateTime.MinValue;

        /// <summary>
        /// 训练持续时间
        /// </summary>
        public static Int64 TimeDuratation = 0;
    }
}
