

using System;

namespace NetCore.ORM.Simple.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColNameAttribute:AbsAttribute
    {
        public ColNameAttribute(string name) : base(name)
        {

        }
    }
}
