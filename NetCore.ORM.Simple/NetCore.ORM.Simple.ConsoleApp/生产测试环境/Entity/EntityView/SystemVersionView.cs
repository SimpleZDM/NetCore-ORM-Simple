using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class SystemVersionView
    {
        public int Id { get; set; }
        public string VersionCode { get; set; }
        public int ClientType { get; set; }
        public string AppId { get; set; }
        public int ForceUpgrade { get; set; }
        public string FileUrl { get; set; }
        public string UpgradeMessage { get; set; }
    }
}
