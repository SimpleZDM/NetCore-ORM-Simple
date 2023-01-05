using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionRecordCreateDto
    {
        public Guid MissionId { get; set; } 
        public Guid UserId { get; set; }
        public Guid TeamGroupId { get; set; }
        public string Record { get; set; }
    }
}
