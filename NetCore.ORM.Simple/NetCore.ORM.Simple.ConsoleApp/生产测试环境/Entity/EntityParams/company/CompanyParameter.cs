using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams.company
 * 接口名称 CompanyParameter
 * 开发人员：-nhy
 * 创建时间：2022/10/14 11:46:06
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class CompanyParameter:BaseParameter
    {
        public string SearchTerm { get; set; }
    }
}
