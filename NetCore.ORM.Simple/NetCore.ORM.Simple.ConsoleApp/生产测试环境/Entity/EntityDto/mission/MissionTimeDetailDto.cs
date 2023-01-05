using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 MissionTimeDetailDto
 * 开发人员：-nhy
 * 创建时间：2022/5/23 18:23:32
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionTimeDetailDto
    {
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
        /// 状态
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// 课程Id
        /// </summary>
        public string CourseIDs { get; set; }
    }
}
