using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.ORM.Simple.Queryable
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public interface ISimpleQuery<TResult> : IQueryResult<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Skip(int Number);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Take(int Number);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="takeNumber"></param>
        /// <param name="skipNumber"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> ToPage(int takeNumber, int skipNumber);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TNewResult> Select<TNewResult>(Expression<Func<TResult, TNewResult>> expression) where TNewResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select(Expression<Func<TResult, TResult>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Where(Expression<Func<TResult, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> OrderByDescending<TOrder>(Expression<Func<TResult, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<TResult, TGroup> GroupBy<TGroup>(Expression<Func<TResult, TGroup>> expression);
    }
}
