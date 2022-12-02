using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.ORM.Simple.ConsoleApp
{
    public class A
    {
        public A()
        {
            b = new B();
            c=new C();
        }
        public B b;
        public C c;
        public void test()
        {
            c.up(b,ref b);
        }

    }
}
