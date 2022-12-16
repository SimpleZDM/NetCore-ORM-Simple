using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Xml;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 BaseQuery
 * 开发人员：11920
 * 创建时间：2022/12/14 14:25:08
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    internal class SimpleQuery<TResult>:QueryResult<TResult>,ISimpleQuery<TResult>
    {
        public SimpleQuery()
        {

        }
        public SimpleQuery(
          SimpleVisitor _visitor,
          ISqlBuilder _builder, IDBDrive DbDrive):base(_visitor, _builder, DbDrive)
        {
         
        }
        public ISimpleQuery<TResult> Skip(int pageNumber)
        {
            sqlEntity.PageNumber = pageNumber;
            return this;
        }

        public ISimpleQuery<TResult> Take(int pageSize)
        {
            sqlEntity.PageSize = pageSize;
            return this;
        }
        public ISimpleQuery<TResult> ToPage(int pageSize, int pageNumber)
        {
            sqlEntity.PageSize = pageSize;
            sqlEntity.PageNumber = pageNumber;
            return this;
        }

        public virtual ISimpleQuery<TResult> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }
        public virtual ISimpleQuery<TResult> OrderByDescending<TOrder>(Expression<Func<TResult, TOrder>> expression)
        {
            visitor.OrderByDescending(expression);
            return this;
        }
        public ISimpleGroupByQueryable<TResult, TGroup> GroupBy<TGroup>(Expression<Func<TResult, TGroup>> expression)
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<TResult, TGroup> simpleGroupBy = new SimpleGroupByQueryable<TResult, TGroup>(visitor, builder, DbDrive);
            return simpleGroupBy;
        }
        public virtual ISimpleQuery<TNewResult> Select<TNewResult>(Expression<Func<TResult, TNewResult>> expression) where TNewResult : class
        {
            visitor.VisitMap(expression);
            ISimpleQuery<TNewResult> query = new SimpleQuery<TNewResult>(visitor, builder, DbDrive);
            return query;
        }
        public ISimpleQuery<TResult> Where(Expression<Func<TResult, bool>> expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }

        public ISimpleQuery<TResult> Select(Expression<Func<TResult, TResult>> expression)
        {
            visitor.VisitMap(expression);
            return this;
        }
    }
}
