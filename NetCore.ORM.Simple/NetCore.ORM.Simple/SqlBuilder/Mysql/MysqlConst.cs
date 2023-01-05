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
    internal static class MysqlConst
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
            dicMethods = new Dictionary<string, Func<MethodEntity, string>>();

        //#region mysql
        //public const string _DateDiff = "TIMESTAMPDIFF";
        //public const string _Truncate = "Truncate";
        //public const string _IF = "IF";
        //public const string _ElseIF = "ElseIF";

        dicMethods.Add(MethodConst._Equals,_Equals);
        dicMethods.Add(MethodConst._IsNullOrEmpty, _IsNullOrEmpty);
        dicMethods.Add(MethodConst._IsNull, _IsNullOrEmpty);
        dicMethods.Add(MethodConst._Sum, _Sum);
        dicMethods.Add(MethodConst._Min, _Min);
        dicMethods.Add(MethodConst._Max, _Max);
        dicMethods.Add(MethodConst._Count, _Count);
        dicMethods.Add(MethodConst._Average, _Average);
        dicMethods.Add(MethodConst._FirstOrDefault,_FirstOrDefault);
        dicMethods.Add(MethodConst._Contains, _Contains);
        dicMethods.Add(MethodConst._LeftContains, _LeftContains);
        dicMethods.Add(MethodConst._RightContains, _RightContains);
        dicMethods.Add(MethodConst._Now, _NOW);
        dicMethods.Add(MethodConst._Month, _Month);
        dicMethods.Add(MethodConst._Day, _Day);
        dicMethods.Add(MethodConst._Hour, _Hour);
        dicMethods.Add(MethodConst._Minute, _Minute);
        dicMethods.Add(MethodConst._Second, _Second);
        dicMethods.Add(MethodConst._DATEDIFF, _DATEDIFF);
        dicMethods.Add(MethodConst._Round, _Round);
        dicMethods.Add(MethodConst._Truncate,_Truncate);
        dicMethods.Add(MethodConst._IF,_IF);
        dicMethods.Add(MethodConst._ElseIF,_ElseIF);
        dicMethods.Add(MethodConst._End,_End);
        //dicMethods.Add(MethodConst._ElseIF,_ElseIF);
        }
        public static void AddMysqlExtensMethod(string methodName,Func<MethodEntity,string> method)
        {
            if (Check.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException(nameof(methodName));
            }
            if (Check.IsNull(method))
            {
                throw new ArgumentException(nameof(method));
            }
            if (dicMethods.ContainsKey(methodName))
            {
                throw new Exception($"{methodName} 已经包含，请重新选取一个方法名称!");
            }

            dicMethods.Add(methodName,method);
        }
        public static Dictionary<string,Func<MethodEntity,string>> dicMethods;

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


        #region method
        public static string _Equals(MethodEntity method)
        {
            string value = null;
           
            if (method.Parameters.Count<2)
            {
                throw new ArgumentException(nameof(method.Parameters));
            }
            if (method.IsNot)
            {
                value = $"{method.Parameters[0].DisplayName}{DBMDConst.LessThan}{DBMDConst.GreaterThan}{method.Parameters[1].DisplayName}";
            }
            else
            {
                value = $"{method.Parameters[0].DisplayName}{DBMDConst.Equal}{method.Parameters[1].DisplayName}";
            }
            return value;
        }
        public static string _IsNullOrEmpty(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new ArgumentException(nameof(method.Parameters));
            }
            if (method.IsNot)
            {
                    value = $"{method.Parameters[0].DisplayName} {DBMDConst.Is} {DBMDConst.Not} {DBMDConst.StrNULL}";
            }
            else
            {
                    value = $"{method.Parameters[0].DisplayName} {DBMDConst.Is} {DBMDConst.StrNULL}";
            }
            return value;
        }
        public static string _Sum(MethodEntity method)
        {
            string value = null;

            if (Check.IsNullOrEmpty(method.Parameters))
            {

            }
            else
            {
                value = $" {MethodConst._Sum}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket} ";

            }
            return value;
        }
        public static string _Min(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {

            }
            else
            {
                value = $" {MethodConst._Min}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket} ";

            }
            return value;
        }
        public static string _Max(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {

            }
            else
            {
                value = $" {MethodConst._Max}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket} ";
            }
            return value;
        }
        public static string _Count(MethodEntity method)
        {
            string value = null;
            string name = string.Empty; ;
            if (Check.IsNullOrEmpty(method.Parameters))
            {

            }
            else
            {
                name = method.Parameters[0].DisplayName;

            }
            name = Check.IsNullOrEmpty(name) ? DBMDConst.Asterisk.ToString() : name;
            value = $" {MethodConst._Count}{DBMDConst.LeftBracket}{name}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Average(MethodEntity method)
        {
            
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {

            }
            else
            {
                value = $" {MethodConst._Average}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket} ";
            }
            return value;
        }
        public static string _FirstOrDefault(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {

            }
            value = $" {method.Parameters[0].DisplayName}";
            return value;
        }
        public static string _Contains(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new Exception("使用该Contains请传递中正确的参数!");
            }
            var left = method.Parameters.Where(m=>m.ConditionType==eConditionType.ColumnName).FirstOrDefault();
            var right = method.Parameters.Where(m => m.ConditionType == eConditionType.Constant).FirstOrDefault();
            if (eDataType.SimpleString == right.DataType)
            {
                if (method.IsNot)
                {
                    value = $"{left.DisplayName} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{right.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                }
                else
                {
                    value = $"{left.DisplayName}  {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{right.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";

                }
            }
            else if ((int)eDataType.SimpleArrayInt <= (int)right.DataType
                && (int)eDataType.SimpleListDecimal >= (int)right.DataType)
            {
                if (method.IsNot)
                {
                    value = $"{left.DisplayName} {DBMDConst.Not} {DBMDConst.In} {DBMDConst.LeftBracket}{right.DisplayName}{DBMDConst.RightBracket} ";
                }
                else
                {
                    value = $"{left.DisplayName}  {DBMDConst.In} {DBMDConst.LeftBracket}{right.DisplayName}{DBMDConst.RightBracket}";
                }
            }
            return value;
        }
        public static string _LeftContains(MethodEntity method)
        {
            string value = null;

            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new Exception("使用该LeftContains请传递中正确的参数!");
            }
            var left = method.Parameters.Where(m => m.ConditionType == eConditionType.ColumnName).FirstOrDefault();
            var right = method.Parameters.Where(m => m.ConditionType == eConditionType.Constant).FirstOrDefault();
            if (eDataType.SimpleString == left.DataType)
            {
                if (method.IsNot)
                {
                    value = $"{left.DisplayName} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{right.DisplayName}{DBMDConst.SingleQuotes} ";
                }
                else
                {
                    value = $"{left.DisplayName} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{right.DisplayName}{DBMDConst.SingleQuotes} ";
                }

            }
            return value;
        }
        public static string _RightContains(MethodEntity method)
        {
            string value = null;

            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new Exception("使用该RightContains请传递中正确的参数!");
            }
            var left = method.Parameters.Where(m => m.ConditionType == eConditionType.ColumnName).FirstOrDefault();
            var right = method.Parameters.Where(m => m.ConditionType == eConditionType.Constant).FirstOrDefault();
            if (eDataType.SimpleString == left.DataType)
            {
                if (method.IsNot)
                {
                    value = $"{left.DisplayName} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{right.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                }
                else
                {
                    value = $"{left.DisplayName} {DBMDConst.Like} {DBMDConst.SingleQuotes}{right.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                }

            }
            return value;
        }
        public static string _Round(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new Exception("使用该Round请传递中正确的参数!");
            }
            var left = method.Parameters[0];
            var right = method.Parameters[1];
            value = $"{MethodConst._Round}{DBMDConst.LeftBracket}{left.DisplayName}{DBMDConst.Comma}{right.Value}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Truncate(MethodEntity method)
        {
            string value = null;

            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new Exception("使用该Truncate请传递中正确的参数!");
            }
            var left = method.Parameters[0];
            var right = method.Parameters[0];
            value = $"{MethodConst._Truncate}{DBMDConst.LeftBracket}{left.DisplayName}{DBMDConst.Comma}{right.DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _DATEDIFF(MethodEntity method)
        {
            string value = null;

            if (Check.IsNullOrEmpty(method.Parameters) || method.Parameters.Count<2)
            {
                throw new Exception("使用该DATEDIFF请传递中正确的参数!");
            }
            var left = method.Parameters[0];
            var right = method.Parameters[1];
            string type = method.Parameters[2].Value.ToString();
            value = $"{MethodConst._DateDiff}{DBMDConst.LeftBracket}{type}{DBMDConst.Comma}{left.DisplayName}{DBMDConst.Comma}{right.DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _NOW(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Now}{DBMDConst.LeftBracket}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Year(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Year}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Month(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Month}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Day(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Day}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Hour(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Hour}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Minute(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Minute}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _Second(MethodEntity method)
        {
            string value = null;
            value = $"{MethodConst._Second}{DBMDConst.LeftBracket}{method.Parameters[0].DisplayName}{DBMDConst.RightBracket}";
            return value;
        }
        public static string _IF(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new ArgumentException();
            }

           
            method.Parameters[0].DisplayName = method.Parameters[0].DisplayName.ToString().Replace("\'","\'\'");
            value = $"{DBMDConst.Case} {DBMDConst.When}{DBMDConst.LeftBracket}{method.Extensions}{DBMDConst.RightBracket}{DBMDConst.Then} {method.Parameters[0].DisplayName}";
            if (method.Parameters.Count==2)
            {

            }
            return value;
        }
        public static string _ElseIF(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new ArgumentException();
            }


            method.Parameters[0].DisplayName = method.Parameters[0].DisplayName.ToString().Replace("\'", "\'\'");

            value = $" {DBMDConst.When}{DBMDConst.LeftBracket}{method.Extensions}{DBMDConst.RightBracket}{DBMDConst.Then} {method.Parameters[0].DisplayName} ";
            return value;
        }
        public static string _End(MethodEntity method)
        {
            string value = null;
            if (Check.IsNullOrEmpty(method.Parameters))
            {
                throw new ArgumentException();
            }
            method.Parameters[0].DisplayName = method.Parameters[0].DisplayName.ToString().Replace("\'", "\'\'");
            value = $" {DBMDConst.Else} {method.Parameters[0].DisplayName} {DBMDConst.End} ";
            return value;
        }
        #endregion

    }
}
