using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("organizationtable")]
    public class OrganizationEntity:RecordEntity<Guid>
    {
        public OrganizationEntity()
        {

        }
        public Guid InstID { get; set; }

        public Guid ParentID { get; set; }

        public string DescName { get; set; }

        public int OrgType { get; set; }
    }
}
