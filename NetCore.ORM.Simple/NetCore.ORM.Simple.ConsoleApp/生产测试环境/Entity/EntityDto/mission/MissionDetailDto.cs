using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionDetailDto
    {
        public MissionDetailDto()
        {

        }

        public Guid ProductDeviceId { get; set; }
       
        public int MissionRole { get; set; }
        public int StatusId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Score { get; set; }
        public string OperationRecord { get; set; }
        public HandleDto [] Handle { get; set; } 
    }
}
