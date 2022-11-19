using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 ProductCourseDto
 * 开发人员：-nhy
 * 创建时间：2022/5/20 16:26:29
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductCourseDto
    {
        /// <summary>
        /// 课程名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 父节点名称
        /// </summary>

        public Guid ParentId { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 节点类型 0-章 1-节
        /// </summary>
        public int CourseType { get; set; }
        /// <summary>>>
        /// 预计花费时间
        /// </summary>
        public int ExpectSpendTime { get; set; }
        public Guid ProductId { get; set; }
    }
}
