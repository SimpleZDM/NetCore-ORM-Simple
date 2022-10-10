using NetCore.ORM.Simple.Common;
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
 * 接口名称 ISimpleQueryable8
 * 开发人员：-nhy
 * 创建时间：2022/9/30 13:40:48
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : QueryResult<T1>, ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> where T1 : class
    {
        public SimpleQueryable(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, JoinInfoEntity>> expression, Builder builder, DBDrive dBDrive)
        {
            Type[] types = ReflectExtension.GetType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
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
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression) where TResult : class
        {
            visitor.VisitMap(expression);
            IQueryResult<TResult> query = new QueryResult<TResult>(visitor, builder, DbDrive);
            return query;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Where(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12> OrderBy<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }

        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> OrderByDescending<TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOrder>> expression)
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
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public ISimpleGroupByQueryable<T1, TGroup> GroupBy<TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<T1, TGroup> simpleGroupBy = new SimpleGroupByQueryable<T1, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
    }
}
