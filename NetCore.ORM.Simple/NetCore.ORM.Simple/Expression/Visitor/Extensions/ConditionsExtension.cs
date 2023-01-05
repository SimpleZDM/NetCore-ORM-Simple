using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

/*********************************************************
 * 命名空间 NetCore.ORM.Simpl.Visitor
 * 接口名称 ConditionsExtension
 * 开发人员：11920
 * 创建时间：2022/12/13 9:15:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal static class ConditionsExtension
    {
        
        public static void VisitUnary(this UnaryExpression node, ref TreeConditionEntity currentTree, ref bool IsComplete)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Add:
                    break;
                case ExpressionType.AddChecked:
                    break;
                case ExpressionType.And:
                    break;
                case ExpressionType.AndAlso:
                    break;
                case ExpressionType.ArrayLength:
                    break;
                case ExpressionType.ArrayIndex:
                    break;
                case ExpressionType.Call:
                    break;
                case ExpressionType.Coalesce:
                    break;
                case ExpressionType.Conditional:
                    break;
                case ExpressionType.Constant:
                    break;
                case ExpressionType.Convert:
                    break;
                case ExpressionType.ConvertChecked:
                    break;
                case ExpressionType.Divide:
                    break;
                case ExpressionType.Equal:
                    break;
                case ExpressionType.ExclusiveOr:
                    break;
                case ExpressionType.GreaterThan:
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    break;
                case ExpressionType.Invoke:
                    break;
                case ExpressionType.Lambda:
                    break;
                case ExpressionType.LeftShift:
                    break;
                case ExpressionType.LessThan:
                    break;
                case ExpressionType.LessThanOrEqual:
                    break;
                case ExpressionType.ListInit:
                    break;
                case ExpressionType.MemberAccess:
                    break;
                case ExpressionType.MemberInit:
                    break;
                case ExpressionType.Modulo:
                    break;
                case ExpressionType.Multiply:
                    break;
                case ExpressionType.MultiplyChecked:
                    break;
                case ExpressionType.Negate:
                    break;
                case ExpressionType.UnaryPlus:
                    break;
                case ExpressionType.NegateChecked:
                    break;
                case ExpressionType.New:
                    break;
                case ExpressionType.NewArrayInit:
                    break;
                case ExpressionType.NewArrayBounds:
                    break;
                case ExpressionType.Not:
                    if (!Check.IsNull(currentTree))
                    {
                        currentTree.IsNot = true;
                        IsComplete = true;
                    }
                    break;
                case ExpressionType.NotEqual:
                    break;
                case ExpressionType.Or:
                    break;
                case ExpressionType.OrElse:
                    break;
                case ExpressionType.Parameter:
                    break;
                case ExpressionType.Power:
                    break;
                case ExpressionType.Quote:
                    break;
                case ExpressionType.RightShift:
                    break;
                case ExpressionType.Subtract:
                    break;
                case ExpressionType.SubtractChecked:
                    break;
                case ExpressionType.TypeAs:
                    break;
                case ExpressionType.TypeIs:
                    break;
                case ExpressionType.Assign:
                    break;
                case ExpressionType.Block:
                    break;
                case ExpressionType.DebugInfo:
                    break;
                case ExpressionType.Decrement:
                    break;
                case ExpressionType.Dynamic:
                    break;
                case ExpressionType.Default:
                    break;
                case ExpressionType.Extension:
                    break;
                case ExpressionType.Goto:
                    break;
                case ExpressionType.Increment:
                    break;
                case ExpressionType.Index:
                    break;
                case ExpressionType.Label:
                    break;
                case ExpressionType.RuntimeVariables:
                    break;
                case ExpressionType.Loop:
                    break;
                case ExpressionType.Switch:
                    break;
                case ExpressionType.Throw:
                    break;
                case ExpressionType.Try:
                    break;
                case ExpressionType.Unbox:
                    break;
                case ExpressionType.AddAssign:
                    break;
                case ExpressionType.AndAssign:
                    break;
                case ExpressionType.DivideAssign:
                    break;
                case ExpressionType.ExclusiveOrAssign:
                    break;
                case ExpressionType.LeftShiftAssign:
                    break;
                case ExpressionType.ModuloAssign:
                    break;
                case ExpressionType.MultiplyAssign:
                    break;
                case ExpressionType.OrAssign:
                    break;
                case ExpressionType.PowerAssign:
                    break;
                case ExpressionType.RightShiftAssign:
                    break;
                case ExpressionType.SubtractAssign:
                    break;
                case ExpressionType.AddAssignChecked:
                    break;
                case ExpressionType.MultiplyAssignChecked:
                    break;
                case ExpressionType.SubtractAssignChecked:
                    break;
                case ExpressionType.PreIncrementAssign:
                    break;
                case ExpressionType.PreDecrementAssign:
                    break;
                case ExpressionType.PostIncrementAssign:
                    break;
                case ExpressionType.PostDecrementAssign:
                    break;
                case ExpressionType.TypeEqual:
                    break;
                case ExpressionType.OnesComplement:
                    break;
                case ExpressionType.IsTrue:
                    break;
                case ExpressionType.IsFalse:
                    break;
                default:
                    break;
            }
        }
        public static ConditionEntity SetConstMember(ContextSelect contextSelect,MemberInfo member)
        {

            ConditionEntity condition = null;//new ConditionEntity(eConditionType.Constant);
            if (member.ToString() == CommonConst.SystemDateTimeNow)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;

            }
            else if (member.ToString() == CommonConst.SystemDateTimeMaxValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MaxValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemDateTimeMinValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemGuidEmpty)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = Guid.Empty.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemintMaxValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = int.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemintMinValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = int.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdoubleMaxValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = double.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdoubleMinValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = double.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemfloatMinValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = float.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemfloatMaxValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = float.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdecimalMaxValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = Decimal.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdecimalMinValue)
            {
                condition = contextSelect.GetCondition(eConditionType.Constant);
                condition.DisplayName = Decimal.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            return condition;
        }
        public static void TreeConditionInit(List<TreeConditionEntity> treeConditions, List<ConditionEntity> conditions, ref int firstConditionIndex)
        {
            if (treeConditions.Count > 0)
            {
                treeConditions[firstConditionIndex].LeftBracket.Add(eSignType.LeftBracket);
                firstConditionIndex = treeConditions.Count - 1;
                treeConditions[firstConditionIndex].RightBracket.Add(eSignType.RightBracket);
                conditions.Add(new ConditionEntity(eConditionType.Sign)
                {
                    SignType = eSignType.And

                });
            }
        }
        public static void SetConstantContValue(this TreeConditionEntity currentTree, ConstantExpression node)
        {
            currentTree.LeftCondition.SetConstantContValue(currentTree.RightCondition, node);
        }
        public static void SetConstantContValue(this ConditionEntity left, ConditionEntity right, ConstantExpression node)
        {
            bool IsRight = true;
            if (Check.IsNull(right))
            {
                IsRight = false;
            }
            if (!Check.IsNull(left))
            {
                if (!Check.IsNullOrEmpty(left.Members))
                {
                    object value = null;
                    MemberEntity meber = left.Members.Pop();
                    if (meber.Member is FieldInfo f)
                    {
                        value = f.GetValue(node.Value);
                        if (!Check.IsNull(meber.OParams))
                        {
                            value = ((dynamic)value)[(dynamic)meber.OParams];
                        }

                    }
                    while (!Check.IsNullOrEmpty(left.Members))
                    {
                        MemberEntity m = left.Members.Pop();
                        if (m.Member is FieldInfo field)
                        {
                            if (!Check.IsNull(m.OParams))
                            {
                                value = ((dynamic)field.GetValue(value))[m.OParams];
                            }
                            else if (!Check.IsNull(m.KeyMember))
                            {
                                if (m.KeyMember is PropertyInfo PropKey)
                                {
                                    var Key = PropKey.GetValue(value);
                                    value = ((dynamic)field.GetValue(value))[(dynamic)Key];
                                }
                                else if (m.KeyMember is FieldInfo fieldKey)
                                {
                                    var Key = fieldKey.GetValue(node.Value);
                                    value = ((dynamic)field.GetValue(value))[(dynamic)Key];
                                }
                            }
                            else
                            {
                                value = field.GetValue(value);
                            }
                        }
                        if (m.Member is PropertyInfo Property)
                        {
                            if (!Check.IsNull(m.OParams))
                            {
                                var o = Property.GetValue(value);
                                value = ((dynamic)o)[(dynamic)m.OParams];
                            }
                            else if (!Check.IsNull(m.KeyMember))
                            {
                                if (m.KeyMember is PropertyInfo PropKey)
                                {
                                    var Key = PropKey.GetValue(node.Value);
                                    value = ((dynamic)Property.GetValue(value))[(dynamic)Key];
                                }
                                else if (m.KeyMember is FieldInfo fieldKey)
                                {
                                    var Key = fieldKey.GetValue(node.Value);
                                    value = ((dynamic)Property.GetValue(value))[(dynamic)Key];
                                }
                            }
                            else
                            {
                                value = Property.GetValue(value);
                            }
                        }
                    }


                    if (value.GetType().IsValueType)
                    {

                        if (IsRight)
                        {
                            right.DisplayName = value.ToString();
                        }
                        else
                        {
                            left.DisplayName=value.ToString();
                        }

                    }
                    else
                    {
                        if (IsRight)
                        {
                            right.Value = value;
                        }
                        else
                        {
                            left.Value = value;
                        }

                    }
                }
                else
                {
                    if (IsRight)
                    {
                        right.Value = node.Value.ToString();
                    }
                    else
                    {
                        left.Value = node.Value.ToString();
                    }
                  
                }

            }

        }
       
    }
}
