using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class HardwareView
    {
        public HardwareView()
        {
            ParentId=Guid.Empty;
        }
        public string ID { get; set; }
        public string ProductDeviceID { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string SerialNumber { get; set; }
       
        public int HardwareType { get; set; }
        public string TypeName { get; set; }
        public string Remark { get; set; }
        public bool IsNew { get; set; }
        public string CheckUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string DisplayName { get; set; }
        public string StatusName { get; set; }
        public int Status { get; set; }
        public int FixStatus { get; set; }
        public string FixStatusName { get; set; }
        public string ProductName { get; set; }
        public Guid ParentId { get; set; }
    }
}
