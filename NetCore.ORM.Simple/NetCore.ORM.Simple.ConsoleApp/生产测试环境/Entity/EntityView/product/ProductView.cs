using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductView
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Introduction { get; set; }
        public string UnityFile { get; set; }
    }
}
