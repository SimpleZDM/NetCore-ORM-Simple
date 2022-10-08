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
        public Action<string, DbParameter[]> AOPSqlLog { get; set; }
        public Task BeginTransactionAsync(); 
        public Task CommitAsync();

        public  Task RollBackAsync();

        public  Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params);
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public  Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity) where TResult : class;
        
        public IEnumerable<TResult> Read<TResult>(QueryEntity entity) where TResult : class;
      
        public TResult ReadFirstOrDefault<TResult>(QueryEntity entity) where TResult : class;
        
        public  Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity) where TResult : class;
        
        public int ReadCount(QueryEntity entity);
       

        public  Task<int> ReadCountAsync(QueryEntity entity);
        

        public  Task<bool> ReadAnyAsync(QueryEntity entity);
        
        public bool ReadAny(QueryEntity entity);
       


        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public  Task<int> ExcuteAsync(SqlCommandEntity entity);

        public  Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand);


        /// <summary>
        /// 添加成功后返回单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public  Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class;

    }
}
