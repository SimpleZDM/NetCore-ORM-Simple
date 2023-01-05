using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView
 * 接口名称 MissionDateTimeView
 * 开发人员：-nhy
 * 创建时间：2022/4/25 10:14:13
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class MissionDateTimeView
    {
        #region Field

        #endregion

        #region Constructors and Property

        public Guid InstitutionID { get; set; }
        /// <summary>
        /// 名称-上午第一节课
        /// </summary>
        /// 
        public string Name { get; set; }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        /// 11:10
        public string EndTime { get; set; }
        /// <summary>
        /// 任务开始时间表
        /// </summary>
        /// 9:10
        public string StartTime { get; set; }

        public int Id { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsDelete { get; set; }

        /// <summary>
        /// 时间表类型
        /// </summary>
        public int TimeTypeId { get; set; }
        public string TimeTypeName { get; set; }
        public float TotalTime { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 克隆一个新的实例对象---(=运算符赋值引用类型会出现浅拷贝问题)
        /// </summary>
        /// <returns></returns>
        public new  MissionDateTimeView MemberwiseClone()
        {
            MissionDateTimeView view = new MissionDateTimeView();
            view.InstitutionID = this.InstitutionID;
            view.Name = this.Name;
            view.EndTime = this.EndTime;
            view.StartTime = this.StartTime;
            view.Id = this.Id;
            view.IsEmpty = this.IsEmpty;
            view.IsDelete = this.IsDelete;
            view.TimeTypeName = this.TimeTypeName;
            view.TotalTime = this.TotalTime;
            view.TimeTypeId = this.TimeTypeId;
            view.Date = this.Date;
            view.Description = this.Description;
            return view;
        }
        #endregion

        #region Public Methods and Operators

        #endregion
    }
}
