using System;

namespace MDT.VirtualSoftPlatform.Common
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
