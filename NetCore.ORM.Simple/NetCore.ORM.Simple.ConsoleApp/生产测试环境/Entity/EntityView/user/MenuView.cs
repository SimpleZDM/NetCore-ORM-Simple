using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class MenuView:IConvertTree<MenuView>
    {
        public string ID { get; set; }
        //菜单名称
        public string MenuName { get; set; }
        //父菜单id
        public string ParentID { get; set; }
        //排序编号
        public int Sort { get; set; }

        //保存前端路由地址
        public string Url { get; set; }
        //后端控制器名称 便于权限管理
        public string ControllerName { get; set; }
        public string Component { get; set; }
        //图标
        public string Icon { get; set; }

        public int MenuTypeID { get; set; }
        public string MenuTypeName { get; set; }
        public List<MenuView> Children { get; set; } = new List<MenuView>();
    }
}
