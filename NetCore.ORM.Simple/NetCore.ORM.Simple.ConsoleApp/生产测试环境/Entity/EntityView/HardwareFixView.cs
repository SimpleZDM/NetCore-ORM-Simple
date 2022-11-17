using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView
 * 接口名称 HardwareFixView
 * 开发人员：-nhy
 * 创建时间：2022/8/16 11:13:19
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class HardwareFixView
    {
        public HardwareFixView()
        {
            HardwareId = Guid.Empty;
            NewHardwareId = Guid.Empty;
        }
        /// <summary>
        /// 需要维修的硬件编号
        /// </summary>
        public Guid HardwareId { get { return hardwareId; } set { hardwareId = value; } }

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
        public string AgentplatformName { get { return agentplatformName; } set { agentplatformName = value; } }
        /// <summary>
        /// 机构id
        /// </summary>
        public Guid InstitutionId { get { return institutionId; } set { institutionId = value; } }
        public string InstitutionName { get { return institutionName; } set { institutionName = value; } }
        /// <summary>
        /// 设备ip
        /// </summary>
        public Guid ProductDeviceId { get { return productDeviceId; } set { productDeviceId = value; } }
        public string ProductDeviceName { get { return productDeviceName; } set { productDeviceName = value; } }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get { return status; } set { status = value; } }
        public string StatusName { get { return statusName; } set { statusName= value; } }
        public int HardwareType { get { return hardwareType; } set { hardwareType = value; } }

        private string description;
        private Guid hardwareId;
        private Guid newHardwareId;
        private Guid agentplatformId;
        private Guid institutionId;
        private Guid productDeviceId;
        private int status;
        private int hardwareType;
        private string institutionName;
        private string agentplatformName;
        private string productDeviceName;
        private string statusName;
    }
}
