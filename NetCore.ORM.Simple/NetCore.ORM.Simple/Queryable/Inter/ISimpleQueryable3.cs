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
    public interface ISimpleQueryable<T1,T2,T3>:IQueryResult<T1>
    { 
        
        public ISimpleQueryable<T1,T2,T3> Where(Expression<Func<T1, T2,T3,bool>> expression);
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1,T2,T3,TResult>> expression);
        
    }
}
