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
    public class SimpleGroupByQueryable<TResult,TGroup>:QueryResult<TResult>,
        ISimpleGroupByQueryable<TResult,TGroup>
    {
        public SimpleGroupByQueryable(SimpleVisitor visitor, ISqlBuilder builder,IDBDrive dBDrive):base(visitor,builder,dBDrive)
        {
            //visitor,builder,DbDrive
        }
        public TGroup Key { get; set; }
        public TField Sum<TField>(Expression<Func<TResult, TField>> expression)
        {
            return default(TField);
        }

        public TField Average<TField>(Expression<Func<TResult,TField>> expression)
        {
            return default(TField);
        }
        public int Count<TField>(Expression<Func<TResult, TField>> expression)
        {
            return default(int);
        }
        public override int Count()
        {
            return default(int);
        }
        public TField Max<TField>(Expression<Func<TResult,TField>> expression)
        {
            return default(TField);
        }
     
        public TField Min<TField>(Expression<Func<TResult,TField>> expression)
        {
            return default(TField);
        }
        public virtual IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<ISimpleGroupByQueryable<TResult,TGroup>,TNewResult>> expression)where TNewResult : class
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

       
        public TField FirstOrDefault<TField>(Expression<Func<TResult,TField>> expression)
        {
            return default(TField);
        }


    }
}
