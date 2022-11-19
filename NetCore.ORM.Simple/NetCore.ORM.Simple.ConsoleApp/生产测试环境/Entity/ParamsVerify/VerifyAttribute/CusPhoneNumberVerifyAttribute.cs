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
    /// 电话验证特性
    /// </summary>
    public class CusPhoneNumberVerifyAttribute:BaseVerifyAttribute
    {
        public CusPhoneNumberVerifyAttribute(string propName):base(propName)
        {
            this._errorMessage = "填写错误,请填写正确格式的电话号码!";
        }
        public override Tuple<string, bool> Verify(object o, PropertyInfo prop)
        {

            bool IsOk = false;
            object value = prop.GetValue(o);
            if (value != null)
            {
                Regex RegPhoneNumber = new Regex("^1[0-9]{10}$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
                Match m = RegPhoneNumber.Match(value.ToString());
                IsOk = m.Success;
            }
            return Tuple.Create(GetErrorMsg(), IsOk);
        }
    }
}
