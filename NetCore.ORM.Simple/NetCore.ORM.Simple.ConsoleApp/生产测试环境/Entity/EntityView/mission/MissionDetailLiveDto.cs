using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    /**
    * 命名空间: MDT.VirtualSoftPlatform.Entity.EntityView.mission
    *
    * 功 能： N/A
    * 类 名： MissionDetailLiveDto
    *
    * Ver 变更日期 负责人 变更内容
    * ───────────────────────────────────
    * V0.01 2022/3/8 10:38:33 
    *
    *┌──────────────────────────────────┐
    *│　***************************************************．　│
    *│　**********************　　　　　　　　　　　　　　│
    *└──────────────────────────────────┘
*/
    public class MissionDetailLiveDto
    {
        public string UserName { get; set; }
        public int MissionRole { get; set; }
        public Guid ProductDeviceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
