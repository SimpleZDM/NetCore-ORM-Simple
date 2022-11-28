using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System.Data.Common;
using System.Data.SqlClient;

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
    public class SqlServiceDrive : BaseDBDrive, IDBDrive
    {
        public SqlServiceDrive(DataBaseConfiguration cfg,ConnectionEntity conntionConfig) :base(cfg)
        {
            currentConnection = new DBDriveEntity(conntionConfig);
        }
        public override int Excute(SqlCommandEntity entity)
        {
            Open(entity);
            return base.Excute(entity);
        }

        public  int Excute(SqlCommandEntity[] sqlCommand)
        {
            if (sqlCommand.Count() <= 0)
            {
                return 0;
            }
            Open(sqlCommand[0]);
            return base.Excute(sqlCommand,SqlServiceConst.INSERTMAXCOUNT);
        }

        public override TEntity Excute<TEntity>(SqlCommandEntity entity,string query) where TEntity : class
        {
            Open(entity);
           return base.Excute<TEntity>(entity, query);
        }

        public override async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            Open(entity);
          return await base.ExcuteAsync(entity);
        }

        public  async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            if (sqlCommand.Count()<=0)
            {
                return 0;
            }
            Open(sqlCommand[0]);
            return await base.ExcuteAsync(sqlCommand,SqlServiceConst.INSERTMAXCOUNT);
        }

        public override async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            Open(entity);
            return await ExcuteAsync<TEntity>(entity, query);
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

        //public override async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        //{
        //    Open();
        //    return await base.ReadAsync<TResult>(sql,Params);
        //}

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

        public override  TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            Open(entity);
            return  base.ReadFirstOrDefault<TResult>(entity);
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
            SetCurrentConnection(entity.DbCommandType);
            base.Open();
        }
    
        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }
    }
}
