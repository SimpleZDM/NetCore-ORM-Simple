using MDT.VirtualSoftPlatform.Common;
using System;




namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserMenuDto: IParamsVerify
    {
        public Guid MenuID { get; set; }
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
    }
}
