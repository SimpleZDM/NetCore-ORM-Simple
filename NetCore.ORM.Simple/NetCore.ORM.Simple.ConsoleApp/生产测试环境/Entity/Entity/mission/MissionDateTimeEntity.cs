using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 MissionDateTimeTable
 * 开发人员：-nhy
 * 创建时间：2022/4/25 9:01:04
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("missiondatetimetable")]
    public class MissionDateTimeEntity:RecordEntity<int>
    {
        #region Field

        #endregion

        #region Constructors and Property


        /// <summary>
        /// 学校ID
        /// </summary>
        /// 
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
        /// <summary>
        /// 任务开始时间表
        /// </summary>
        /// 9:10
        public TimeSpan StartTime { get; set; }
        /// <summary>
        /// 时间表类型
        /// </summary>
        public int TimeTypeId { get; set; }

        #endregion

        #region Public Methods and Operators

        #endregion
    }
}
