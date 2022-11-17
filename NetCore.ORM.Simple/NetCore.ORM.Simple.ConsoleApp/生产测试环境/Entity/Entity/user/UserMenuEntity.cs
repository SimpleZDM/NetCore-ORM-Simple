using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;



namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("usermenutable")]
    public class UserMenuEntity : RecordEntity<Guid>
    {
        public Guid UserID { get; set; }
        public Guid MenuID { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
    }
}
