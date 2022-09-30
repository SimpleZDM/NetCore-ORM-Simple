using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.Inter
 * 接口名称 ISimpleQueryable8
 * 开发人员：-nhy
 * 创建时间：2022/9/30 13:42:47
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public interface ISimpleQueryable<T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12>:IQueryResult<T1>
    {
        public ISimpleQueryable<T1, T2, T3, T4, T5, T6, T7,T8,T9,T10,T11,T12> Where();
        public IQueryResult<TResult> Select<TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8,T9,T10,T11,T12, TResult>> expression);
    }
}
