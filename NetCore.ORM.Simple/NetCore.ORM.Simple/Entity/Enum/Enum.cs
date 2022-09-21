﻿using System;
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
        SqlService=1,
    }
    //数据库读写类型
    public enum eWriteOrReadType
    {
        Read=0,
        ReadOrWrite=1,
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
}
