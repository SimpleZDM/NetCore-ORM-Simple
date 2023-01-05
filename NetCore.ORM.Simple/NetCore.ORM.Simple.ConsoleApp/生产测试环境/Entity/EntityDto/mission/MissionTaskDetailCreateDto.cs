using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 MissionTaskDetailCreateDto
 * 开发人员：-nhy
 * 创建时间：2022/5/16 16:53:15
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionTaskDetailCreateDto
    {
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
    }
}
