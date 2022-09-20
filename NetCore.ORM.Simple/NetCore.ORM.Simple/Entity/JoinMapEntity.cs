using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 JoinMapEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/16 9:05:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class JoinMapEntity
    {
        public eJoinType JoinType { get { return joinType; } set { joinType = value; } }

        public JoinMapEntity(eJoinType joinType,bool map)
        {
            JoinType = joinType;
        }
        private eJoinType joinType;

    }
}
