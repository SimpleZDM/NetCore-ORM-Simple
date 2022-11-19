using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.product
 * 接口名称 GroupHardwareView
 * 开发人员：-nhy
 * 创建时间：2022/8/10 10:40:03
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class GroupHardwareView
    {
        public GroupHardwareView()
        {

        }
        public int BindCount { get; set; }
        public int UnBindCount { get; set; }
        public int BadCount { get; set; }
        public int FixCount { get; set; }
    }
}
