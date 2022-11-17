using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.Template
 * 接口名称 MissionReportTeamTemplate
 * 开发人员：-nhy
 * 创建时间：2022/9/13 11:24:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionReportTeamTemplate
    {
        public MissionReportTeamTemplate()
        {

        }

        [ColName("成绩")]
        public decimal Score { get; set; }

      
        [ColName("状态")]
        public string StatusName { get; set; }
        [ColName("开始时间")]
        public string StartTime { get; set; }
        [ColName("结束时间")]
        public string EndTime { get; set; }

        [ColName("项目管理员")]
        public string RoleAdmin { get; set; }

        [ColName("构件工艺员")]
        public string ComponentBuilding { get; set; }// = 1,//构建

        [ColName("构件装配员")]
        public string ComponentAssemble { get; set; }//= 3,//装配员
        [ColName("土建施工员")]
        public string ProcessOfConstruction { get; set; }//= 3,//施工员

        [ColName("实训时长")]
        public string UserTime { get; set; }

        [ColName("实训设备")]
        public string ProductDeviceName { get; set; }
    }
}
