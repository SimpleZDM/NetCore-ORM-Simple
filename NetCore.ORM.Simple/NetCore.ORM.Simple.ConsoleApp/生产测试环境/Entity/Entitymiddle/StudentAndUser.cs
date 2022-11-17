using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentAndUser
    {
        public StudentEntity student { get; set; }
        public UserEntity user { get; set; }
        public DictionaryEntity gender { get; set; }
        public OrganizationEntity department { get; set; }
        public OrganizationEntity special { get; set; }
        public OrganizationEntity sclass { get; set; }
    }
}
