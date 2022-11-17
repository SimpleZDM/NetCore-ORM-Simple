using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class OrganizationCreateOrUpdate:IParamsVerify
    {
        public OrganizationCreateOrUpdate() 
        {
            InstID = Guid.Empty;
            ParentID = Guid.Empty;
        }

        public Guid InstID { get; set; }

        public Guid ParentID { get; set; }
        [CusRequired("名称")]
        public string DescName { get; set; }
    }
}
