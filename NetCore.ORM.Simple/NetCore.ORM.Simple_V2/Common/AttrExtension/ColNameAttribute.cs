

using System;

namespace NetCore.ORM.Simple.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColNameAttribute:AbsAttribute,IColumn
    {
        /// <summary>
        /// 主键
        /// </summary>
        public bool Key { get { return key; } set { key = value; } }

        /// <summary>
        /// 自增主键
        /// </summary>
        public bool AutoIncrease { get { return autoIncrease; } set { autoIncrease = value; } }
        public bool Ignore { get { return ignore; } set { ignore = value; } }
        public ColNameAttribute(string name="",bool key=false,bool autoIncrease=false,bool ignore=false) : base(name)
        {
            Key = key;
            AutoIncrease = autoIncrease;
            Ignore = ignore;
        }

        private bool key;
        private bool autoIncrease;
        private bool ignore;

    }
}
