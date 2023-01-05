using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class KeyValueView<T>
    {

        public string Name { get; set; }
        public T Value { get; set; }
        public string Description { get; set; }
        public string NickName { get; set; }
        public int Code { get; set; }
        public string Ext1 { get; set; }

    }
}
