using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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
    public class SqlServiceDrive : BaseDBDrive, IDBDrive
    {
        public SqlServiceDrive(DataBaseConfiguration cfg):base(cfg)
        {
            connection = new SqlConnection(configuration.CurrentConnectInfo.ConnectStr);
            command = new SqlCommand();
            command.Connection= connection;
        }
        public override int Excute(SqlCommandEntity entity)
        {
            Open();
            return base.Excute(entity);
        }

        public  int Excute(SqlCommandEntity[] sqlCommand)
        {
            Open();
            return base.Excute(sqlCommand,SqlServiceConst.INSERTMAXCOUNT);
        }

        public override TEntity Excute<TEntity>(SqlCommandEntity entity,string query) where TEntity : class
        {
            Open();
           return base.Excute<TEntity>(entity, query);
        }

        public override async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            Open();
          return await base.ExcuteAsync(entity);
        }

        public  async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            Open();
            return await base.ExcuteAsync(sqlCommand,SqlServiceConst.INSERTMAXCOUNT);
        }

        public override async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            Open();
            return await ExcuteAsync<TEntity>(entity, query);
        }

        public override IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            Open();
            return base.Read<TResult>(entity);
        }

        public override bool ReadAny(QueryEntity entity)
        {
            Open();
            return base.ReadAny(entity);
        }

        public override async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
            Open();
            return await base.ReadAnyAsync(entity);
        }

        public override async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        {
            Open();
            return await base.ReadAsync<TResult>(sql,Params);
        }

        public override async Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {
            Open();
            return await base.ReadAsync<TResult>(entity);
        }


        public override int ReadCount(QueryEntity entity)
        {
            Open();
            return base.ReadCount(entity);
        }

        public override async Task<int> ReadCountAsync(QueryEntity entity)
        {
            Open();
            return await base.ReadCountAsync(entity);
        }

        public override  TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            Open();
            return  base.ReadFirstOrDefault<TResult>(entity);
        }

        public override async Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity)
        {
            Open();
            return await base.ReadFirstOrDefaultAsync<TResult>(entity);
        }

       

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected override void Open()
        {
            if (Check.IsNull(connection))
            {
                if (Check.IsNullOrEmpty(configuration.CurrentConnectInfo.ConnectStr))
                {
                    throw new ArgumentException(CommonConst.GetErrorInfo(ErrorType.ConnectionStrIsNull));
                }
                connection = new SqlConnection(configuration.CurrentConnectInfo.ConnectStr);
            }
            base.Open();
        }
    
        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }
    }
}
