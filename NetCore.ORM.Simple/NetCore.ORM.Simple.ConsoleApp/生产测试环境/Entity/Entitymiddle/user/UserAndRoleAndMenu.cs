using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserAndRoleAndMenu
    {
        public MenuEntity menu { get; set; }
        public RoleMenuEntity roleMenu { get; set; }
        public UserMenuEntity userMenu { get; set; }
        public RoleEntity role { get; set; }
        public UserEntity user { get; set; }
    }
}
