using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 MethodExtension
 * 开发人员：11920
 * 创建时间：2022/12/14 10:54:13
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal static class MethodExtension
    {
        public static bool VisitConstant(this ConstantExpression node,ref MethodEntity currentMethod,ContextSelect contextSelect)
        {
            if (Check.IsNull(currentMethod))
            {
                throw new ArgumentException(nameof(currentMethod));
            }
            ConditionEntity right;
            if (Check.IsNullOrEmpty(currentMethod.Parameters))
            {
                right = new ConditionEntity(eConditionType.Constant);
            }
            else
            {
                right = currentMethod.Parameters[currentMethod.Parameters.Count - 1];
                right.ConditionType = eConditionType.Constant;
            }
            if (node.Type.IsValueType)
            {
                ConditionEntity condition = contextSelect.GetCondition(eConditionType.Constant);
                if (node.Type.IsEnum)
                {
                    int.TryParse(node.Value.ToString(), out int value);
                    condition.Value = node.Value;
                }
                else
                {
                    condition.DisplayName = node.Value.ToString();
                }
                currentMethod.Parameters.Add(condition);
            }
            else
            {

                right.SetConstantContValue(null,node);
            }

            return true;
        }
    }
}
