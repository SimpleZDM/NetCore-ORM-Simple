using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 IQueryResult
 * 开发人员：-nhy
 * 创建时间：2022/9/21 9:15:24
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
    public interface IQueryResult<TResult>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         int Count();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         bool Any();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task<int> CountAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task<bool> AnyAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         TResult First();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task<TResult> FirstAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         TResult FirstOrDefault();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task<TResult> FirstOrDefaultAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         List<TResult> ToList();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         Task<List<TResult>> ToListAsync();
    }
}
