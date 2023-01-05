using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityDto
 * 接口名称 MissionDateTimeDto
 * 开发人员：-nhy
 * 创建时间：2022/4/25 9:50:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionDateTimeDto
    {
        #region Field

        #endregion

        #region Constructors and Property
        public Guid InstitutionID { get; set; }
        /// <summary>
        /// 名称-上午第一节课
        /// </summary>
        /// 
        public string Name { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        /// 11:10
        public TimeSpan EndTime { get; set; }
        public string SpanEndTime { get; set; }
        /// <summary>
        /// 任务开始时间表
        /// </summary>
        /// 9:10
        public TimeSpan StartTime { get; set; }
        public string SpanStartTime { get; set; }

        /// <summary>
        /// 时间表类型
        /// </summary>
        public int TimeTypeId { get; set; }
        #endregion

        #region Public Methods and Operators

        #endregion
    }
}
