using MDT.VirtualSoftPlatform.Common;
using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("dictionarytable")]
    [TableName("dictionarytable")]
    public class DictionaryEntity:BaseEntity<int>
    {
        public int MainID { get; set; }
        public int RowID { get; set; }
        public string RowName { get; set; }
        public string RowDesc { get; set; }
    }
}
