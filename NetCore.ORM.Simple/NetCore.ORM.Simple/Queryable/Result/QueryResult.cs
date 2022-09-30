using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Common;
using System.Data.Common;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.Result
 * 接口名称 QueryResult
 * 开发人员：-nhy
 * 创建时间：2022/9/21 9:14:25
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class QueryResult<TResult> : IQueryResult<TResult> where TResult : class
    {

        protected eDBType DBType;
        protected Builder builder;
        protected SimpleVisitor visitor;
        protected QueryEntity sqlEntity;
        protected int PageNumber;
        protected int PageSize;
        protected DBDrive DbDrive;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="DbType"></param>
        /// <param name="tableNames"></param>
        protected void Init(Builder _builder,DBDrive DbDrive,params Type[] typs)
        {
            visitor = new SimpleVisitor(typs);
            this.DbDrive = DbDrive;
            builder = _builder;
        }
        public QueryResult()
        {
            sqlEntity = new QueryEntity();
        }
        public QueryResult(
            SimpleVisitor _visitor,
            Builder _builder,DBDrive DbDrive)
        {
            visitor = _visitor;
            builder = _builder;
            sqlEntity=new QueryEntity();
            this.DbDrive = DbDrive;
        }

        

        public IQueryResult<TResult> Skip(int pageNumber)
        {
            sqlEntity.PageNumber = pageNumber;
            return this;
        }

        public IQueryResult<TResult> Take(int pageSize)
        {
            sqlEntity.PageSize = pageSize;
            return this;
        }
        public IQueryResult<TResult> ToPage(int pageSize, int pageNumber)
        {
            sqlEntity.PageSize=pageSize;
            sqlEntity.PageNumber = pageNumber;
            return this;
        }

        public virtual ISimpleGroupByQueryable<TResult,TOrder> OrderBy<TOrder>(Expression<Func<TResult,TOrder>> expression)where TOrder :class
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<TResult,TOrder> simpleOrder = new SimpleGroupByQueryable<TResult,TOrder>(visitor, builder, DbDrive);
            return simpleOrder;
        }

        public  ISimpleGroupByQueryable<TResult, TGroup> GroupBy<TGroup>(Expression<Func<TResult,TGroup>> expression)where TGroup : class
        {
            visitor.GroupBy(expression);
            ISimpleGroupByQueryable<TResult, TGroup> simpleGroupBy = new SimpleGroupByQueryable<TResult,TGroup>(visitor,builder,DbDrive);
            return simpleGroupBy;
        }
        public virtual IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<TResult,TNewResult>>expression)where TNewResult:class
        {
            visitor.VisitMap(expression);
            IQueryResult<TNewResult> query = new QueryResult<TNewResult>(visitor,builder,DbDrive);
            return query;
        }
        public IQueryResult<TResult> Where(Expression<Func<TResult,bool>>expression)
        {
            visitor.VisitorCondition(expression);
            return this;
        }

        public IQueryResult<TResult> Select(Expression<Func<TResult, TResult>> expression)
        {
            visitor.VisitMap(expression);
            return this;
        }

        public int Count()
        {
            builder.GetCount(visitor.GetSelectInfo(), sqlEntity);
            return default(int);
        }
        public bool Any()
        {
            sqlEntity.PageSize = 1;
            sqlEntity.PageNumber = 1;
            builder.GetCount(visitor.GetSelectInfo(), sqlEntity);
            return true;
        }
        public async Task<int> CountAsync()
        {
            builder.GetCount(visitor.GetSelectInfo(), sqlEntity);
            return default(int);
        }
        public async Task<bool> AnyAsync()
        {
            sqlEntity.PageSize = 1;
            sqlEntity.PageNumber = 1;
            builder.GetCount(visitor.GetSelectInfo(), sqlEntity);
            return true;
        }
        public TResult First()
        {
            return default(TResult);
        }
        public async Task<TResult> FirstAsync()
        {
            sqlEntity.PageSize = 1;
            sqlEntity.PageNumber = 1;
            builder.GetSelect<TResult>(visitor.GetSelectInfo(), sqlEntity);
            return default(TResult);
        }
        public TResult FirstOrDefault()
        {
            sqlEntity.PageSize = 1;
            sqlEntity.PageNumber = 1;
            builder.GetSelect<TResult>(visitor.GetSelectInfo(), sqlEntity);
            return default(TResult);
        }
        public async Task<TResult> FirstOrDefaultAsync()
        {
            sqlEntity.PageSize = 1;
            sqlEntity.PageNumber = 1;
            builder.GetSelect<TResult>(visitor.GetSelectInfo(), sqlEntity);

            return default(TResult);
        }
        public IEnumerable<TResult> ToList()
        {

            builder.GetSelect<TResult>(visitor.GetSelectInfo(), sqlEntity);
            Console.WriteLine(sqlEntity);
            return null;
        }
      
        public async Task<IEnumerable<TResult>> ToListAsync()
        {
            sqlEntity.StrSqlValue.Clear();
            sqlEntity.DbParams.Clear();
            builder.GetSelect<TResult>(visitor.GetSelectInfo(), sqlEntity);
            return await DbDrive.ReadAsync<TResult>(sqlEntity);
        }


    }
}
