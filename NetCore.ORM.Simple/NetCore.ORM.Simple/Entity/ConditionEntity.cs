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
            ConstFieldType = new List<FieldInfo>();
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

        public Type PropertyType { get { return propertyType; } set { propertyType = value; } }
        public PropertyInfo ConstPropType { get { return constType; } set { constType = value; } }
        public List<FieldInfo> ConstFieldType { get { return constFieldType; } set { constFieldType = value; } }

        /// <summary>
        /// 将一些值进行保存
        /// </summary>
        public object Value { get { return val; } set { val = value; } }



        private string displayName;
        private eConditionType conditionType;
        private eSignType signType;
        private Type propertyType;
        private PropertyInfo constType;
        private List<FieldInfo> constFieldType;
        private object val;
        private eDataType dataType;
    }
}
