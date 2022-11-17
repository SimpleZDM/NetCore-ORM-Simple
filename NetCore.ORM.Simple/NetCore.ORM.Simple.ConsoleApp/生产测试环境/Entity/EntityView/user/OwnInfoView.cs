using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class OwnInfoView
    {
        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avatar { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }


        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string IdCard { get; set; }

        public string DisplayName { get; set; }

        public int RoleCode { get; set; }


        public string ConcurrencyStamp { get; set; }
        public string DisplayRoleName { get; set; }
        public bool IsBandOpenID { get; set; }
    }
}
