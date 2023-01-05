using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 HardwareFixEntity
 * 开发人员：-nhy
 * 创建时间：2022/8/8 10:33:09
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class HardwareFixEntity:RecordEntity<int>
    {
        public HardwareFixEntity()
        {
            HardwareId = Guid.Empty;  
        }
        /// <summary>
        /// 需要维修的硬件编号
        /// </summary>
        public Guid HardwareId { get { return hardwareId; } set { hardwareId =value; } }

        /// <summary>
        /// 新的硬件编号
        /// </summary>
        public Guid NewHardwareId { get { return newHardwareId; } set { newHardwareId = value; } }

        /// <summary>
        /// 描述-详细说明
        /// </summary>

        public string Description { get { return description; } set { description = value; } }

        /// <summary>
        /// 平台id
        /// </summary>
        public Guid AgentplatformId { get { return agentplatformId; } set { agentplatformId = value; } }
        /// <summary>
        /// 机构id
        /// </summary>
        public Guid InstitutionId { get { return institutionId; } set { institutionId = value; } }
        /// <summary>
        /// 设备ip
        /// </summary>
        public Guid ProductDeviceId { get { return productDeviceId; } set { productDeviceId = value; } }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get { return status; } set { status = value; } }

        /// <summary>
        /// 硬件类型
        /// </summary>
        public int HardwareType { get { return hardwareType; } set { hardwareType = value; } }

        private string description;
        private Guid hardwareId;
        private Guid newHardwareId;
        private Guid agentplatformId;
        private Guid institutionId;
        private Guid productDeviceId;
        private int status;
        private int hardwareType;


    }
}
