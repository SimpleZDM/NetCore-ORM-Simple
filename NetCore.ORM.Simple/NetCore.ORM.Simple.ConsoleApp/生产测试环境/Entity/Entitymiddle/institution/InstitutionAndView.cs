using MDT.VirtualSoftPlatform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class InstitutionAndView
    {

        //var query = from inst in dbContext.Institutions.AsNoTracking().Where(t => t.IsDelete == false)
        //            join user in dbContext.Users.AsNoTracking().Where(t => t.IsDelete == false) on inst.AdminUserID equals user.ID
        //            join province in dbContext.Regions.AsNoTracking() on inst.ProvinceID equals province.Id
        //            join city in dbContext.Regions.AsNoTracking() on inst.CityID equals city.Id
        //            join area in dbContext.Regions.AsNoTracking() on inst.AreaID equals area.Id
        //            join industry in dbContext.Dictionaries.AsNoTracking().Where(t => t.MainID == (int)eDictionary.industryType) on inst.IndustryID equals industry.Id

        public InstitutionEntity institution { get; set; }
        public UserEntity user { get; set; }
        public RegionEntity city { get; set; }
        public RegionEntity area { get; set; }
        public RegionEntity province { get; set; }
        public DictionaryEntity industry { get; set; }
        public AgentPlatformEntity platform { get; set; }
    }
}
