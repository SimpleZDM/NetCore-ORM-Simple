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

            cStrSign =new string[] { "(", ")", "=", ">=", "<=", ">", "<", "AND", "OR", "<>" };

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

            SqlMainWord = new string[]
            {
                "INSERT","INTO", "VALUE","VALUES",
                "UPDATE","SET","WHERE","DELETE",
                "FROM","SELECT","AS","ORDER","BY",
                "GROUP","LIMIT","TOP","SimpleTable",
                "NoIndex","OFFSET","SkipNumber","TakeNumber",
                "COUNT","@","SimpleNumber"

            };
        }
        /// <summary>
        /// 判断是否是匿名对象的标记
        /// </summary>
        
        private static string Anonymity;

        public static string[] cStrSign;

        private static string[] ErrorDescriptions;

        private static string[] SqlMainWord;

        public static int ZeroOrNull=0;

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

        public static string GetMainWordStr(this MainWordType type)
        {
            if ((int)type <= SqlMainWord.Length)
            {
                return SqlMainWord[(int)type];
            }
            return SqlMainWord[0];
        }
        public static eDataType GetType(Type type)
        {
            eDataType dataType = eDataType.SimpleString;
            if (type.Equals(typeof(int)))
            {
                dataType = eDataType.SimpleInt;
            }
            else if (type.Equals(typeof(float)))
            {
                dataType = eDataType.SimpleFloat;
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
            }
            return dataType;
        }
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

    public enum MainWordType
    {
        Insert= 0,
        Into,
        Value,
        Values,
        Update,
        Set,
        Where,
        Delete,
        From,
        Select,
        As,
        Order,
        By,
        Group,
        Limit,
        Top,
        SimpleTable,//用于sqlService 表的别名
        NoIndex,//数据的行号
        Offset,
        SkipNumber,
        TakeNumber,
        Count,
        AT,
        SimpleNumber//作为count的映射值
    }



    
}
