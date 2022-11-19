using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("fileResourcetable")]

    public class FileResourceEntity:RecordEntity<int>
    {
        public FileResourceEntity()
        {

        }

        public string FileUrl { get; set; }
        public string Hashcode { get; set; }
        public Byte Filetype { get; set; }
        public Guid HardwareId { get; set; }
    }
}
