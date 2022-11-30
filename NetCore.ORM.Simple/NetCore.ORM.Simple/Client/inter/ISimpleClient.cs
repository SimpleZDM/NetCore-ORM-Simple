using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Queryable;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
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
        /// 插入
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
         ISimpleCommand<TEntity> Insert<TEntity>(TEntity entity) where TEntity : class, new();
         ISimpleCommand<TEntity> Insert<TEntity>(List<TEntity> entitys) where TEntity : class, new();
         ISimpleCommand<TEntity> Insert<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// 
         ISimpleCommand<TEntity> Update<TEntity>(TEntity entity) where TEntity : class, new();
         ISimpleCommand<TEntity> Update<TEntity>(List<TEntity> entitys) where TEntity : class, new();

         ISimpleCommand<TEntity> Update<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>

         ISimpleCommand<TEntity> Delete<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class, new();
         ISimpleCommand<TEntity> Delete<TEntity>(TEntity entity) where TEntity : class, new();
         ISimpleCommand<TEntity> Delete<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        /// 


         IEnumerable<TEntity> Read<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();

         Task<IEnumerable<TEntity>> ReadAsync<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();

         Task<TEntity> FirstOrDefaultAsync<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        
         TEntity FirstOrDefault<TEntity>(string sql, Dictionary<string, object> Params) where TEntity : class, new();
        
         ISimpleQueryable<T1> Queryable<T1>() where T1 : class, new();

         ISimpleQueryable<T1, T2> Queryable<T1, T2>(Expression<Func<T1, T2, JoinInfoEntity>> expression) where T1 : class;


         ISimpleQueryable<T1, T2, T3> Queryable<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinInfoEntity>> expression) where T1 : class;
       
         ISimpleQueryable<T1, T2, T3, T4> Queryable<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, JoinInfoEntity>> expression) where T1 : class;
         ISimpleQueryable<T1, T2, T3, T4, T5> Queryable<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, JoinInfoEntity>> expression) where T1 : class;


         ISimpleQueryable<T1, T2, T3, T4, T5, T6> Queryable<T1, T2, T3, T4, T5, T6>(Expression<Func<T1, T2, T3, T4, T5, T6, JoinInfoEntity>> expression) where T1 : class;
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7> Queryable<T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, JoinInfoEntity>> expression) where T1 : class;
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8> Queryable<T1, T2, T3, T4, T5, T6, T7, T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, JoinInfoEntity>> expression) where T1 : class;
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, JoinInfoEntity>> expression) where T1 : class;


         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, JoinInfoEntity>> expression) where T1 : class;
       
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, JoinInfoEntity>> expression) where T1 : class;
        
         ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Queryable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, JoinInfoEntity>> expression) where T1 : class;

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
         Task<int> SaveChangeAsync();

         int SaveChange();

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="action"></param>
         void SetAOPLog(Action<string,DbParameter[]> action);

         void SetAttr(Type Table = null, Type Column = null);
        /// <summary>
        /// 事务
        /// </summary>

         void BeginTransaction();

          Task BeginTransactionAsync();

         void Commit();

          Task CommitAsync();


         void RollBack();

          Task RollBackAsync();

        //public List<T1> GetEntity<T, T1>(T t, Expression<Func<T, T1>> expression);

        //public List<T2> GetEntity<T, T1, T2>(T t, Expression<Func<T, T1>> expression, Expression<Func<T1, T2>> expression1);

        //public List<T> GetEntity<T>(Dictionary<string, object> data);
    }
}
