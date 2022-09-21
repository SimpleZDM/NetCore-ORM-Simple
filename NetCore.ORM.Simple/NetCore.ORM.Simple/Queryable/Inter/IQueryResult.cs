using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable.Inter
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
        public IEnumerable<TResult> ToList();
    }
}
