using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    public interface IConvertTree<T>
    {
        public string ID { get; set; }
        public string ParentID { get; set; }
        public List<T> Children { get; set; }
    }
}
