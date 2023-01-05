using NetCore.ORM.Simple;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.ORM.Simple.Entity
{
    public interface ICondition
    {
         eConditionType ConditionType { get; set; }
         eDataType DataType { get; set; }
         Stack<MemberEntity> Members { get; set; }
         List<MethodEntity> Methods { get;set; }
         object Value { get; set; }
         string GetConditionName();
       
    }
}
