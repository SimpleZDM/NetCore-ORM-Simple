﻿using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Visitor;

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
    internal class SimpleQueryable<T1, T2, T3> : SimpleQuery<T1>, ISimpleQueryable<T1, T2, T3> where T1 : class
    {
        public SimpleQueryable(Expression<Func<T1, T2, T3, JoinInfoEntity>> expression, ISqlBuilder builder, IDBDrive dBDrive)
        {
            Type[] types = ReflectExtension.GetType<T1, T2, T3>();
            Init(builder, dBDrive, types);
            visitor.VisitJoin(expression);
        }

        public SimpleQueryable(Expression<Func<T1,T2,T3, bool>> expression, ISqlBuilder builder, IDBDrive dbDrive, SimpleVisitor _visitor, eJoinType joinType)
        {
            Type types = ReflectExtension.GetType<T3>();
            SimpleInit(builder, dbDrive, _visitor, types);
            visitor.VisitJoin(expression,joinType);
        }



        public ISimpleQueryable<T1, T2, T3,T4> LeftJoin<T4>(Expression<Func<T1, T2, T3,T4, bool>> expression)
        {
            return new SimpleQueryable<T1, T2, T3,T4>(expression, builder, this.DbDrive, this.visitor, eJoinType.Left);
        }
        public ISimpleQueryable<T1, T2, T3,T4> RightJoin<T4>(Expression<Func<T1, T2, T3,T4,bool>> expression)
        {
            return new SimpleQueryable<T1, T2, T3,T4>(expression, builder, this.DbDrive, this.visitor, eJoinType.Right);
        }
        public ISimpleQueryable<T1, T2, T3,T4> InnerJoin<T4>(Expression<Func<T1, T2, T3,T4, bool>> expression)
        {
            return new SimpleQueryable<T1, T2, T3,T4>(expression, builder, this.DbDrive, this.visitor, eJoinType.Inner);
        }
        public ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            ISimpleQuery<TResult> query = new SimpleQuery<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public ISimpleQuery<TResult> Select<TResult>(Expression<Func<T1, T2, T3, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            ISimpleQuery<TResult> query = new SimpleQuery<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public ISimpleQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }

        public ISimpleQueryable<T1, T2, T3> Where(Expression<Func<T1, T2, T3, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }

        public ISimpleQueryable<T1, T2, T3> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
    }
}
