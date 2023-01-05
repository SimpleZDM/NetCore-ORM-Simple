using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NPOICoreExcel
 * 接口名称 MatchLog
 * 开发人员：-nhy
 * 创建时间：2022/10/12 14:59:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    [TableName("MatchLogs")]
    public class MatchLog : BaseEntity
    {
        public int ExamId { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SubmissionTime { get; set; }
        /// <summary>
        /// 记录人
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 状态，0=草稿，1=已提交
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public string WD { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public string SD { get; set; }
        /// <summary>
        /// CO2浓度
        /// </summary>
        public string CO2 { get; set; }
        /// <summary>
        /// 光照时间
        /// </summary>
        public string GZ { get; set; }
        /// <summary>
        /// 营养液管理
        /// </summary>
        public string YYYGL { get; set; }
        /// <summary>
        /// 出苗株数
        /// </summary>
        public string CMZS { get; set; }
        /// <summary>
        /// 植株状态
        /// </summary>
        public string ZZZT { get; set; }
        /// <summary>
        /// 植株照片
        /// </summary>
        public string ZZZP { get; set; }
        /// <summary>
        /// 遇到问题及解决方案
        /// </summary>
        public string Question { get; set; }
        public string Other { get; set; }

        //[ColName("SheetName")]
        public int ExamDay { get; set; }
    }
}
