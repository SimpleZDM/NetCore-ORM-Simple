using System;
using System.Collections.Generic;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity.Condition
 * 接口名称 BaseCondition
 * 开发人员：11920
 * 创建时间：2022/12/29 13:37:33
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class BaseCondition
    {
        public BaseCondition(eConditionType ConditionType)
        {
            this.ConditionType = ConditionType;
        }
       public eConditionType ConditionType { get; set; }
       public eDataType DataType { get; set; }
       public Stack<MemberEntity> Members { get; set; }
       public List<MethodEntity> Methods { get; set; }
       public object Value { get; set; }
    }
}
