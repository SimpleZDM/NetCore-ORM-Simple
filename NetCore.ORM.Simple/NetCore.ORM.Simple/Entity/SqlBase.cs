using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using NetCore.ORM.Simple.Common;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

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
        /// 参数
        /// </summary>
        public List<DbParameter> DbParams { get { return dbParams; } private set {  dbParams = value; } }

        /// <summary>
        /// 语句的类型 添加 删除 查询 更新
        /// </summary>
        public eDbCommandType DbCommandType { get { return dbCommandType; } set { dbCommandType = value; } }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="DbType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"></exception>
        public void AddParameter(eDBType DbType, string key, object value)
        {
            if (Check.IsNullOrEmpty(key))
            {
                throw new Exception(CommonConst.GetErrorInfo(ErrorType.ParamsIsNull));
            }
            DbParameter Parameter = null;
            switch (DbType)
            {
                case eDBType.Mysql:
                    Parameter = new MySqlParameter(key, value);
                    break;
                case eDBType.SqlService:
                    Parameter= new SqlParameter(key,value);
                    break;
                case eDBType.Sqlite:
                    Parameter = new SqliteParameter(key, value);
                    break;
                default:
                    throw new Exception("不支持添加该类型的参数!");
            }
            DbParams.Add(Parameter);
        }


        private List<DbParameter> dbParams;
        private StringBuilder strSqlValue;
        private eDbCommandType dbCommandType;
    }
}
