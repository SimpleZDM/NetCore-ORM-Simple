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
            Index = -1;
            ConstFieldType = new List<FieldInfo>();
        }
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        public eConditionType ConditionType { get { return conditionType; } set { conditionType = value; } }
        public eSignType SignType { get { return signType; } set { signType = value; } }

        public Type PropertyType { get { return propertyType; } set { propertyType = value; } }
        public PropertyInfo ConstPropType { get { return constType; } set { constType = value; } }
        public List<FieldInfo> ConstFieldType { get { return constFieldType; } set { constFieldType = value; } }
        public int Index { get { return index; } set { index = value; } }
        public string Key { get { return key; } set { key = value; } }



        private string displayName;
        private eConditionType conditionType;
        private eSignType signType;
        private Type propertyType;
        private PropertyInfo constType;
        private List<FieldInfo> constFieldType;
        private int index;
        private string key;
    }
}
