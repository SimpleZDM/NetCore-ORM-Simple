using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserInfoView
    {
        public UserInfoView()
        {
            CompanyId=Guid.Empty;
        }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }

        public bool IsBandOpenID { get; set; }
       
        public string PhoneNumber { get; set; }
      
        public string Email { get; set; }
       
        public string IdCard { get; set; }

  
        
        public string DisplayName { get; set; }
   
        public int RoleCode{ get; set; }
     
        public int Status { get; set; }
     
        public string AgentPlatformID { get; set; }

        public string AgentPlatformName { get; set; }

        public string InstitutionID { get; set; }
        public string InstitutionName { get; set; }
        
        public string DepartmentName { get; set; }
        public string DepartmentID { get; set; }
       
        public string SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
      
        public string SClassID { get; set; }

        public DateTime CreateTime { get; set; }

        public string ConcurrencyStamp { get; set; }
        public string DisplayRoleName { get; set; }

        public string ClassName { get; set; }
        public string CompanyName { get; set; }
        public Guid CompanyId { get; set; }
        public string StudentCode { get; set; }
        public string Key { get; set; }

    }
}
