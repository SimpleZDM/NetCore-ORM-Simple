//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using NetCore.ORM.Simple.Common;
//using NetCore.ORM.ISimple.IQueryable;
///*********************************************************
// * 命名空间 NetCore.ORM.Simple.Queryable
// * 接口名称 MyQuable
// * 开发人员：-nhy
// * 创建时间：2022/9/15 10:36:08
// * 描述说明：
// * 更改历史：
// * 
// * *******************************************************/
//namespace NetCore.ORM.Simple.Queryable
//{

//    /// <summary>
//    /// 解析查询
//    /// </summary>
//    /// <typeparam name="T1"></typeparam>
//    public class SimpleQueryable<T1> : ISimpleQueryable<T1>
//    {
//        private StringBuilder queryString;
//        private string querySelect;
//        private Builder.SqlBuilder sqlBuilder;
//        private DBHelper dbHelper;
//        private Type entityType;
//        private const int MaxTableCount=11;
//        private string[] TableName;
//        private const int TableCount = 1;
       
//        /// <summary>
//        /// cotr 初始化
//        /// </summary>
//        /// <param name="builder"></param>
//        /// <param name="helper"></param>
//        /// <exception cref="ArgumentException"></exception>
//        public SimpleQueryable(Builder.SqlBuilder builder,DBHelper helper)
//        {
//            if(Check.IsNull(builder)){
//                throw new ArgumentException("Sql builder is null;");
//            }
//            if (Check.IsNull(helper))
//            {
//                throw new ArgumentException("DataBase helper is null;");
//            }
//            queryString=new StringBuilder();
//            sqlBuilder=builder;
//            dbHelper = helper;
//            TableName=new string[MaxTableCount];
//            Type type = typeof(T1);
//            TableName[0] = type.GetClassName();
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public ISimpleQueryable<T1> Where(Expression<Func<T1, bool>> matchCondition)
//        {

//            return this;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="map"></param>
//        /// <returns></returns>
//        public ISimpleQueryable<T1> Select<TResult>(Expression<Func<T1, TResult>> map)
//        {

//            return this;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public TResult First<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {

//            TResult result = default(TResult);
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public async Task<TResult> FirstAsync<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
//            return await Task.Run(() =>
//            {
//                TResult result = default(TResult);
//                return result;
//            });
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>

//        public TResult FirstOrDefault<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
            
//                TResult result = default(TResult);
//                return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
//            return await Task.Run(() =>
//            {
//                TResult result = default(TResult);
//                return result;
//            });
//        }
//    }


//    public class SimpleQueryable<T1,T2> : IMyQuable<T1,T2>
//    {
//        private StringBuilder queryString;
//        private string querySelect;
//        private Builder.SqlBuilder sqlBuilder;
//        private DBHelper dbHelper;
//        private Type entityType;
//        private const int MaxTableCount = 11;
//        private string[] TableName;
//        private const int TableCount = 1;

//        /// <summary>
//        /// cotr 初始化
//        /// </summary>
//        /// <param name="builder"></param>
//        /// <param name="helper"></param>
//        /// <exception cref="ArgumentException"></exception>
//        public SimpleQueryable(Builder.SqlBuilder builder, DBHelper helper)
//        {
//            if (Check.IsNull(builder))
//            {
//                throw new ArgumentException("Sql builder is null;");
//            }
//            if (Check.IsNull(helper))
//            {
//                throw new ArgumentException("DataBase helper is null;");
//            }
//            queryString = new StringBuilder();
//            sqlBuilder = builder;
//            dbHelper = helper;
//            TableName = new string[MaxTableCount];
//            Type type = typeof(T1);
//            TableName[0] = type.GetClassName();
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public IMyQuable<T1,T2> Where(Expression<Func<T1, bool>> matchCondition)
//        {

//            return this;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="map"></param>
//        /// <returns></returns>
//        public IMyQuable<T1,T2> Select<TResult>(Expression<Func<T1, TResult>> map)
//        {

//            return this;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public TResult First<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {

//            TResult result = default(TResult);
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public async Task<TResult> FirstAsync<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
//            return await Task.Run(() =>
//            {
//                TResult result = default(TResult);
//                return result;
//            });
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>

//        public TResult FirstOrDefault<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {

//            TResult result = default(TResult);
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
//            return await Task.Run(() =>
//            {
//                TResult result = default(TResult);
//                return result;
//            });
//        }
//    }

//    public class SimpleQueryable<T1, T2,T3> : IMyQuable<T1,T2,T3>
//    {
//        private StringBuilder queryString;
//        private string querySelect;
//        private Builder.SqlBuilder sqlBuilder;
//        private DBHelper dbHelper;
//        private Type entityType;
//        private const int MaxTableCount = 11;
//        private string[] TableName;
//        private const int TableCount = 1;

//        /// <summary>
//        /// cotr 初始化
//        /// </summary>
//        /// <param name="builder"></param>
//        /// <param name="helper"></param>
//        /// <exception cref="ArgumentException"></exception>
//        public SimpleQueryable(Builder.SqlBuilder builder, DBHelper helper)
//        {
//            if (Check.IsNull(builder))
//            {
//                throw new ArgumentException("Sql builder is null;");
//            }
//            if (Check.IsNull(helper))
//            {
//                throw new ArgumentException("DataBase helper is null;");
//            }
//            queryString = new StringBuilder();
//            sqlBuilder = builder;
//            dbHelper = helper;
//            TableName = new string[MaxTableCount];
//            Type type = typeof(T1);
//            TableName[0] = type.GetClassName();
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public IMyQuable<T1, T2,T3> Where(Expression<Func<T1, bool>> matchCondition)
//        {

//            return this;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="map"></param>
//        /// <returns></returns>
//        public IMyQuable<T1, T2,T3> Select<TResult>(Expression<Func<T1, TResult>> map)
//        {

//            return this;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public TResult First<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {

//            TResult result = default(TResult);
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public async Task<TResult> FirstAsync<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
//            return await Task.Run(() =>
//            {
//                TResult result = default(TResult);
//                return result;
//            });
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>

//        public TResult FirstOrDefault<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {

//            TResult result = default(TResult);
//            return result;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <typeparam name="TResult"></typeparam>
//        /// <param name="matchCondition"></param>
//        /// <returns></returns>
//        public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T1, TResult>> matchCondition)
//        {
//            return await Task.Run(() =>
//            {
//                TResult result = default(TResult);
//                return result;
//            });
//        }
//    }
//}
