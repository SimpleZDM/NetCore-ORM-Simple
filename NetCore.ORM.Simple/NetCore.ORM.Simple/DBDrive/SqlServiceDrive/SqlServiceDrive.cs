using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.DBDrive.SqlServiceDrive
 * 接口名称 SqlServiceDrive
 * 开发人员：-nhy
 * 创建时间：2022/9/21 14:50:14
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class SqlServiceDrive : IDBDrive
    {
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
