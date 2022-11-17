using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductDeviceParameter:BaseParameter
    {
        public ProductDeviceParameter()
        {
            MissionID = Guid.Empty;
            InventoryStatus =1;
            SortByCreateDate = 0;
        }
        public string Name { get; set; }

        public Guid ProductDeviceID { get; set; }
        /// <summary>
        /// 平台
        /// </summary>
        public string AgentPlatIDs { get; set; }
        /// <summary>
        /// 机构
        /// </summary>
        public string InstitutionIDs{ get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Guid DepartmentID { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public string ProductIds { get; set; }

        public Guid MissionID { get; set; }

        public int InventoryStatus { get; set; }

        public uint SortByCreateDate { get; set; }

        public string SerialNumber { get; set; }

        public string SearchTerm { get; set; }

        public string StatusIds { get; set; }


    }
}
