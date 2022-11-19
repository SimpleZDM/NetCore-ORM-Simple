using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserMenuParameter:BaseParameter
    {
        public UserMenuParameter()
        {
            IsTree = false;
            IsSort = true;
        }
        public Guid UserId { get; set; }
        public bool IsTree { get; set; }
        public bool IsSort { get; set; }
    }
}
