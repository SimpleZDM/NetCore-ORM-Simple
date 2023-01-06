using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Queryable;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client
 * 接口名称 ISimpleClient
 * 开发人员：-nhy
 * 创建时间：2022/9/20 17:40:52
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple
{
    public interface ISimpleClient
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Insert<TEntity>(List<TEntity> entitys) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Insert<TEntity>(params TEntity[] entitys) where TEntity : class, new()
        {
            var sql = builder.GetInsert(entitys, changeOffset);
            sql.DbCommandType = eDbCommandType.Insert;
            sqls.Add(sql);
            ISimpleCommand<TEntity> command = new SimpleCommand<TEntity>(builder, dbType, sql, sqls, dbDrive);
            changeOffset = entitys.Count() + changeOffset;
            return command;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Insert<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// 
        ISimpleCommand<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Update<TEntity>(List<TEntity> entitys) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Update<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        ISimpleCommand<TEntity> Delete<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        ISimpleCommand<TEntity> Delete<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> Read<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> ReadAsync<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        TEntity FirstOrDefault<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        ISimpleQueryable<T1> Queryable<T1>() where T1 : class, new();
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2> Queryable<T1, T2>(Expression<Func<T1, T2, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3> Queryable<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4> Queryable<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5> Queryable<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6> Queryable<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Queryable<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8> Queryable<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, JoinInfoEntity>> expression) where T1 : class;
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangeAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChange();
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="action"></param>
        void SetAOPLog(Action<string, DbParameter[]> action);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="Column"></param>
        void SetAttr(Type Table = null, Type Column = null);
        /// <summary>
        /// 添加可扩展方法
        /// </summary>
        /// <param name="DBType"></param>
        /// <param name="methodName"></param>
        /// <param name="method"></param>
        void AddDataBaseMethod(eDBType DBType, string methodName, Func<MethodEntity, string> method);
        /// <summary>
        /// 添加可扩展方法
        /// </summary>
        /// <param name="DBType"></param>
        /// <param name="methodName"></param>
        /// <param name="method"></param>
        void AddDataBaseMethod(string methodName, Func<MethodEntity, string> method);
        /// <summary>
        /// 事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();
        /// <summary>
        /// 
        /// </summary>
        void Commit();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();
        /// <summary>
        /// 
        /// </summary>
        void RollBack();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task RollBackAsync();
    }
}
