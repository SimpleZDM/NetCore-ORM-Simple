using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 CEnum
 * 开发人员：-nhy
 * 创建时间：2022/9/14 11:54:36
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public enum eDBType
    {
        Mysql=0,
        SqlService,
        Sqlite
    }
    //数据库读写类型
    public enum eWriteOrReadType
    {
        Read=0,
        Write=1,
        ReadOrWrite=2,
        
    }

    public enum eJoinType
    {
        Inner=0,
        Left,
        Right,
    }
    // new string[] {"(",")","=",">=","<=",">","<","AND","OR"};
    public enum eSignType
    {
        LeftBracket=0,
        RightBracket,
        Equal,
        GreatThanOrEqual,
        LessThanOrEqual,
        GrantThan,
        LessThan,
        And,
        Or,
        NotEqual,
    }

    public enum eTableType
    {
        Master=0,
        Slave
    }

    public enum eDbCommandType
    {
        Insert=0,//执行命令-
        Update,//执行命令-
        Delete,//执行命令-
        Query//执行查询语句 select 
        
    }

    public enum eConditionType
    {
        Sign=0,//符号-
        Method,//方法
        ColumnName,//列名称
        Constant,//常量
        IgnoreSign//忽视的符号

    }

    public enum eOrderOrGroupType
    {
        OrderBy=0,
        GroupBy
    }
    /// <summary>
    /// 升序降序
    /// </summary>
    public enum eOrderType
    {
       Ascending=0,
       Descending=1,
    }
    
    public enum eDataType
    {
        SimpleString=0,//字符串
        SimpleInt,//字符串
        SimpleGuid,//字符串
        SimpleTime,//字符串
        SimpleFloat,//字符串
        SimpleDouble,//字符串
        SimpleDecimal,//字符串
        SimpleArrayInt,//字符串
        SimpleArrayString,
        SimpleArrayGuid,
        SimpleArrayDouble,
        SimpleArrayFloat,
        SimpleArrayDecimal,
        SimpleListInt,
        SimpleListString,
        SimpleListGuid,
        SimpleListFloat,
        SimpleListDouble,
        SimpleListDecimal,
        SimpleArray,
        SimpleList,
        NuKnow
    }
}
