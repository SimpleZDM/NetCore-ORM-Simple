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
    public class MysqlDrive : IDBDrive
    {
        /// <summary>
        /// 设置或者获取当前链接字符串
        /// </summary>
        public string ConnectStr
        {
            get { return connectStr; }
            set
            {
                if (isBeginTransaction)
                {
                    throw new ArgumentNullException("事务已经开启,清先完成事务再进行数据库的切换!");
                }
                connectStr = value;
                connection.ConnectionString = connectStr;
            }
        }

        public Action<string, DbParameter[]> AOPSqlLog { get { return aopSqlLog; } set { aopSqlLog = value; } }

        private MySqlConnection connection;
        private MySqlCommand command;
        private DbDataReader dataRead;
        private string connectStr;
        private bool isBeginTransaction;
        private MySqlTransaction transaction;
        private DataBaseConfiguration configuration;
        private Action<string, DbParameter[]> aopSqlLog;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connect"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MysqlDrive(DataBaseConfiguration cfg)
        {
            configuration = cfg;
            if (Check.IsNullOrEmpty(configuration.CurrentConnectInfo.ConnectStr))
            {
                throw new ArgumentNullException("connect");
            }
            try
            {
                connection = new MySqlConnection(configuration.CurrentConnectInfo.ConnectStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            isBeginTransaction = false;
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
            //TResult data = null;
            int value = 0;
            Excute(entity, (command) =>
           {
               dataRead = command.ExecuteReader();
               while (dataRead.Read())
               {
                   string strValue = dataRead[CommonConst.StrDataCount].ToString();
                   int.TryParse(strValue, out value);
               }
               //var data = MapDataFirstOrDefault<TResult>(entity);
           });
            return value;
        }

        public async Task<int> ReadCountAsync(QueryEntity entity)
        {
            //TResult data = null;
            int value = 0;
            await ExcuteAsync(entity, async (command) =>
            {
                dataRead = await command.ExecuteReaderAsync();
                while (dataRead.Read())
                {
                    string strValue = dataRead[CommonConst.StrDataCount].ToString();
                    int.TryParse(strValue, out value);
                }
                //var data = MapDataFirstOrDefault<TResult>(entity);
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


        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name=""></param>
        /// <returns></returns>
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
                await ExcuteAsync(sqlCommand[0], async (command) =>
                {
                    result = await command.ExecuteNonQueryAsync();
                });
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
            return result;
        }

        /// <summary>
        /// 添加成功后返回单个实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="query"></param>
        /// <returns></returns>
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
            command = new MySqlCommand(entity.StrSqlValue.ToString(), connection);
            if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
            {
                command.Parameters.AddRange(entity.DbParams.ToArray());
            }
            if (!Check.IsNull(AOPSqlLog))
            {
                AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
            }
            action(command);
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
                if (Check.IsNull(action))
                {
                    throw new ArgumentNullException(nameof(action));
                }
                if (!IsOpenConnect())
                {
                    Open();
                }
                command = new MySqlCommand(entity.StrSqlValue.ToString(), connection);

                if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
                {
                    command.Parameters.AddRange(entity.DbParams.ToArray());
                }
                if (!Check.IsNull(AOPSqlLog))
                {
                    AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
                }
                action(command);

                if (configuration.CurrentConnectInfo.IsAutoClose)
                {
                    command.Dispose();
                    Close();
                }
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
            command = new MySqlCommand(entity.StrSqlValue.ToString(), connection);
            if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
            {
                command.Parameters.AddRange(entity.DbParams.ToArray());
            }

            if (!Check.IsNull(AOPSqlLog))
            {
                AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
            }
            action(command);

            if (configuration.CurrentConnectInfo.IsAutoClose && !isBeginTransaction)
            {
                Close();
            }
        }

        private async Task ExcuteAsync(SqlCommandEntity entity, Action<MySqlCommand> action)
        {
            await Task.Run(() =>
            {
                if (Check.IsNull(action))
                {
                    throw new ArgumentNullException(nameof(action));
                }
                if (!IsOpenConnect())
                {
                    Open();
                }
                command = new MySqlCommand(entity.StrSqlValue.ToString(), connection);
                if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
                {
                    command.Parameters.AddRange(entity.DbParams.ToArray());
                }
                if (!Check.IsNull(AOPSqlLog))
                {
                    AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
                }
                action(command);

                if (configuration.CurrentConnectInfo.IsAutoClose && !isBeginTransaction)
                {
                    Close();
                }
            });

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        private IEnumerable<TResult> MapData<TResult>()
        {
            if (Check.IsNull(dataRead))
            {
                return null;
            }
            Type type = typeof(TResult);
            Dictionary<string, PropertyInfo> MapProps = new Dictionary<string, PropertyInfo>();
            foreach (var item in type.GetNoIgnore())
            {
                string name = item.GetColName();
                if (!MapProps.ContainsKey(name))
                {
                    MapProps.Add(name, item);
                }
            }
            List<TResult> data = new List<TResult>();
            while (dataRead.Read())
            {
                TResult tresult = (TResult)Activator.CreateInstance(type);
                for (int i = 0; i < dataRead.FieldCount; i++)
                {
                    string key = dataRead.GetName(i);
                    if (MapProps.ContainsKey(key))
                    {
                        var Prop = MapProps[key];
                        Prop.SetPropValue(tresult, dataRead[i]);
                        //Prop.SetValue(tresult,dataRead[i]);
                    }
                }
                data.Add(tresult);
            }
            return data;
        }
        private IEnumerable<TResult> MapData<TResult>(QueryEntity entity)
        {
            if (Check.IsNull(dataRead))
            {
                return null;
            }
            Type type = typeof(TResult);
            Dictionary<string, PropertyInfo> PropMapNames = GetPropMapNames(type.GetProperties());
            IEnumerable<TResult> data = null;
            if (entity.LastAnonymity)
            {
                if (entity.LastType.Count().Equals(1))
                {
                    data = ReadDataAnonymity<TResult>(entity);
                }
                else
                {
                    data = ReadDataAnonymitys<TResult>(entity);
                }

            }
            else
            {
                data = ReadData<TResult>(entity, PropMapNames);
            }

            return data;
        }
        private TResult MapDataFirstOrDefault<TResult>(QueryEntity entity)
        {
            if (Check.IsNull(dataRead))
            {
                return default(TResult);
            }
            Type type = typeof(TResult);
            Dictionary<string, PropertyInfo> PropMapNames = GetPropMapNames(type.GetProperties());
            TResult tResult = default(TResult);
            if (entity.LastAnonymity)
            {
                if (entity.LastType.Count().Equals(1))
                {
                    tResult = ReadDataAnonymityFirstOrDefault<TResult>(entity);
                }
                else
                {
                    tResult = ReadDataAnonymitysFirstOrDefault<TResult>(entity);
                }

            }
            else
            {
                tResult = ReadDataFirstOrDefault<TResult>(entity, PropMapNames);
            }
            return tResult;
        }
        private TResult ReadDataAnonymityFirstOrDefault<TResult>(QueryEntity entity)
        {
            return ReadDataAnonymity<TResult>(entity,true).FirstOrDefault();
        }
        private TResult ReadDataAnonymitysFirstOrDefault<TResult>(QueryEntity entity)
        {
            return ReadDataAnonymitys<TResult>(entity,true).FirstOrDefault();
        }

        private IEnumerable<TResult> ReadDataAnonymity<TResult>(QueryEntity entity, bool IsFirst = false)
        {
            Type type = null;
            foreach (var item in entity.LastType)
            {
                type = item.Value;
            }
            if (Check.IsNull(type))
            {
                throw new Exception();
            }
            List<TResult> data = new List<TResult>();
            while (dataRead.Read())
            {
                object obj = Activator.CreateInstance(type);

                foreach (var item in entity.MapInfos.Where(m => m.IsNeed))
                {
                    var Prop = type.GetProperty(item.LastPropName);
                    Prop.SetPropValue(obj, dataRead[item.AsColumnName]);
                }
                data.Add(entity.GetResult<TResult>(obj));
                if (IsFirst)
                {
                    break;
                }
            }
            return data;
        }

        private IEnumerable<TResult> ReadDataAnonymitys<TResult>(QueryEntity entity, bool IsFirst = false)
        {
            Dictionary<string, object> dicobjs = new Dictionary<string,object>();
                foreach (var item in entity.LastType)
                {
                    object obj = Activator.CreateInstance(item.Value);
                    dicobjs.Add(item.Key, obj);
                }
            List<TResult> data = new List<TResult>();
            while (dataRead.Read())
            {

                foreach (var item in dicobjs.Keys)
                {
                    dicobjs[item]= Activator.CreateInstance(entity.LastType[item]);
                }
                foreach (var item in entity.MapInfos.Where(m => m.IsNeed))
                {
                    var Prop = entity.LastType[item.ClassName].GetProperty(item.LastPropName);
                    Prop.SetPropValue(dicobjs[item.ClassName], dataRead[item.AsColumnName]);
                }
                data.Add(entity.GetResult<TResult>(dicobjs.Values.ToArray()));
                if (IsFirst)
                {
                    break;
                }
            }
            return data;
        }


        private TResult ReadDataFirstOrDefault<TResult>(QueryEntity entity, Dictionary<string, PropertyInfo> PropMapNames)
        {
            TResult tResult = default(TResult);
            while (dataRead.Read())
            {
                tResult = Activator.CreateInstance<TResult>();

                foreach (var item in entity.MapInfos)
                {
                    if (PropMapNames.ContainsKey(item.PropName))
                    {
                        PropMapNames[item.PropName].SetPropValue(tResult, dataRead[item.AsColumnName]);
                    }
                }
                break;
            }
            return tResult;
        }

        private IEnumerable<TResult> ReadData<TResult>(QueryEntity entity, Dictionary<string, PropertyInfo> PropMapNames)
        {
            List<TResult> data = new List<TResult>();
            while (dataRead.Read())
            {
                TResult tresult = Activator.CreateInstance<TResult>();

                foreach (var item in entity.MapInfos)
                {
                    if (PropMapNames.ContainsKey(item.PropName))
                    {
                        PropMapNames[item.PropName].SetPropValue(tresult, dataRead[item.AsColumnName]);
                    }
                }
                data.Add(tresult);
            }
            return data;
        }

        /// <summary>
        /// 开启一个事务
        /// </summary>
        /// <returns></returns>
        public async Task BeginTransactionAsync()
        {
            if (isBeginTransaction)
            {
                Console.WriteLine("上一个事务没有提交!");
                return;
            }
            if (!IsOpenConnect())
            {
                Open();
            }
            transaction = await connection.BeginTransactionAsync();
            isBeginTransaction = true;
        }
        public void BeginTransaction()
        {
            if (isBeginTransaction)
            {
                Console.WriteLine("上一个事务没有提交!");
                return;
            }
            if (!IsOpenConnect())
            {
                Open();
            }
            transaction = connection.BeginTransaction();
            isBeginTransaction = true;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns></returns>
        public async Task RollBackAsync()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            await transaction.RollbackAsync();
            isBeginTransaction = false;
            await transaction.DisposeAsync();
        }
        public void RollBack()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            transaction.Rollback();
            isBeginTransaction = false;
            transaction.Dispose();
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            await transaction.CommitAsync();
            isBeginTransaction = false;
            await transaction.DisposeAsync();
        }

        public void Commit()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            transaction.Commit();
            isBeginTransaction = false;
            transaction.Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!Check.IsNull(connection))
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
                connection.Dispose();
            }

            if (!Check.IsNull(command))
            {
                command.Dispose();
            }

            if (!Check.IsNull(dataRead))
            {
                dataRead.Dispose();
            }

            if (!Check.IsNull(transaction))
            {
                transaction.Dispose();
            }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void Open()
        {
            if (Check.IsNull(connection))
            {
                if (Check.IsNullOrEmpty(configuration.CurrentConnectInfo.ConnectStr))
                {
                    throw new ArgumentException("连接字符串不能为空!");
                }
                connection = new MySqlConnection(configuration.CurrentConnectInfo.ConnectStr);
            }
            if (connection.State == System.Data.ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// 检查是否打开连接
        /// </summary>
        /// <returns></returns>
        private bool IsOpenConnect()
        {
            if (!Check.IsNull(connection))
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        private void Close()
        {
            if (!Check.IsNull(connection))
            {
                connection.Close();
            }
        }
        private Dictionary<string, PropertyInfo> GetPropMapNames(PropertyInfo[] Props)
        {
            Dictionary<string, PropertyInfo> PropsMapNames = new Dictionary<string, PropertyInfo>();
            foreach (var item in Props)
            {
                PropsMapNames.Add(item.GetColName(), item);
            }
            return PropsMapNames;
        }
    }
}
