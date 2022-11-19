using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class RoleMenuParameter: BaseParameter
    {
        public Guid RoleId { get; set; }
        public int MenuTypeID { get; set; }
    }
}
