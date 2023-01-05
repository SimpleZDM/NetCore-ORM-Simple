using MDT.VirtualSoftPlatform.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.product
 * 接口名称 ProductCourseView
 * 开发人员：-nhy
 * 创建时间：2022/5/20 16:45:21
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductCourseView:IConvertTree<ProductCourseView>
    {

        public ProductCourseView()
        {
            Children = new List<ProductCourseView>();
        }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 节点类型 0-章 1-节
        /// </summary>
        public int CourseType { get; set; }

        public string CourseTypeName { get; set; }
        /// <summary>>>
        /// 预计花费时间
        /// </summary>
        public int ExpectSpendTime { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ID { get; set; }
        public string ParentID { get; set; }
        public List<ProductCourseView> Children { get; set; }
    }
}
