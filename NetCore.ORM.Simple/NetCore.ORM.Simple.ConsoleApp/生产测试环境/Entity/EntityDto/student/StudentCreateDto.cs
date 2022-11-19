using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentCreateDto
    {
        public StudentCreateDto()
        {
            DepartmentID=Guid.NewGuid();
            SpecialtyID = Guid.NewGuid();
            ClassID = Guid.NewGuid();
        }

        public Guid DepartmentID { get; set; }

        public Guid SpecialtyID { get; set; }

        public Guid ClassID { get; set; }
        //public int GenderID { get; set; }


    }
}
