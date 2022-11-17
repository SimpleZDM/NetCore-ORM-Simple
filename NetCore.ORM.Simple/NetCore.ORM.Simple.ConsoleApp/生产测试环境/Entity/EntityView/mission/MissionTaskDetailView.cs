using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView
 * 接口名称 MissionTaskDetailView
 * 开发人员：-nhy
 * 创建时间：2022/5/16 17:14:15
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionTaskDetailView
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public string TaskId { get; set; }
        [ColName("任务名称")]
        public string TaskName { get; set; }
        /// <summary>
        /// 任务类型：0=操作时长，1=堆放时长，2=流程任务
        /// </summary>
        public int TaskType { get; set; }
        public string TaskTypeName { get; set; }
        public int Player { get; set; }
        public string PlayerName { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        /// 
        [ColName("得分")]
        public decimal Score { get; set; }
        /// <summary>
        ///总分
        /// </summary> 
        [ColName("任务总分")]
        public decimal TotalScore { get; set; }
        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsRight { get; set; }

        public string UserData { get; set; }

        /// <summary>
        /// 错误点，json字符串
        /// </summary>
        public string ErrorInfo { get; set; }
        public bool TaskStatus { get; set; }
        [ColName("任务状态")]
        public string TaskStatusName { get; set; }
        public int FloorCount { get; set; }
        public float FirstFloorScore{get;set;}
        public float SecondFloorScore{get;set;}


    }
}
