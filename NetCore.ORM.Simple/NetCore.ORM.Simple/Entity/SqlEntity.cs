using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 SqlEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/14 11:59:20
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class SqlEntity
    {
        //const int pLength = 10;
        public SqlEntity()
        {
            Sb_Sql=new StringBuilder();
            DbParams = new List<DbParameter>(10);
            PageSize = -1;
            PageNumber = -1;
        }
        /// <summary>
        /// 拼装之后的语句
        /// </summary>
        public StringBuilder Sb_Sql { get { return sb_Sql; } set { sb_Sql=value ; } }
        /// <summary>
        /// 参数化
        /// </summary>
        public List<DbParameter> DbParams { get { return dbParams; } set { dbParams=value; } }
        /// <summary>
        /// 取出的数据
        /// </summary>
        public int PageNumber { get { return pageNumber; } set { pageNumber = value; } }
        /// <summary>
        /// 跳过的数据
        /// </summary>
        public int PageSize { get { return pageSize; } set { pageSize = value; } }

        /// <summary>
        /// 语句的类型
        /// </summary>
        public eDbCommandType DbCommandType { get { return dbCommandType; } set { dbCommandType = value; } }


        private List<DbParameter> dbParams;
        private StringBuilder sb_Sql;
        private int pageNumber;
        private int pageSize;
        private eDbCommandType dbCommandType;


    }
}
