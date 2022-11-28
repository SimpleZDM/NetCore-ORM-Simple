using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 DBDriveEntity
 * 开发人员：-nhy
 * 创建时间：2022/11/28 14:02:13
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class DBDriveEntity
    {
        public DBDriveEntity(DbConnection connection,DbCommand command,string name)
        {
            Connection = connection;
            Command=command;
            Name = name;
        }
        public string Name { get { return name; } set { name = value; } }
        public DBDriveEntity(ConnectionEntity configuration)
        {
            switch (configuration.DBType)
            {
                case eDBType.Mysql:
                    Connection = new MySqlConnection(configuration.ConnectStr);
                    command=new MySqlCommand();
                    command.Connection = Connection;
                    break;
                case eDBType.SqlService:
                    Connection = new SqlConnection(configuration.ConnectStr);
                    command = new SqlCommand();
                    command.Connection = Connection;
                    break;
                case eDBType.Sqlite:
                    Connection = new SqliteConnection(configuration.ConnectStr);
                    command=new SqliteCommand();
                    command.Connection = Connection;
                    break;
                default:
                    break;
            }
            Name=configuration.Name;
        }
        public DbDataReader DataRead { get { return dataRead; } set { dataRead = value; } }
        public DbConnection Connection { get { return connection; } set { connection = value; } }
        public DbTransaction Transaction { get { return transaction; } set { transaction = value; } }
        public DbCommand Command { get { return command; } set { command = value; } }

        private DbDataReader dataRead;
        private DbConnection connection;
        private DbTransaction transaction;
        private DbCommand command;
        private string name;

    }
}
