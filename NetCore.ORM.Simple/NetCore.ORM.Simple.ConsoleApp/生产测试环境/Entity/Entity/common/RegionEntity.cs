using MDT.VirtualSoftPlatform.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("regiontable")]
    public class RegionEntity:IntKeyEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public int ParentID { get; set; }
        public int RegionTypeID { get; set; }
    }
}
