using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class TeacherAndUserAndSchool
    {
        public TeacherEntity teacher { get; set; }
        public UserEntity user { get; set; }
        public OrganizationEntity organization { get; set; }
        public OrganizationEntity special { get; set; }
        public DictionaryEntity gender { get; set; }
    }
}
