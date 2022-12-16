using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.Inter
 * 接口名称 ISimpleGroupByQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/27 11:00:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public interface ISimpleGroupByQueryable<TResult,TGroup>:IQueryResult<TResult>
    {
         TGroup Key { get; set; }
         TField Sum<TField>(Expression<Func<TResult, TField>> expression);


         TField Average<TField>(Expression<Func<TResult, TField>> expression);

         int Count<TField>(Expression<Func<TResult, TField>> expression);

         TField Max<TField>(Expression<Func<TResult, TField>> expression);
       

         TField Min<TField>(Expression<Func<TResult, TField>> expression);

        TField FirstOrDefault<TField>(Expression<Func<TResult, TField>> expression);



         IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<ISimpleGroupByQueryable<TResult, TGroup>,TNewResult>> expression)where TNewResult : class;

         ISimpleGroupByQueryable<TResult,TGroup> OrderBy<TOrder>(Expression<Func<ISimpleGroupByQueryable<TResult,TGroup>,TOrder>> expression);
         ISimpleGroupByQueryable<TResult, TGroup> Where(Expression<Func<TGroup, bool>> expression);

    }
}
