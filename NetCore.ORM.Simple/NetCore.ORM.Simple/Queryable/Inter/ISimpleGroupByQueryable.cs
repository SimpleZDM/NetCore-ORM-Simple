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
    public interface ISimpleGroupByQueryable<TGroup>:IQueryResult<TGroup>
    {
        public TGroup Key { get; set; }
        public double Sum(Expression<Func<TGroup, double>> expression);

        public float Sum(Expression<Func<TGroup, float>> expression);

        public int Sum(Expression<Func<TGroup, int>> expression);


        public decimal Sum(Expression<Func<TGroup, decimal>> expression);

        public decimal Average(Expression<Func<TGroup, decimal>> expression);
        
        public double Average(Expression<Func<TGroup, double>> expression);
        
        public float Average(Expression<Func<TGroup, float>> expression);
      
        public int Average(Expression<Func<TGroup, int>> expression);
        
        public int Count<TField>(Expression<Func<TGroup, TField>> expression);
        public int Count();
        public ISimpleGroupByQueryable<TGroup> Select<TResult>(Expression<Func<SimpleGroupByQueryable<TGroup>,TResult>> expression);
        

    }
}
