using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
   public class InstitutionCreateOrUpdateDto:IParamsVerify
    {
        public InstitutionCreateOrUpdateDto()
        {
            agentPlatformId = Guid.Empty;
            GenderID =(int)eGender.man;//
        }
        [CusRequired("机构名称")]
        public string InstName { get; set; }
        [CusRequired("机构管理员名称")]
        public string DisplayName { get; set; }
        public int IndustryID { get; set; }

        public int AreaID { get; set; }
        public int CityID { get; set; }
        public int ProvinceID { get; set; }

        [CusPhoneNumberVerify("电话")]
        public string PhoneNumber { get; set; }

        [CusEmailVerify("邮箱")]
        public string Email { get; set; }
        [CusRequired("身份证")]
        public string IDCard { get; set; }
        [CusRequired("机构详细地址")]
        public string PlaceDetail { get; set; }
        public int GenderID { get; set; }

        public Guid agentPlatformId { get; set; }
    }
}
