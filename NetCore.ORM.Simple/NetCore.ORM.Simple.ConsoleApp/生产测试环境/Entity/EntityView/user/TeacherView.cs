using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class TeacherView
    {
        public string SchoolID { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string SchoolName { get; set; }
        public Guid TeacherID { get; set; }
        public Guid SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
        public string ClassIds { get; set; }

        public string IDCard { get; set; }

        public Guid UserID { get; set; }
        public bool IsBindOpenID { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }

    }
}
