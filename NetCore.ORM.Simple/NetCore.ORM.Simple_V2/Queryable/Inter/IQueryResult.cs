using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

         int Count();
        
         bool Any();
        
         Task<int> CountAsync();
       
         Task<bool> AnyAsync();
        
         TResult First();
        
         Task<TResult> FirstAsync();
       
         TResult FirstOrDefault();
       
         Task<TResult> FirstOrDefaultAsync();
      
         List<TResult> ToList();
        

         Task<List<TResult>> ToListAsync();
       
    }
}
