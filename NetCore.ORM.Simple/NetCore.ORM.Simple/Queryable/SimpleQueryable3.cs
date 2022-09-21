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
 * 接口名称 SimpleQueryable2
 * 开发人员：-nhy
 * 创建时间：2022/9/20 17:49:02
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleQueryable<T1, T2, T3> :QueryResult<T1>,ISimpleQueryable<T1, T2, T3>
    {
        public SimpleQueryable(Expression<Func<T1,T2,T3,JoinInfoEntity>> expression,eDBType DbType)
        {
            string[] tableNames = ReflectExtension.GetTypeName<T1, T2,T3>();
            Init(DbType,tableNames);
            joinVisitor.Modify(expression);
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression)
        {
            mapVisitor.Modify(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(mapVisitor, joinVisitor, conditionVisitor, DBType);
            return query;
        }

        public ISimpleQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> expression)
        {
            return this;
        }
    }
}
