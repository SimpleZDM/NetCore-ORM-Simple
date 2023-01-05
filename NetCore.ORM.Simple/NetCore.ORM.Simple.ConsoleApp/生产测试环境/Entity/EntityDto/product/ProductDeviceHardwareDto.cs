using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductDeviceHardwareDto:IParamsVerify
    {
        public ProductDeviceHardwareDto()
        {
            ProductDeviceId = Guid.Empty;
            Id = Guid.Empty;

        }

        public Guid Id { get; set; }
        public Guid ProductDeviceId { get; set; }
        /// <summary>
        /// 平台id
        /// </summary>
        public Guid AgentplatformId { get; set; }
        /// <summary>
        /// 机构id
        /// </summary>
        public Guid InstitutionId { get; set; }

        public string SerialNumber { get; set; }
    

        public int HardwareType { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int FixStatus { get; set; }

        public string Remark { get; set; }
        public string Description { get; set; }
        public Guid ParentId { get; set; }
    }
}
