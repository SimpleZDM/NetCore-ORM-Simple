using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityDto
 * 接口名称 IpAddressCreateDto
 * 开发人员：-nhy
 * 创建时间：2022/4/1 9:45:47
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class IpAddressCreateDto:IConsoleShow
    {
        #region Property
        /// <summary>
        /// ip地址
        /// </summary>
        public long IpAddress { get; set; }
        /// <summary>
        /// 是否是糟糕的ip
        /// </summary>
        public bool IsBad { get; set; }
        /// <summary>
        /// 没分组访问次数
        /// </summary>
        public float VisitCountPreMinute { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int VisitCount { get; set; }
        /// <summary>
        /// 描述名称
        /// </summary>
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 访问的时间
        /// </summary>
        public DateTime VisitTime { get; set; }

        /// <summary>
        /// 周期分钟为单位
        /// </summary>
        public int Period { get; set; }
        #endregion
    }
}
