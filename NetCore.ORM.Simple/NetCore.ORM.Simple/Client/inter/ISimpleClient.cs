using NetCore.ORM.Simple.Queryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client.inter
 * 接口名称 ISimpleClient
 * 开发人员：-nhy
 * 创建时间：2022/9/20 17:40:52
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Client
{
    public interface ISimpleClient
    {
        public ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetInsert(entity, sqls.Count);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, configuration.CurrentConnectInfo.DBType, sql, sqls);
            return command;
        }
        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entity, sqls.Count);
            sqls.Add(sql);
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public ISimpleQueryable<T1> Queryable<T1>() where T1 : class, new()
        {
            return new SimpleQueryable<T1>(configuration.ConnectMapName[configuration.CurrentUseConnectName].DBType);
        }
        public ISimpleQueryable<T1, T2> Queryable<T1, T2>()
        {
            return new SimpleQueryable<T1, T2>(configuration.ConnectMapName[configuration.CurrentUseConnectName].DBType);
        }
        public ISimpleQueryable<T1, T2, T3> Queryable<T1, T2, T3>();
      
        public ISimpleQueryable<T1, T2, T3, T4> Queryable<T1, T2, T3, T4>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SaveChange();
    }
}
