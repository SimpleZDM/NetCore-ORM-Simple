using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{

    /// <summary>
    /// Ignore 标记ignore 将不在excel 中输出该列
    /// </summary>
    public class MissionDetailView
    {
        [Ignore()]
        public Guid MissionId { get; set; }
        [Ignore()]
        public Guid Id { get; set; }
        [Ignore()]
        public string MissionName { get; set; }
        [ColName("姓名")]
        public string StudentName { get; set; }
        [Ignore()]
        public string TeacherName { get; set; }
        [Ignore()]
        public int RoleId { get; set; }
        /// <summary>
        /// 状态完成情况
        /// </summary>
        [Ignore()]
        public int Status { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        [ColName("实训设备")]
        public string ProductDeviceName { get; set; }
       
        public DateTime StartTime { get; set; } 
        [ColName("开始时间")]
        public string StrStartTime { get; set; }

        public DateTime EndTime { get; set; }
        [ColName("结束时间")]
        public string StrEndTime { get; set; }
        [ColName("成绩")]
        public decimal Score { get; set; }
        [ColName("状态")]

        public string StatusName { get; set; }
        [Ignore()]
        public Guid UserId { get; set; }
        [ColName("角色")]
        public string RoleName { get; set; }

        [Ignore()]
        public TimeSpan Minutes { get; set; }
        [Ignore()]
        public string OperationRecord { get; set; }
        [Ignore()]
        public Guid ProductDeviceId { get; set; }
        [Ignore()]
        public HandleDto[] Handle { get; set; }
        [Ignore()]
        public int GroupMode { get; set; }
        [Ignore()]
        public string GroupModeName { get; set; }
        /// <summary>
        /// 一层-二层
        /// </summary>
        [Ignore()]
        public int BuildMode { get; set; }
        [Ignore()]
        public string BuildModeName { get; set; }
        /// <summary>
        /// 
        /// 任务模式，0=学习模式，1=考核模式
        /// </summary>
        [Ignore()]
        public int MissionMode { get; set; }
        [Ignore()]
        public string MissionModeName { get; set; }
        /// <summary>
        /// 每个小组唯一
        /// </summary>
        [Ignore()]
        public Guid GroupID { get; set; }
        [Ignore()]
        public Guid ClassID { get; set; }
        [Ignore()]
        public string SerialNumber { get; set; }
        [ColName("实训时长")]
        public string StrTimeLength { get; set; }
        public float TimeLength { get; set; }
        [Ignore()]
        public string ProductDeviceStatusName { get; set; }
        [Ignore()]
        public int ProductDeviceStatusId { get; set; }
        [Ignore()]
        public string UnityFile { get; set; }
        [Ignore()]
        public string HardwareSerialNumber { get; set; }

         [Ignore()]
        public string Avatar { get; set; }
        [Ignore()]
        ///专业名称
        public string SpecialName { get; set; }

        [Ignore()]
        public string ClassName { get; set; }

    }
}
