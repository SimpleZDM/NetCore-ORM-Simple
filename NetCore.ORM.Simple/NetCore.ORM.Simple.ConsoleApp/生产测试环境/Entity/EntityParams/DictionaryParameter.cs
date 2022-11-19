using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams
 * 接口名称 DictionaryParameter
 * 开发人员：-nhy
 * 创建时间：2022/8/10 16:06:08
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class DictionaryParameter:BaseParameter
    {
        public DictionaryParameter()
        {
            RowId = -1;
            MainId = -1;
        }

        public int RowId { get; set; }
        public int MainId { get; set; }
    }
}
