using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity.EntityView.mission
 * 接口名称 TaskScoreView
 * 开发人员：-nhy
 * 创建时间：2022/9/14 9:31:58
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class TaskScoreView
    {

        public int Id { get { return id; } set { id = value; } }
        /// <summary>
        /// 一层分数
        /// </summary>
        public float FirstFloorScore { get { return firstFloorScore; } set { firstFloorScore = value; } }
        /// <summary>
        /// 二层分数
        /// </summary>
        public float SecondFloorScore { get { return secondFloorScore; } set { secondFloorScore = value; } }

        /// <summary>
        /// 名称
        /// </summary>
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get { return taskType; } set { taskType = value; } }
        /// <summary>
        /// 角色类型
        /// </summary>
        /// 
        public int PlayerType { get { return playerType; } set { playerType = value; } }
        /// <summary>
        /// 层数-1层 二层
        /// </summary>
        /// 
        public int FloorCount { get { return floorCount; } set { floorCount = value; } }

        private float firstFloorScore;
        private float secondFloorScore;
        private string displayName;
        private int taskType;
        private int playerType;
        private int floorCount;
        private int id;
    }
}
