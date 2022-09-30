
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Queryable;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Visitor;


/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client
 * 接口名称 SimpleClient
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:31:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Client
{
    public class SimpleClient:ISimpleClient
    {
        private DataBaseConfiguration configuration;
        private List<SqlCommandEntity> sqls;
        private Builder builder;
        private DBDrive dbDrive;
        public SimpleClient(DataBaseConfiguration _configuration)
        {
            configuration=_configuration;
            builder = new Builder(configuration.CurrentConnectInfo.DBType);
            sqls = new List<SqlCommandEntity>();
            dbDrive = new DBDrive(configuration);
        }

        public void SetAPOLog(Action<string,DbParameter[]> action)
        {
            dbDrive.AOPSqlLog = action;
        }
        /// <summary>
        /// 插入语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity)where TEntity : class,new ()
        {
            var sql=builder.GetInsert(entity,sqls.Count);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,configuration.CurrentConnectInfo.DBType,sql,sqls,dbDrive);
            return command;
        }
        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entity,sqls.Count);
            sqls.Add(sql);
        }

        public void Delete<TEntity>(Expression<Func<TEntity,bool>>expression) where TEntity : class, new()
        {
            List<ConditionEntity> conditions = new List<ConditionEntity>();
            List<TreeConditionEntity> treeConditions = new List<TreeConditionEntity>();
            Type type = typeof(TEntity);
            var Visitor = new ConditionVisitor(new TableEntity(type),conditions,treeConditions);
            Visitor.Modify(expression);
            sqls.Add(builder.GetDelete<TEntity>(type,conditions,treeConditions));
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class, new()
        {
            sqls.Add(builder.GetDelete(entity));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public ISimpleQueryable<T1> Queryable<T1>()where T1:class,new()
        {
            return new SimpleQueryable<T1>(builder,dbDrive);
        }
        public ISimpleQueryable<T1,T2> Queryable<T1,T2>(Expression<Func<T1,T2,JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1,T2>(expression,builder,dbDrive);
        }
        public ISimpleQueryable<T1, T2,T3> Queryable<T1,T2,T3>(Expression<Func<T1,T2,T3,JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2,T3>(expression,builder,dbDrive);
        }
        public ISimpleQueryable<T1,T2,T3,T4> Queryable<T1,T2,T3,T4>(Expression<Func<T1,T2,T3,T4,JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1,T2,T3,T4>(expression,builder,dbDrive);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangeAsync()
        {
            int result=0;
            foreach (var entity in sqls)
            {
                switch (entity.DbCommandType)
                {
                    case eDbCommandType.Insert:
                    case eDbCommandType.Update:
                    case eDbCommandType.Delete:
                        result +=await dbDrive.ExcuteAsync(entity);
                        break;
                    case eDbCommandType.Query:
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        //public List<T2> GetEntity<T, T1, T2>(T t,Expression<Func<T,T1>>expression,Expression<Func<T1,T2>>expression1)
        //{
        //    var result = new List<T2>();
        //    //T1 t1=(T1)expression.Compile().Invoke(t);
        //    //T2 t2=(T2)expression1.Compile().Invoke(t1);
        //    dynamic[] dys = new dynamic[] { expression.Compile(), expression1.Compile() };
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        var t11 = dys[0].Invoke(t);
        //        T2 t22 = dys[1].Invoke(t11);
        //        result.Add(t22);
        //    }
        //    //result.Add(t2);
             
        //    return result;
        //}

        //public List<T> GetEntity<T>(Dictionary<string,object>data)
        //{
        //    List<T> result = new List<T>();
        //    Type type = typeof(T);
        //    Dictionary<string,PropertyInfo> dic = new Dictionary<string,PropertyInfo>();

        //    foreach (var item in type.GetProperties())
        //    {
        //        dic.Add(item.Name,item);
        //    }

        //    for (int i = 0; i < 100000; i++)
        //    {
        //        T t=(T)Activator.CreateInstance(type);
        //        foreach (var item in data)
        //        {
        //            if (dic.ContainsKey(item.Key))
        //            {
        //                dic[item.Key].SetValue(t,item.Value);
        //            }
        //        }
        //        result.Add(t);
        //    }
        //    return result;
        //}


    }
}
