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
    public interface ISimpleQueryable<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11>:IQueryResult<T1>
    {
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression) where TResult : class;

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression) where TResult : class;

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression);


        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>> expression);

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression);


        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression);


        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, TGroup>> expression);
       
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, TGroup>> expression);
       
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TGroup>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TGroup>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TGroup>> expression);
        
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TGroup>> expression);
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TGroup>> expression);
    }
}