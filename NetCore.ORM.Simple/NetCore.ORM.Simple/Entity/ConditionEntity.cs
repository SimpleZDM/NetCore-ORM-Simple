using System;
using System.Collections.Generic;
using System.Linq;
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
        }
        public string DisplayName { get { return displayName; } set { displayName = value; } }
        public eConditionType ConditionType { get { return conditionType; } set { conditionType = value; } }
        public eSignType SignType { get { return signType; } set { signType = value; } }

        public Type PropertyType { get { return propertyType; } set { propertyType = value; } }



        private string displayName;
        private eConditionType conditionType;
        private eSignType signType;
        private Type propertyType;
    }
}
