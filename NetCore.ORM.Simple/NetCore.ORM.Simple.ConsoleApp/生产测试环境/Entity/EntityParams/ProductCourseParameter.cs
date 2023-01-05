using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityParams
 * 接口名称 ProductCourseParameter
 * 开发人员：-nhy
 * 创建时间：2022/5/20 16:47:17
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductCourseParameter:BaseParameter
    {
        public ProductCourseParameter()
        {
        }
        public bool IsTree { get; set; }
        public string ProductId { get; set; }

        public string SearchTerm { get; set; }

        /// <summary>
        /// 课程类型集合
        /// </summary>
        public int[] arrayCourseType { get; set; }
        public string CourseTypes { get; set; }
        /// <summary>
        /// 父id集合
        /// </summary>
        public string ParentIds{ get; set; }
        public string[] arrayParentId{ get; set; }
        /// <summary>
        /// 最大节点数量
        /// </summary>

        public int MaxNodeCount { get; set; }

        /// <summary>
        /// 最大时长
        /// </summary>
        public int MaxTime { get; set; }

    }
}
