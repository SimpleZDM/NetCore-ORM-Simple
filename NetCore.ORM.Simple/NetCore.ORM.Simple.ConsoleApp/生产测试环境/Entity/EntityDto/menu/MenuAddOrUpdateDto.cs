using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
   public class MenuAddOrUpdateDto:IParamsVerify
    {
        public MenuAddOrUpdateDto()
        {
            ParentID=Guid.Empty;
        }
        public Guid ParentID { get; set; }
        [CusRequired("菜单名称")]
       
        public string MenuName { get; set; }
        [CusRequired("组织部级ID")]

        public string ControllerName { get; set; }

        [CusRequired("组织部级ID")]

        public string MenuUrl { get; set; }

        public string Component { get; set; }
      
        public string Icon { get; set; }
     
        public int Sort { get; set; }

        public int MenuTypeID { get; set; }

    }
}
