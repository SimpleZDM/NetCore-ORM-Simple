using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 SimpleQueryable2
 * 开发人员：-nhy
 * 创建时间：2022/9/20 17:49:02
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleQueryable<T1,T2,T3,T4>:QueryResult<T1>,ISimpleQueryable<T1,T2,T3,T4>
    {
        public SimpleQueryable(eDBType DbType, params string[] tableNames) : base(DbType, tableNames)
        {

        }

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression)
        {
            mapVisitor.Modify(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(mapVisitor, joinVisitor, conditionVisitor, DBType);
            return query;
        }

        public ISimpleQueryable<T1, T2, T3, T4> Where()
        {
            return this;
        }
    }
}
