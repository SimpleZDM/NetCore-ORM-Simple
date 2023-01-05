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
    * 类 名： MissionScoreView
    *
    * Ver 变更日期 负责人 变更内容
    * ───────────────────────────────────
    * V0.01 2022/3/8 10:39:18 
    *
    *┌──────────────────────────────────┐
    *│　***************************************************．　│
    *│　**********************　　　　　　　　　　　　　　│
    *└──────────────────────────────────┘
*/
    public class MissionScoreView
    {
        public string DepartmentName { get; set; }
        public string SpecialtyName { get; set; }
        public string ClassName { get; set; }
        public string MissionName { get; set; }
        public string MissionUsedTime { get; set; }
        public string PlayerUsedTime { get; set; }
        public string PlayerScore { get; set; }
        public string GroupScore { get; set; }
        public string UserName { get; set; }
        public string StudentNumber { get; set; }
        public string Avatar { get; set; } ="";
        public Guid ProductDeviceId { get; set; }
    }
}
