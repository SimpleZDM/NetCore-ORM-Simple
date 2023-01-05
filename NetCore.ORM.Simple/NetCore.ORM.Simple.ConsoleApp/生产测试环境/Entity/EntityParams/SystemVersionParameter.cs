using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class SystemVersionParameter:BaseParameter
    {
        public SystemVersionParameter()
        {
            ClientType = -1;
        }
        public string AppId { get; set; }
        public int ClientType { get; set; }
    }
}
