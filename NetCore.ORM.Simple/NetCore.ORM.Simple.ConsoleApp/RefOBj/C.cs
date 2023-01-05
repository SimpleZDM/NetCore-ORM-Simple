using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.ORM.Simple.ConsoleApp
{
    public class C
    {
        public void up(B b,ref B b1)
        {
            b.value = 1;
            Console.WriteLine(object.ReferenceEquals(b,b1));
        }
    }
}
