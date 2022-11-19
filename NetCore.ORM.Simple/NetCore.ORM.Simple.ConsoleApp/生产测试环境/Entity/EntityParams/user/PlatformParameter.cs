using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 PlatformParameter
 * 开发人员：-nhy
 * 创建时间：2022/3/28 17:28:02
 * 描述说明：
 * 更改历史：
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class PlatformParameter
    {
        public string PlatformId { get; set; }
        public string InstitutionId { get; set; }

        public string DepartmentId { get; set; }
    }
}
