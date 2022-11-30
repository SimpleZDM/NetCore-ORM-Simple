using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        IDBDrive databaseDrive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        DataBaseConfiguration configuration { get; set; }
        public Action<string, DbParameter[]> AOPSqlLog { get { return aopSqlLog; } 
               set {databaseDrive.AOPSqlLog= value;}}
        private Action<string, DbParameter[]> aopSqlLog;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfg"></param>
        public DBDrive(DataBaseConfiguration cfg,ConnectionEntity currentConnection)
        {
            configuration = cfg;
            switch (currentConnection.DBType)
            {
                case eDBType.Mysql:
                    databaseDrive = new MysqlDrive(configuration,currentConnection);
                    break;
                case eDBType.SqlService:
                    databaseDrive = new SqlServiceDrive(configuration,currentConnection);
                    break;
                case eDBType.Sqlite:
                    databaseDrive = new SqliteDrive(configuration,currentConnection);
                    break;
                default:
                    break;
            }

        }

        public void SetAttr(Type Table = null, Type Column = null)
        {
            databaseDrive.SetAttr(Table, Column);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {

            await databaseDrive.CommitAsync();
                
        }
        public void Commit()
        {
            databaseDrive.Commit();
                
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task RollBackAsync()
        {
            await databaseDrive.RollBackAsync();
        }
        public void RollBack()
        {
            databaseDrive.RollBack();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task BeginTransactionAsync()
        {
            await databaseDrive.BeginTransactionAsync();
        }
        public void BeginTransaction()
        {
            databaseDrive.BeginTransaction();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {

            return await databaseDrive.ExcuteAsync(entity);
                
        }
        public int Excute(SqlCommandEntity entity)
        {
            return databaseDrive.Excute(entity);
        }
        public async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            return await databaseDrive.ExcuteAsync(sqlCommand);
        }
        public int Excute(SqlCommandEntity[] sqlCommand)
        {
            return databaseDrive.Excute(sqlCommand);
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
            return await databaseDrive.ReadAsync<TResult>(entity);
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
            return await databaseDrive.ExcuteAsync<TEntity>(entity, query);
        }
        public  TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            return databaseDrive.Excute<TEntity>(entity, query);
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
            return await databaseDrive.ReadAsync<TResult>(sql, Params);
        }
       
      

        public IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            return databaseDrive.Read<TResult>(entity);
                  
        }

        public TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            return databaseDrive.ReadFirstOrDefault<TResult>(entity);
        }

        public async Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity) 
        {
            return await databaseDrive.ReadFirstOrDefaultAsync<TResult>(entity);
                     
        }

        public int ReadCount(QueryEntity entity)
        {
            return databaseDrive.ReadCount(entity);
                    
        }

        public async Task<int> ReadCountAsync(QueryEntity entity)
        {
            return await databaseDrive.ReadCountAsync(entity);
                      
        }

        public async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
            return await databaseDrive.ReadAnyAsync(entity);
        }

        public  bool ReadAny(QueryEntity entity)
        {
            return  databaseDrive.ReadAny(entity);
        }
    }
}
