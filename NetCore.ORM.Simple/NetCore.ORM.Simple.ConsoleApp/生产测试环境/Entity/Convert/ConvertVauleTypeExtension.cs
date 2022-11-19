using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    public static class ConvertVauleTypeExtension
    {
        public static Guid ConvertToGuid(this string str )
        {
            if (Guid.TryParse(str,out Guid guid))
            {
                return guid;
            }
            return Guid.Empty;
        }
        public static Guid[] ConvertToGuids(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            if (str.Contains(','))
            {
               return str.Split(',').Select(s=>s.ConvertToGuid()).ToArray();
               
            }
            return new Guid[] {str.ConvertToGuid()};
        }
    }
}
