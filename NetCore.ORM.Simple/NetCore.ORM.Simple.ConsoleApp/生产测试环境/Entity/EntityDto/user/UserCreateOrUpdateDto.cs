using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;



namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserCreateOrUpdatDto :IParamsVerify
    {
        public UserCreateOrUpdatDto()
        {
            CompanyId=Guid.Empty;
        }
        [CusPhoneNumberVerify("电话号码")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱（登录）
        /// </summary>
        [CusEmailVerify("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [CusRequired("昵称")]
        public string DisplayName { get; set; }

        public string Avatar { get; set; }
        public Guid CompanyId { get; set; }
        public List<UserMenuDto> userMenus { get; set; }
    }
}
