using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.ORM.Simple.Queryable
{
    public interface ISimpleQuery<TResult>:IQueryResult<TResult>
    {
        ISimpleQuery<TResult> Skip(int Number);
        ISimpleQuery<TResult> Take(int Number);
        ISimpleQuery<TResult> ToPage(int takeNumber, int skipNumber);
        ISimpleQuery<TNewResult> Select<TNewResult>(Expression<Func<TResult, TNewResult>> expression) where TNewResult : class;
        ISimpleQuery<TResult> Select(Expression<Func<TResult, TResult>> expression);
        ISimpleQuery<TResult> Where(Expression<Func<TResult, bool>> expression);
        ISimpleQuery<TResult> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression);
        ISimpleQuery<TResult> OrderByDescending<TOrder>(Expression<Func<TResult, TOrder>> expression);
        ISimpleGroupByQueryable<TResult, TGroup> GroupBy<TGroup>(Expression<Func<TResult, TGroup>> expression);
    }
}
