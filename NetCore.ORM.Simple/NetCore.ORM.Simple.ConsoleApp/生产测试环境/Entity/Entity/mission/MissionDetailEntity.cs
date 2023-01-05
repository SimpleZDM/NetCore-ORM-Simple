using System;
using MDT.VirtualSoftPlatform.Common;
using System.ComponentModel.DataAnnotations.Schema;
using NetCore.ORM.Simple.Common;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 任务详情
    /// </summary>\
    /// 
    [Table("missiondetailtable")]
    [TableName("missiondetailtable")]
    public class MissionDetailEntity : RecordEntity<Guid>
    {
        public MissionDetailEntity()
        {
            ID = Guid.NewGuid();
            GroupID = Guid.NewGuid();
            MissionRole=-1;
        }
        /// <summary>
        /// 设备型号
        /// </summary>
        public Guid ProductId { get; set; }
        public Guid ProductDeviceId { get; set; }
        public Guid MissionId { get; set; }
        public Guid UserId { get; set; }
        /// <summary>
        /// 任务角色
        /// 0=项目管理员
        /// 1=构件工艺员
        /// 2=构件装配员
        /// 3=土建施工员
        /// </summary>
        public int MissionRole { get; set; }
        /// <summary>
        /// 状态
        /// 0=未开始
        /// 1=未完成
        /// 2=完成
        /// </summary>
        public int StatusId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Score { get; set; }



        public string OperationRecord { get; set; }
        public Guid GroupID { get; set; }

        /// <summary>
        /// 任务具体的开始时间表
        /// </summary>
        //public Guid MissionTimeId { get; set; }
    }
}
