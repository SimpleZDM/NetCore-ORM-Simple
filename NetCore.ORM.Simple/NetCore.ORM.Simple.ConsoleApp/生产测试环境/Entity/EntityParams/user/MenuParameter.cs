using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;


namespace MDT.VirtualSoftPlatform.Entity
{
    public class MenuParameter : BaseParameter
    {
        public MenuParameter()
        {
            IsTree = false;
            MenuTypeID = -1;
        }
        public int MenuTypeID { get; set; }
        public bool IsTree { get; set; }

        public bool IsSort { get; set; }

    }
}
