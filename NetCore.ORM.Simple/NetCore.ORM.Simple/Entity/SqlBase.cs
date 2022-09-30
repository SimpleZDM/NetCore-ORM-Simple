using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 SqlBase
 * 开发人员：-nhy
 * 创建时间：2022/9/28 14:03:27
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public abstract class SqlBase
    {
        public SqlBase()
        {
            StrSqlValue = new StringBuilder();
            DbParams = new List<DbParameter>(10);
         
        }

       
        /// <summary>
        /// 拼装之后的语句
        /// </summary>
        public StringBuilder StrSqlValue { get { return strSqlValue; } set { strSqlValue = value; } }
        /// <summary>
        /// 参数化
        /// </summary>
        public List<DbParameter> DbParams { get { return dbParams; } set { dbParams = value; } }

        /// <summary>
        /// 语句的类型
        /// </summary>
        public eDbCommandType DbCommandType { get { return dbCommandType; } set { dbCommandType = value; } }

     
        private List<DbParameter> dbParams;
        private StringBuilder strSqlValue;
        private eDbCommandType dbCommandType;
    }
}
