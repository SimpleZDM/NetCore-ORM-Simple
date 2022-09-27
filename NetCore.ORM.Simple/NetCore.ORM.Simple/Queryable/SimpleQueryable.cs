using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 SimpleQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/21 10:25:31
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleQueryable<T>
        :QueryResult<T>,ISimpleQueryable<T>
    {
        public SimpleQueryable(eDBType DbType,DBDrive dbDrive)
        {
            string tableName=ReflectExtension.GetTypeName<T>();
            Init(DbType, dbDrive,tableName);
        }

        ISimpleQueryable<T> ISimpleQueryable<T>.SimpleQueryable()
        {
            return this;
        }
    }
}
