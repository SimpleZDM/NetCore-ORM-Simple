using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.OrderBy
 * 接口名称 SimpleOrderByQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/27 10:50:46
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleGroupByQueryable<TGroup>:QueryResult<TGroup>,ISimpleGroupByQueryable<TGroup>
    {
        public TGroup Key { get; set; }
        public double Sum(Expression<Func<TGroup,double>>expression)
        {
            return default(double);
        }
        public float Sum(Expression<Func<TGroup,float>> expression)
        {
            return default(float);
        }
        public int Sum(Expression<Func<TGroup,int>> expression)
        {
            return default(int);
        }

        public decimal Sum(Expression<Func<TGroup,decimal>> expression)
        {
            return default(decimal);
        }

        public decimal Average(Expression<Func<TGroup,decimal>> expression)
        {
            return default(decimal);
        }
        public double Average(Expression<Func<TGroup, double>> expression)
        {
            return default(double);
        }
        public float Average(Expression<Func<TGroup, float>> expression)
        {
            return default(float);
        }

        public int Average(Expression<Func<TGroup,int>> expression)
        {
            return default(int);
        }
        public int Count<TField>(Expression<Func<TGroup,TField>> expression)
        {
            return default(int);
        }
        public int Count()
        {
            return default(int);
        }
        public ISimpleGroupByQueryable<TGroup> Select<TResult>(Expression<Func<SimpleGroupByQueryable<TGroup>,TResult>> expression)
        {

            return this;
        }

    }
}
