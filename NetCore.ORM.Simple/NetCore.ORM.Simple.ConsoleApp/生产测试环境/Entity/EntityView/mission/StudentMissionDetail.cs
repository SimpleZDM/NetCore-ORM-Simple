using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 StudentMissionDetail
 * 开发人员：-nhy
 * 创建时间：2022/4/21 11:19:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentMissionDetail
    {
        [Ignore()]
        public Guid MissionId { get; set; }
        [Ignore()]
        public Guid MissionDetailId { get; set; }
        //[Ignore()]
        public string TeacherName { get; set; }
       // [Ignore()]
        public string MissionName { get; set; }
        //[ColName("分数")]
        public decimal Score { get; set; }
        [Ignore()]
        public int GroupMode { get; set; }
        //[Ignore()]
        public string GroupModeName { get; set; }

        //[Ignore()]
        public int BuildMode { get; set; }
        public string BuildModeName { get; set; }
        /// <summary>
        /// 任务模式，0=学习模式，1=考核模式
        /// </summary>
        [Ignore()]
        public int MissionMode { get; set; }
        //[Ignore()]
        public string MissionModeName { get; set; }
        [Ignore()]
        public int Status { get; set; }
        //[Ignore()]
        //[ColName("状态")]
        public string StatusName { get; set; }
        //[ColName("开始时间")]
        public DateTime StartTime { get; set; }
        //[ColName("结束时间")]
        public DateTime EndTime { get; set; }
        [Ignore()]
        public KeyValueView<decimal> [] Roles { get; set; }

        //[ColName("项目管理员")]
        public string RoleAdmin { get; set; }
        [Ignore()]
        public decimal RoleAdminScore { get; set; }
        [Ignore()]
        public decimal ComponentBuildingScore { get; set; }// = 1,//构建

        //[ColName("构件工艺员")]
        public string ComponentBuilding { get; set; }// = 1,//构建
        [Ignore()]
        public decimal ComponentAssembleScore { get; set; }//= 3,//装配员

        //[ColName("构建装配员")]
        public string ComponentAssemble { get; set; }//= 3,//装配员
        [Ignore()]
        public decimal ProcessOfConstructionScore { get; set; }//= 3,//施工员
        //[ColName("土建施工员")]
        public string ProcessOfConstruction { get; set; }//= 3,//施工员

        //[ColName("使用时长")]
        public float UserTime { get; set; }//使用时长

        //[ColName("实训设备")]
        public string ProductDeviceName { get; set; }
        [Ignore]
        public Guid ProductDeviceId { get; set; }
        [Ignore]
        public int ProductDeviceStatusId { get; set; }
        [Ignore]
        public string ProductDeviceIdStatusName { get; set; }

        [Ignore]
        public Guid GroupId { get; set; }
        /// <summary>
        /// 构建总数
        /// </summary>
        [Ignore]
        public int BuilderCount { get; set; }

        /// <summary>
        /// 归位数量
        /// </summary>
        [Ignore]
        public int OnlineBuilderCount { get; set; }
        [Ignore]
        public string unityFile { get; set; }
        [Ignore]
        public string HardwareSerialNumber { get; set; }
        /// <summary>
        /// 当前分数
        /// </summary>
        public decimal CurrentScore { get; set; }
        /// <summary>
        /// 当前使用时间
        /// </summary>
        public float CurrentUserTime { get; set; }
        /// <summary>
        /// 当前角色
        /// </summary>
        public string CurrentUserRole { get; set; }
        [Ignore()]
        /// <summary>
        /// 
        /// </summary>
        public int CurrentUserRoleId { get; set; }
        /// <summary>
        /// 当前用户名称
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentCode { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string SpecialtyName { get; set; }
        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; }
        public string UserTimeStr { get; set; }


    }
}
