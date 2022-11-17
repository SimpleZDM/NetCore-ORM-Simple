using MDT.VirtualSoftPlatform.Common;
using System;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserRole : RecordEntity<Guid>

    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
