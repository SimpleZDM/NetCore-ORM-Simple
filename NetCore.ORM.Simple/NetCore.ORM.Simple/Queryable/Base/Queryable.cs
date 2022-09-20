using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.ISimple.IQueryable;
using NetCore.ORM.Simple.Visitor;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 Queryable
 * 开发人员：-nhy
 * 创建时间：2022/9/20 10:41:17
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public class Queryable<T>:ISimpleQueryable<T>where T : class,new()
    {
        public MapVisitor mapVisitor; 
        public JoinVisitor joinVisitor; 
        public ConditionVisitor conditionVisitor;
        public Queryable()
        {
           
            mapVisitor=new MapVisitor();
            joinVisitor=new JoinVisitor(); 
            conditionVisitor=new ConditionVisitor();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ISimpleQueryable<T> Select<TResult>(Expression<Func<T,TResult>>expression)
        {
            return this;
        }

        public IEnumerable<TResult> ToList<TResult>()
        {
            return null;
        }
        public ISimpleQueryable<T> SimpleQueryable()
        {
            return this;
        }
    }
}
