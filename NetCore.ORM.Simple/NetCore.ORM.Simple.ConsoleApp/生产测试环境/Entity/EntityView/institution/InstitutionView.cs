using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class InstitutionView
    {
        public Guid ID { get; set; }
        public Guid APID { get; set; }
        public string APIdName { get; set; }
        public string InstName { get; set; }
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }

        public int AreaID { get; set; }
        public string AreaName { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public Guid AdminUserID { get; set; }
        public string AdminUserName { get; set; }
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string IDCard { get; set; }
        public string PlaceDetail { get; set; }
        public string Avatar { get; set; }

        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public int ProductDeviceCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
