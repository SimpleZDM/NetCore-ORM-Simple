using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common
 * 接口名称 MethodConst
 * 开发人员：-nhy
 * 创建时间：2022/10/21 9:49:55
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    public class MethodConst
    {
        static MethodConst()
        {
        }

        public const string _ToString= "ToString";
        public const string _Equals= "Equals";
        public const string _IsNullOrEmpty= "IsNullOrEmpty";
        public const string _IsNull= "IsNull";
        public const string _Sum="Sum";
        public const string _Min = "Min";
        public const string _Max="Max";
        public const string _Count="Count";
        public const string _Average= "Average";
        public const string _FirstOrDefault= "FirstOrDefault";
        public const string _Contains= "Contains";
        public const string _LeftContains= "LeftContains";
        public const string _RightContains= "RightContains";
    }
}
