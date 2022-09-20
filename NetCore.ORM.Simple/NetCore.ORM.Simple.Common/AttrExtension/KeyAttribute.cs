using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.Common.EntityArrt
 * 接口名称 KeyAttribute
 * 开发人员：-nhy
 * 创建时间：2022/9/14 16:31:35
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute:Attribute
    {
        public bool AutoIncrease { get { return autoIncrease; } private set { autoIncrease = value; } }
        public KeyAttribute(bool AutoIncrease)
        {
            autoIncrease = AutoIncrease;
        }
        
        private bool autoIncrease;

       
    }
}
