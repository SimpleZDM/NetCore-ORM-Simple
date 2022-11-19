using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 产品实体
    /// </summary>
    [Table("producttable")]
    public class ProductEntity : RecordEntity<Guid>
    {
        [MaxLength(50)]
        public string DisplayName { get; set; }
        public string IconUrl { get; set; }
        public string Introduction { get; set; }
        public int ComponentCount { get; set; }
        public string UnityFile { get; set; }
    }
}
