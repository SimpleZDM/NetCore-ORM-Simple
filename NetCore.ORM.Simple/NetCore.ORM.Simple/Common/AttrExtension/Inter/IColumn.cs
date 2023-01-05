using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common.AttrExtension
 * 接口名称 IColumn
 * 开发人员：-nhy
 * 创建时间：2022/10/12 16:18:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    public interface IColumn:IName
    {
         bool Key { get; set; }
         bool AutoIncrease { get; set; }
         bool Ignore { get; set; }
    }
}
