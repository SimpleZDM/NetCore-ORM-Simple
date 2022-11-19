using System;
using MDT.VirtualSoftPlatform.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetCore.ORM.Simple.Common;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("studenttable")]
    [TableName("studenttable")]

    public class StudentEntity:RecordEntity<Guid>
    {

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentCode { get; set; }

        public Guid UserID { get; set; }
        public Guid ClassID { get; set; }

        public Guid SpecialtyID { get; set; }

        public Guid DepartmentID { get; set; }

    }
}
