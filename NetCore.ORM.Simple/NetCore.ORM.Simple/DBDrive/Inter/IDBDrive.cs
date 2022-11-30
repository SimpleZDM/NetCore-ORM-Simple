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
    public interface IDBDrive:IDisposable
    {
         Action<string,DbParameter[]> AOPSqlLog { get; set; }
         Task BeginTransactionAsync(); 
         void BeginTransaction(); 
         Task CommitAsync();
         void Commit();

          Task RollBackAsync();
          void RollBack();

          Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params);
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
          Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity);
        
         IEnumerable<TResult> Read<TResult>(QueryEntity entity) ;

         TResult ReadFirstOrDefault<TResult>(QueryEntity entity);
        
         Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity);
        
         int ReadCount(QueryEntity entity);
       

          Task<int> ReadCountAsync(QueryEntity entity);
        

          Task<bool> ReadAnyAsync(QueryEntity entity);
        
         bool ReadAny(QueryEntity entity);
       


        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name=""></param>
        /// <returns></returns>
          Task<int> ExcuteAsync(SqlCommandEntity entity);
          int Excute(SqlCommandEntity entity);

          Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand);
          int Excute(SqlCommandEntity[] sqlCommand);


        /// <summary>
        /// 添加成功后返回单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="query"></param>
        /// <returns></returns>
          Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class;
         TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class;
         void SetAttr(Type Table = null, Type Column = null);

    }
}
