using System;

namespace NetCore.ORM.Simple.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// 
   
    public abstract class AbsAttribute:Attribute
    {
        private string _name { get; set; }
        public AbsAttribute(string name)
        {
            this._name = name;
        }
        public string GetName()
        {
            return _name;
        }
    }
}
