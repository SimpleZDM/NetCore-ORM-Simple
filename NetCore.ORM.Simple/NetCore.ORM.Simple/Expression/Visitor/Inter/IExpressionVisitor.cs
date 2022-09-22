using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 IExpressionVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/19 14:14:24
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public interface IExpressionVisitor
    {
        public string GetValue();
    }
}
