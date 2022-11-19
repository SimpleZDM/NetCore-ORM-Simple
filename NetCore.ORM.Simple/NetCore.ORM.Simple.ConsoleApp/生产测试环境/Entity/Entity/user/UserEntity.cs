using MDT.VirtualSoftPlatform.Common;
using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("usertable")]
    [TableName("usertable")]
    public class UserEntity : RecordEntity<Guid>
    {

        public UserEntity()
        {
            //Security.CreateHash(, out byte[] hash, out byte[] salt);
           // PasswordHash = hash;
            //PasswordSalt = salt;

            AgentPlatformID = Guid.Empty;
            InstitutionID = Guid.Empty;
            DepartmentID = Guid.Empty;
            SpecialtyID = Guid.Empty;
            SClassID = Guid.Empty;
            ID = Guid.NewGuid();
            OnlineMax = 1;
            TokenEffectiveTime = 14400;//默认四个小时过期
            GenderID =(int)eGender.man;
        }
        /// <summary>
        /// 头像URL
        /// </summary>
        public string Avatar { get; set; }

        public Guid RoleID { get; set; }
        /// <summary>
        /// 微信登录openId
        /// </summary>
        [MaxLength(50)]
        public string OpenID { get; set; }

        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string IDCard { get; set; }



        public string DisplayName { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        public int StatusID { get; set; }

        public Guid AgentPlatformID { get; set; }

        public Guid InstitutionID { get; set; }

        public Guid DepartmentID { get; set; }

        public Guid SpecialtyID { get; set; }

        public Guid SClassID { get; set; }

        public string UserName { get; set; }
        public int GenderID { get; set; }
        public int OnlineMax { get; set; }
        public int TokenEffectiveTime { get; set; }
    }
}
