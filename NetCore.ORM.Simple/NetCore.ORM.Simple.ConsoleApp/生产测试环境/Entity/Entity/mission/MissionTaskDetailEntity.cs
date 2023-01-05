using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("missiontaskdetailtable")]
    public class MissionTaskDetailEntity:RecordEntity<Guid>
    {
        public MissionTaskDetailEntity()
        {
            ID = Guid.NewGuid();
        }
        public Guid UserId { get; set; }
        public Guid MissionId { get; set; }
        public string TaskId { get; set; }

        public string TaskName { get; set; }
        /// <summary>
        /// 任务类型：0=操作时长，1=堆放时长，2=流程任务
        /// </summary>

        public int TaskType { get; set; }
        public int Player { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public decimal TotalScore { get; set; }
        /// <summary>
        /// 是否正确
        /// </summary>
        public bool IsRight { get; set; }
        /// <summary>
        /// 用户数据
        /// </summary>
        public string UserData { get; set; }
        /// <summary>
        /// 错误点，json字符串
        /// </summary>
        public string ErrorInfo { get; set; }

    }
}
