using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common
 * 接口名称 DBMDConst
 * 开发人员：-nhy
 * 创建时间：2022/10/17 11:45:40
 * 描述说明：关键词和分号常量
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
  {

    public class DBMDConst
    {
        static DBMDConst()
        {
            #region init DB Main Word;
            Insert = "INSERT ";
            Into = " INTO ";
            Value = " VALUE ";
            Values = " VALUES ";
            Update = "UPDATE ";
            Set = " SET ";
            Where = " WHERE ";
            Delete = "DELETE ";
            From = " FROM ";
            Select = " SELECT ";
            As = " AS ";
            Order = " ORDER ";
            By = " BY ";
            Group = " GROUP ";
            Limit = " LIMIT ";
            Top = " TOP ";
            SimpleTable = "SimpleTable";
            NoIndex = "NoIndex";
            Offset = "Offset";
            SkipNumber = "SkipNumber";
            TakeNumber = "TakeNumber";
            Count = "Count";
            AT = "@";
            SimpleNumber = "SimpleNumber";
            Not = " NOT ";
            Is = " Is ";
            StrNULL = " NULL ";
            On = " On ";
            Over = "Over";
            Ascending = "ASC";
            Descending = "DESC";
            In = "IN";
            Like = "Like";
            True ="true";
            And =" AND ";
            Or =" OR ";
            Left = "Left";
            Right = "Right";
            Inner = "Inner";
            Join = "Join";
            #endregion

            #region sign
            Semicolon = ';';
            Comma = ',';
            Asterisk = '*';
            LeftBracket = '(';
            RightBracket = ')';
            Equal = '=';
            DownLine = '_';
            UnSingleQuotes = '`';
            GreaterThan = '>';
            LessThan = '<';
            Percent = '%';
            SingleQuotes = '\'';
            Dot = '.';


            #endregion

            #region method
            LAST_INSERT_ID = "LAST_INSERT_ID()";
            Scope_identity = "Scope_identity()";
            ROW_NUMBER = "ROW_NUMBER()";

            #endregion

        }
        #region db main word
        public static string Insert;
        public static string Into;
        public static string Value;//
        public static string Values;
        public static string Update;
        public static string Set;
        public static string Where;
        public static string Delete;
        public static string From;
        public static string Select;
        public static string As;
        public static string Order;
        public static string By;
        public static string Group;
        public static string Limit;
        public static string Top;
        public static string SimpleTable;
        public static string NoIndex;
        public static string Offset;
        public static string SkipNumber;
        public static string TakeNumber;
        public static string Count;
        public static string AT;
        public static string SimpleNumber;
        public static string Not;
        public static string Is;
        public static string StrNULL;
        public static string On;
        public static string Over;
        public static string Ascending;
        public static string Descending;
        public static string In;
        public static string Like;
        public static string True;
        public static string And;
        public static string Or;
        public static string Left;
        public static string Right;
        public static string Inner;
        public static string Join;
        #endregion

        #region sign
        /// <summary>
        /// 分号 ;
        /// </summary>
        public static char Semicolon;
        /// <summary>
        /// 逗号
        /// </summary>
        public static char Comma;
        /// <summary>
        /// 星号
        /// </summary>
        public static char Asterisk;
        public static char LeftBracket;
        public static char RightBracket;
        public static char Equal;
        public static char DownLine;
        /// <summary>
        /// 反单引号
        /// </summary>
        public static char UnSingleQuotes;
        public static char GreaterThan;
        public static char LessThan;
        public static char Percent;
        public static char SingleQuotes;
        public static char Dot;
        #endregion

        #region method
        public static string LAST_INSERT_ID;//LAST_INSERT_ID()
        public static string Scope_identity;//Scope_identity()
        public static string ROW_NUMBER;//ROW_NUMBER()
        #endregion;
    }
}
