using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 ConditionEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/21 17:57:30
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class ConditionEntity
    {
        public ConditionEntity(eConditionType type)
        {
            ConditionType = type;
            Members = new Stack<MemberEntity>();
        }
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        /// <summary>
        /// 常量-字段
        /// </summary>
        public eConditionType ConditionType { get { return conditionType; } set { conditionType = value; } }
        /// <summary>
        /// 符号类型
        /// </summary>
        public eSignType SignType { get { return signType; } set { signType = value; } }
        public eDataType DataType { get { return dataType; } set { dataType = value; } }

        public Stack<MemberEntity> Members { get { return members; } set { members = value; } }
        public Type PropertyType { get { return propertyType; } set { propertyType = value; } }
        /// <summary>
        /// 将一些值进行保存
        /// </summary>
        public object Value { get { return val; } set { val = value; } }

        public void SetDisplayName(string TableName,string PropName)
        {
            if (Check.IsNullOrEmpty(TableName)||Check.IsNullOrEmpty(PropName))
            {
                throw new Exception("未找到属性名称或者表名称!");
            }
            DisplayName=$"{TableName}{DBMDConst.Dot}{PropName}";
        }



        private string displayName;
        private eConditionType conditionType;
        private eSignType signType;
        private Stack<MemberEntity> members;
        private Type propertyType;
        private object val;
        private eDataType dataType;
    }
}
