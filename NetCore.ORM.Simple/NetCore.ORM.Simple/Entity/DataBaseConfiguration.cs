using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 DataBaseConfiguration
 * 开发人员：-nhy
 * 创建时间：2022/9/15 17:27:07
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class DataBaseConfiguration
    {
        public Dictionary<string,ConnectionEntity> ConnectMapName { get { return connectMapName; } set { connectMapName = value; } }
        /// <summary>
        /// 设置连接名称将更新连接字符串
        /// </summary>
        public string CurrentUseConnectName
        {
            get
            {
                return currentUseConnectName;
            }
            set
            {
                currentUseConnectName = value;
                if (!ConnectMapName.ContainsKey(currentUseConnectName))
                {
                    throw new Exception("未添加该名称的数据连接字符串!");
                }
                currentUseConnectStr = ConnectMapName[currentUseConnectName].ConnectStr;
            }
        }
        /// <summary>
        /// 当前连接字符串
        /// 配置了读写分离之后将返回权重的值
        /// </summary>
        public string CurrentUseConnectStr
        {
            get
            {
                if (RwSplit)
                {
                    int rNumber = new Random().Next(int.MinValue, int.MaxValue);
                    int rang = rNumber % rwWight;
                    var connect = ConnectMapName.Where(c => c.Value.Start <= rang && c.Value.End > rang).FirstOrDefault();
                    return connect.Value.ConnectStr;
                }
                return currentUseConnectStr;
            }
            private set
            {
                currentUseConnectStr = value;
            }
        }
        /// <summary>
        /// 是否支持读写分离
        /// </summary>
        public bool RwSplit { get { return rwSplit; } set { rwSplit = value; } }


        public DataBaseConfiguration(bool rwSplit=false, params ConnectionEntity[] connections)
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
                if (rwWight == 0)
                {
                    CurrentUseConnectName = connection.Name;
                    CurrentUseConnectStr = connection.ConnectStr;
                }
                connection.Start = rwWight;
                rwWight += connection.ReadWeight;
                connection.End = rwWight;
                AddConnect(connection);
            }
        }

        private Dictionary<string, ConnectionEntity> connectMapName;
        private string currentUseConnectName;
        private string currentUseConnectStr;
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
            if (!ConnectMapName.ContainsKey(connections.Name))
            {
                ConnectMapName.Add(connections.Name, connections);
            }
        }

    }
}
