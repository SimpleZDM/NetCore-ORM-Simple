using MDT.VirtualSoftPlatform.Common;
using System;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionCreateDto:IParamsVerify
    {
        public MissionCreateDto()
        {
            GroupCount = 4;
        }
        public string MissionName { get; set; }
       
        public Guid ProductId { get; set; }
     
        public Guid CadId { get; set; }
      
        public int GroupMode { get; set; }
       
        public int MissionMode { get; set; }
    
        public DateTime StartTime { get; set; }
    
        public DateTime EndTime { get; set; }
     
        public int TargetModeId { get; set; }
      
        public string TargetId { get; set; }

        public int BuildMode { get; set; }
        
        /// <summary>
        /// 任务类型0-综合实训 1-课程实训
        /// </summary>
        public int MissionType { get; set; }
        /// <summary>
        /// 默认四个人
        /// </summary>
        public int GroupCount { get; set; }
        /// <summary>
        /// 时间表
        /// </summary>
        public MissionTimeDetailDto [] MissionTimes { get; set; }

        /// <summary>
        /// 课程实训的编号
        /// </summary>

        public string CourseIds { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public string CourseId { get; set; }
    }
}
