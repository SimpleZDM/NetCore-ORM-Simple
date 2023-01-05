using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.product
 * 接口名称 HardwareStatusView
 * 开发人员：-nhy
 * 创建时间：2022/8/10 15:54:41
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class HardwareStatusView
    {
        public HardwareStatusView()
        {
            BatLevel = 0;
            Incharge = 0;
            IsOnline = false;
            State = 0;
        }

        public int BatLevel { get { return batLevel; } set { batLevel = value; } }
        public int Incharge { get { return incharge; } set { incharge = value; } }
        public string Name { get { return name; } set { name = value; } }
        public bool IsOnline { get { return isOnline; } set { isOnline = value; } }
        public int State { get { return state; } set { state = value; } }

        private int batLevel;
        private int incharge;
        private string name;
        private bool isOnline;
        private int state;
    }
}
