using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.Inter
 * 接口名称 ISimpleGroupByQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/27 11:00:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TGroup"></typeparam>
    public interface ISimpleGroupByQueryable<TResult, TGroup> : IQueryResult<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        TGroup Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TField Sum<TField>(Expression<Func<TResult, TField>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TField Average<TField>(Expression<Func<TResult, TField>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Count<TField>(Expression<Func<TResult, TField>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TField Max<TField>(Expression<Func<TResult, TField>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TField Min<TField>(Expression<Func<TResult, TField>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TField FirstOrDefault<TField>(Expression<Func<TResult, TField>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNewResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<ISimpleGroupByQueryable<TResult, TGroup>, TNewResult>> expression) where TNewResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<TResult, TGroup> OrderBy<TOrder>(Expression<Func<ISimpleGroupByQueryable<TResult, TGroup>, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<TResult, TGroup> Where(Expression<Func<TGroup, bool>> expression);

    }
}
