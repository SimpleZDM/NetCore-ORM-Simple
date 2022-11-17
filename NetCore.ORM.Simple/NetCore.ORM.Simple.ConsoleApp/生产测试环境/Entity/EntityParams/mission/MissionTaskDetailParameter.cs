using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams
 * 接口名称 MissionTaskDetailParameter
 * 开发人员：-nhy
 * 创建时间：2022/5/16 17:12:06
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionTaskDetailParameter: BaseParameter
    {
        public MissionTaskDetailParameter()
        {
            PlayerType = -1;
            FloorCount = -1;
        }
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public Guid GroupId { get; set; }
        public int PlayerType { get; set; }
        public int FloorCount { get; set; }

    }
}
