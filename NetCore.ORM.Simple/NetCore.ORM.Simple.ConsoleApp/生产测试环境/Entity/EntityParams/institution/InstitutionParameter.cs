using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
   public class InstitutionParameter : BaseParameter
    {
        public InstitutionParameter()
        {
            InsitutionId = Guid.Empty;
        }
        public string InstName { get; set; }

        public string AgentPlatformID { get; set; }
        public Guid InsitutionId { get; set; }
        public string Ids { get; set; }
    }
}
