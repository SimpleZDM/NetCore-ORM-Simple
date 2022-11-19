using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    ///  角色
    /// </summary>
    /// 
    [Table("roletable")]
    public class RoleEntity:RecordEntity<Guid>
    {
        [MaxLength(20)]
        public string RoleName { get; set; }
        [MaxLength(20)]
        public string DisplayName { get; set; }
        [MaxLength(20)]
        public int RoleCode { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }

    }
}
