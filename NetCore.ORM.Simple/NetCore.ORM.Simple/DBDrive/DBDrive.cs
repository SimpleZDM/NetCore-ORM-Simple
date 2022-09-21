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

        IDBDrive mysqlDrive { get; set; }
        DataBaseConfiguration configuration { get; set; }
        public DBDrive(DataBaseConfiguration cfg)
        {
            configuration = cfg;
            mysqlDrive = new MysqlDrive(configuration);
        }
        public async Task BeginTransactionAsync()
        {
            await MatchDBDrive(() => mysqlDrive.BeginTransactionAsync());
        }

        public async Task CommitAsync()
        {
             await MatchDBDrive(() => mysqlDrive.CommitAsync());
        }

        public void Dispose()
        {
        }

        public async Task<int> ExcuteAsync(string sql, params DbParameter[] Params)
        {
            return await MatchDBDrive(() => mysqlDrive.ExcuteAsync(sql,Params));
        }

        public async Task<TEntity> ExcuteAsync<TEntity>(string sql, string query, params DbParameter[] Params) where TEntity : class
        {
            return await MatchDBDrive(() => mysqlDrive.ExcuteAsync<TEntity>(sql,query,Params));
        }

        public async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        {
           return await MatchDBDrive(() => mysqlDrive.ReadAsync<TResult>(sql,Params));
        }

        public async Task RollBackAsync()
        {
              await MatchDBDrive(() =>mysqlDrive.RollBackAsync());
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task<TResult> MatchDBDrive<TResult>(params Func<Task<TResult>>[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType > funcs.Length)
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
