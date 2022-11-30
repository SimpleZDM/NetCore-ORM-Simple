using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 IQueryResult
 * 开发人员：-nhy
 * 创建时间：2022/9/21 9:15:24
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public interface IQueryResult<TResult>
    { 
         IQueryResult<TResult> Skip(int Number);
         IQueryResult<TResult> Take(int Number);
         IQueryResult<TResult> ToPage(int takeNumber, int skipNumber);
         List<TResult> ToList();
         Task<List<TResult>> ToListAsync();
         IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<TResult,TNewResult>> expression) where TNewResult : class;
         IQueryResult<TResult> Select(Expression<Func<TResult,TResult>> expression);
         IQueryResult<TResult> Where(Expression<Func<TResult,bool>>expression);
         IQueryResult<TResult> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression);
          IQueryResult<TResult> OrderByDescending<TOrder>(Expression<Func<TResult, TOrder>> expression);
         ISimpleGroupByQueryable<TResult, TGroup> GroupBy<TGroup>(Expression<Func<TResult, TGroup>> expression);

         int Count();
       
         bool Any();
        
          Task<int> CountAsync();
       
          Task<bool> AnyAsync();
       
         TResult First();
       
          Task<TResult> FirstAsync();
       
         TResult FirstOrDefault();
       
          Task<TResult> FirstOrDefaultAsync();



    }
}
