using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams.mission
 * 接口名称 MissionRecordParameter
 * 开发人员：-nhy
 * 创建时间：2022/6/20 15:43:32
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionRecordParameter:BaseParameter
    {
        public MissionRecordParameter()
        {
            GroupId = Guid.Empty;
            UserId = Guid.Empty;
            MissionID = Guid.Empty;
        }

        public Guid GroupId { get; set; }
        public Guid UserId { get; set; }

        public Guid MissionID { get; set; }

    }
}
