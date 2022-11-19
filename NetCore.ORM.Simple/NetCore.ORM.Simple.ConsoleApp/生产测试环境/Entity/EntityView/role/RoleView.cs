using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class RoleView:BaseView
    {
        public string DisplayName { get; set; }
        public string RoleName { get; set; }
        public int RoleCode { get; set; }
        public string RoleID { get; set; }
        public int RoleType { get; set; }
        public string RoleTypeName { get; set; }
    }
}
