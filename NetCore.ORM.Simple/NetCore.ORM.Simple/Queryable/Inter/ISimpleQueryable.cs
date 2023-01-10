using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.ISimple.IQuable
 * 接口名称 IQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:48:00
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISimpleQueryable<T>:ISimpleQuery<T> where T : class
    {
         ISimpleQueryable<T, T2> LeftJoin<T2>(Expression<Func<T, T2,bool>> expression);

        public ISimpleQueryable<T, T2> RightJoin<T2>(Expression<Func<T, T2, bool>> expression);
        public ISimpleQueryable<T, T2> InnerJoin<T2>(Expression<Func<T, T2, bool>> expression);
    }
}
