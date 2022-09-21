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

        protected void Init(eDBType DbType, params string[] tableNames)
        {
            mapVisitor = new MapVisitor(tableNames);
            joinVisitor = new JoinVisitor(tableNames);
            conditionVisitor = new ConditionVisitor(tableNames);
            this.DBType = DbType;
            builder = new Builder(this.DBType);
        }
        public QueryResult()
        {
        }
        public QueryResult(
            MapVisitor _mapVisitor, JoinVisitor _joinVisitor,
            ConditionVisitor _conditionVisitor,
            eDBType DbType)
        {
            mapVisitor = _mapVisitor;
            joinVisitor = _joinVisitor;
            conditionVisitor = _conditionVisitor;
            this.DBType = DbType;
            builder = new Builder(this.DBType);
        }

        public IEnumerable<TResult> ToList()
        {
            var joinInfos = joinVisitor.GetJoinInfos();
            var mapInfos = mapVisitor.GetMapInfos();
            var condition = conditionVisitor.GetValue();
            sqlEntity = builder.GetSelect<TResult>(mapInfos, joinInfos, condition);
            Console.WriteLine(sqlEntity.Sb_Sql.ToString());
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TResult> ToListAsync()
        {
            var joinInfos = joinVisitor.GetJoinInfos();
            var mapInfos = mapVisitor.GetMapInfos();
            var condition = conditionVisitor.GetValue();
            SqlEntity sql = sql = builder.GetSelect<TResult>(mapInfos, joinInfos, condition);
            Console.WriteLine(sql.Sb_Sql.ToString());
            return null;
        }

        public IQueryResult<TResult> Skip(int Number)
        {
            sqlEntity.SkipNumber = Number;
            return this;
        }

        public IQueryResult<TResult> Take(int Number)
        {
            sqlEntity.TakeNumber = Number;
            return this;
        }
        public IQueryResult<TResult> ToPage(int takeNumber, int skipNumber)
        {
            sqlEntity.TakeNumber = takeNumber;
            sqlEntity.SkipNumber = skipNumber;
            return this;
        }

        public IQueryResult<TResult> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression)
        {
            return this;
        }
        public IQueryResult<TResult> Select<TNewResult>(Expression<Func<TResult,TNewResult>>expression)
        {
            return this;
        }
        public IQueryResult<TResult> Where(Expression<Func<TResult,bool>>expression)
        {
            return this;
        }
    }
}
