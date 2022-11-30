using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 DBDriveEntity
 * 开发人员：-nhy
 * 创建时间：2022/11/28 14:02:13
 * 描述说明：数据库操作实体聚合类
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class DBDriveEntity
    {
        public DBDriveEntity(DbConnection connection, DbCommand command, string name)
        {
            Connection = connection;
            Command = command;
            Name = name;
        }
        /// <summary>
        /// 数据库链接名称
        /// </summary>
        public string Name { get { return name; } set { name = value; } }
        public DBDriveEntity(ConnectionEntity configuration)
        {
            Connection = DataBaseConfiguration.CreateDBConnection(configuration);
            Command = Connection.CreateCommand();
            Name = configuration.Name;
            WriteOrReadType = configuration.WriteReadType;

        }
        /// <summary>
        /// 读取
        /// </summary>
        public DbDataReader DataRead { get { return dataRead; } set { dataRead = value; } }
        /// <summary>
        /// 链接
        /// </summary>
        public DbConnection Connection { get { return connection; } set { connection = value; } }
        /// <summary>
        /// 事务
        /// </summary>
        public DbTransaction Transaction { get { return transaction; } set { transaction = value; } }
        /// <summary>
        /// 命令
        /// </summary>
        public DbCommand Command { get { return command; } set { command = value; } }
        public eWriteOrReadType WriteOrReadType { get { return writeOrReadType; } set { writeOrReadType = value; } }

        private DbDataReader dataRead;
        private DbConnection connection;
        private DbTransaction transaction;
        private DbCommand command;
        private string name;
        private eWriteOrReadType writeOrReadType;

    }
}
