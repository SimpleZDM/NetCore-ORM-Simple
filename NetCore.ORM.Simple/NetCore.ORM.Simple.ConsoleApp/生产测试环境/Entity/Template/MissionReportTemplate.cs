using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityDto
 * 接口名称 MissionReportTemplate
 * 开发人员：-nhy
 * 创建时间：2022/9/9 18:21:53
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionReportTemplate
    {
        public MissionReportTemplate()
        {
            Times = new List<MissionTimeView>();
            Courses = new List<ProductCourseView>();
        }
        public string MissionName { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string ProductName { get; set; }
     
        /// <summary>
        /// 分组模式，0=1人模式，1=4人模式
        /// </summary>
        public int GroupMode { get; set; }
        public string GroupModeName { get; set; }
        /// <summary>
        /// 任务模式，0=学习模式，1=考核模式
        /// </summary>
        public int MissionMode { get; set; }
        public string MissionModeName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        ///总人数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 完成人数
        /// </summary>
        public int ComplateCount { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string ComplatePercent { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 班级或者个人实训
        /// </summary>
        public string TargetModeName { get; set; }
        /// <summary>
        ///状态
        /// </summary>
        public string StatusName { get; set; }

        public string MissionTypeName { get; set; }

        public int BuildMode { get; set; }
        public string BuildModeName { get; set; }
        public List<MissionTimeView> Times { get; set; }

        public List<ProductCourseView> Courses { get; set; }
    }
}
