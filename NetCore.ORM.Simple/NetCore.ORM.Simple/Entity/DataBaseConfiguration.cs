using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 数据库连接的字典集合
        /// </summary>
        public Dictionary<string, ConnectionEntity> ConnectMapName { get { return connectMapName; } set { connectMapName = value; } }

        /// <summary>
        /// 获取链接字符串
        /// 查询,存在多个从库将根据权重返回链接字符串
        /// </summary>
        /// <returns></returns>
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
        private ConnectionEntity GetRandomConnection(eDbCommandType commandType)
        {
            ConnectionEntity connection=null;
            if (RwSplit && commandType == eDbCommandType.Query)
            {
                int rNumber = new Random().Next(0,int.MaxValue);
                int rang = rNumber % (rwWight-1);
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
            else if(Check.IsNull(connection))
            {
                connection = ConnectMapName. FirstOrDefault().Value;
            }
            return connection;
        }

        /// <summary>
        /// 是否支持读写分离
        /// </summary>
        public bool RwSplit { get { return rwSplit; } set { rwSplit = value; } }


        public DataBaseConfiguration(bool rwSplit = false, params ConnectionEntity[] connections)
        {
            if (Check.IsNull(connections))
            {
                throw new ArgumentException("connections");
            }
            RwSplit = rwSplit;
            ConnectMapName = new Dictionary<string, ConnectionEntity>();
            rwWight = 0;

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

        private Dictionary<string, ConnectionEntity> connectMapName;
        private string currentUseConnectName;
        private string currentUseConnectStr;
        private ConnectionEntity currentConnectInfo;
        private bool rwSplit;

        /// <summary>
        /// 读的总权重值
        /// </summary>
        private short rwWight;
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

    }
}
