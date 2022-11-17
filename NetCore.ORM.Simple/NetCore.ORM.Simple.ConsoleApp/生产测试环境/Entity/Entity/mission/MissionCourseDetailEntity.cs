using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.Entity.mission
 * 接口名称 MissionCourseDetailEntity
 * 开发人员：-nhy
 * 创建时间：2022/5/26 16:04:29
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("missioncoursedetailtable")]
    public class MissionCourseDetailEntity:RecordEntity<Guid>
    {
        public Guid MissionId { get; set; }

        /// <summary>
        /// 需要做的课程id集合
        /// </summary>
        public string CourseIds { get; set; }

        /// <summary>
        /// 需要做的课程
        /// </summary>
        public string CourseId { get; set; }

        /// <summary>
        /// json数据 记录张节点分数
        /// </summary>

        public string CompleteCourse { get; set; }
        /// <summary>
        /// 分数
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>

        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public Guid UserId { get; set; }
    }
    public class MissionNodeGrade
    {
        /// <summary>
        /// 节点分数
        /// </summary>
        public Guid NodeId { get; set; }
        /// <summary>
        /// 每个节点分数
        /// </summary>

        public double Grade { get; set; }
    }
}
