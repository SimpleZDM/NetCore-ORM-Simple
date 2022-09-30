using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.DBDrive
 * 接口名称 DBDrive
 * 开发人员：-nhy
 * 创建时间：2022/9/21 14:49:32
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class DBDrive : IDBDrive
    {

        /// <summary>
        /// 
        /// </summary>
        IDBDrive mysqlDrive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DataBaseConfiguration configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        public DBDrive(DataBaseConfiguration cfg)
        {
            configuration = cfg;
            mysqlDrive = new MysqlDrive(configuration);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
             await MatchDBDrive(() => mysqlDrive.CommitAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task RollBackAsync()
        {
              await MatchDBDrive(() =>mysqlDrive.RollBackAsync());
        } 
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public async Task BeginTransactionAsync()
        {
            await MatchDBDrive(() => mysqlDrive.BeginTransactionAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            return await MatchDBDrive(() => mysqlDrive.ExcuteAsync(entity));
        }
       
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        } 
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {
            return await MatchDBDrive(() => mysqlDrive.ReadAsync<TResult>(entity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity,string query) where TEntity : class
        {
            return await MatchDBDrive(() => mysqlDrive.ExcuteAsync<TEntity>(entity,query));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        {
           return await MatchDBDrive(() => mysqlDrive.ReadAsync<TResult>(sql,Params));
        }
       
        /// <summary>
        /// 
        /// </summary>
        private async Task<TResult> MatchDBDrive<TResult>(params Func<Task<TResult>>[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType<funcs.Length)
                {
                   return await funcs[(int)configuration.CurrentConnectInfo.DBType].Invoke();
                }
            }
            return default(TResult);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcs"></param>
        /// <returns></returns>
        private async Task MatchDBDrive(params Func<Task>[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType > funcs.Length)
                {
                     await funcs[(int)configuration.CurrentConnectInfo.DBType].Invoke();
                }
            }
        }
    }
}
