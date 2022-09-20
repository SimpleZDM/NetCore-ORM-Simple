//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

///*********************************************************
// * 命名空间 NetCore.ORM.Simple.Queryable
// * 接口名称 IMyQuable
// * 开发人员：-nhy
// * 创建时间：2022/9/15 10:35:33
// * 描述说明：
// * 更改历史：
// * 
// * *******************************************************/
//namespace NetCore.ORM.Simple.Queryable
//{

//    public interface ISimpleQueryable<T1>:IMyQuable
//    {
//        public ISimpleQueryable<T1> Where(Expression<Func<T1,bool>> matchCondition);
//        public ISimpleQueryable<T1> Select<TResult>(Expression<Func<T1,TResult>> map);
//    }
//    public interface IMyQuable
//    {

//    }

//    public interface IMyQuable<T1,T2> : IMyQuable
//    {
//        public IMyQuable<T1,T2> Where(Expression<Func<T1, bool>> matchCondition);
//        public IMyQuable<T1,T2> Select<TResult>(Expression<Func<T1, TResult>> map);
//    }

//    public interface IMyQuable<T1, T2,T3> : IMyQuable
//    {
//        public IMyQuable<T1, T2,T3> Where(Expression<Func<T1, bool>> matchCondition);
//        public IMyQuable<T1, T2,T3> Select<TResult>(Expression<Func<T1, TResult>> map);
//    }
//}
