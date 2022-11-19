using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class RoleCreateOrUpdateDto:IParamsVerify
    {
        public RoleCreateOrUpdateDto()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString();
            RoleMenus = new List<RoleMenuAddOrUpdateDto>();
        }
        public string RoleName { get; set; }
        
        public string DisplayName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<RoleMenuAddOrUpdateDto> RoleMenus { get; set; }
    }
}
