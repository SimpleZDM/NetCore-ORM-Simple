using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityDto
 * 接口名称 DictionaryDto
 * 开发人员：-nhy
 * 创建时间：2022/7/22 8:57:16
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class DictionaryDto:IParamsVerify
    {
        public int MainID { get; set; }
        public int RowID { get; set; }
        public string RowName { get; set; }
        public string RowDesc { get; set; }
    }
}
