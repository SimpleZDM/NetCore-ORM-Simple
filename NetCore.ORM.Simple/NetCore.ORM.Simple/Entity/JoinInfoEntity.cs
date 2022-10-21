using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 JoinInfoEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/19 11:38:56
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class JoinInfoEntity
    {
        public JoinMapEntity[] JoinMaps { get { return _joinMaps; } set { _joinMaps=value; } }
        public JoinInfoEntity(params JoinMapEntity[] joinMaps)
        {
            if (Check.IsNull(joinMaps))
            {
                throw new ArgumentNullException(nameof(joinMaps));
            }
            JoinMaps = joinMaps;
        }
        private JoinMapEntity[] _joinMaps;
    }
}
