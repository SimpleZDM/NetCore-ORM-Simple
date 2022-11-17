using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 Class1
 * 开发人员：-nhy
 * 创建时间：2022/4/24 15:41:53
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class GroupMissionView
    {
        //UnStart=0,
        //Run=1,
        //Complete=2, 
        //end=3,
        [ColName("未开始")]
        public int UnStart { get; set; }
        [ColName("进行中")]
        public int Run { get; set; }
        [ColName("完成")]
        public int Complete { get; set; }
        [ColName("中止")]
        public int Terminate { get; set; }

        public int TotalCount { get; set; }
    }
}
