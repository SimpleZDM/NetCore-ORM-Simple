using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.mission
 * 接口名称 MissionRecordView
 * 开发人员：-nhy
 * 创建时间：2022/6/20 15:45:09
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionRecordView
    {
        public MissionRecordView()
        {

        }
        public Guid MissionId { get; set; }
        public Guid ID { get; set; }
        public Guid TeamGroupId { get; set; }
        public Guid UserId { get; set; }
        public string Record { get; set; }
    }
}
