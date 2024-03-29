﻿using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Queryable;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;



/*********************************************************
 * 命名空间 NetCore.ORM.Simple
 * 接口名称 SimpleClient
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:31:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class SimpleClient:ISimpleClient
    {
        #region prop or ctor
        /// <summary>
        /// 数据库配置
        /// </summary>
        private DataBaseConfiguration configuration;

        /// <summary>
        /// 缓存一些需要执行插入或者更新的数据
        /// </summary>
        private List<SqlCommandEntity> sqls;
        /// <summary>
        /// sql 语句构造器
        /// </summary>
        private ISqlBuilder builder;

        /// <summary>
        /// sql语句执行器
        /// </summary>
       
        private IDBDrive dbDrive;
        private eDBType dbType;

        /// <summary>
        /// 更新或者插入数据的量
        /// </summary>
        private int changeOffset;
        public SimpleClient(DataBaseConfiguration _configuration)
        {
            configuration=_configuration;
            var currentContion = configuration.GetConnection();
            dbType=currentContion.DBType;
            builder = new Builder(dbType);
            sqls = new List<SqlCommandEntity>();
            dbDrive = new DBDrive(configuration,currentContion);
            changeOffset = 0;
        }
        #endregion

        /// <summary>
        /// 关于sql语句和参数
        /// </summary>
        /// <param name="action"></param>
        public void SetAOPLog(Action<string,DbParameter[]> action)
        {
            dbDrive.AOPSqlLog = action;
        }

        public void SetAttr(Type Table=null,Type Column=null)
        {
            ExtensionAttribute.SetAttr(Table, Column);
        }
        public void AddDataBaseMethod(eDBType DBType,string methodName,Func<MethodEntity,string>method)
        {
            switch (DBType)
            {
                case eDBType.Mysql:
                    MysqlConst.AddMysqlExtensMethod(methodName, method);
                    break;
                case eDBType.SqlService:
                    break;
                case eDBType.Sqlite:
                    break;
                default:
                    break;
            }
        }
        public void AddDataBaseMethod(string methodName,Func<MethodEntity, string> method)
        {
            AddDataBaseMethod(dbType, methodName, method);
        }
        #region 执行部分
        public ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity)where TEntity : class,new ()
        {
            var sql=builder.GetInsert(entity, changeOffset);
            sqls.Add(sql);
            sql.DbCommandType = eDbCommandType.Insert;
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,dbType,sql,sqls,dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Insert<TEntity>(string sql,Dictionary<string,object> Params) where TEntity : class, new()
        {   var sqlCommand = builder.GetInsert(sql,Params);
            sqls.Add(sqlCommand);
            sqlCommand.DbCommandType = eDbCommandType.Insert;
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, dbType,sqlCommand, sqls, dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Insert<TEntity>(List<TEntity> entitys) where TEntity : class, new()
        {
            var sql = builder.GetInsert(entitys, changeOffset);
            sql.DbCommandType = eDbCommandType.Insert;
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, dbType, sql, sqls, dbDrive);
            changeOffset = entitys.Count() + changeOffset;
            return command;
        }
        public ISimpleCommand<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entity,changeOffset);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,dbType, sql, sqls, dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Update<TEntity>(string sql,Dictionary<string,object> Params) where TEntity : class, new()
        {
            var sqlCommand = builder.GetUpdate(sql,Params);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, dbType,sqlCommand, sqls, dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Update<TEntity>(List<TEntity> entitys) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entitys, changeOffset);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, dbType, sql, sqls, dbDrive);
            changeOffset = entitys.Count() + changeOffset;
            return command;
        }
        public ISimpleCommand<TEntity> Delete<TEntity>(Expression<Func<TEntity,bool>>expression) where TEntity : class, new()
        {
            Type type = typeof(TEntity);
            var Visitor = new SimpleVisitor(type);
            Visitor.VisitorCondition<TEntity>(expression);
            var contextSelect = Visitor.GetContextSelect();
            var sql = builder.GetDelete(type,contextSelect.Conditions,contextSelect.TreeConditions);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,dbType, sql, sqls, dbDrive);
            return command;
        }
        public ISimpleCommand<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetDelete(entity,changeOffset);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,dbType, sql, sqls, dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Delete<TEntity>(string sql,Dictionary<string,object> Params) where TEntity : class, new()
        {
            var sqlCommand = builder.GetDelete(sql, changeOffset);
            sqls.Add(sqlCommand);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, dbType, sqlCommand, sqls, dbDrive);
            changeOffset++;
            return command;
        }

        #endregion

        #region 查询部分
        public IEnumerable<TEntity> Read<TEntity>(string sql,Dictionary<string,object> Params)where TEntity : class,new()
        {
            var Query=builder.GetSelect(sql,Params);
            return  dbDrive.Read<TEntity>(Query);
        }
        public async Task<IEnumerable<TEntity>> ReadAsync<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new()
        {
            var Query = builder.GetSelect(sql, Params);
            return await dbDrive.ReadAsync<TEntity>(Query);
        }
        public async Task<TEntity> FirstOrDefaultAsync<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new()
        {
            var Query = builder.GetSelect(sql, Params);
            Query.PageSize = 1;
            Query.PageNumber = 1;
            return await dbDrive.ReadFirstOrDefaultAsync<TEntity>(Query);
        }

        public TEntity FirstOrDefault<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new()
        {
            var Query = builder.GetSelect(sql, Params);
            Query.PageSize = 1;
            Query.PageNumber = 1;
            return  dbDrive.ReadFirstOrDefault<TEntity>(Query);
        }
        public ISimpleQueryable<T1> Queryable<T1>() where T1 : class, new()
        {
            return new SimpleQueryable<T1>(builder, dbDrive);
        }
        public ISimpleQueryable<T1,T2> Queryable<T1,T2>(Expression<Func<T1,T2,JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1,T2>(expression,builder,dbDrive);
        }
        public ISimpleQueryable<T1, T2,T3> Queryable<T1,T2,T3>(Expression<Func<T1,T2,T3,JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2,T3>(expression,builder,dbDrive);
        }
        public ISimpleQueryable<T1,T2,T3,T4> Queryable<T1,T2,T3,T4>(Expression<Func<T1,T2,T3,T4,JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1,T2,T3,T4>(expression,builder,dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4,T5> Queryable<T1, T2, T3, T4,T5>(Expression<Func<T1, T2, T3, T4,T5, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4,T5>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5,T6> Queryable<T1, T2, T3, T4, T5,T6>(Expression<Func<T1, T2, T3, T4, T5,T6, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5,T6>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6,T7> Queryable<T1, T2, T3, T4, T5, T6,T7>(Expression<Func<T1, T2, T3, T4, T5,T6,T7, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6,T7>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7,T8> Queryable<T1, T2, T3, T4, T5, T6, T7,T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7,T8>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8,T9> Queryable<T1, T2, T3, T4, T5, T6, T7, T8,T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8,T9>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12>(expression, builder, dbDrive);
        }

        #endregion

        public async Task<int> SaveChangeAsync()
        {
            int result=0;
            var array = sqls.Where(command => !command.DbCommandType.Equals(eDbCommandType.Query)).ToArray();
            result = await dbDrive.ExcuteAsync(array);
            return result;
        }
        public int SaveChange()
        {
            int result = 0;
            var array = sqls.Where(command => !command.DbCommandType.Equals(eDbCommandType.Query)).ToArray();
            result =  dbDrive.Excute(array);
            foreach (var item in array)
            {
                sqls.Remove(item);
            }
            changeOffset = 0;
            return result;
        }
        public void BeginTransaction()
        {
            dbDrive.BeginTransaction();
        }
        public async Task BeginTransactionAsync()
        {
            await dbDrive.BeginTransactionAsync();
        }
        public void Commit()
        {
           dbDrive.Commit();
        }
        public async Task CommitAsync()
        {
            await dbDrive.CommitAsync();
        }

        public void RollBack()
        {
            dbDrive.RollBack();
        }
        public async Task RollBackAsync()
        {
           await dbDrive.RollBackAsync();
        }

    }
}
