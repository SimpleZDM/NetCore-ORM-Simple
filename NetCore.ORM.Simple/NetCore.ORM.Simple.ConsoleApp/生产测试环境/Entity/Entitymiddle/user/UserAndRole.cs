using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserAndRole
    {
        public UserEntity user { get; set; }
        public RoleEntity role { get; set; }

        public DictionaryEntity gender { get; set; }
        public AgentPlatformEntity agentplatform { get; set; }
        public OrganizationEntity department { get; set; }
        public OrganizationEntity specialty { get; set; }
        public InstitutionEntity institution { get; set; }

        public OrganizationEntity sclass { get; set; }
        public StudentEntity student { get; set; }

    }
}
