using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("systemversiontable")]
    public class SystemVersionEntity:RecordEntity<int>
    {
        public SystemVersionEntity()
        {

        }

        /// <summary>
        ///版本
        /// </summary>
        public string VersionCode { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public int ClientType { get; set; }

        /// <summary>
        ///  app id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ForceUpgrade { get; set; }

        /// <summary>
        /// url
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 更新信息
        /// </summary>
        public string UpgradeMessage { get; set; }
    }
}
