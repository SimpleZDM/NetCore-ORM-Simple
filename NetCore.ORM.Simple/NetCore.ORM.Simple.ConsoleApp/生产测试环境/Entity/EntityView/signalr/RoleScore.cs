using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class RoleScore
    {
        public decimal Score { get; set; }
        public int UsedTime { get; set; }
        public int Right { get; set; }
        public int Wrong { get; set; }
    }
}
