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
        public double Sum(Expression<Func<TResult, double>> expression);

        public float Sum(Expression<Func<TResult, float>> expression);
        
        public int Sum(Expression<Func<TResult, int>> expression);
        

        public decimal Sum(Expression<Func<TResult, decimal>> expression);
      

        public decimal Average(Expression<Func<TResult, decimal>> expression);


        public double Average(Expression<Func<TResult, double>> expression);
      
        public float Average(Expression<Func<TResult, float>> expression);
        

        public int Average(Expression<Func<TResult, int>> expression);
       
        public int Count<TField>(Expression<Func<TResult, TField>> expression);
      
        public int Count();
       
        public IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<ISimpleGroupByQueryable<TResult, TGroup>,TNewResult>> expression)where TNewResult : class;

        public ISimpleGroupByQueryable<TResult,TGroup> OrderBy<TOrder>(Expression<Func<ISimpleGroupByQueryable<TResult,TGroup>,TOrder>> expression);

    }
}
