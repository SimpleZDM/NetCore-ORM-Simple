

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 TreeConditionEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/22 9:41:16
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
using System.Collections.Generic;

namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 记录等式
    /// </summary>
    public class TreeConditionEntity
    {
        public TreeConditionEntity()
        {
            LeftBracket = new List<eSignType>();
            RightBracket = new List<eSignType>();
        }
        /// <summary>
        /// 左括号
        /// </summary>

        public List<eSignType> LeftBracket { get { return leftBracket; } set { leftBracket = value; } }
        /// <summary>
        /// 右边括号
        /// </summary>
        public List<eSignType> RightBracket { get { return rightBracket; } set { rightBracket = value; } }
        /// <summary>
        /// 条件左值
        /// </summary>
        public ConditionEntity LeftCondition { get { return left; } set { left = value; } }
        /// <summary>
        /// 条件右值
        /// </summary>
        public ConditionEntity RightCondition { get { return right; } set { right = value; } }
        /// <summary>
        /// 关系
        /// </summary>
        public ConditionEntity RelationCondition { get { return relation; } set { relation = value; } }
        /// <summary>
        /// 方法组
        /// </summary>
        public bool IsNot { get { return isNot; } set { isNot = value; } }

        private ConditionEntity left;
        private ConditionEntity right;
        private ConditionEntity relation;
        private List<eSignType> leftBracket;
        private List<eSignType> rightBracket;
        private bool isNot;
    }
}
