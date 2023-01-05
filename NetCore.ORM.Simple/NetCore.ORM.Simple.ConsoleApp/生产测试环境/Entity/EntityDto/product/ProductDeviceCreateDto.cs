using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductDeviceCreateDto
    {
        public ProductDeviceCreateDto()
        {
            ProductDeviceHardwaresDto =new ProductDeviceHardwareDto[0];
            StatusId = -1;
            LimitServiceTime = -1;
            UsedUsers = -1;
            ExpireTime = DateTime.MinValue;
            ActivedTime = DateTime.MinValue;
        }

        public string DisplayName { get; set; }
        public Guid ProductId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid APId { get; set; }
        public Guid InstitutionId { get; set; }
        public DateTime ActivedTime { get; set; }
      
        public int UsedUsers { get; set; }
      
        public int UsedTime { get; set; }
      
        public DateTime ExpireTime { get; set; }
     
        public int LimitServiceTime { get; set; }
        public string SerialNumber { get; set; }
        public int StatusId { get; set; }

        public ProductDeviceHardwareDto [] ProductDeviceHardwaresDto { get; set; }
    }
}
