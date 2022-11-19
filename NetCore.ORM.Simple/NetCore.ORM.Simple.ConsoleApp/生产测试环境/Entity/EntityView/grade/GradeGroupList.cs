using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.grade
 * 接口名称 GradeGroupList
 * 开发人员：-nhy
 * 创建时间：2022/7/6 9:58:07
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    /// <summary>
    /// 小组排行实体
    /// </summary>
    public class GradeGroupList
    {
        [Ignore()]
        public KeyValueView<decimal>[] Roles { get; set; }
        /// <summary>
        /// 使用时长
        /// </summary>
        public float UserTime { get; set; }
        /// <summary>
        /// 小组分数
        /// </summary>
        public Decimal Score { get; set; }
    }
}
