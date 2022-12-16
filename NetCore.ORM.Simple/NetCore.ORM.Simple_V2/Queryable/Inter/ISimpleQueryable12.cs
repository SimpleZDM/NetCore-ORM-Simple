﻿using System;
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
    public interface ISimpleQueryable<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>:ISimpleQuery<T1>
    {
         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression) where TResult : class;

         ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression) where TResult : class;

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> expression);
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOrder>> expression);

         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression);

         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression);

         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression);

         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, TGroup>> expression);
        
         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, TGroup>> expression);
         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TGroup>> expression);

         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TGroup>> expression);
        
         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TGroup>> expression);


         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TGroup>> expression);
        
         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TGroup>> expression);
        
         ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TGroup>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression);
       
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOrder>> expression);
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>> expression);

         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOrder>> expression);
    }
}