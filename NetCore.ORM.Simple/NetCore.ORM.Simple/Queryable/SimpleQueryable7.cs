﻿using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 SimpleQueryable2
 * 开发人员：-nhy
 * 创建时间：2022/9/20 17:49:02
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleQueryable<T1,T2,T3,T4,T5,T6,T7>:QueryResult<T1>,
        ISimpleQueryable<T1,T2,T3,T4,T5,T6,T7> where T1 : class
    {
        public SimpleQueryable(Expression<Func<T1, T2, T3, T4, T5, T6,T7, JoinInfoEntity>> expression, Builder builder, DBDrive dBDrive)
        {
            Type[] types = ReflectExtension.GetType<T1, T2, T3, T4, T5, T6,T7>();
            Init(builder, dBDrive, types);
            visitor.VisitJoin(expression);
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }

        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression)where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6,T7> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6,T7> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            return this;
        }
    }
}