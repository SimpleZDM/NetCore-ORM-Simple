using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SimpleQueryable<T1,T2,T3,T4,T5,T6>:QueryResult<T1>,ISimpleQueryable<T1,T2,T3,T4,T5,T6>
    {
        public SimpleQueryable(eDBType DbType, params string[] tableNames) 
        {

        }

        public IQueryResult<TResult> Select<TResult>(System.Linq.Expressions.Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression)
        {
            throw new NotImplementedException();
        }

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6> Where()
        {
            throw new NotImplementedException();
        }
    }
}
