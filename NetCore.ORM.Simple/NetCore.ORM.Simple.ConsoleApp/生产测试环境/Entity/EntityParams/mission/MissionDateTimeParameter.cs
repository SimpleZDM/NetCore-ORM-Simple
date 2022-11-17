using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 MissionDateTimeParameter
 * 开发人员：-nhy
 * 创建时间：2022/4/25 10:22:21
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionDateTimeParameter : BaseParameter
    {

        public MissionDateTimeParameter()
        {
            IsDelete = -1;
            TimeType = -1;
        }
        public Guid InstitutionID { get; set; }
        /// <summary>
        /// 名称-上午第一节课
        /// </summary>
        /// 
        public string SearchTerm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// true 表示发布任务的时候查询 false 表示其他地方查询
        /// </summary>
        public bool IsPublish { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int IsDelete { get; set; }

        public int TimeType { get; set; }

    }
}
