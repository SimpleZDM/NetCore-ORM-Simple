using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserPlatformView
    {

        public string AgentName { get; set; }
        public string UrlAddress { get; set; }
        public string CompanyName { get; set; }
        
        public string IndustryName { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }

        public int LimitServiceTime { get; set; }
      
        public DateTime ExpireTime { get; set; }
      
        public int StatusCode { get; set; }
        public string StatusDesc { get; set; }

        public string LogoImg { get; set; }
      
        public string BackgroundImg { get; set; }
        public string PlaceDetail { get; set; }
    }
}
