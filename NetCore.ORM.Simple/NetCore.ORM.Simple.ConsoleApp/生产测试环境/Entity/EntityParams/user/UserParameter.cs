using MDT.VirtualSoftPlatform.Common;
using System;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserParameter : BaseParameter
    {
        public UserParameter()
        {
            RoleCode = -1;
            UserId=Guid.Empty;
        }
        /// <summary>
        /// 登录账号
        /// </summary>

        public string SearchTerm { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        public int RoleCode { get; set; }
        /// <summary>
        /// 机构id
        /// </summary>
        public string PlatformId { get; set; }
        public string PlatformIds { get; set; }
        public string InstitutionIds{ get; set; }
        public string SchoolIds{ get; set; }
        public string CompanyIds{ get; set; }
        public string SpecialIds{ get; set; }
        public string ClassIds{ get; set; }
        public Guid UserId { get; set; }
    }
}
