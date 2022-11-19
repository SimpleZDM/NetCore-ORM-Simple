using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.Entity.product
 * 接口名称 ProductDeviceRecordEntity
 * 开发人员：-nhy
 * 创建时间：2022/6/24 15:46:20
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductDeviceRecordEntity:RecordEntity<int>
    {
        public Guid ProductDeviceId { get { return productDeviceId; } set { productDeviceId = value; } }
       
        /// <summary>
        /// 操作类型 0-设备操作开关机 1-转移记录
        /// </summary>
        public int OperationalType { get { return operationalType; } set { operationalType = value; } }
      

        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperationalDescription { get { return operationalDescription; } set { operationalDescription = value; } }



        private string operationalDescription; 
        private int operationalType; 
        private Guid productDeviceId;
    }
}
