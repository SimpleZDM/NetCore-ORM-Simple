using System;
using MDT.VirtualSoftPlatform.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("teachertable")]
    public class TeacherEntity : RecordEntity<Guid>
    {
        public Guid UserID { get; set; }
        public Guid DepartmentID { get; set; }
        public string ClassIds { get; set; }
    }
}
