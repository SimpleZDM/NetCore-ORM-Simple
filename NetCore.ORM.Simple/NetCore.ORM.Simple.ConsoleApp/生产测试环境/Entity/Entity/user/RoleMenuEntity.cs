using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("rolemenutable")]
    public class RoleMenuEntity:RecordEntity<Guid>
    {
        public Guid RoleID { get; set; }
        public Guid MenuID { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
    }
}
