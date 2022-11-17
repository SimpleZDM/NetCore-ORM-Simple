using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.mission
 * 接口名称 MissionTimeView
 * 开发人员：-nhy
 * 创建时间：2022/5/25 17:23:44
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionTimeView
    {
        public Guid Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid InstitutionId { get; set; }

        public int TimeId { get; set; }

        public Guid MissionId { get; set; }
        public string TimeName { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }

    }
}
