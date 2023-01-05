using System;
using MDT.VirtualSoftPlatform.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 机构
    /// </summary>
   [Table("institutiontable")]
    public class InstitutionEntity:RecordEntity<Guid>
    {
        public InstitutionEntity()
        {
            ID= Guid.NewGuid(); 
        }
        public Guid APID { get; set; }
        public string InstName { get; set; }
        public int IndustryID { get; set; }

        public int AreaID { get; set; }
        public int CityID { get; set; }
        public int ProvinceID { get; set; }
        public Guid AdminUserID { get; set; }
        public string PhoneNumber { get; set; }
      
        public string Email { get; set; }
       
        public string IDCard { get; set; }
        public string PlaceDetail { get; set; }
    }
}
