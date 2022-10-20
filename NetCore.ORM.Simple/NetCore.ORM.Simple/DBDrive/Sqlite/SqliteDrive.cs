using Microsoft.Data.Sqlite;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
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
        public SqliteDrive(DataBaseConfiguration cfg) : base(cfg)
        {
            connection = new SqliteConnection(configuration.CurrentConnectInfo.ConnectStr);
        }
        public override int Excute(SqlCommandEntity entity)
        {
            Open();
            return base.Excute(entity);
        }

        public int Excute(SqlCommandEntity[] sqlCommand)
        {
            int result = 0;
            int count = 0;
            int current = 0;
            if (sqlCommand.Length == 1)
            {
                result=Excute(sqlCommand[0]);
            }
            else
            {
                for (int i = 1; i < sqlCommand.Length; i++)
                {
                    if (count > SqliteConst.INSERTMAXCOUNT)
                    {
                        Excute(sqlCommand[current], () =>
                        {
                            result += command.ExecuteNonQuery();
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

        public override TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            Open();
            return base.Excute<TEntity>(entity, query);
        }

        public override async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            Open();
            return await base.ExcuteAsync(entity);
        }

        public async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            Open();
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
                    if (count > SqliteConst.INSERTMAXCOUNT)
                    {
                        await ExcuteAsync(sqlCommand[current], async () =>
                        {
                            result += await command.ExecuteNonQueryAsync();
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

        public override async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            Open();
            return await ExcuteAsync<TEntity>(entity, query);
        }

        public override IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            Open();
            return Read<TResult>(entity);
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
            return await base.ReadAsync<TResult>(sql, Params);
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

        public override TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            Open();
            return base.ReadFirstOrDefault<TResult>(entity);
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
                connection = new SqliteConnection(configuration.CurrentConnectInfo.ConnectStr);
            }
            base.Open();
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }
    }
}
