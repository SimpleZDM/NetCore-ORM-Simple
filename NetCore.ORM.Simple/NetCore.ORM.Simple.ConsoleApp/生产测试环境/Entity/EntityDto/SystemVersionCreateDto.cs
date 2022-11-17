using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class SystemVersionCreateDto:IParamsVerify
    {
        [CusRequired("版本号")]
        public string VersionCode { get; set; }

        [CusRequired("")]
        public int ClientType { get; set; }

        [CusRequired("")]
        public string AppId { get; set; }

        [CusRequired("")]
        public int ForceUpgrade { get; set; }

        //public IFormFile file { get; set; }
        public string FileUrl { get; set; }

        [CusRequired("")]
        public string UpgradeMessage { get; set; }
    }
}
