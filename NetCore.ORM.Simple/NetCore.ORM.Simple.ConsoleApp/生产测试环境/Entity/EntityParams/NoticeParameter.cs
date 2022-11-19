using MDT.VirtualSoftPlatform.Common;
using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class NoticeParameter:BaseParameter
    {
        public bool  IsRead { get; set; }
        public DateTime  dateStart { get; set; }
        public DateTime  dateEnd { get; set; }
        public Guid UserID { get; set; }
        public Guid CreateUserID { get; set; }
        public bool IsDelete { get; set; }

        public string level { get; set; }
    }
}
