using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams.mission
 * 接口名称 NodeTimeParameter
 * 开发人员：-nhy
 * 创建时间：2022/5/27 15:51:16
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class NodeTimeParameter
    {
        public NodeTimeParameter()
        {
            UserTime = -1;
        }
        public int Number { get; set; }
        public string ProductId { get; set; }

        public int UserTime { get; set; }
    }
}
