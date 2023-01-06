using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.DBDrive.Inter
 * 接口名称 IDBDrive
 * 开发人员：-nhy
 * 创建时间：2022/9/21 14:50:41
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public interface IDBDrive : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        Action<string, DbParameter[]> AOPSqlLog { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();
        /// <summary>
        /// 
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
        /// <summary>
        /// 
        /// </summary>
        void Commit();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task RollBackAsync();
        /// <summary>
        /// 
        /// </summary>
        void RollBack();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IEnumerable<TResult> Read<TResult>(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TResult ReadFirstOrDefault<TResult>(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int ReadCount(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> ReadCountAsync(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> ReadAnyAsync(QueryEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool ReadAny(QueryEntity entity);
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name=""></param>
        /// <returns></returns>
        Task<int> ExcuteAsync(SqlCommandEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Excute(SqlCommandEntity entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        int Excute(SqlCommandEntity[] sqlCommand);
        /// <summary>
        /// 添加成功后返回单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class;

    }
}
