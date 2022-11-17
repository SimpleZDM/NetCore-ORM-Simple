using MDT.VirtualSoftPlatform.Common;
using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 实训任务
    /// </summary>
    [Table("missiontable")]
    [TableName("missiontable")]
    public class MissionEntity : RecordEntity<Guid>
    {
        public MissionEntity()
        {
            ID = Guid.NewGuid();
        }
        [MaxLength(50)]
        public string MissionName { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 实训图纸
        /// </summary>
        public Guid CadId { get; set; }
        /// <summary>
        /// 分组模式，0=1人模式，1=4人模式
        /// </summary>
        public int GroupMode { get; set; }
        /// <summary>
        /// 任务模式，0=学习模式，1=考核模式
        /// </summary>
        public int MissionMode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 对象模式，0=班级实训，1=个人实训
        /// </summary>
        public int TargetModeId { get; set; }
        /// <summary>
        /// 实训对象，
        /// 如果targetMode=0,target=classId 
        /// targetMode=1,target=userIds(用,隔开)
        /// </summary>
        public string TargetId { get; set; }
        /// <summary>
        /// 0=未开始，1=进行中，2=完成，3=终止
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 搭建模式，1=一层搭建，2=2层搭建
        /// </summary>
        public int BuildMode { get; set; }


        public int MissionType { get; set; }
        /// <summary>
        /// 时间表 的id 表示这个任务在某个时间段
        /// </summary>
        //public int TimeId { get; set; }

        public string CourseIds { get; set; }
    }
}
