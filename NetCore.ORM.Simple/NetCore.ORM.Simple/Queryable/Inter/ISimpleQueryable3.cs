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
    public interface ISimpleQueryable<T1,T2,T3>:IQueryResult<T1> where T1 : class
    {

         IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class;


         IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class;
       
         ISimpleQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, bool>> expression);

         ISimpleQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> expression);
        

         ISimpleQueryable<T1, T2, T3> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);
         ISimpleQueryable<T1, T2, T3> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression);
       
         ISimpleQueryable<T1, T2, T3> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);


         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression);
        
         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression);


    }
}
