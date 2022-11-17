using MDT.VirtualSoftPlatform.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MDT.VirtualSoftPlatform.Entity
{
    [Table("MenuTable")]
    public class MenuEntity: RecordEntity<Guid>
    {
        public MenuEntity()
        {
            ID=Guid.NewGuid();
        }
        public Guid ParentID { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(20)]
        public string MenuName { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        [MaxLength(50)]
        public string ControllerName { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [MaxLength(50)]
        public string MenuUrl { get; set; }
        /// <summary>
        /// 前端组件
        /// </summary>
        [MaxLength(50)]
        public string Component { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort{ get; set; }

        public int MenuTypeID { get; set; }

        public bool IncludeChildren { get; set; }


    }
}
