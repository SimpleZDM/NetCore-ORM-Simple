using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
   public class StudentAddorUpdateDto:IParamsVerify
    {
        public Guid DepartmentID { get; set; }

        public Guid SpecialtyID { get; set; }

        public Guid ClassID { get; set; }

       [CusRequired("学生名称")]
        public string StudentName { get; set; }

        public int GenderID { get; set; }

        [CusRequired("学生号码")]
       
        public string StudentCode { get; set; }

       [CusEmailVerify("学生邮箱")]
        public string Email { get; set; }
        [CusPhoneNumberVerify("学生电话")]
        public string PhoneNumber { get; set; }
        public string IDCard { get; set; }
    }
}
