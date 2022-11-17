using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.mission
 * 接口名称 MissionCourseView
 * 开发人员：-nhy
 * 创建时间：2022/5/27 11:17:24
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionCourseView
    {
        public MissionCourseView()
        {
            Times = new List<MissionTimeView>();
            Courses = new List<ProductCourseView>();
        }
        public string MissionName { get; set; }

        public string MissionID { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }


        public DateTime EndTime { get; set; }


        public int MissionTypeId { get; set; }
        public string MissionTypeName { get; set; }
        public Guid TeacherUserID { get; set; }
        public string TeacherName { get; set; }
        public string StudentName { get; set; }
        public Guid StudentUserID { get; set; }

        public string SchoolName { get; set; }
        public Guid SchoolId { get; set; }
        public string InstitutionName { get; set; }
        public string CourseIds { get; set; }
        public Guid InstitutionId { get; set; }

        public float TotalTime { get; set; }

        public string CourseName { get; set; }
        public string CourseId { get; set; }
        public int StatusId { get; set; }

        public Guid ProductId { get; set; }

        public double Score { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName { get; set; }

        public List<MissionTimeView> Times { get; set; }

        public List<ProductCourseView> Courses { get; set; }
    }
}
