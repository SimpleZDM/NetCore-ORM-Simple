using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class HandleView
    {
        public int Player { get; set; }
        public string taskName { get; set; }
        public string Action { get; set; }
        public TimeSpan Time { get; set; }
    }
}
