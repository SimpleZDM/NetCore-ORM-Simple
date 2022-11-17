using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class AgentPlatformCreateOrUpdateDto:IParamsVerify
    {
        public AgentPlatformCreateOrUpdateDto()
        {
            GanderID = (int)eGender.man;
        }
        [CusRequired("平台名称")]
        public string AgentName { get; set; }

        [CusRequired("公司名称")]
        public string UrlAddress { get; set; }

        [CusRequired("公司名称")]
        public string CompanyName { get; set; }
      
        public int IndustryID { get; set; }
        public int ProvinceID { get; set; }
        public int CityID { get; set; }
        public int AreaID { get; set; }
      
        public int LimitServiceTime { get; set; }
        /// <summary>
        /// 
       /// </summary>
        public DateTime ExpireTime { get; set; }

       [CusPhoneNumberVerify("电话")]
        public string PhoneNumber { get; set; }

        [CusEmailVerify("邮箱")]
        public string Email { get; set; }

        [CusRequired("身份证")]
        public string IDCard { get; set; }
     
        public string LogoImg { get; set; }
      
        public string BackgroundImg { get; set; }

        [CusRequired("详细地址")]
        public string PlaceDetail { get; set; }
        [CusRequired("用户名称")]
        public string DisplayName { get; set; }
        public int GanderID { get; set; }
    }
}
