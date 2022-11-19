using System;
using System.Reflection;

namespace MDT.VirtualSoftPlatform.Common
{
   /// <summary>
   /// 必须填写验证特性
   /// </summary>
    public class CusRequiredAttribute : BaseVerifyAttribute
    {
        public CusRequiredAttribute(string propName):base(propName)
        {
            this._errorMessage = "为必填项!";
        }
        protected override string GetErrorMsg()
        {
            return base.GetErrorMsg();
        }
        public override Tuple<string, bool> Verify(object o,PropertyInfo prop)
        {

            bool IsOk = true;
            if (prop.PropertyType==typeof(string) || prop.PropertyType==typeof(Guid))
            {
                object value=prop.GetValue(o);
                
                if (value == null||string.IsNullOrEmpty(value.ToString())==true)
                {
                    IsOk = false;
                }
            }

            if(prop.PropertyType == typeof(int) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(float) || prop.PropertyType == typeof(decimal))
            {
                object value = prop.GetValue(o);

                if ((int)value<=0)
                {
                    IsOk = false;
                }
            }
            return Tuple.Create(GetErrorMsg(),IsOk);
        }
    }
}
