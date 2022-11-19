using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.Entity
 * 接口名称 IpAddressEntity
 * 开发人员：-nhy
 * 创建时间：2022/3/30 11:40:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("ipAddresstable")]
    public class IpAddressEntity: RecordEntity<int>
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

        #endregion

    }
}
