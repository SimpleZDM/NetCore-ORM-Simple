using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MDT.VirtualSoftPlatform.Common;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{

    public class LoginParameter:IParamsVerify
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        public int userType { get; set; }
        /// <summary>
        /// 0密码1微信
        /// </summary>
        public int loginPattern { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }
        public string OpenID { get; set; }

        public string RefrishToken { get; set; }

        public string grant_type { get; set; }
    }
}
