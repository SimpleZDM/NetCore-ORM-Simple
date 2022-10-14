using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp.AttrTest
 * 接口名称 MyColumn
 * 开发人员：-nhy
 * 创建时间：2022/10/14 14:48:32
 * 描述说明：使用自定义特性,做出以下基本实现即可
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    public class MyColumnAttrbute :AbsAttribute, IColumn
    {
        public MyColumnAttrbute(string name=null,bool key=false,bool autoIncrease=false,bool ignore=false) :base(name)
        {
            Key= key;
            AutoIncrease= autoIncrease;
            Ignore= ignore;
        }

        public bool Key { get{ return key; } set { key = value; } }
        public bool AutoIncrease { get{ return autoIncrease; } set { autoIncrease = value; } }
        public bool Ignore { get { return ignore; } set { ignore = value; } }

        private bool key;
        private bool autoIncrease;
        private bool ignore;



    }
}
