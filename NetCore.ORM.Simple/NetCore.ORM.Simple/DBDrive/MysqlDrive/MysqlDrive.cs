using MySqlConnector;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
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
        public MysqlDrive(DataBaseConfiguration cfg) : base(cfg)
        {
            connection = new MySqlConnection(configuration.CurrentConnectInfo.ConnectStr);
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        {

            var entity = new QueryEntity();
            entity.StrSqlValue.Append(sql);
            entity.DbParams.AddRange(Params);
            IEnumerable<TResult> data = null;
            await ExcuteAsync(entity, async (command) =>
            {
                dataRead = await command.ExecuteReaderAsync();
                data = MapData<TResult>();
            });
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {
            IEnumerable<TResult> data = null;
            await ExcuteAsync(entity, async (command) =>
            {
                dataRead = await command.ExecuteReaderAsync();
                data = MapData<TResult>(entity);
            });
            return data;
        }
        public IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            IEnumerable<TResult> data = null;
            Excute(entity, (command) =>
           {
               dataRead = command.ExecuteReader();
               data = MapData<TResult>(entity);
           });
            return data;
        }
        public TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            TResult data = default(TResult);
            Excute(entity, (command) =>
            {
                dataRead = command.ExecuteReader();
                data = MapDataFirstOrDefault<TResult>(entity);
            });
            return data;
        }
        public async Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity)
        {
            TResult data = default(TResult);
            await ExcuteAsync(entity, async (command) =>
            {
                dataRead = await command.ExecuteReaderAsync();
                data = MapDataFirstOrDefault<TResult>(entity);
            });
            return data;
        }
        public int ReadCount(QueryEntity entity)
        {
            int value = 0;
            Excute(entity, (command) =>
           {
               dataRead = command.ExecuteReader();
               while (dataRead.Read())
               {
                   string strValue = dataRead[CommonConst.StrDataCount].ToString();
                   int.TryParse(strValue, out value);
               }
           });
            return value;
        }
        public async Task<int> ReadCountAsync(QueryEntity entity)
        {
            int value = 0;
            await ExcuteAsync(entity, async (command) =>
            {
                dataRead = await command.ExecuteReaderAsync();
                while (dataRead.Read())
                {
                    string strValue = dataRead[CommonConst.StrDataCount].ToString();
                    int.TryParse(strValue, out value);
                }
            });
            return value;
        }
        public async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
            return await ReadCountAsync(entity) > CommonConst.ZeroOrNull;
        }
        public bool ReadAny(QueryEntity entity)
        {
            return ReadCount(entity) > CommonConst.ZeroOrNull;
        }

        public async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            int result = 0;
            await ExcuteAsync(entity, async (command) =>
            {
                result = await command.ExecuteNonQueryAsync();
            });
            return result;
        }
        public int Excute(SqlCommandEntity entity)
        {
            int result = 0;
            Excute(entity, (command) =>
            {
                result = command.ExecuteNonQuery();
            });
            return result;
        }
        public async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand)
        {
            int result = 0;
            int count = 0;
            int current = 0;
            if (sqlCommand.Length == 1)
            {
                result=await ExcuteAsync(sqlCommand[0]);
            }
            else
            {
                for (int i = 1; i < sqlCommand.Length; i++)
                {
                    if (count > MysqlConst.INSERTMAXCOUNT)
                    {
                        await ExcuteAsync(sqlCommand[current], async (command) =>
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
        public int Excute(SqlCommandEntity[] sqlCommand)
        {
            int result = 0;
            int count = 0;
            int current = 0;
            if (sqlCommand.Length == 1)
            {
                result = Excute(sqlCommand[0]);
            }
            else
            {
                for (int i = 1; i < sqlCommand.Length; i++)
                {
                    if (count > MysqlConst.INSERTMAXCOUNT)
                    {
                        Excute(sqlCommand[current], (command) =>
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

        public async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            TEntity Entity = null;
            await ExcuteAsync(entity, async (command) =>
            {
                int result = await command.ExecuteNonQueryAsync();
                if (result == 0)
                {
                    return;
                }
                command.CommandText = query;
                command.Parameters.Clear();
                dataRead = await command.ExecuteReaderAsync();
                Entity = MapData<TEntity>().FirstOrDefault();
            });
            return Entity;
        }
        public TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            TEntity Entity = null;

            Excute(entity, (command) =>
            {
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    return;
                }
                command.CommandText = query;
                command.Parameters.Clear();
                dataRead = command.ExecuteReader();
                Entity = MapData<TEntity>().FirstOrDefault();
            });
            return Entity;
        }

        #region
        private void Excute(QueryEntity entity, Action<MySqlCommand> action)
        {
            if (Check.IsNull(action))
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (!IsOpenConnect())
            {
                Open();
            }
            command = new MySqlCommand(entity.StrSqlValue.ToString(), (MySqlConnection)connection);
            if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
            {
                command.Parameters.AddRange(entity.DbParams.ToArray());
            }
            if (!Check.IsNull(AOPSqlLog))
            {
                AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
            }
            action((MySqlCommand)command);
            if (configuration.CurrentConnectInfo.IsAutoClose)
            {
                command.Dispose();
                Close();
            }
        }

        private async Task ExcuteAsync(QueryEntity entity, Action<MySqlCommand> action)
        {
            await Task.Run(() =>
            {
                Excute(entity, action);
            });
        }

        private void Excute(SqlCommandEntity entity, Action<MySqlCommand> action)
        {
            if (Check.IsNull(action))
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (!IsOpenConnect())
            {
                Open();
            }
            command = new MySqlCommand(entity.StrSqlValue.ToString(), (MySqlConnection)connection);
            if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
            {
                command.Parameters.AddRange(entity.DbParams.ToArray());
            }

            if (!Check.IsNull(AOPSqlLog))
            {
                AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
            }
            action((MySqlCommand)command);

            if (configuration.CurrentConnectInfo.IsAutoClose && !isBeginTransaction)
            {
                Close();
            }
        }

        private async Task ExcuteAsync(SqlCommandEntity entity, Action<MySqlCommand> action)
        {
            await Task.Run(() =>
            {
                Excute(entity, action);
            });

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
                connection = new MySqlConnection(configuration.CurrentConnectInfo.ConnectStr);
            }
            base.Open();
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }

        #endregion
    }
}
