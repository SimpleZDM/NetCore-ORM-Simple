using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 MysqlConst
 * 开发人员：-nhy
 * 创建时间：2022/9/22 17:40:49
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public static class MysqlConst
    {
        static MysqlConst()
        {
            StrJoins = new string[] { 
                $"{DBMDConst.Inner} {DBMDConst.Join} ", 
                $"{DBMDConst.Left} {DBMDConst.Join} ", 
                $"{DBMDConst.Right} {DBMDConst.Join} " };

            cStrSign = new string[] {
              DBMDConst.LeftBracket.ToString(),
              DBMDConst.RightBracket.ToString(),
              DBMDConst.Equal.ToString(),
              $"{DBMDConst.GreaterThan}{DBMDConst.Equal}",
              $"{DBMDConst.LessThan}{DBMDConst.Equal}",
              DBMDConst.GreaterThan.ToString(),
              DBMDConst.LessThan.ToString(),
              DBMDConst.And, DBMDConst.Or,
              $"{DBMDConst.LessThan}{DBMDConst.GreaterThan}" };
        }

        /// <summary>
        /// 
        /// </summary>
        public static string[] StrJoins;

        /// <summary>
        /// 方法的映射
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>


        /// <summary>
        /// 常用的符号
        /// </summary>
        public static string[] cStrSign;



    }
}
