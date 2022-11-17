using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 任务操作记录
    /// </summary>
    [Table("missionrecordtable")]
    public class MissionRecordEntity : RecordEntity<Guid>
    {
        public MissionRecordEntity()
        {
            ID = Guid.NewGuid();
        }
        public Guid MissionId { get; set; }
        public Guid TeamGroupId { get; set; }
        public Guid UserId { get; set; }
        public string Record { get; set; }

        //public virtual MissionEntity Mission { get; set; }

    }
}
