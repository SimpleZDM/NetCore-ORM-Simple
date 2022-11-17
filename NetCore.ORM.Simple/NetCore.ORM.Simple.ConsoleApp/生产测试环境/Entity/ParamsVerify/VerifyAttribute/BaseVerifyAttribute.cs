using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseVerifyAttribute:Attribute
    {
        protected virtual string _errorMessage { get; set; }
        protected virtual string _propName { get; set; }
        public BaseVerifyAttribute(string propName)
        {
            _propName = propName;
        }
        protected virtual string GetErrorMsg()
        {
            return $"{_propName}{_errorMessage}";
        }

        /// <summary>
        /// 返回元祖 item_1-信息 item_2-验证结果
        /// </summary>
        /// <param name="o"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public virtual Tuple<string,bool> Verify(object o, PropertyInfo prop)
        {
            return Tuple.Create(GetErrorMsg(),true);
        }
    }
}
