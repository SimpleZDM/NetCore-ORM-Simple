
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Queryable;
using NetCore.ORM.Simple.SqlBuilder;


/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client
 * 接口名称 SimpleClient
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:31:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Client
{
    public class SimpleClient:ISimpleClient
    {
        private DataBaseConfiguration configuration;
        private List<SqlEntity> sqls;
        private Builder builder;
        private DBDrive dbdrive;
        public SimpleClient(DataBaseConfiguration _configuration)
        {
            configuration=_configuration;
            sqls = new List<SqlEntity>();
        }
        /// <summary>
        /// 插入语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity)where TEntity : class,new ()
        {
            var sql=builder.GetInsert(entity,sqls.Count);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,configuration.CurrentConnectInfo.DBType,sql,sqls);
            return command;
        }
        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entity,sqls.Count);
            sqls.Add(sql);
        }

        public void Delete<TEntity>(Expression<Func<TEntity,bool>>expression) where TEntity : class, new()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public ISimpleQueryable<T1> Queryable<T1>()where T1:class,new()
        {
            return new SimpleQueryable<T1>(configuration.ConnectMapName[configuration.CurrentUseConnectName].DBType);
        }
        public ISimpleQueryable<T1,T2> Queryable<T1,T2>(Expression<Func<T1,T2,JoinInfoEntity>> expression)
        {
            return new SimpleQueryable<T1,T2>(expression,configuration.ConnectMapName[configuration.CurrentUseConnectName].DBType);
        }
        public ISimpleQueryable<T1, T2,T3> Queryable<T1,T2,T3>(Expression<Func<T1,T2,T3,JoinInfoEntity>> expression)
        {
            return new SimpleQueryable<T1, T2,T3>(expression,configuration.ConnectMapName[configuration.CurrentUseConnectName].DBType);
        }
        public ISimpleQueryable<T1,T2,T3,T4> Queryable<T1,T2,T3,T4>(Expression<Func<T1,T2,T3,T4,JoinInfoEntity>> expression)
        {
            return new SimpleQueryable<T1,T2,T3,T4>(expression,configuration.ConnectMapName[configuration.CurrentUseConnectName].DBType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveChange()
        {
            return true;
        }
    }
}
