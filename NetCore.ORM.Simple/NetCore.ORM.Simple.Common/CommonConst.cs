using System;
using System.Collections.Generic;
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
        const string Anonymity = "<>f__AnonymousType";
        public static string[]
          cStrSign =
          new string[] { "(", ")", "=", ">=", "<=", ">", "<", "AND", "OR", "<>" };

        public static string[] ErrorDescriptions = new string[] 
        {    "错误!",
            "没有为实体配置主键!", 
            "删除数据,请指定删除的条件!",
            "sql 语句条件部分解析有误!"
        }; 

        public static int ZeroOrNull=0;

        public static char[] Letters = new char[] 
        { 'a', 'b','c','d','e','f','g','h','i','j'
         ,'k','l','m','n','o','p','q','r','s','t',
          'u','v','w','x','y','z',
          'A','B','C','D','E','F','G','H','I','J'
         ,'K','L','M','N','O','P','Q','R','S','T'
         ,'U','V','W','X','Y','Z'};

        public static bool IsAnonymityObject<T>()
        {
            Type type = typeof(T);
            if (type.GetClassName().Contains(Anonymity))
            {
                return true;
            }
            return false;
        }

        public static string StrDataCount = "Number";

        public static string GetErrorInfo(this ErrorType type)
        {
            if ((int)type<= ErrorDescriptions.Length)
            {
                return ErrorDescriptions[(int)type];
            }
            return ErrorDescriptions[0];
        }
    }
    public enum ErrorType
    {
        Error=0,
        NotKey,
        DeleteNotMatch,
        SqlAnalysis
    }
}
