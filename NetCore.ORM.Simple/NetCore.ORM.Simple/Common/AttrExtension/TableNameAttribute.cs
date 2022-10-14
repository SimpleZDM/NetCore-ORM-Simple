
using System;

namespace NetCore.ORM.Simple.Common
{
    /// <summary>
    /// 可以标记类的别名
    /// </summary>
    /// 

    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute:AbsAttribute,IName
    {
        public TableNameAttribute(string name):base(name)
        {

        }
    }
}
