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
    public static class Check
    {
        public static bool IsNull<T>(this T t) where T : class
        {
            if (t.Equals(null))
            {
                return true;
            }
            return false;
        }

        public static bool IsNullOrEmpty(this string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return true;
            }
            return false;
        }
    }
}
