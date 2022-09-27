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
        public Task BeginTransactionAsync(); 
        public Task CommitAsync();
        public Task<int> ExcuteAsync(string sql, params DbParameter[] Params);

        public Task<TEntity> ExcuteAsync<TEntity>(string sql, string query, params DbParameter[] Params) where TEntity : class;

        public  Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params);
        public  Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql,MapEntity[] mapInfos, params DbParameter[] Params);









        public  Task RollBackAsync();
       
       
       
    }
}
