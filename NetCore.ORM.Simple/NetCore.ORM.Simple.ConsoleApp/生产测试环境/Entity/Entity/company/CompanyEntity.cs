using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 PlatformOpenKeyEntity
 * 开发人员：-nhy
 * 创建时间：2022/6/27 17:55:40
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 平台公钥实体
    /// </summary>
    public class CompanyEntity:RecordEntity<Guid>
    {
        public CompanyEntity()
        {

        }
        /// <summary>
        /// 描述一下公钥的
        /// </summary>
        public string Descritpion { get { return descritpion; } set { descritpion = value; } }
        /// <summary>
        /// 开放的key
        /// </summary>
        public string Key { get { return key; } set { key = value; } }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string SpecificLoaction { get { return specificLoaction; } set { specificLoaction= value; } }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }

        /// <summary>
        /// 责任人
        /// </summary>
        public string PRDisplay { get { return pRDisplay; } set { pRDisplay = value; } }

        /// <summary>
        /// 公司类型-生产公司
        /// </summary>
        public int CompanyType { get { return companyType; } set { companyType = value; } }

        private string descritpion;
        private string key;
        private string displayName;
        private string specificLoaction;
        private string phoneNumber;
        private string pRDisplay;
        private int companyType;
    }
}
