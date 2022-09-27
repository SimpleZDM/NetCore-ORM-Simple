using MySql.Data.MySqlClient;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.DBDrive.MysqlDrive
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
        private MySqlConnection connection;
        private MySqlCommand command;
        private DbDataReader dataRead;
        private string connectStr;
        private bool isBeginTransaction;
        private MySqlTransaction transaction;
        private DataBaseConfiguration configuration;

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
            if (!IsOpenConnect())
            {
                Open();
            }
            command = new MySqlCommand(sql, connection);
            if (!Check.IsNull(Params)&&Params.Length>0)
            {
                command.Parameters.AddRange(Params);
            }
            dataRead = await command.ExecuteReaderAsync();
            var data = MapData<TResult>();
            if (configuration.CurrentConnectInfo.IsAutoClose)
            {
                Close();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TResult>> ReadAsync<TResult>(string sql,MapEntity[]mapInfos,params DbParameter[] Params)
        {
            if (!IsOpenConnect())
            {
                Open();
            }
            command = new MySqlCommand(sql, connection);
            if (!Check.IsNull(Params) && Params.Length > 0)
            {
                command.Parameters.AddRange(Params);
            }
            dataRead = await command.ExecuteReaderAsync();
            var data = MapData<TResult>(mapInfos);
            if (configuration.CurrentConnectInfo.IsAutoClose)
            {
                Close();
            }
            return data;
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
                TResult tresult = Activator.CreateInstance<TResult>();
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

        private IEnumerable<TResult> MapData<TResult>(MapEntity[]mapInfos)
        {
            if (Check.IsNull(dataRead))
            {
                return null;
            }
            Type type = typeof(TResult);
            Dictionary<string, PropertyInfo> PropMapNames = GetPropMapNames(type.GetProperties());
            List<TResult> data = new List<TResult>();
            while (dataRead.Read())
            {
                TResult tresult = Activator.CreateInstance<TResult>();

                foreach (var item in mapInfos)
                {
                    if (PropMapNames.ContainsKey(item.PropName))
                    {
                        PropMapNames[item.PropName].SetPropValue(tresult,dataRead[item.AsColumnName]);
                    }
                }
                data.Add(tresult);
            }
            return data;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="sql"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<int> ExcuteAsync(string sql, params DbParameter[] Params)
        {
            if (!IsOpenConnect())
            {
                Open();
            }
            command = new MySqlCommand(sql, connection);
            command.Parameters.Add(Params);
            int result = await command.ExecuteNonQueryAsync();

            if (configuration.CurrentConnectInfo.IsAutoClose && !isBeginTransaction)
            {
                Close();
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
        public async Task<TEntity> ExcuteAsync<TEntity>(string sql, string query, params DbParameter[] Params) where TEntity : class
        {
            if (!IsOpenConnect())
            {
                Open();
            }
            command = new MySqlCommand(sql, connection);
            command.Parameters.Add(Params);
            int result = await command.ExecuteNonQueryAsync();
            if (result == 0)
            {
                return null;
            }
            command.CommandText = query;
            command.Parameters.Clear();
            dataRead = await command.ExecuteReaderAsync();
            var data = MapData<TEntity>().FirstOrDefault();
            if (configuration.CurrentConnectInfo.IsAutoClose)
            {
                Close();
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
                if (Check.IsNullOrEmpty(connectStr))
                {
                    throw new ArgumentException("连接字符串不能为空!");
                }
                connection = new MySqlConnection(connectStr);
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

        private Dictionary<string,PropertyInfo> GetPropMapNames(PropertyInfo[] Props)
        {
            Dictionary<string, PropertyInfo> PropsMapNames = new Dictionary<string, PropertyInfo>();
            foreach (var item in Props)
            {
                PropsMapNames.Add(item.GetColName(),item);
            }
            return PropsMapNames;
        }
    }
}
