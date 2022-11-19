using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionParameter:BaseParameter
    {
        public MissionParameter()
        {
            MissionMode = -1;
            GroupMode = -1;
            MissionType =(int)eMissionType.synthesize;
            SpecialId=Guid.Empty;
        }
        public int MissionMode { get; set; } = -1;
        public int GroupMode { get; set; } = -1;

        public Guid TeacherUserID { get; set; }
        public Guid StudentUserID { get; set; }
        public Guid ClassID { get; set; }
        public int [] StatusTypes { get; set; }
        public string StatusType { get; set; }
        public string MissionId { get; set; }

        public int MissionType { get; set; }

        public Guid SpecialId { get; set; }

        public Guid InstitutionId { get; set; }
        public Guid SchoolId { get; set; }

        public int OrderByTimeType { get; set; }

        public int TemplateType { get; set; }
    }
}
