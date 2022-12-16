using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.DBDrive.Sqlite
 * 接口名称 SqliteDrive
 * 开发人员：-nhy
 * 创建时间：2022/10/11 14:51:51
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class SqliteDrive : BaseDBDrive, IDBDrive
    {
        public SqliteDrive(DataBaseConfiguration cfg, ConnectionEntity conntionConfig) : base(cfg)
        {
            currentConnection = new DBDriveEntity(conntionConfig);
        }
        public override int Excute(SqlCommandEntity entity)
        {
            Open(entity);
            return base.Excute(entity);
        }

        public int Excute(SqlCommandEntity[] sqlCommand)
        {
            if (sqlCommand.Count() <= 0)
            {
                return -0;
            }
            Open(sqlCommand[0]);
            return base.Excute(sqlCommand,DataBaseConfiguration.INSERTMAXCOUNT);
        }

        public override TEntity Excute<TEntity>(SqlCommandEntity entity, string query)
        {
            Open(entity);
            return base.Excute<TEntity>(entity, query);
        }

        public override async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            Open(entity);
            return await base.ExcuteAsync(entity);
        }

        public async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            if (sqlCommand.Count()<=0)
            {
                return -0;
            } 
            Open(sqlCommand[0]);
            return await base.ExcuteAsync(sqlCommand,DataBaseConfiguration.INSERTMAXCOUNT);
        }

        public override async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) 
        {
            Open(entity);
            return await base.ExcuteAsync<TEntity>(entity, query);
        }

        public override IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            Open(entity);
            return base.Read<TResult>(entity);
        }

        public override bool ReadAny(QueryEntity entity)
        {
            Open(entity);
            return base.ReadAny(entity);
        }

        public override async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
            Open(entity);
            return await base.ReadAnyAsync(entity);
        }


        public override async Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {
            Open(entity);
            return await base.ReadAsync<TResult>(entity);
        }


        public override int ReadCount(QueryEntity entity)
        {
            Open(entity);
            return base.ReadCount(entity);
        }

        public override async Task<int> ReadCountAsync(QueryEntity entity)
        {
            Open(entity);
            return await base.ReadCountAsync(entity);
        }

        public override TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            Open(entity);
            return base.ReadFirstOrDefault<TResult>(entity);
        }

        public override async Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity)
        {
            Open(entity);
            return await base.ReadFirstOrDefaultAsync<TResult>(entity);
        }


        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected  void Open(SqlBase entity)
        {
            SetCurrentConnection(entity.DbCommandType,isBeginTransaction);
            base.Open();
        }

        
    }
}
