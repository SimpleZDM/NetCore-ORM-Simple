

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 TreeConditionEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/22 9:41:16
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
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
            Index = -1;
        }

        public List<eSignType> LeftBracket { get { return leftBracket; } set { leftBracket = value; } }
        public List<eSignType> RightBracket { get { return rightBracket; } set { rightBracket = value; } }
        public ConditionEntity LeftCondition { get { return left; } set { left = value; } }
        public ConditionEntity RightCondition { get { return right; } set { right = value; } }
        public ConditionEntity RelationCondition { get { return relation; } set { relation = value; } }
        public string Key { get { return key; } set { key = value; } }
        public int Index { get { return index; } set { index = value; } }
        public bool IsNot { get { return isNot; } set { isNot = value; } }

        private ConditionEntity left;
        private ConditionEntity right;
        private ConditionEntity relation;
        private List<eSignType> leftBracket;
        private List<eSignType> rightBracket;
        private string key;
        private int index;
        private bool isNot;
    }
}
