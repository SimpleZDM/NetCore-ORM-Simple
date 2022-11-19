using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.Entity.mission
 * 接口名称 MissionTimeDetailEntity
 * 开发人员：-nhy
 * 创建时间：2022/5/23 14:35:43
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("missiontimedetailtable")]
    public class MissionTimeDetailEntity:RecordEntity<Guid>
    {
        public MissionTimeDetailEntity()
        {
            ID= Guid.NewGuid();
        }
        /// <summary>
        /// 任务id
        /// </summary>
        public Guid MissionId { get; set; }

        public int TimeId { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 0=未开始，1=进行中，2=完成，3=终止
        /// </summary>
        public int StatusId { get; set; }
        /// 课程Id
        /// </summary>
        // string CourseIDs { get; set; }

    
    }
}
