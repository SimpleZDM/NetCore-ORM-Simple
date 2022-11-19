using MDT.VirtualSoftPlatform.Common;
using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{

    [Table("productdevicetable")]
    [TableName("productdevicetable")]
    public class ProductDeviceEntity:RecordEntity<Guid>
    {
        public ProductDeviceEntity()
        {
            InventoryStatus =(int)eInventoryStatus.putInStorage;
            CheckUserId = Guid.Empty;
        }
        /// <summary>
        /// 显示名称，为空时显示产品名称
        /// </summary>
        [MaxLength(100)]
        public string DisplayName { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// AgentPlatformId 平台Id
        /// </summary>
        public Guid APId { get; set; }
        /// <summary>
        /// InstitutionId 机构Id
        /// </summary>
        public Guid InstitutionId { get; set; }
        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime ActivedTime { get; set; }
        /// <summary>
        /// 使用人数
        /// </summary>
        public int UsedUsers { get; set; }
        /// <summary>
        /// 使用时长
        /// </summary>
        public float UsedTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 服务时长 9999
        /// </summary>
        public int LimitServiceTime { get; set; }
        /// <summary>
        /// 设备状态 未激活，冻结，离线，在线
        /// </summary>
        /// 
        public int StatusId { get; set; }

        public int ComponentCount { get; set; }

        public string SerialNumber { get; set; }

        /// <summary>
        /// 是否在仓库状态 
        /// </summary>
        public int InventoryStatus { get; set; }
        public Guid CheckUserId { get; set; }
       
    }
}
