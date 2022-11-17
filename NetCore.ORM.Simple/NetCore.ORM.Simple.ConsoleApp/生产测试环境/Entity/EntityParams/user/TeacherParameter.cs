using MDT.VirtualSoftPlatform.Common;
using System;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class TeacherParameter : BaseParameter
    {
        public string SearchTerm { get; set; }

        public string institutionID { get; set; }

        public string SchoolIds { get; set; }
    }
}
