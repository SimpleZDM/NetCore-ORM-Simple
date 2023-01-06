using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.ISimple.IQuable
 * 接口名称 IQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:48:00
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public interface ISimpleQueryable<T1, T2> : ISimpleQuery<T1> where T1 : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2> Where(Expression<Func<T1, T2, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression);

    }
}
