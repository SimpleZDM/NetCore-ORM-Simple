using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionScoreModel
    {
        public int GroupUsedTime { get; set; }
        public decimal GroupScore { get; set; }
        public int DFUsedTime { get; set; }
        public decimal DFScore { get; set; }
        public List<RoleScore> RoleScores { get; set; } = new List<RoleScore>();
    }
}
