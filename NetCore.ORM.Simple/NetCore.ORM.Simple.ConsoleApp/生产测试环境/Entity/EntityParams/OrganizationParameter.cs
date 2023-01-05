using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class OrganizationParameter:BaseParameter
    {

        public OrganizationParameter():base()
        {
            InstitutionID = Guid.Empty;
            orgType = -1;
        }
        public Guid InstitutionID { get; set; }
        public bool Istree { get; set; }

        public string Ids { get; set; }
        public int orgType { get; set; }
        public string ParentId { get; set; }
    }
}
