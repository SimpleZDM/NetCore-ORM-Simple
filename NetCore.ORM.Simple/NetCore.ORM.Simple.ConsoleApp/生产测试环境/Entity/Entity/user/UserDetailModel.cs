using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserDetailModel : UserEntity
    {
        public string AgentName { get; set; }
        public string InName { get; set; }
        public string DepartmentName { get; set; }
        public string SName { get; set; }
        public string ClassName { get; set; }
    }
}
