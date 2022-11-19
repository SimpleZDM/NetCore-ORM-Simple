using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.mission
 * 接口名称 DayMissionTimeView
 * 开发人员：-nhy
 * 创建时间：2022/5/31 17:09:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class DayMissionTimeView
    {
        public DayMissionTimeView()
        {
            missionTimes = new List<MissionDateTimeView>();
        }
        public string Date { get; set; }

        public List<MissionDateTimeView> missionTimes { get; set; }
    }
}
