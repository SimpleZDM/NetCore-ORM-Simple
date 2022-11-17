using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionView
    {
        public MissionView()
        {
            Times=new List<MissionTimeView>();
            Courses = new List<ProductCourseView>();
        }
        public string MissionName { get; set; }

        public string MissionID { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        /// <summary>
        /// 实训图纸
        /// </summary>
        public Guid CadId { get; set; }
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
        public DateTime StartTime { get; set; }

        public int Count { get; set; }

        public DateTime EndTime { get; set; }

        public int TargetModeId { get; set; }
        public string TargetModeName { get; set; }

        public string TargetId { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public int MissionTypeId { get; set; }
        public string MissionTypeName { get; set; }
        public Guid TeacherUserID { get; set; }
        public string TeacherName { get; set; }
        public Guid StudentUserID { get; set; }

        public string SchoolName { get; set; }
        public Guid SchoolId { get; set; }
        public string InstitutionName { get; set; }
        public Guid InstitutionId { get; set; }

        public int BuildMode { get; set; }
        public string BuildModeName { get; set; }
        public int TimeId { get; set; }
        public float TotalTime { get; set; }
        public string CourseIds { get; set; }
        public string ClassName { get; set; }

        public List<MissionTimeView> Times {get;set;}

        public List<ProductCourseView> Courses { get; set; }
    }
}
