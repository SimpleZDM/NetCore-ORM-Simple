using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    /// <summary>
    /// 邮箱验证特性
    /// </summary>
    public class CusEmailVerifyAttribute : BaseVerifyAttribute
    {
        public CusEmailVerifyAttribute(string propName) : base(propName)
        {
            this._errorMessage = "填写错误,请填写正确格式的邮箱!";
        }
        public override Tuple<string, bool> Verify(object o, PropertyInfo prop)
        {
            bool IsOk = false;
            object value = prop.GetValue(o);
            if (value != null)
            {
                Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
                Match m = RegEmail.Match(value.ToString());
                IsOk = m.Success;
            }

            return Tuple.Create(GetErrorMsg(),IsOk);
        }
    }
}
