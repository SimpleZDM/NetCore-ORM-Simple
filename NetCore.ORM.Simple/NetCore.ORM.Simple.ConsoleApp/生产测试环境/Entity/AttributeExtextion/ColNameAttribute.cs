

using System;

namespace MDT.VirtualSoftPlatform.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColNameAttribute:AbsAttribute
    {
        public ColNameAttribute(string name) : base(name)
        {

        }
    }
}
