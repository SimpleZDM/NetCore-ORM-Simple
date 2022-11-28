using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System.Data.Common;
using System.Reflection;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple
 * 接口名称 BaseDBDrive
 * 开发人员：-nhy
 * 创建时间：2022/10/11 13:38:11
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public class BaseDBDrive
    {
        public BaseDBDrive(DataBaseConfiguration cfg)
        {
            configuration = cfg;
            isBeginTransaction = false;
            drives = new Dictionary<string, DBDriveEntity>();
        }
        public void AddConnection(string key,DBDriveEntity entity)
        {
            if (drives.ContainsKey(key))
            {
                throw new Exception("请给链接字符串配置去不同的名称!");
            }
            drives.Add(key,entity);
        }

        public void SetCurrentConnection(eDbCommandType commandType)
        {
            if (configuration.RwSplit && commandType==eDbCommandType.Query)
            {
              var config = configuration.GetConnection(commandType);
                if (drives.ContainsKey(config.Name))
                {
                    currentConnection = drives[config.Name];
                }
                else
                {
                    currentConnection = new DBDriveEntity(config);
                    AddConnection(config.Name, currentConnection);
                }

            }else if (Check.IsNull(currentConnection))
            {
                var config=configuration.GetConnection(commandType);
                currentConnection = new DBDriveEntity(config);
                AddConnection(config.Name, currentConnection);
            }
        }
        protected Dictionary<string, DBDriveEntity> drives;
        protected string connectStr;
        protected bool isBeginTransaction;
        protected DataBaseConfiguration configuration;
        protected DBDriveEntity currentConnection;
        protected Type tableAtrr;
        protected Type columnAttr;

        //public string ConnectStr
        //{
        //    get { return connectStr; }
        //    set
        //    {
        //        if (isBeginTransaction)
        //        {
        //            throw new ArgumentNullException("事务已经开启,清先完成事务再进行数据库的切换!");
        //        }
        //        connectStr = value;
        //        connection.ConnectionString = connectStr;
        //    }
        //}

        protected Action<string, DbParameter[]> aopSqlLog;
        public Action<string, DbParameter[]> AOPSqlLog { get { return aopSqlLog; } set { aopSqlLog = value; } }
        

        public  void BeginTransaction()
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
            currentConnection.Transaction=currentConnection.Connection.BeginTransaction();
            isBeginTransaction = true;
        }
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
            currentConnection.Transaction = await currentConnection.Connection.BeginTransactionAsync();
            isBeginTransaction = true;
        }
        public void Commit()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            currentConnection.Transaction.Commit();
            isBeginTransaction = false;
            currentConnection.Transaction.Dispose();
        }
        public async Task CommitAsync()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            await currentConnection.Transaction.CommitAsync();
            isBeginTransaction = false;
            await currentConnection.Transaction.DisposeAsync();
        }

        public void RollBack()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            currentConnection.Transaction.Rollback();
            isBeginTransaction = false;
            currentConnection.Transaction.Dispose();
        }
        public async Task RollBackAsync()
        {
            if (!isBeginTransaction)
            {
                return;
            }
            await currentConnection.Transaction.RollbackAsync();
            isBeginTransaction = false;
            await currentConnection.Transaction.DisposeAsync();
        }
        public void Dispose()
        {
            if (!Check.IsNull(currentConnection.Connection))
            {
        
                if (currentConnection.Connection.State != System.Data.ConnectionState.Closed)
                {
                    currentConnection.Connection.Close();
                }
                currentConnection.Connection.Dispose();
            }

            if (!Check.IsNull(currentConnection.Command))
            {
                currentConnection.Command.Dispose();
            }

            if (!Check.IsNull(currentConnection.DataRead))
            {
                currentConnection.DataRead.Dispose();
            }

            if (!Check.IsNull(currentConnection.Transaction))
            {
                currentConnection.Transaction.Dispose();
            }
        }
        protected Dictionary<string, PropertyInfo> GetPropMapNames(PropertyInfo[] Props)
        {
            Dictionary<string, PropertyInfo> PropsMapNames = new Dictionary<string, PropertyInfo>();
            foreach (var item in Props)
            {
                PropsMapNames.Add(GetColName(item), item);
            }
            return PropsMapNames;
        }

        #region map data
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        /// 
        protected IEnumerable<TResult> MapData<TResult>()
        {
            if (Check.IsNull(currentConnection.DataRead))
            {
                return null;
            }
            Type type = typeof(TResult);
            Dictionary<string, PropertyInfo> MapProps = new Dictionary<string, PropertyInfo>();
            foreach (var item in GetNoIgnore(type))
            {
                string name = GetColName(item);
                if (!MapProps.ContainsKey(name))
                {
                    MapProps.Add(name, item);
                }
            }
            List<TResult> data = new List<TResult>();
            while (currentConnection.DataRead.Read())
            {
                TResult tresult = (TResult)Activator.CreateInstance(type);
                for (int i = 0; i < currentConnection.DataRead.FieldCount; i++)
                {
                    string key = currentConnection.DataRead.GetName(i);
                    if (MapProps.ContainsKey(key))
                    {
                        var Prop = MapProps[key];
                        Prop.SetPropValue(tresult,currentConnection.DataRead[i]);
                    }
                }
                data.Add(tresult);
            }
            return data;
        }
        protected IEnumerable<TResult> MapData<TResult>(QueryEntity entity)
        {
            if (Check.IsNull(currentConnection.DataRead))
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
        protected TResult MapDataFirstOrDefault<TResult>(QueryEntity entity)
        {
            if (Check.IsNull(currentConnection.DataRead))
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

        #endregion

        #region read data
        protected TResult ReadDataAnonymityFirstOrDefault<TResult>(QueryEntity entity)
        {
            return ReadDataAnonymity<TResult>(entity, true).FirstOrDefault();
        }
        protected TResult ReadDataAnonymitysFirstOrDefault<TResult>(QueryEntity entity)
        {
            return ReadDataAnonymitys<TResult>(entity, true).FirstOrDefault();
        }
        

        /// <summary>
        /// 映射匿名对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsFirst"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        protected IEnumerable<TResult> ReadDataAnonymity<TResult>(QueryEntity entity, bool IsFirst = false)
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
            while (currentConnection.DataRead.Read())
            {
                object obj = Activator.CreateInstance(type);

                foreach (var item in entity.MapInfos.Where(m => m.IsNeed))
                {
                    var Prop = type.GetProperty(item.PropName);
                    Prop.SetPropValue(obj,currentConnection.DataRead[item.AsColumnName]);
                }
                data.Add(entity.GetResult<TResult>(obj));
                if (IsFirst)
                {
                    break;
                }
            }
            return data;
        }
        /// <summary>
        /// 多个对象映射成一个匿名对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="IsFirst"></param>
        /// <returns></returns>
        protected IEnumerable<TResult> ReadDataAnonymitys<TResult>(QueryEntity entity, bool IsFirst = false)
        {
            Dictionary<string, object> dicobjs = new Dictionary<string, object>();
            foreach (var item in entity.LastType)
            {
                object obj = Activator.CreateInstance(item.Value);
                dicobjs.Add(item.Key, obj);
            }
            List<TResult> data = new List<TResult>();
            while (currentConnection.DataRead.Read())
            {

                foreach (var item in dicobjs.Keys)
                {
                    dicobjs[item] = Activator.CreateInstance(entity.LastType[item]);
                }
                foreach (var item in entity.MapInfos.Where(m => m.IsNeed))
                {
                    var Prop = entity.LastType[item.ClassName].GetProperty(item.PropName);
                    Prop.SetPropValue(dicobjs[item.ClassName],currentConnection.DataRead[item.AsColumnName]);
                }
                data.Add(entity.GetResult<TResult>(dicobjs.Values.ToArray()));
                if (IsFirst)
                {
                    break;
                }
            }
            return data;
        }

        /// <summary>
        /// 读取第一条数据
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="PropMapNames"></param>
        /// <returns></returns>
        protected TResult ReadDataFirstOrDefault<TResult>(QueryEntity entity, Dictionary<string, PropertyInfo> PropMapNames)
        {
            TResult tResult = default(TResult);
            while (currentConnection.DataRead.Read())
            {
                tResult = Activator.CreateInstance<TResult>();

                foreach (var item in entity.MapInfos)
                {
                    if (PropMapNames.ContainsKey(item.PropName))
                    {
                        PropMapNames[item.PropName].SetPropValue(tResult,currentConnection.DataRead[item.AsColumnName]);
                    }
                }
                break;
            }
            return tResult;
        }

        /// <summary>
        /// 将读取的数据映射
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="entity"></param>
        /// <param name="PropMapNames"></param>
        /// <returns></returns>
        protected IEnumerable<TResult> ReadData<TResult>(QueryEntity entity, Dictionary<string, PropertyInfo> PropMapNames)
        {
            List<TResult> data = new List<TResult>();
            while (currentConnection.DataRead.Read())
            {
                TResult tresult = Activator.CreateInstance<TResult>();

                foreach (var item in entity.MapInfos.Where(m=>m.IsNeed))
                {
                    if (PropMapNames.ContainsKey(item.PropName))
                    {
                        PropMapNames[item.PropName].SetPropValue(tresult,currentConnection.DataRead[item.AsColumnName]);
                    }
                }
                data.Add(tresult);
            }
            return data;
        }

        #endregion

        #region command
        protected void Excute(QueryEntity entity, Action action)
        {

            if (Check.IsNull(action))
            {
                throw new ArgumentNullException(nameof(action));
            }
            currentConnection.Command.CommandText = entity.StrSqlValue.ToString();

            if (!Check.IsNull(entity.DbParams) && entity.DbParams.Count > 0)
            {
                if (currentConnection.Command.Parameters.Count > 0)
                {
                    currentConnection.Command.Parameters.Clear();
                }
                currentConnection.Command.Parameters.AddRange(entity.DbParams.ToArray());
            }
            if (!Check.IsNull(AOPSqlLog))
            {
                AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
            }
            action();

            if (configuration.ConnectMapName[currentConnection.Name].IsAutoClose)
            {
                Close();
            }
        }

        protected async Task ExcuteAsync(QueryEntity entity, Action action)
        {
            await Task.Run(() =>
            {
                Excute(entity,action);
            });
        }

        protected void Excute(SqlCommandEntity entity, Action action)
        {
            if (Check.IsNull(action))
            {
                throw new ArgumentNullException(nameof(action));
            }
            currentConnection.Command.CommandText = entity.StrSqlValue.ToString();

            if (!Check.IsNullOrEmpty(entity.DbParams))
            {
                if (currentConnection.Command.Parameters.Count > 0)
                {
                    currentConnection.Command.Parameters.Clear();
                }
                currentConnection.Command.Parameters.AddRange(entity.DbParams.ToArray());
            }

            if (!Check.IsNull(AOPSqlLog))
            {
                AOPSqlLog.Invoke(entity.StrSqlValue.ToString(), entity.DbParams.ToArray());
            }
            action();

            if (configuration.ConnectMapName[currentConnection.Name].IsAutoClose && !isBeginTransaction)
            {
                Close();
            }
        }

        protected async Task ExcuteAsync(SqlCommandEntity entity,Action action)
        {
            await Task.Run(() =>
            {
                Excute(entity, action);
            });

        }
        #endregion

        #region connection
        protected virtual  void Open()
        {
           
            if (currentConnection.Connection.State == System.Data.ConnectionState.Broken)
            {
                currentConnection.Connection.Close();
                currentConnection.Connection.Open();
            }
            if (currentConnection.Connection.State == System.Data.ConnectionState.Closed)
            {
                currentConnection.Connection.Open();
            }

            if (!Check.IsNull(currentConnection.DataRead) && !currentConnection.DataRead.IsClosed)
            {
                currentConnection.DataRead.Close();
            }
        }
        protected virtual bool IsOpenConnect()
        {
            if (!Check.IsNull(currentConnection.Connection))
            {
                if (currentConnection.Connection.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }
            }
            return false;
        }
        protected void Close()
        {
            if (!Check.IsNull(currentConnection.DataRead))
            {
                currentConnection.DataRead.Close();
            }
        }
        protected void CloseConnection()
        {
            if (!Check.IsNull(currentConnection.Connection))
            {
                currentConnection.Connection.Close();
            }
        }
        #endregion

        #region extension attr
        protected string GetTableName(Type type)
        {
            return type.GetTableName(tableAtrr);
        }
        protected string GetColName(PropertyInfo Prop)
        {
            return Prop.GetColName(columnAttr);
        }

        protected IEnumerable<PropertyInfo> GetNotKeyAndIgnore(Type type)
        {
            return type.GetNotKeyAndIgnore(columnAttr);
        }
        protected IEnumerable<PropertyInfo> GetNoIgnore(Type type)
        {
            return type.GetNoIgnore(columnAttr);
        }
        protected PropertyInfo GetKey(Type type)
        {
            return type.GetKey(columnAttr);
        }
        protected PropertyInfo GetAutoKey(Type type)
        {
            return type.GetAutoKey(columnAttr);
        }
        public virtual void SetAttr(Type Table = null, Type Column = null)
        {
            tableAtrr = Table;
            columnAttr = Column;
        }
        #endregion


        #region api

        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql, params DbParameter[] Params)
        {

            var entity = new QueryEntity();
            entity.StrSqlValue.Append(sql);
            entity.DbParams.AddRange(Params);
            IEnumerable<TResult> data = null;
            await ExcuteAsync(entity, async () =>
            {
                currentConnection.DataRead = await currentConnection.Command.ExecuteReaderAsync();
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
        public virtual async Task<IEnumerable<TResult>> ReadAsync<TResult>(QueryEntity entity)
        {

            IEnumerable<TResult> data = null;
            await ExcuteAsync(entity, async () =>
            {

                currentConnection.DataRead = await currentConnection.Command.ExecuteReaderAsync();
                data = MapData<TResult>(entity);

            });
            return data;
        }
        public virtual IEnumerable<TResult> Read<TResult>(QueryEntity entity)
        {
            IEnumerable<TResult> data = null;
            Excute(entity, () =>
            {
                currentConnection.DataRead = currentConnection.Command.ExecuteReader();
                data = MapData<TResult>(entity);
            });
            return data;
        }
        public virtual TResult ReadFirstOrDefault<TResult>(QueryEntity entity)
        {
            TResult data = default(TResult);
            Excute(entity, () =>
            {
                currentConnection.DataRead = currentConnection.Command.ExecuteReader();
                data = MapDataFirstOrDefault<TResult>(entity);
            });
            return data;
        }
        public virtual async Task<TResult> ReadFirstOrDefaultAsync<TResult>(QueryEntity entity)
        {
            TResult data = default(TResult);
            await ExcuteAsync(entity, async () =>
            {
                currentConnection.DataRead = await currentConnection.Command.ExecuteReaderAsync();
                data = MapDataFirstOrDefault<TResult>(entity);
            });
            return data;
        }
        public virtual int ReadCount(QueryEntity entity)
        {
            int value = 0;
            Excute(entity, () =>
            {
                currentConnection.DataRead = currentConnection.Command.ExecuteReader();
                while (currentConnection.DataRead.Read())
                {
                    string strValue = currentConnection.DataRead[CommonConst.StrDataCount].ToString();
                    int.TryParse(strValue, out value);
                }
            });
            return value;
        }
        public virtual async Task<int> ReadCountAsync(QueryEntity entity)
        {
            int value = 0;
            await ExcuteAsync(entity, async () =>
            {
                currentConnection.DataRead = await currentConnection.Command.ExecuteReaderAsync();
                while (currentConnection.DataRead.Read())
                {
                    string strValue = currentConnection.DataRead[CommonConst.StrDataCount].ToString();
                    int.TryParse(strValue, out value);
                }
            });
            return value;
        }
        public virtual async Task<bool> ReadAnyAsync(QueryEntity entity)
        {
            return await ReadCountAsync(entity) > CommonConst.Zero;
        }
        public virtual bool ReadAny(QueryEntity entity)
        {
            return ReadCount(entity) > CommonConst.Zero;
        }
        public virtual async Task<int> ExcuteAsync(SqlCommandEntity entity)
        {
            int result = 0;
            await ExcuteAsync(entity, async () =>
            {
                result = await currentConnection.Command.ExecuteNonQueryAsync();
            });
            return result;
        }
        public virtual int Excute(SqlCommandEntity entity)
        {
            int result = 0;
            Excute(entity, () =>
            {
                result = currentConnection.Command.ExecuteNonQuery();
            });
            return result;
        }

        public virtual async Task<TEntity> ExcuteAsync<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            TEntity Entity = null;
            await ExcuteAsync(entity, async () =>
            {
                int result = await currentConnection.Command.ExecuteNonQueryAsync();
                if (result == 0)
                {
                    return;
                }
                currentConnection.Command.CommandText = query;
                currentConnection.Command.Parameters.Clear();
                currentConnection.DataRead = await currentConnection.Command.ExecuteReaderAsync();
                Entity = MapData<TEntity>().FirstOrDefault();
            });
            return Entity;
        }
        public virtual TEntity Excute<TEntity>(SqlCommandEntity entity, string query) where TEntity : class
        {
            TEntity Entity = null;

            Excute(entity, () =>
            {
                int result = currentConnection.Command.ExecuteNonQuery();
                if (result == 0)
                {
                    return;
                }
                currentConnection.Command.CommandText = query;
                currentConnection.Command.Parameters.Clear();
                currentConnection.DataRead = currentConnection.Command.ExecuteReader();
                Entity = MapData<TEntity>().FirstOrDefault();
            });
            return Entity;
        }

        public virtual int Excute(SqlCommandEntity[] sqlCommand,int InsertMaxCount=800)
        {
            Open();
            int result = 0;
            int count = 0;
            int current = 0;
            if (sqlCommand.Length == 1)
            {
                result = Excute(sqlCommand[0]);
            }
            for (int i = 1; i < sqlCommand.Length; i++)
            {
                if (count >InsertMaxCount)
                {
                    Excute(sqlCommand[current], () =>
                    {
                        result += currentConnection.Command.ExecuteNonQuery();
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

        public virtual async Task<int> ExcuteAsync(SqlCommandEntity[] sqlCommand,int InsertMaxCount=800)
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
                    if (count > InsertMaxCount)
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
        #endregion

    }
}
