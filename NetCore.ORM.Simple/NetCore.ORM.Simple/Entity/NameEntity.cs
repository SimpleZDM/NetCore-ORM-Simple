using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 NameEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/23 13:41:54
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class NameEntity
    {
        public NameEntity()
        {

        }
        public string DisplayNmae { get { return name; } set { name = value; } }
        public int Count { get { return count; } set { count=value; } }
        private string name;
        private int count;
    }
}
