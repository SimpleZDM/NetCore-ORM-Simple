using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentParameter : BaseParameter
    {
        public StudentParameter()
        {
            InstitutionID = Guid.Empty;
            DepartmentID = Guid.Empty;
            SpecialtyID = Guid.Empty;
            ClassID = Guid.Empty;
        }
        public Guid InstitutionID { get; set; }

        public string[] SclassIDs { get; set; }

        public string SearchTerm { get; set; }
        public Guid ClassID { get; set; }
        public Guid UserId { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid SpecialtyID { get; set; }
    }
}
