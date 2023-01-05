using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class FileResourceParameter:BaseParameter
    {
        public Byte FileType { get; set; }
        public int Id { get; set; }
    }
}
