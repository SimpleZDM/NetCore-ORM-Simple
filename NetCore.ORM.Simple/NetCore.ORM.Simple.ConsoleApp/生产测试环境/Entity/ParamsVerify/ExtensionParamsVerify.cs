using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    public static class ExtensionParamsVerify
    {
        public static Tuple<string, bool> Verify<T>(this T t) where T : IParamsVerify
        {
            Type type = typeof(T);
            Tuple<string, bool> tupleMsg = Tuple.Create(string.Empty, true);
            foreach (var prop in type.GetProperties())
            {
                foreach (var attr in prop.GetCustomAttributes(typeof(BaseVerifyAttribute),true).ToArray())
                {
                    if (attr is BaseVerifyAttribute)
                    {
                        BaseVerifyAttribute verify = (BaseVerifyAttribute)attr;
                        tupleMsg = verify.Verify(t, prop);
                        if (tupleMsg.Item2 == false)
                        {
                            return tupleMsg;
                        }
                    }
                }
            }
            return tupleMsg;
        }
    }
}
