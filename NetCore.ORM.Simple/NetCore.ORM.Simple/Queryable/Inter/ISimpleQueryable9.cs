using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.Inter
 * 接口名称 ISimpleQueryable8
 * 开发人员：-nhy
 * 创建时间：2022/9/30 13:42:47
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
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <typeparam name="T6"></typeparam>
    /// <typeparam name="T7"></typeparam>
    /// <typeparam name="T8"></typeparam>
    /// <typeparam name="T9"></typeparam>
    public interface ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ISimpleQuery<T1>
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
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression) where TResult : class;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression);
        /// <summary>
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TGroup"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TGroup>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression);
    }
}
