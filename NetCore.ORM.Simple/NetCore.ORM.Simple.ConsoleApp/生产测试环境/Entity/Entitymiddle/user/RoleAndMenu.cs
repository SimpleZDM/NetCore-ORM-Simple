using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class RoleAndMenu
    {
        public MenuEntity menu { get; set; }
        public RoleMenuEntity roleMenu { get; set; }
    }
}
