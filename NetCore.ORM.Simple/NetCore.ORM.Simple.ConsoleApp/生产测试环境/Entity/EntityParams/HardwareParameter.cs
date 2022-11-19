using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams
 * 接口名称 HardwareParameter
 * 开发人员：-nhy
 * 创建时间：2022/5/7 15:32:49
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class HardwareParameter : BaseParameter
    {
        public HardwareParameter()
        {
            //HardwareType = -1;
            HardwareId = Guid.Empty;
            ParentId = Guid.Empty;
            SortByCreateDate = 0;//0 不排序 1- 升序 ，2-降序
            SortByHardwareType = 1;
        }
        /// <summary>
        /// 产品
        /// </summary>
        public Guid ProductDeviceId { get; set; }
        /// <summary>
        /// 硬件类型
        /// </summary>

        public string HardwareType { get; set; }
        /// <summary>
        /// 是否绑定
        /// </summary>
        public int BindStatus { get; set; }
        /// <summary>
        /// 构件状态
        /// </summary>
        public string Status { get; set; }

        public int NewStatus{get;set;}
        public int InventoryStatus { get;set;}
        public string SerialNumber { get; set; }

        public Guid HardwareId { get; set; }

        public uint SortByCreateDate { get; set; }  

        public uint SortByHardwareType { get; set; }
        public Guid ParentId { get; set; }


    }
}
