using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.agentplatfrom
 * 接口名称 GroupPlatform
 * 开发人员：-nhy
 * 创建时间：2022/3/28 16:38:09
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class GroupPlatform
    {
        #region Field

        #endregion

        #region Constructors and Property

        #endregion

        #region Public Methods and Operators

        #endregion

        public int Total { get; set; }
        [ColName("过期")]
        public int PastCount { get; set; }

        [ColName("过期")]

        public int FreezeCount { get; set; }
        
        public int InstitutionTotal { get; set; }
    }
}
