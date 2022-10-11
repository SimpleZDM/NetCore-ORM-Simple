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
        public TGroup Key { get; set; }
        public TField Sum<TField>(Expression<Func<TResult, TField>> expression);


        public TField Average<TField>(Expression<Func<TResult, TField>> expression);

        public int Count<TField>(Expression<Func<TResult, TField>> expression);

        public TField Max<TField>(Expression<Func<TResult, TField>> expression);
       

        public TField Min<TField>(Expression<Func<TResult, TField>> expression);



        public IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<ISimpleGroupByQueryable<TResult, TGroup>,TNewResult>> expression)where TNewResult : class;

        public ISimpleGroupByQueryable<TResult,TGroup> OrderBy<TOrder>(Expression<Func<ISimpleGroupByQueryable<TResult,TGroup>,TOrder>> expression);
        public TField FirstOrDefault<TField>(Expression<Func<TResult, TField>> expression);

    }
}
