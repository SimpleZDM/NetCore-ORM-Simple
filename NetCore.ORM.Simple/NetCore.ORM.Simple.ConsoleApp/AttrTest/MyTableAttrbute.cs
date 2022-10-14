using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp
 * 接口名称 MyTableAttrbute
 * 开发人员：-nhy
 * 创建时间：2022/10/14 14:48:10
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    public class MyTableAttrbute : AbsAttribute,IName
    {
        public MyTableAttrbute(string name = null) : base(name)
        {
          
        }
    }
}
