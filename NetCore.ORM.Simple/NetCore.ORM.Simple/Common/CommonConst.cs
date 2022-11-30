using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common
 * 接口名称 CommonConst
 * 开发人员：-nhy
 * 创建时间：2022/9/20 11:47:39
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    public static class CommonConst
    {
        static CommonConst()
        {
            Anonymity = "<>f__AnonymousType";


            ErrorDescriptions = new string[]
                    {"错误!",
                    "没有为实体配置主键!",
                    "删除数据,请指定删除的条件!",
                    "sql 语句条件部分解析有误!",
                     "链接字符串为空!",
                    "数据库参数不能为空!",
                     };
            Letters = new char[]
                { 'a', 'b','c','d','e','f','g','h','i','j'
                 ,'k','l','m','n','o','p','q','r','s','t',
                 'u','v','w','x','y','z',
                  'A','B','C','D','E','F','G','H','I','J'
                 ,'K','L','M','N','O','P','Q','R','S','T'
                    ,'U','V','W','X','Y','Z'
                };
            StrDataCount = "SimpleNumber";

            SystemDateTimeNow ="System.DateTime Now";
            SystemDateTimeMaxValue= "System.DateTime MaxValue";
            SystemDateTimeMinValue= "System.DateTime MinValue";
            SystemGuidEmpty= "System.Guid Empty";
            SystemintMaxValue= "System.int MaxValue";
            SystemintMinValue = "System.int MinValue";
            SystemdoubleMinValue = "System.double MinValue";
            SystemdoubleMaxValue = "System.double MaxValue";
            SystemfloatMaxValue = "System.float MaxValue";
            SystemfloatMinValue = "System.float MinValue";
            SystemdecimalMinValue = "System.decimal MinValue";
            SystemdecimalMaxValue = "System.decimal MaxValue";
        }
        /// <summary>
        /// 判断是否是匿名对象的标记
        /// </summary>
        
        private static string Anonymity;


        private static string[] ErrorDescriptions;


        public static int Zero=0;
        public static int One=1;

        /// <summary>
        /// 大小写字母
        /// </summary>
        public static char[] Letters; 

        public static string StrDataCount;

        public static string[] StrDataType;



        public static bool IsAnonymityObject<T>(Type AttrType)
        {
            Type type = typeof(T);
            if (type.GetTableName(AttrType).Contains(Anonymity))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取错误描述信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetErrorInfo(this ErrorType type)
        {
            if ((int)type<= ErrorDescriptions.Length)
            {
                return ErrorDescriptions[(int)type];
            }
            return ErrorDescriptions[0];
        }

       
        public static eDataType GetType(Type type)
        {
            eDataType dataType = eDataType.NuKnow;
            if (type.Equals(typeof(int)))
            {
                dataType = eDataType.SimpleInt;
            }
            else if (type.Equals(typeof(float)))
            {
                dataType = eDataType.SimpleFloat;
            }
            else if (type.Equals(typeof(string)))
            {
                dataType = eDataType.SimpleString;
            }
            else if (type.Equals(typeof(double)))
            {
                dataType = eDataType.SimpleDouble;
            }
            else if (type.Equals(typeof(decimal)))
            {
                dataType = eDataType.SimpleDecimal;
            }
            else if (type.Equals(typeof(List<int>)))
            {
                dataType = eDataType.SimpleListInt;
            }
            else if (type.Equals(typeof(List<string>)))
            {
                dataType = eDataType.SimpleListString;
            }
            else if (type.Equals(typeof(List<Guid>)))
            {
                dataType = eDataType.SimpleListGuid;
            }
            else if (type.Equals(typeof(List<float>)))
            {
                dataType = eDataType.SimpleListFloat;
            }
            else if (type.Equals(typeof(List<double>)))
            {
                dataType = eDataType.SimpleListDouble;
            }
            else if (type.Equals(typeof(List<decimal>)))
            {
                dataType = eDataType.SimpleListDecimal;
            }
            else if (type.Equals(typeof(int[])))
            {
                dataType = eDataType.SimpleArrayInt;
            }
            else if (type.Equals(typeof(Guid[])))
            {
                dataType = eDataType.SimpleArrayGuid;
            }
            else if (type.Equals(typeof(float[])))
            {
                dataType = eDataType.SimpleArrayFloat;
            }
            else if (type.Equals(typeof(double[])))
            {
                dataType = eDataType.SimpleArrayDouble;
            }
            else if (type.Equals(typeof(decimal[])))
            {
                dataType = eDataType.SimpleArrayDecimal;
            }
            else if (type.Equals(typeof(string[])))
            {
                dataType = eDataType.SimpleArrayString;
            }else if (type.IsArray)
            {
                dataType = eDataType.SimpleArray;

            }else if (type.Name.Contains("List`"))
            {
                dataType=eDataType.SimpleList;
            }
            return dataType;
        }


        public static string SystemDateTimeNow;
        public static string SystemDateTimeMaxValue;
        public static string SystemDateTimeMinValue;
        public static string SystemGuidEmpty;
        public static string SystemintMaxValue;
        public static string SystemintMinValue;
        public static string SystemdoubleMinValue;
        public static string SystemdoubleMaxValue;
        public static string SystemfloatMaxValue;
        public static string SystemfloatMinValue;
        public static string SystemdecimalMinValue;
        public static string SystemdecimalMaxValue;
    }
    public enum ErrorType
    {
        Error=0,//错误
        NotKey,//没有主键
        DeleteNotMatch,//删除没有指定条件
        SqlAnalysis,//sql解析错误
        ConnectionStrIsNull,
        ParamsIsNull
    }

    


    




}
