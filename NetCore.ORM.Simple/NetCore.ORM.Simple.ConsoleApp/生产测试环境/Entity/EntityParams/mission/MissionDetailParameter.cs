using System;
using MDT.VirtualSoftPlatform.Common;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionDetailParameter:BaseParameter
    {
        public MissionDetailParameter()
        {
            Status = -1;
            MissionRole = -1;
            MissionID = Guid.Empty;
            UserID= Guid.Empty;
            ProductDeviceID = Guid.Empty;
            ProductID = Guid.Empty;
            ClassID = Guid.Empty;
            OrderByCreateTime = 1;
            IsOwnOrder = false;

        }
        public Guid MissionID { get; set; }
        public Guid ProductDeviceID { get; set; }
        public Guid ProductID { get; set; }
        public  Guid UserID { get; set; }
        public int Status { get; set; }
        public Guid ClassID { get; set; }
        public int Year { get; set; }

        public int MissionRole { get; set; }

        public int TimeId { get; set; }

        public Guid TeacherId { get; set; }
        public Guid GroupId { get; set; }
        public int TemplateType { get; set; }
        public Guid SpecialtyID { get; set; }
        /// <summary>
        /// 1升序 2-降序
        /// </summary>
        public int OrderByCreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOwnOrder { get; set; }
    }
}
