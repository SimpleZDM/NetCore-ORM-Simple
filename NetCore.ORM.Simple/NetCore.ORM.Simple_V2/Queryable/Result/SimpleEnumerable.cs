using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple_V2.Queryable.Result
 * 接口名称 SimpleEnumerable
 * 开发人员：11920
 * 创建时间：2022/12/14 13:57:03
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public static class SimpleEnumerable
    {
        public static TField FirstOrDefault<TField,TResult,TGroup>(this ISimpleGroupByQueryable<TResult, TGroup> grupquery, Expression<Func<TResult, TField>> expression)
        {
            return default(TField);
        }
    }
}
