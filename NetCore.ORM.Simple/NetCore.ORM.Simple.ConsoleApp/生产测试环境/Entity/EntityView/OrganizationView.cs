using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MDT.VirtualSoftPlatform.Common;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class OrganizationView:IConvertTree<OrganizationView>
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public string Name { get; set; }
        public int orgType { get; set; }
        public List<OrganizationView> Children { get; set; } = new List<OrganizationView>();
    }
}
