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
        IDBDrive sqlServicelDrive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DataBaseConfiguration configuration { get; set; }
        public Action<string, DbParameter[]> AOPSqlLog { get { return aopSqlLog; } 
               set {
                switch (configuration.CurrentConnectInfo.DBType)
                {
                    case eDBType.Mysql:
                        mysqlDrive.AOPSqlLog= value;
                        break;
                    case eDBType.SqlService:
                        break;
                    default:
                        break;
                }
            }}
        private Action<string, DbParameter[]> aopSqlLog;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        public DBDrive(DataBaseConfiguration cfg)
        {
            configuration = cfg;
            mysqlDrive = new MysqlDrive(configuration);
            sqlServicelDrive = new SqlServiceDrive(configuration);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await MatchDBDrive(
                () => mysqlDrive.CommitAsync(),
                () => sqlServicelDrive.CommitAsync()
                );
        }
        public void Commit()
        {
             MatchDBDrive(
                () => mysqlDrive.Commit(),
                () => sqlServicelDrive.Commit()
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task RollBackAsync()
        {
              await MatchDBDrive(
                     () =>mysqlDrive.RollBackAsync(),
                    () => sqlServicelDrive.RollBackAsync()
                  );
        }
        public void RollBack()
        {
               MatchDBDrive(
                () => mysqlDrive.RollBack(),
                () => sqlServicelDrive.RollBack()
               );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task BeginTransactionAsync()
        {
            await MatchDBDrive(
                () => mysqlDrive.BeginTransactionAsync(),
                () => sqlServicelDrive.BeginTransactionAsync()
               );
        }
        public void BeginTransaction()
        {
             MatchDBDrive(
                () => mysqlDrive.BeginTransaction(),
                () => sqlServicelDrive.BeginTransaction()
               );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            return await MatchDBDrive(
                () => mysqlDrive.ExcuteAsync(entity),
                () => sqlServicelDrive.ExcuteAsync(entity)
               );;
        }
        public int Excute(SqlCommandEntity entity)
        {
            return MatchDBDrive(
                () => mysqlDrive.Excute(entity),
                () => sqlServicelDrive.Excute(entity)
               );
        }
        public async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            return await MatchDBDrive(
                () => mysqlDrive.ExcuteAsync(sqlCommand),
                () => sqlServicelDrive.ExcuteAsync(sqlCommand)
               );
        }
        public int Excute(SqlCommandEntity[] sqlCommand)
        {
            return  MatchDBDrive(
                () => mysqlDrive.Excute(sqlCommand),
                () => sqlServicelDrive.Excute(sqlCommand)
               );
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
            return await MatchDBDrive(
                () => mysqlDrive.ReadAsync<TResult>(entity),
                () => sqlServicelDrive.ReadAsync<TResult>(entity)
               );
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
            return await MatchDBDrive(
                () => mysqlDrive.ExcuteAsync<TEntity>(entity, query),
                () => sqlServicelDrive.ExcuteAsync<TEntity>(entity, query)
               ) ;
        }
        public  TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            return  MatchDBDrive(
                () => mysqlDrive.Excute<TEntity>(entity, query),
                () => sqlServicelDrive.Excute<TEntity>(entity, query)
               );
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
           return await MatchDBDrive(
               () => mysqlDrive.ReadAsync<TResult>(sql,Params),
               () => mysqlDrive.ReadAsync<TResult>(sql,Params)
               );
        }
       
      

        public IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            return  MatchDBDrive(
                       () => mysqlDrive.Read<TResult>(entity),
                       () => sqlServicelDrive.Read<TResult>(entity)
                    );
        }

        public TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            return MatchDBDrive(
                       () => mysqlDrive.ReadFirstOrDefault<TResult>(entity),
                       () => sqlServicelDrive.ReadFirstOrDefault<TResult>(entity)
                    );
        }

        public async Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity) 
        {
            return await MatchDBDrive(
                       () => mysqlDrive.ReadFirstOrDefaultAsync<TResult>(entity),
                       () => sqlServicelDrive.ReadFirstOrDefaultAsync<TResult>(entity)
                    );
        }

        public int ReadCount(QueryEntity entity)
        {
            return  MatchDBDrive(
                       () => mysqlDrive.ReadCount(entity),
                       () => sqlServicelDrive.ReadCount(entity)
                    );
        }

        public async Task<int> ReadCountAsync(QueryEntity entity)
        {
            return await MatchDBDriveAsync(
                      () => mysqlDrive.ReadCountAsync(entity),
                      () => sqlServicelDrive.ReadCountAsync(entity)
                   ) ;
        }

        public async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
             return await MatchDBDriveAsync(
                      () => mysqlDrive.ReadAnyAsync(entity),
                      () => sqlServicelDrive.ReadAnyAsync(entity)
                   );
        }

        public  bool ReadAny(QueryEntity entity)
        {
            return  MatchDBDrive(
                      () => mysqlDrive.ReadAny(entity),
                      () => sqlServicelDrive.ReadAny(entity)
                   );
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task<TResult> MatchDBDriveAsync<TResult>(params Func<Task<TResult>>[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType < funcs.Length)
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

        private async Task MatchDBDriveAsync(params Func<Task>[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType < funcs.Length)
                {
                    await funcs[(int)configuration.CurrentConnectInfo.DBType].Invoke();
                }
            }
        }

        private TResult MatchDBDrive<TResult>(params Func<TResult>[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType < funcs.Length)
                {
                    return funcs[(int)configuration.CurrentConnectInfo.DBType].Invoke();
                }
            }
            return default(TResult);
        }

        private void MatchDBDrive(params Action[] funcs)
        {
            if (!Check.IsNull(funcs))
            {
                if ((int)configuration.CurrentConnectInfo.DBType < funcs.Length)
                {
                     funcs[(int)configuration.CurrentConnectInfo.DBType].Invoke();
                }
            }
        }


    }
}
