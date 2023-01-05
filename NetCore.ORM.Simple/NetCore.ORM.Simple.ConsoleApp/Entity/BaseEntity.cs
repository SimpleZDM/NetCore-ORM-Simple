using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NPOICoreExcel
 * 接口名称 BaseEntity
 * 开发人员：-nhy
 * 创建时间：2022/10/12 15:00:36
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    public class BaseEntity
    {
        [Key(true)]
        public int Id { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual DateTime LastModificationTime { get; set; }

        public virtual bool IsDelete { get { return isDelete; } set { isDelete = value; } }

        private bool isDelete = false;
        public virtual DateTime DeletionTime { get; set; }

    }
}
