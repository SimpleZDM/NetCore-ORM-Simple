using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("AgentPlatformTable")]
    public class AgentPlatformEntity:RecordEntity<Guid>
    {
        public AgentPlatformEntity()
        {
            ID = Guid.NewGuid();
        }
        [MaxLength(50)]
        public string AgentName { get; set; }
        [MaxLength(100)]
        public string UrlAddress { get; set; }
        [MaxLength(50)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [MaxLength(50)]
        public int IndustryID { get; set; }
        [MaxLength(100)]
        public int ProvinceID { get; set; }
        public int CityID { get; set; }
        public int AreaID { get; set; }
        /// <summary>
        /// 服务时长，9999为永久
        /// </summary>
        public int LimitServiceTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 状态，冻结
        /// </summary>
        public int StatusID { get; set; }
        
        /// <summary>
        /// 管理员姓名，对应user-displayname
        /// </summary>
        [MaxLength(50)]
        public Guid AdminUserID { get; set; }
        /// <summary>
        /// 手机号码（登录）
        /// </summary>
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 邮箱（登录）
        /// </summary>
        [MaxLength(50, ErrorMessage = "邮箱长度不能大于50")]

        public string Email { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDCard { get; set; }
        /// <summary>
        /// Logo图
        /// </summary>
        public string LogoImg { get; set; }
        /// <summary>
        /// 背景图
        /// </summary>
        public string BackgroundImg { get; set; }
        public string PlaceDetail { get; set; }
    }
}
