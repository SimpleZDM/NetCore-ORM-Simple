using MDT.VirtualSoftPlatform.Common;
using System;



namespace MDT.VirtualSoftPlatform.Entity
{
   public class RoleMenuAddOrUpdateDto:IParamsVerify
    {
        public Guid MenuId { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
    }
}
