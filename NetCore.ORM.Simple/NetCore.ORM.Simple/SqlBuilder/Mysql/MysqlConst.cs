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
            StrJoins= new string[] { "INNER JOIN", "LEFT JOIN", "RIGHT JOIN" };
        }
        public static char EqualSign = '=';

        public const int INSERTMAX = 800;

        public static string[] StrJoins;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static string MapMethod(string methodName,string leftValue,string rightValue)
        {
            string value = EqualSign.ToString();
            if (Check.IsNullOrEmpty(methodName))
            {
                return value;
            }
            switch (methodName)
            {
                case "ToString":
                    break;
                case "Equals":
                    value = $"{leftValue}={rightValue}";
                    break;
                case "IsNullOrEmpty":
                    if (!Check.IsNullOrEmpty(leftValue)) 
                    {
                        value = $"{leftValue} IS NULL";
                    }else if (!Check.IsNullOrEmpty(rightValue))
                    {
                        value = $"{rightValue} IS NULL";
                    }
                    break;
                case "Sum":
                    value = $" SUM({leftValue}) ";
                    break;
                case "Min":
                    value = $" Min({leftValue}) ";
                    break;
                case "Max":
                    value = $" Max({leftValue}) ";
                    break;
                case "Count":
                    var star = "*";
                    leftValue = Check.IsNullOrEmpty(leftValue) ? star : leftValue;
                    value = $" COUNT({leftValue}) ";
                    break;
                case "Average":
                    value = $" AVG({leftValue}) ";
                    break;
                default:
                    break;
            }
            return value;
        }

        public static string[]
          cStrSign =
          new string[] { "(", ")", "=", ">=", "<=", ">", "<", "AND", "OR", "<>" };

        public static string AscendOrDescend(eOrderType OrderType)
        {
            string value=string.Empty;
            switch (OrderType)
            {
                case eOrderType.Ascending:
                    value = "ASC";
                    break;
                case eOrderType.Descending:
                    value = "DESC";
                    break;
                default:
                    break;
            }
            return value;
        }


    }
}
