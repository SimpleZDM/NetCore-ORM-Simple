using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("productdevicehardwaretable")]
    public  class ProductDeviceHardwareEntity:RecordEntity<Guid>
    {
        public ProductDeviceHardwareEntity()
        {
            Status = (int)eHardwareStatus.normal;
            IsNew =true;
            InventoryStatus =(int)eInventoryStatus.putInStorage;
            CheckUserId = Guid.Empty;
        }
        public Guid ProductDeviceId { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        [MaxLength(50)]
        public string SerialNumber { get; set; }
        /// <summary>
        /// 硬件类型，
        /// 0=沙盘底座，
        /// 1=主控板，
        /// 2=平板
        /// </summary>
        [MaxLength(50)]

        public int HardwareType { get; set; }
        /// <summary>
        /// 备注，平板可以备注 项目管理员，构件装配员等
        /// </summary>
        [MaxLength(50)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否在仓库状态 
        /// </summary>
        public int InventoryStatus { get; set; }
        /// <summary>
        /// 0-良好，1-良好（维修过之后），1-替换中，2-报废，4-维修中
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 是否是新的
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 检测成功
        /// </summary>
        public Guid CheckUserId { get; set; }
        public Guid ParentId { get; set; }
    }
}
