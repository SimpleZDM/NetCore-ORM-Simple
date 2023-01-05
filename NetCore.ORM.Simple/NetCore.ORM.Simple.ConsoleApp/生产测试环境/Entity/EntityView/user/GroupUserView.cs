using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class GroupUserView
    {
        [ColName("学生")]
        public int StuCount { get; set; }

        [ColName("教师")]
        public int TeaCount { get; set; }

        [ColName("平台管理员")]
        public int AgentCount { get; set; }

        [ColName("机构管理员")]
        public int InstCount { get; set; }

        [ColName("工厂生产")]
        public int FactoryCount { get; set; }

    }
}
