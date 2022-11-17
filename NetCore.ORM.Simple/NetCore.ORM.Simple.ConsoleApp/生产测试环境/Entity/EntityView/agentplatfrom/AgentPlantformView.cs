using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class AgentPlatformView
    {
        public string ID { get; set; }
        public string AgentName { get; set; }
        public string UrlAddress { get; set; }
        public string CompanyName { get; set; }
        
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int AreaID { get; set; }
        public string AreaName { get; set; }

        public int LimitServiceTime { get; set; }
      
        public DateTime ExpireTime { get; set; }

        public string ExpireDay { get; set; }
      
        public int StatusCode { get; set; }
        public string StatusDesc { get; set; }

      
        public string UsreName { get; set; }

        public string PhoneNumber { get; set; }
    
        public string Email { get; set; }
  
        public string IDCard { get; set; }
      
        public string LogoImg { get; set; }
      
        public string BackgroundImg { get; set; }
        public string PlaceDetail { get; set; }

        public DateTime CreateTime { get; set; }
        public int StatusID { get; set; }
    }
}
