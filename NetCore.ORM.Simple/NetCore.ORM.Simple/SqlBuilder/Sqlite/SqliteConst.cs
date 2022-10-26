using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 SqlServiceConst
 * 开发人员：-nhy
 * 创建时间：2022/10/11 13:58:24
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public class SqliteConst
    {
        static SqliteConst()
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
             DBMDConst.And,DBMDConst.Or,
              $"{DBMDConst.LessThan}{DBMDConst.GreaterThan}" };
        }
        /// <summary>
        /// 单条语句最多插入的量
        /// insert into [table](*****) value(),value();罪过八百个value
        /// </summary>
        public const int INSERTMAX = 800;
        /// <summary>
        ///多个insert组合
        ///insert into [table]() value();insert into [table]() value();.....
        /// </summary>
        public const int INSERTMAXCOUNT = 30;

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

        public static string AscendOrDescend(eOrderType OrderType)
        {
            string value = string.Empty;
            switch (OrderType)
            {
                case eOrderType.Ascending:
                    value = DBMDConst.Ascending;
                    break;
                case eOrderType.Descending:
                    value = DBMDConst.Descending;
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}
