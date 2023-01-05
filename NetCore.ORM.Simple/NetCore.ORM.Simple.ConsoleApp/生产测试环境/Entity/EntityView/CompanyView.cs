using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView
 * 接口名称 PlatformOpenKeyView
 * 开发人员：-nhy
 * 创建时间：2022/6/28 9:30:34
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class CompanyView
    {
        public CompanyView()
        {
            Id=Guid.NewGuid();
        }
        
        public Guid Id { get { return id; } set { id = value; } }

        /// <summary>
        /// 描述一下公钥的
        /// </summary>
        public string Descritpion { get { return descritpion; } set { descritpion = value; } }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string SpecificLoaction { get { return specificLoaction; } set { specificLoaction = value; } }

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
        /// <summary>
        /// 公司类型名称
        /// </summary>
        public string CompanyTypeName { get { return companyTypeName; } set { companyTypeName = value; } }

        private string descritpion;
        private string displayName;
        private string specificLoaction;
        private string phoneNumber;
        private string pRDisplay;
        private string companyTypeName;
        private int companyType;
        private Guid id;
    }
}
