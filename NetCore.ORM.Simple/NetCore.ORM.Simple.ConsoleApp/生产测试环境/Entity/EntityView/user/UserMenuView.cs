using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Entity
{
    public class UserMenuView:IConvertTree<UserMenuView>
    {
        //菜单id
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
        public string ControllerName{ get; set; }
        public string Component { get; set; }
        //图标
        public string Icon { get; set; }
        //编辑权限
        public bool CanEdit { get; set; }
        public bool CanView { get; set; }
        public int MenuTypeID { get; set; }
        public List<UserMenuView> Children { get; set; } = new List<UserMenuView>();
    }
}
