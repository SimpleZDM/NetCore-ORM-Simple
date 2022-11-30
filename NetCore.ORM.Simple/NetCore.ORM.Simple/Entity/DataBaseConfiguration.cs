using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple
 * 接口名称 DataBaseConfiguration
 * 开发人员：-nhy
 * 创建时间：2022/9/15 17:27:07
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 数据库链接基本配置
    /// </summary>
    public class DataBaseConfiguration
    {
        /// <summary>
        /// 静态
        /// </summary>
        static DataBaseConfiguration()
        {
            DBDrives = new Dictionary<eDBType, Tuple<Type, Type>>();
            INSERTMAX = 800;
            INSERTMAXCOUNT = 30;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rwSplit"></param>
        /// <param name="connections"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        /// 
        public DataBaseConfiguration(bool rwSplit = false, params ConnectionEntity[] connections)
        {
            if (Check.IsNull(connections))
            {
                throw new ArgumentException("connections");
            }
            if (Check.IsNull(DBDrives))
            {
                throw new Exception("请应用数据库相关的驱动,然后配置数据库的连接,不然将无法使用!");
            }
            RwSplit = rwSplit;
            ConnectMapName = new Dictionary<string, ConnectionEntity>();
            rwWight = 1;

            foreach (var connection in connections)
            {
                AddConnect(connection);

                switch (connection.WriteReadType)
                {
                    case eWriteOrReadType.Read:
                    case eWriteOrReadType.ReadOrWrite:
                        connection.Start = rwWight;
                        rwWight += connection.ReadWeight;
                        connection.End = rwWight;
                        break;
                    case eWriteOrReadType.Write:
                    default:
                        break;
                }


            }
        }
        public ConnectionEntity GetConnection(eDbCommandType commandType = eDbCommandType.Insert)
        {
            return GetRandomConnection(commandType);
        }
        /// <summary>
        /// 返回一个链接数据链接信息的名称
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public string GetConnectionKey(eDbCommandType commandType)
        {
            var conection = GetRandomConnection(commandType);
            if (Check.IsNull(conection))
            {
                return null;
            }
            return conection.Name;
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DbConnection CreateDBConnection(ConnectionEntity configuration)
        {

            if (Check.IsNull(DBDrives))
            {
                throw new Exception("请应用数据库相关的驱动,然后配置数据库的连接!");
            }
            if (!DBDrives.ContainsKey(configuration.DBType))
            {
                throw new Exception("请配置相关数据库的驱动！");
            }

            DbConnection connecion = (DbConnection)Activator.CreateInstance(DBDrives[configuration.DBType].Item1, configuration.ConnectStr);

            return connecion;
        }
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="DbType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public static DbParameter CreateDBParameter(eDBType DbType, string key, object value)
        {

            if (Check.IsNull(DBDrives))
            {
                throw new Exception("请应用数据库相关的驱动,然后配置数据库的连接!");
            }
            if (!DBDrives.ContainsKey(DbType))
            {
                throw new Exception("请配置相关数据库的驱动！");
            }
            DbParameter Parameter = (DbParameter)Activator.CreateInstance(DBDrives[DbType].Item2, key, value);
            return Parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <returns></returns>
        private ConnectionEntity GetRandomConnection(eDbCommandType commandType)
        {
            ConnectionEntity connection = null;
            if (RwSplit && commandType == eDbCommandType.Query)
            {
                int rNumber = new Random().Next(0, int.MaxValue);
                int rang = 0;
                if (rNumber > 0 && rwWight-1 > 0)
                {
                    rang = rNumber % (rwWight-1);
                }
                rang++;
                connection = ConnectMapName.Where(c =>
                (c.Value.WriteReadType == eWriteOrReadType.ReadOrWrite ||
                c.Value.WriteReadType == eWriteOrReadType.Read)
                && c.Value.Start <= rang && c.Value.End > rang).FirstOrDefault().Value;

            }
            else if (commandType != eDbCommandType.Query)
            {
                connection = ConnectMapName.Where(
                           c => c.Value.WriteReadType == eWriteOrReadType.Write ||
                           c.Value.WriteReadType == eWriteOrReadType.ReadOrWrite).
                           FirstOrDefault().Value;
            }
            else if (Check.IsNull(connection))
            {
                connection = ConnectMapName.FirstOrDefault().Value;
            }
            return connection;
        }

        private void AddConnect(ConnectionEntity connections)
        {
            if (Check.IsNull(connections))
            {
                throw new Exception("error:connections is null!");
            }
            if (Check.IsNullOrEmpty(connections.Name))
            {
                throw new Exception("error:connections.Name is null!");
            }
            if (Check.IsNullOrEmpty(connections.ConnectStr))
            {
                throw new Exception("error:connections.ConnectStr is null!");
            }

            if (ConnectMapName.ContainsKey(connections.Name))
            {
                throw new Exception("error:Name of database connection string is repetition");
            }
            ConnectMapName.Add(connections.Name, connections);
        }

        /// <summary>
        /// 是否支持读写分离
        /// </summary>
        public bool RwSplit { get { return rwSplit; } set { rwSplit = value; } }
        /// <summary>
        /// 数据库连接的字典集合
        /// </summary>
        public Dictionary<string, ConnectionEntity> ConnectMapName { get { return connectMapName; } set { connectMapName = value; } }



        private Dictionary<string, ConnectionEntity> connectMapName;
        private bool rwSplit;
        /// <summary>
        /// 读的总权重值
        /// </summary>
        private short rwWight;
        private static Dictionary<eDBType, Tuple<Type, Type>> dbDrives;



        public static int INSERTMAX { get { return insertMax; } set { if (value <= 0) throw new Exception("必须大于零!"); insertMax = value; } }
        public static int INSERTMAXCOUNT { get { return insertMAXCOUNT; } set { if (value <= 0) { throw new Exception("必须大于零!"); } insertMAXCOUNT = value; } }
        /// <summary>
        /// 获取链接字符串
        /// 查询,存在多个从库将根据权重返回链接字符串
        /// </summary>
        /// <returns></returns>

        public static Dictionary<eDBType, Tuple<Type, Type>> DBDrives { get { return dbDrives; } set { dbDrives = value; } }
        /// <summary>
        /// 单条语句最多插入的量
        /// insert into [table](*****) value(),value();罪过八百个value
        /// </summary>
        public static int insertMax;
        /// <summary>
        ///多个insert组合
        ///insert into [table]() value();insert into [table]() value();.....
        /// </summary>
        public static int insertMAXCOUNT;



    }
}
