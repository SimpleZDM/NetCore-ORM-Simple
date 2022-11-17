using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class HandleDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Ignore]

        public string Player { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ColName("任务名称")]
            public string taskName { get; set; }
        /// <summary>
        /// 
        /// </summary>
       [ColName("动作")]
        public string Action { get; set; }

        [ColName("时间")]
        public TimeSpan Time { get; set; }
    }
}
