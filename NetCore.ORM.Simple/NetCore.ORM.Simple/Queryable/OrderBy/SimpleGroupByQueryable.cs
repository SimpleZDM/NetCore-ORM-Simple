using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.SqlBuilder;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 SimpleOrderByQueryable
 * 开发人员：-nhy
 * 创建时间：2022/9/27 10:50:46
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class SimpleGroupByQueryable<TResult,TGroup>:QueryResult<TGroup>,ISimpleGroupByQueryable<TResult,TGroup>
    {
        public SimpleGroupByQueryable(SimpleVisitor visitor,Builder builder,DBDrive dBDrive):base(visitor,builder,dBDrive)
        {
            //visitor,builder,DbDrive
        }
        public TGroup Key { get; set; }
        public double Sum(Expression<Func<TResult, double>>expression)
        {
            return default(double);
        }
        public float Sum(Expression<Func<TResult, float>> expression)
        {
            return default(float);
        }
        public int Sum(Expression<Func<TResult,int>> expression)
        {
            return default(int);
        }

        public decimal Sum(Expression<Func<TResult, decimal>> expression)
        {
            return default(decimal);
        }

        public decimal Average(Expression<Func<TResult, decimal>> expression)
        {
            return default(decimal);
        }
        public double Average(Expression<Func<TResult, double>> expression)
        {
            return default(double);
        }
        public float Average(Expression<Func<TResult, float>> expression)
        {
            return default(float);
        }

        public int Average(Expression<Func<TResult, int>> expression)
        {
            return default(int);
        }
        public int Count<TField>(Expression<Func<TResult, TField>> expression)
        {
            return default(int);
        }
        public int Count()
        {
            return default(int);
        }
        public double Max(Expression<Func<TResult, double>> expression)
        {
            return default(double);
        }
        public float Max(Expression<Func<TResult, float>> expression)
        {
            return default(float);
        }
        public int Max(Expression<Func<TResult, int>> expression)
        {
            return default(int);
        }

        public decimal Max(Expression<Func<TResult, decimal>> expression)
        {
            return default(decimal);
        }
        public double Min(Expression<Func<TResult, double>> expression)
        {
            return default(double);
        }
        public float Min(Expression<Func<TResult, float>> expression)
        {
            return default(float);
        }
        public int Min(Expression<Func<TResult, int>> expression)
        {
            return default(int);
        }

        public decimal Min(Expression<Func<TResult, decimal>> expression)
        {
            return default(decimal);
        }
        public virtual IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<ISimpleGroupByQueryable<TResult,TGroup>,TNewResult>> expression)
        {
            visitor.VisitMap(expression);
            IQueryResult<TNewResult> result = new QueryResult<TNewResult>(visitor,builder, DbDrive);
            return result;
        }
        public ISimpleGroupByQueryable<TResult,TGroup> OrderBy<TOrder>(Expression<Func<ISimpleGroupByQueryable<TResult,TGroup>,TOrder>> expression)
        {
            visitor.OrderBy(expression);
            return this;
        }

    }
}
