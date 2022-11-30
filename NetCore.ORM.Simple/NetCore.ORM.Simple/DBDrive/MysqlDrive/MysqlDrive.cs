using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.DBDrive
 * 接口名称 MysqlDrive
 * 开发人员：-nhy
 * 创建时间：2022/9/21 14:50:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class MysqlDrive : BaseDBDrive, IDBDrive
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connect"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// 
        public MysqlDrive(DataBaseConfiguration cfg,ConnectionEntity conntionConfig) : base(cfg)
        {
            currentConnection = new DBDriveEntity(conntionConfig);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        //public override async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        //{
        //    Open();
        //    return await base.ReadAsync<TResult>(sql,Params);
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public override async Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {
            Open(entity);
            return await base.ReadAsync<TResult>(entity);
        }
        public override IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            Open(entity);
            return base.Read<TResult>(entity);
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
        public override async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
            Open(entity);
            return await ReadCountAsync(entity) > CommonConst.Zero;
        }
        public override bool ReadAny(QueryEntity entity)
        {
            Open();
            return base.ReadAny(entity);
        }

        public override async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            Open(entity);
            return await base.ExcuteAsync(entity);
        }
        public override int Excute(SqlCommandEntity entity)
        {
            Open(entity);
            return base.Excute(entity);
        }

        public override async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) 
        {
            Open(entity);
            return await base.ExcuteAsync<TEntity>(entity,query);
        }
        public override TEntity Excute<TEntity>(SqlCommandEntity entity, string query)
        {
            Open(entity);
            return base.Excute<TEntity>(entity, query);
        }
        public async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            IsOpenConnect();
            int result = 0;
            int count = 0;
            int current = 0;
            if (sqlCommand.Length == 1)
            {
                result = await ExcuteAsync(sqlCommand[0]);
            }
            else
            {
                for (int i = 1; i < sqlCommand.Length; i++)
                {
                    if (count > DataBaseConfiguration.INSERTMAXCOUNT)
                    {
                        await ExcuteAsync(sqlCommand[current], async () =>
                        {
                            result += await currentConnection.Command.ExecuteNonQueryAsync();
                        });
                        count = 0;
                        current = i;
                        i++;
                    }
                    sqlCommand[current].StrSqlValue.Append(sqlCommand[i].StrSqlValue.ToString());
                    sqlCommand[current].DbParams.AddRange(sqlCommand[i].DbParams);
                    count++;
                }
            }

            return result;
        }
        public int Excute(SqlCommandEntity[] sqlCommand)
        {
            if (sqlCommand.Count() <= 0)
            {
                return 0;
            }
            Open(sqlCommand[0]);
            return base.Excute(sqlCommand, DataBaseConfiguration.INSERTMAXCOUNT);
        }

        #region
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        protected  void Open(SqlBase entity)
        {
            SetCurrentConnection(entity.DbCommandType,isBeginTransaction);
            base.Open();
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }

        #endregion
    }
}
