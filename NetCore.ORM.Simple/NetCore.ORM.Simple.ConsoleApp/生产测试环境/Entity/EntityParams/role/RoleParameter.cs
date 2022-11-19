using MDT.VirtualSoftPlatform.Common;
using System;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class RoleParameter : BaseParameter
    {
        public RoleParameter()
        {
            RoleType = -1;
        }
        public string Name { get; set; }
        public int RoleType { get; set; }
    }
}
