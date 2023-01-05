using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class GroupProductDeviceView
    {
        public int TotalCount { get; set; }
        [ColName("在线")]
        public int OnlineCount { get; set; }
        [ColName("离线")]
        public int OfflineCount { get; set; }
       [ColName("未激活")]
        public int UnActivateCount { get; set; }
       [ColName("过期")]
        public int PastCount { get; set; }

    }
}
