using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.Entity.user
 * 接口名称 UserCompanyEntity
 * 开发人员：-nhy
 * 创建时间：2022/8/4 10:35:03
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserCompanyEntity:BaseEntity<int>
    {
        public UserCompanyEntity()
        {
            UserId = Guid.Empty;
            CompanyId=Guid.Empty;
        }
        public Guid UserId { get { return userId; } set { userId = value; } }
        public Guid CompanyId { get { return companyId; } set { companyId = value; } }

        private Guid userId;
        private Guid companyId;
    }
}
