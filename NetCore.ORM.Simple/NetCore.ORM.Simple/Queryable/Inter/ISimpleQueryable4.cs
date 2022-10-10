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
    public interface ISimpleQueryable<T1,T2,T3,T4>:IQueryResult<T1> where T1 : class
    {
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class;
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression) where TResult : class;


        public ISimpleQueryable<T1, T2, T3, T4> Where(Expression<Func<T1, T2, bool>> expression);



        public ISimpleQueryable<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, bool>> expression);
       

        public ISimpleQueryable<T1, T2, T3, T4> Where(Expression<Func<T1, T2, T3, T4, bool>> expression);
        public ISimpleQueryable<T1, T2, T3, T4> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression);


        public ISimpleQueryable<T1, T2, T3, T4> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression);
       
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression);
       
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression);
        
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression);

        public ISimpleQueryable<T1, T2, T3, T4> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression);
    }
}
