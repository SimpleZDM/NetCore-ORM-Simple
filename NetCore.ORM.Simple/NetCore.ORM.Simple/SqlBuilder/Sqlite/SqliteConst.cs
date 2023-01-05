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
    internal class SqliteConst
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

        #region 不同数据库实现不同
        public static string _Equals(string leftValue, string rightValue, bool IsNot)
        {
            string value = null;
            if (IsNot)
            {
                value = $"{leftValue}{DBMDConst.LessThan}{DBMDConst.GreaterThan}{rightValue}";
            }
            else
            {
                value = $"{leftValue}{DBMDConst.Equal}{rightValue}";
            }
            return value;
        }
        public static string _IsNullOrEmpty(string leftValue, string rightValue, bool IsNot)
        {
            string value = null;
            if (IsNot)
            {
                if (!Check.IsNullOrEmpty(leftValue))
                {
                    value = $"{leftValue} {DBMDConst.Is} {DBMDConst.Not} {DBMDConst.StrNULL}";
                }
                else if (!Check.IsNullOrEmpty(rightValue))
                {
                    value = $"{rightValue} {DBMDConst.Is} {DBMDConst.Not} {DBMDConst.StrNULL}";
                }
            }
            else
            {
                if (!Check.IsNullOrEmpty(leftValue))
                {
                    value = $"{leftValue} {DBMDConst.Is} {DBMDConst.StrNULL}";
                }
                else if (!Check.IsNullOrEmpty(rightValue))
                {
                    value = $"{rightValue} {DBMDConst.Is}  {DBMDConst.StrNULL}";
                }
            }
            return value;
        }

        public static string _Sum(string leftValue)
        {
            string value = null;

            value = $" {MethodConst._Sum}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";

            return value;
        }
        public static string _Min(string leftValue)
        {
            string value = null;

            value = $" {MethodConst._Min}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";

            return value;
        }
        public static string _Max(string leftValue)
        {
            string value = null;

            value = $" {MethodConst._Min}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";

            return value;
        }
        public static string _Count(string leftValue)
        {
            string value = null;
            leftValue = Check.IsNullOrEmpty(leftValue) ? DBMDConst.Asterisk.ToString() : leftValue;
            value = $" {MethodConst._Count}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Average(string leftValue)
        {
            string value = null;
            value = $" {MethodConst._Average}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";
            return value;
        }
        public static string _FirstOrDefault(string leftValue)
        {
            string value = null;
            value = $" {leftValue}";
            return value;
        }

        public static string _Contains(string leftValue, bool IsNot, params ConditionEntity[] conditions)
        {
            string value = null;
            if (Check.IsNullOrEmpty(conditions) || conditions.Length < 1)
            {
                throw new Exception("使用该Contains请传递中正确的参数!");
            }
            var condition = conditions[0];
            if (eDataType.SimpleString == condition.DataType)
            {
                if (IsNot)
                {
                    value = $"{leftValue} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                }
                else
                {
                    value = $"{leftValue}  {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";

                }
            }
            else if ((int)eDataType.SimpleArrayInt <= (int)condition.DataType
                && (int)eDataType.SimpleListDecimal >= (int)condition.DataType)
            {
                if (IsNot)
                {
                    value = $"{leftValue} {DBMDConst.Not} {DBMDConst.In} {DBMDConst.LeftBracket}{condition.DisplayName}{DBMDConst.RightBracket} ";
                }
                else
                {
                    value = $"{leftValue}  {DBMDConst.In} {DBMDConst.LeftBracket}{condition.DisplayName}{DBMDConst.RightBracket}";
                }
            }
            return value;
        }

        public static string _LeftContains(string leftValue, bool IsNot, params ConditionEntity[] conditions)
        {
            string value = null;

            if (Check.IsNullOrEmpty(conditions) || conditions.Length < 1)
            {
                throw new Exception("使用该LeftContains请传递中正确的参数!");
            }
            var condition = conditions[0];
            if (eDataType.SimpleString == condition.DataType)
            {
                if (IsNot)
                {
                    value = $"{leftValue} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.SingleQuotes} ";
                }
                else
                {
                    value = $"{leftValue} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.SingleQuotes} ";
                }

            }
            return value;
        }

        public static string _RightContains(string leftValue, bool IsNot, params ConditionEntity[] conditions)
        {
            string value = null;

            if (Check.IsNullOrEmpty(conditions) || conditions.Length < 1)
            {
                throw new Exception("使用该RightContains请传递中正确的参数!");
            }
            var condition = conditions[0];
            if (eDataType.SimpleString == condition.DataType)
            {
                if (IsNot)
                {
                    value = $"{leftValue} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                }
                else
                {
                    value = $"{leftValue} {DBMDConst.Like} {DBMDConst.SingleQuotes}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                }

            }
            return value;
        }

        public static string _Round(string leftValue, params ConditionEntity[] conditions)
        {
            string value = null;

            if (Check.IsNullOrEmpty(conditions) || conditions.Length < 1)
            {
                throw new Exception("使用该Round请传递中正确的参数!");
            }
            var condition = conditions[0];
            value = $"{MethodConst._Round}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.Comma}{condition.Value}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Truncate(string leftValue, params ConditionEntity[] conditions)
        {
            string value = null;

            if (Check.IsNullOrEmpty(conditions) || conditions.Length < 1)
            {
                throw new Exception("使用该Truncate请传递中正确的参数!");
            }
            var condition = conditions[0];
            value = $"{MethodConst._Truncate}{DBMDConst.LeftBracket}{leftValue}{DBMDConst.Comma}{condition.Value}{DBMDConst.RightBracket}";
            return value;
        }

        public static string _DATEDIFF(string leftValue, params ConditionEntity[] conditions)
        {
            string value = null;

            if (Check.IsNullOrEmpty(conditions) || conditions.Length < 1)
            {
                throw new Exception("使用该DATEDIFF请传递中正确的参数!");
            }
            var condition = conditions[0];
            condition = conditions[0];
            string type = conditions[1].Value.ToString();
            value = $"{MethodConst._DateDiff}{DBMDConst.LeftBracket}{type}{DBMDConst.Comma}{leftValue}{DBMDConst.Comma}{condition.DisplayName}{DBMDConst.RightBracket}";
            return value;
        }

        public static string _NOW()
        {
            string value = null;
            value = $"{MethodConst._Now}{DBMDConst.LeftBracket}{DBMDConst.RightBracket}";
            return value;
        }
        #endregion

    }
}
