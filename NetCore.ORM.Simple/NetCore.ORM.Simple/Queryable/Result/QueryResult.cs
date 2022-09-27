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
    public class QueryResult<TResult> : IQueryResult<TResult>
    {

        protected eDBType DBType;
        protected Builder builder;
        protected MapVisitor mapVisitor;
        protected JoinVisitor joinVisitor;
        protected ConditionVisitor conditionVisitor;
        protected SqlEntity sqlEntity;
        protected int PageNumber;
        protected int PageSize;
        protected DBDrive DbDrive;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="DbType"></param>
        /// <param name="tableNames"></param>
        protected void Init(eDBType DbType,DBDrive DbDrive, params string[] tableNames)
        {
            mapVisitor = new MapVisitor(tableNames);
            joinVisitor = new JoinVisitor(tableNames);
            conditionVisitor = new ConditionVisitor(tableNames);
            this.DBType = DbType;
            this.DbDrive = DbDrive;
            builder = new Builder(this.DBType);
        }
        public QueryResult()
        {
            sqlEntity = new SqlEntity();
        }
        public QueryResult(
            MapVisitor _mapVisitor, JoinVisitor _joinVisitor,
            ConditionVisitor _conditionVisitor,
            eDBType DbType,DBDrive DbDrive)
        {
            mapVisitor = _mapVisitor;
            joinVisitor = _joinVisitor;
            conditionVisitor = _conditionVisitor;
            this.DBType = DbType;
            builder = new Builder(this.DBType);
            sqlEntity=new SqlEntity();
            this.DbDrive = DbDrive;
        }

        public  IEnumerable<TResult> ToList()
        {
            var joinInfos = joinVisitor.GetJoinInfos();
            var mapInfos = mapVisitor.GetMapInfos();
            var condition = conditionVisitor.GetCondition();
            builder.GetSelect<TResult>(mapInfos, joinInfos, condition.Item1,condition.Item2,sqlEntity);
            Console.WriteLine(sqlEntity.Sb_Sql.ToString());
          
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ToListAsync()
        {
            var joinInfos = joinVisitor.GetJoinInfos();
            var mapInfos = mapVisitor.GetMapInfos();
            var condition = conditionVisitor.GetCondition();
             builder.GetSelect<TResult>(mapInfos,joinInfos,condition.Item1,condition.Item2,sqlEntity);
            return await DbDrive.ReadAsync<TResult>(sqlEntity.Sb_Sql.ToString(),mapInfos.ToArray(),sqlEntity.DbParams.ToArray());
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

        public IQueryResult<TResult> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression)
        {
            return this;
        }
        public IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<TResult,TNewResult>>expression)
        {
            mapVisitor.Modify(expression);
            IQueryResult<TNewResult> query = new QueryResult<TNewResult>(mapVisitor, joinVisitor, conditionVisitor, DBType, DbDrive);
            return query;
        }
        public IQueryResult<TResult> Where(Expression<Func<TResult,bool>>expression)
        {
            conditionVisitor.Modify(expression,mapVisitor.GetMapInfos());
            return this;
        }

        public IQueryResult<TResult> Select(Expression<Func<TResult, TResult>> expression)
        {
            mapVisitor.Modify(expression);
            return this;
        }
    }
}
