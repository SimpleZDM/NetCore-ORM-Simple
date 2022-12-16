using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.SqlBuilder;

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
    internal class SimpleQueryable<T>
        :SimpleQuery<T>,ISimpleQueryable<T> where T : class
    {
        public SimpleQueryable(ISqlBuilder builder,IDBDrive dbDrive)
        {
            Type tableName=ReflectExtension.GetType<T>();
            Init(builder,dbDrive,tableName);
        }

        ISimpleQueryable<T> ISimpleQueryable<T>.SimpleQueryable()
        {
            return this;
        }
    }
}
