using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    /**
    * 命名空间: MDT.VirtualSoftPlatform.Entity.EntityParams
    *
    * 功 能： N/A
    * 类 名： RegionParameter
    *
    * Ver 变更日期 负责人 变更内容
    * ───────────────────────────────────
    * V0.01 2022/3/7 13:03:36 
    *
    *┌──────────────────────────────────┐
    *│　***************************************************．　│
    *│　**********************　　　　　　　　　　　　　　│
    *└──────────────────────────────────┘
*/
    public class RegionParameter:BaseParameter
    {
        public RegionParameter() 
        {
         RegionType = (int)eRegionType.Province;
        }
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Name { get; set; }
        public int RegionType { get; set; }
    }
}
