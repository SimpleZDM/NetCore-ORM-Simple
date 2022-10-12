
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
        #region prop or ctor
        /// <summary>
        /// 数据库配置
        /// </summary>
        private DataBaseConfiguration configuration;

        /// <summary>
        /// 缓存一些需要执行插入或者更新的数据
        /// </summary>
        private List<SqlCommandEntity> sqls;
        /// <summary>
        /// sql 语句构造器
        /// </summary>
        private ISqlBuilder builder;

        /// <summary>
        /// sql语句执行器
        /// </summary>
       
        private IDBDrive dbDrive;
        /// <summary>
        /// 更新或者插入数据的量
        /// </summary>
        private int changeOffset;
        public SimpleClient(DataBaseConfiguration _configuration)
        {
            configuration=_configuration;
            builder = new Builder(configuration.CurrentConnectInfo.DBType);
            sqls = new List<SqlCommandEntity>();
            dbDrive = new DBDrive(configuration);
            changeOffset = 0;
        }
        #endregion

        /// <summary>
        /// 关于sql语句和参数
        /// </summary>
        /// <param name="action"></param>
        public void SetAPOLog(Action<string,DbParameter[]> action)
        {
            dbDrive.AOPSqlLog = action;
        }

        #region 执行部分
        public ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity)where TEntity : class,new ()
        {
            var sql=builder.GetInsert(entity, changeOffset);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder,configuration.CurrentConnectInfo.DBType,sql,sqls,dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Insert<TEntity>(List<TEntity> entitys) where TEntity : class, new()
        {
            var sql = builder.GetInsert(entitys,changeOffset);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, configuration.CurrentConnectInfo.DBType, sql, sqls, dbDrive);
            changeOffset = entitys.Count()+ changeOffset;
            return command;
        }
        public ISimpleCommand<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entity,changeOffset);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, configuration.CurrentConnectInfo.DBType, sql, sqls, dbDrive);
            changeOffset++;
            return command;
        }
        public ISimpleCommand<TEntity> Update<TEntity>(List<TEntity> entitys) where TEntity : class, new()
        {
            var sql = builder.GetUpdate(entitys,changeOffset);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, configuration.CurrentConnectInfo.DBType, sql, sqls, dbDrive);
            changeOffset = entitys.Count()+ changeOffset;
            return command;
        }

        public ISimpleCommand<TEntity> Delete<TEntity>(Expression<Func<TEntity,bool>>expression) where TEntity : class, new()
        {
            List<ConditionEntity> conditions = new List<ConditionEntity>();
            List<TreeConditionEntity> treeConditions = new List<TreeConditionEntity>();
            Type type = typeof(TEntity);
            var Visitor = new ConditionVisitor(new TableEntity(type),conditions,treeConditions);
            Visitor.Modify(expression);
            var sql = builder.GetDelete(type,conditions, treeConditions);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, configuration.CurrentConnectInfo.DBType, sql, sqls, dbDrive);
            return command;
        }
        public ISimpleCommand<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, new()
        {
            var sql = builder.GetDelete(entity,changeOffset);
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, configuration.CurrentConnectInfo.DBType, sql, sqls, dbDrive);
            changeOffset++;
            return command;
        }
        #endregion

        #region 查询部分
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
        public ISimpleQueryable<T1, T2, T3, T4,T5> Queryable<T1, T2, T3, T4,T5>(Expression<Func<T1, T2, T3, T4,T5, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4,T5>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5,T6> Queryable<T1, T2, T3, T4, T5,T6>(Expression<Func<T1, T2, T3, T4, T5,T6, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5,T6>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6,T7> Queryable<T1, T2, T3, T4, T5, T6,T7>(Expression<Func<T1, T2, T3, T4, T5,T6,T7, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6,T7>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7,T8> Queryable<T1, T2, T3, T4, T5, T6, T7,T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7,T8>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8,T9> Queryable<T1, T2, T3, T4, T5, T6, T7, T8,T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8,T9>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(expression, builder, dbDrive);
        }
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12, JoinInfoEntity>> expression) where T1 : class
        {
            return new SimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12>(expression, builder, dbDrive);
        }

        #endregion

        public async Task<int> SaveChangeAsync()
        {
            int result=0;
            var array = sqls.Where(command => !command.DbCommandType.Equals(eDbCommandType.Query)).ToArray();
            result = await dbDrive.ExcuteAsync(array);
            return result;
        }
        public int SaveChange()
        {
            int result = 0;
            var array = sqls.Where(command => !command.DbCommandType.Equals(eDbCommandType.Query)).ToArray();
            result =  dbDrive.Excute(array);
            foreach (var item in array)
            {
                sqls.Remove(item);
            }
            changeOffset = 0;
            return result;
        }

        //public List<T2> GetEntity<T, T1, T2>(T t, Expression<Func<T, T1>> expression, Expression<Func<T1, T2>> expression1)
        //{
        //    var result = new List<T2>();
        //    T1 t1 = (T1)expression.Compile().Invoke(t);
        //    T2 t2 = (T2)expression1.Compile().Invoke(t1);
        //    dynamic[] dys = new dynamic[] { expression.Compile(), expression1.Compile() };
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        var t11 = dys[0].Invoke(t);
        //        T2 t22 = dys[1].Invoke(t11);
        //        result.Add(t22);
        //    }
        //    result.Add(t2);

        //    return result;
        //}
        public List<T1> GetEntity<T,T1>(T t, Expression<Func<T, T1>> expression)
        {
            var result = new List<T1>();
            T1 t1 = (T1)expression.Compile().Invoke(t);
            dynamic[] dys = new dynamic[] { expression.Compile()};
            for (int i = 0; i < 100000; i++)
            {
                var t11 = dys[0].Invoke(t);
            }
            result.Add(t1);

            return result;
        }

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
