using System;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionDto
    {
        public MissionDto()
        {
            GroupNumberOfPeople = 4;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
       
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
     
        public Guid CadId { get; set; }
      
        public int GroupMode { get; set; }
     
        public int MissionMode { get; set; }
    
        public DateTime StartTime { get; set; }
        public int Count { get; set; }
        
        public DateTime EndTime { get; set; }
     
        public int TargetMode { get; set; }
       
        public string Target { get; set; }
       
        public int Status { get; set; }
     
        public int MissionType { get; set; }

        public int BuildMode { get; set; }

        /// <summary>
        /// 任务的时间表
        /// </summary>
        public MissionTimeDetailDto[] missionTimes { get; set; }
        /// <summary>
        /// 分组人数
        /// </summary>
        public int GroupNumberOfPeople { get; set; }

        public int CourseId { get; set; }
        /// <summary>
        /// ，
        /// </summary>
        public string CourseIds { get; set; }

    }
}
