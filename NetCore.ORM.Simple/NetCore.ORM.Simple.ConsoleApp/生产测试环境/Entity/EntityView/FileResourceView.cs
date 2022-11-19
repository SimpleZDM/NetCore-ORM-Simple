using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class FileResourceView
    {
        public int Id { get; set; }
        public string FileUrl { get; set; }
        public string [] FileUrls { get; set; }
        public string Hashcode { get; set; }
        public Byte Filetype { get; set; }
        public Guid HardwareId { get; set; }
        public string FileTypeName { get; set; }
    }
}
