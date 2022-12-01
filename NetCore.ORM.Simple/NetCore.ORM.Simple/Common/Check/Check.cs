using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common
 * 接口名称 Check
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:19:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    internal static class Check
    {
        public static bool IsNull<T>(this T t) where T : class
        {
            if (t==null)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T>(this T[]datas)
        {
            if (datas==null||datas.Count()==0)
            {
                return true;
            }return false;
        }
        public static bool IsNullOrEmpty<T>(this List<T> datas)
        {
            if (datas == null || datas.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty<T,Value>(this T datas)where T : IEnumerable<Value>
        {
            if (datas == null || datas.Count() == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty(this string strValue)
        {
            if (strValue==null||string.IsNullOrEmpty(strValue))
            {
                return true;
            }
            return false;
        }
       
        public static bool IsMethodMultiParams(string methodName)
        {
            if (Check.IsNullOrEmpty(methodName))
            {
                return false;
            }
            if (methodName==MethodConst._DATEDIFF)
            {
                return true;
            }
            return false;
        }

    }
}
