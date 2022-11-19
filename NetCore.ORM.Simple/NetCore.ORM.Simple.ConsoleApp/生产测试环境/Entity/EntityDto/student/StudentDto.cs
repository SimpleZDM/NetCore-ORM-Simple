using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentDto:IParamsVerify
    {
        [CusRequired("性别")]
        [ColName("性别")]
        public string Gender { get; set; }


        [CusRequired("学生名称")]
        [ColName("学生名称")]
        public string StudentName { get; set; }


        [CusEmailVerify("邮箱")]
        [ColName("邮箱")]
        public string Email { get; set; }
        [ColName("电话")]
        [CusPhoneNumberVerify("电话")]
        public string PhoneNumber { get; set; }

        [CusRequired("学号")]
        [ColName("学号")]
        public string StudentCode { get; set; }

        [CusRequired("身份证号")]
        [ColName("身份证号")]
        public string IDCard { get; set; }
    }
}
