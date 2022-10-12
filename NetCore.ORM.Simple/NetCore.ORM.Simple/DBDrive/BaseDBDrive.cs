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
        protected DbDataReader dataRead;
        protected string connectStr;
        protected bool isBeginTransaction;
        protected DbConnection connection;
        protected DataBaseConfiguration configuration;
        protected DbTransaction transaction;
        protected DbCommand command;

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

        protected Action<string, DbParameter[]> aopSqlLog;
        public Action<string, DbParameter[]> AOPSqlLog { get { return aopSqlLog; } set { aopSqlLog = value; } }
        public BaseDBDrive(DataBaseConfiguration cfg)
        {
            configuration = cfg;
            if (Check.IsNullOrEmpty(configuration.CurrentConnectInfo.ConnectStr))
            {
                throw new ArgumentNullException(CommonConst.GetErrorInfo(ErrorType.ConnectionStrIsNull));
            }
            isBeginTransaction = false;
        }

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
            transaction = connection.BeginTransaction();
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
            transaction =await connection.BeginTransactionAsync();
            isBeginTransaction = true;
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
        protected Dictionary<string, PropertyInfo> GetPropMapNames(PropertyInfo[] Props)
        {
            Dictionary<string, PropertyInfo> PropsMapNames = new Dictionary<string, PropertyInfo>();
            foreach (var item in Props)
            {
                PropsMapNames.Add(item.GetColName(), item);
            }
            return PropsMapNames;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        protected IEnumerable<TResult> MapData<TResult>()
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
        protected IEnumerable<TResult> MapData<TResult>(QueryEntity entity)
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
        protected TResult MapDataFirstOrDefault<TResult>(QueryEntity entity)
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
            while (dataRead.Read())
            {

                foreach (var item in dicobjs.Keys)
                {
                    dicobjs[item] = Activator.CreateInstance(entity.LastType[item]);
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
            while (dataRead.Read())
            {
                TResult tresult = Activator.CreateInstance<TResult>();

                foreach (var item in entity.MapInfos.Where(m=>m.IsNeed))
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
        protected virtual  void Open()
        {
           
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
        protected bool IsOpenConnect()
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
        protected void Close()
        {
            if (!Check.IsNull(connection))
            {
                connection.Close();
            }
        }
    }
}
