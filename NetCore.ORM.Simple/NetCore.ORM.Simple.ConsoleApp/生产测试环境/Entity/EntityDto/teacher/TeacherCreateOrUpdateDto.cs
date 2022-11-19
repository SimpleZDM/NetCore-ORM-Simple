using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class TeacherCreateOrUpdateDto:IParamsVerify
    {
        public TeacherCreateOrUpdateDto()
        {
            GenderID = (int)eGender.man;
        }
        [CusRequired("老师名称")]
        public string TeacherName { get; set; }
        public int GenderID { get; set; }
        public Guid DepartmentID { get; set; }
        public Guid SpecialtyID { get; set; }
        [CusRequired("班级号")]
        public string ClassIds { get; set; }

        [CusRequired("身份证号")]
        public string IdCard { get; set; }

        [CusEmailVerify("老师邮箱")]
        public string Email { get; set; }
        [CusPhoneNumberVerify("老师电话")]
        public string PhoneNumber { get; set; }
        public List<UserMenuDto> Menus{ get; set; }

    }
}
