using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 SimpleConst
 * 开发人员：-nhy
 * 创建时间：2022/9/19 14:16:23
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public static class SimpleConst
    {
        static SimpleConst()
        {
            map_Method=new Dictionary<string, string>();
            map_Method.Add("Equal","=");
            map_Method.Add("IsNullOrEmpty", "IS NULL");
            map_Method.Add("Contains", "Like");

        }
        public static string[]
           cStrSign =
           new string[] { "(", ")", "=", ">=", "<=", ">", "<", "AND", "OR", "<>" };

        public const int minTableCount =0;
        /// <summary>
        /// 与sql对应的一些方法
        /// </summary>
        public static Dictionary<string, string> map_Method;
        public static Dictionary<string,string> MysqlJoins;
    }
}
