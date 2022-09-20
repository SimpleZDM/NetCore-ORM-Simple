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
        }

        public StringBuilder Sb_Sql { get { return sb_Sql; } set { sb_Sql=value ; } }
        public List<DbParameter> DbParams { get { return dbParams; } set { dbParams=value; } }


        private List<DbParameter> dbParams;
        private StringBuilder sb_Sql;

    }
}
