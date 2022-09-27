using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 SelectEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/27 13:32:46
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class SelectEntity
    {
        public SelectEntity()
        {
            MapInfos = new List<MapEntity>(15);
            JoinInfos = new Dictionary<string,JoinTableEntity>();
            Conditions=new List<ConditionEntity>(15);
            TreeConditions = new List<TreeConditionEntity>(15);
        }
        /// <summary>
        /// 
        /// </summary>
        public List<MapEntity> MapInfos { get { return mapInfos; } set { mapInfos = value; } }
        /// <summary>
        /// 链接信息
        /// </summary>
        public  Dictionary<string,JoinTableEntity> JoinInfos { get { return joinInfos; } set { joinInfos = value; } }
        /// <summary>
        /// 多个二元表达式中的连接
        /// </summary>
        public List<ConditionEntity> Conditions { get { return conditions; } set { conditions = value; } }
        /// <summary>
        /// 等式集合
        /// </summary>
        public List<TreeConditionEntity> TreeConditions { get { return treeConditions; } set { treeConditions = value; } }

        private List<MapEntity> mapInfos;
        private Dictionary<string,JoinTableEntity> joinInfos;
        private List<ConditionEntity> conditions;
        private List<TreeConditionEntity> treeConditions;
    }
}
