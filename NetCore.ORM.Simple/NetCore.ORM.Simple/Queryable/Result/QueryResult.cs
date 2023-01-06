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
using System.Diagnostics;
using System.Text.RegularExpressions;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 QueryResult
 * 开发人员：-nhy
 * 创建时间：2022/9/21 9:14:25
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    internal class QueryResult<TResult>:IQueryResult<TResult>
    {
        protected int PageSize;
        protected int PageNumber;
        protected eDBType DBType;
        protected IDBDrive DbDrive;
        protected ISqlBuilder builder;
        protected SimpleVisitor visitor;
        protected QueryEntity sqlEntity;
       
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="DbType"></param>
        /// <param name="tableNames"></param>
        protected void Init(ISqlBuilder _builder,IDBDrive DbDrive,params Type[] typs)
        {
            visitor = new SimpleVisitor(typs);
            this.DbDrive = DbDrive;
            builder = _builder;
        }
        /// <summary>
        /// 
        /// </summary>
        public QueryResult()
        {
            sqlEntity = new QueryEntity();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_visitor"></param>
        /// <param name="_builder"></param>
        /// <param name="DbDrive"></param>
        public QueryResult(SimpleVisitor _visitor, ISqlBuilder _builder,IDBDrive DbDrive)
        {
            visitor = _visitor;
            builder = _builder;
            sqlEntity=new QueryEntity();
            this.DbDrive = DbDrive;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            sqlEntity.SetPage(1, 1);
            builder.GetCount(visitor.GetContextSelect(), sqlEntity);
            return DbDrive.ReadAny(sqlEntity); ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            builder.GetCount(visitor.GetContextSelect(), sqlEntity);
            return DbDrive.ReadCount(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AnyAsync()
        {
            sqlEntity.SetPage(1, 1);
            builder.GetCount(visitor.GetContextSelect(), sqlEntity);
            return await DbDrive.ReadAnyAsync(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            builder.GetCount(visitor.GetContextSelect(), sqlEntity);
            return await DbDrive.ReadCountAsync(sqlEntity);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TResult First()
        {

            sqlEntity.SetPage(1, 1);
            builder.GetSelect<TResult>(visitor.GetContextSelect(), sqlEntity);
            return  DbDrive.ReadFirstOrDefault<TResult>(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TResult> ToList()
        {
            builder.GetSelect<TResult>(visitor.GetContextSelect(), sqlEntity);
            List<TResult> data = DbDrive.Read<TResult>(sqlEntity).ToList();
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TResult FirstOrDefault()
        {
            sqlEntity.SetPage(1, 1);
            builder.GetSelect<TResult>(visitor.GetContextSelect(), sqlEntity);
            return DbDrive.ReadFirstOrDefault<TResult>(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<TResult> FirstAsync()
        {
            sqlEntity.SetPage(1, 1);
            builder.GetSelect<TResult>(visitor.GetContextSelect(),sqlEntity);
            return await DbDrive.ReadFirstOrDefaultAsync<TResult>(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<TResult> FirstOrDefaultAsync()
        {
            sqlEntity.SetPage(1, 1);
            builder.GetSelect<TResult>(visitor.GetContextSelect(),sqlEntity);
            return await DbDrive.ReadFirstOrDefaultAsync<TResult>(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<TResult>> ToListAsync()
        {
            sqlEntity.StrSqlValue.Clear();
            sqlEntity.DbParams.Clear();
            builder.GetSelect<TResult>(visitor.GetContextSelect(),sqlEntity);
            return (await DbDrive.ReadAsync<TResult>(sqlEntity)).ToList();
        }


    }
}
