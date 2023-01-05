using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 MemberEntity
 * 开发人员：-nhy
 * 创建时间：2022/10/25 17:56:38
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 收集表达式中的变量
    /// </summary>
    public class MemberEntity
    {
        public MemberEntity() 
        {
            OParams = new List<object>();
        }
        public MemberInfo Member { get { return member; } set { member = value; } }
        public List<object> OParams { get { return oParams; } set { oParams = value; } }
        public MemberInfo KeyMember { get { return keyMember; } set { keyMember = value; } }

        private MemberInfo member;
        private List<object> oParams;
        private MemberInfo keyMember;
    }
}
