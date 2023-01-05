using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView
 * 接口名称 StudentScoreView
 * 开发人员：-nhy
 * 创建时间：2022/4/22 10:17:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class StudentScoreView
    {
        /// <summary>
        /// 平均使用时长
        /// </summary>
        public double TimeLength { get; set; }
        /// <summary>
        /// 综合分数
        /// </summary>
        public decimal SynthesizeTotal { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 各种角色
        /// </summary>

        public KeyValueView<decimal>[] Roles { get; set; }

        /// <summary>
        /// 总次数
        /// </summary>
        public int TotalCount { get; set; }

        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }

    }
}
