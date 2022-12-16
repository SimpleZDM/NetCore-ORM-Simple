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
        //public static void VisitMember(ref ConditionEntity condition,List<ConditionEntity> conditions, MemberInfo member, ref bool IsCompleteMember, MemberEntity currentMember)
        //{
        //    string PropName = node.Member.Name;
        //    PropertyInfo prop = null;
        //    string asTableName = null;
        //    if ((node.Expression is ParameterExpression Parameter) && currentTables.ContainsKey(Parameter.Name))
        //    {
        //        prop = table.GetProperty(currentTables[Parameter.Name], PropName);
        //        if (!Check.IsNull(prop))
        //        {
        //            PropName = prop.Name;
        //        }
        //        asTableName = table.GetAsTableName(currentTables[Parameter.Name]);
        //        ConditionEntity condition = new ConditionEntity(eConditionType.ColumnName);
        //        currentMethod.Conditions.Add(condition);
        //        condition.AsTableName = asTableName;
        //        condition.ColumnName = prop.GetColName();
        //    }
        //    else
        //    {

        //    }
        //    if (Check.IsNull(condition))
        //    {
        //        condition = ConditionsExtension.GetCondition(eConditionType.ColumnName);
        //    }
        //    conditions.Add(condition);
        //    var memberCondition = ConditionsExtension.SetConstMember(member);
        //    if (Check.IsNull(memberCondition))
        //    {
        //        if (Check.IsNull(currentMember))
        //        {
        //            currentMember = new MemberEntity();
        //        }
        //        currentMember.Member = member;
        //        condition.Members.Push(currentMember);
        //    }
        //    else
        //    {
        //        conditions.Add(memberCondition);
        //    }
        //}


        public static bool VisitConstant(this ConstantExpression node, ref MethodEntity currentMethod)
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
                right=currentMethod.Parameters[currentMethod.Parameters.Count - 1];
                right.ConditionType = eConditionType.Constant;
            }
            if (node.Type.IsValueType)
            {
                ConditionEntity condition = GetCondition(eConditionType.Constant);
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

                right.SetConstantContValue(node);
            }

            return true;
        }

        public static void VisitMethod(this MethodCallExpression node,
            ref MethodEntity currentMethod,
             ref MemberEntity currentMember, List<MethodEntity> methods)
        {
            if (Check.IsNull(currentMethod))
            {
                currentMethod = new MethodEntity();
              
            }
            VisitMethod(node, ref currentMethod,ref currentMember);
            if (Check.IsNullOrEmpty(methods)||!object.ReferenceEquals(methods[methods.Count-1],currentMethod))
            {
                methods.Add(currentMethod);
            }
        }


        public static void VisitMethod(this MethodCallExpression node, ref MethodEntity currentMethod,
           ref MemberEntity currentMember)
        {
            if (Check.IsNull(currentMethod))
            {
                currentMethod = new MethodEntity();
            }
            currentMethod.Name = node.Method.Name;

            if (!Check.IsNullOrEmpty(node.Arguments))
            {
                if (node.Arguments[0] is MethodCallExpression call)
                {
                        currentMember = new MemberEntity();
                        currentMember.OParams = call.Arguments[0];
                }
                if (node.Arguments[0] is ConstantExpression constant)
                {
                        currentMember = new MemberEntity();
                        currentMember.OParams = constant.Value;
                }

            }
        }



        public static void VisitMember(this MemberExpression node,
            Dictionary<string, int> currentTables,
            TableEntity table, MapEntity[] mapInfos, ref MemberEntity currentMember,
             List<ConditionEntity> conditions)
        {
            ConditionEntity condition=null;
            string PropName = node.Member.Name;
            ParameterExpression Parameter = node.Expression as ParameterExpression;
            int Index = -1;

            if ((!Check.IsNull(Parameter) && currentTables.ContainsKey(Parameter.Name)))
            {
                Index = currentTables[Parameter.Name];
            }

            if (Index>=0|| mapInfos.Any(u=>u.PropName==PropName))
            {
                   condition = GetCondition(table, PropName, Index, node.Type, mapInfos);
            }
            else
            {
                VisitMember(ref condition,conditions, node.Member, currentMember);
            }
            conditions.Add(condition);
        }
        public static void VisitMember(ref ConditionEntity condition, List<ConditionEntity> conditions,MemberInfo member, MemberEntity currentMember)
        {
            if (Check.IsNull(condition))
            {
                condition = GetCondition(eConditionType.ColumnName);
            }
            var memberCondition = SetConstMember(member);
            if (Check.IsNull(memberCondition))
            {


                if (Check.IsNull(currentMember))
                {
                    currentMember = new MemberEntity();
                }
                currentMember.Member = member;
                condition.Members.Push(currentMember);
            }
            else
            {
                conditions.Add(memberCondition);
            }
        }
      

        public static void VisitUnary(this UnaryExpression node, ref MethodEntity currentMethod)
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
                    if (!Check.IsNull(currentMethod))
                    {
                        currentMethod.IsNot = true;
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
        public static void SetConstantContValue(this ConditionEntity left, ConstantExpression node)
        {
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
                        left.DisplayName = value.ToString();
                    }
                    else
                    {
                        left.Value = value;
                    }
                }
                else
                {
                    left.Value = node.Value.ToString();
                }

            }

        }
        public static ConditionEntity GetCondition(eConditionType conditionType)
        {
            ConditionEntity condition = new ConditionEntity(conditionType);
            return condition;
        }

        public static ConditionEntity GetCondition(TableEntity table, string PropName, int Index, Type type,MapEntity[] mapInfos)
        {
            ConditionEntity conditon = GetCondition(eConditionType.ColumnName);

            var maps = mapInfos.Where(m => m.PropName.Equals(PropName)).ToArray();

            if (!Check.IsNullOrEmpty(maps))
            {
                conditon.DisplayName = ConditionsExtension.GetDisplayName(maps[0].TableName, maps[0].ColumnName);
                conditon.AsTableName = maps[0].ClassName;
                conditon.ColumnName = maps[0].ColumnName;
                conditon.TableName = maps[0].TableName;
                conditon.PropertyType = maps[0].PropertyType;

            }
            else if (Index>=0 && Index < table.TableNames.Length)
            {

                var Prop = ConditionsExtension.GetPropertyType(table, Index, PropName);
                var NameEntity = table.GetTableName(Index);
                conditon.AsTableName = NameEntity.AsName;
                conditon.TableName = NameEntity.DisplayNmae;

                if (!Check.IsNull(Prop))
                {
                    PropName = table.GetColumnName(Index, PropName);
                    conditon.DisplayName = ConditionsExtension.GetDisplayName(conditon.AsTableName, PropName);
                    conditon.ColumnName= Prop.GetColName();
                }
            }
            conditon.PropertyType = type;
            return conditon;
        }

        public static ConditionEntity SetConstMember(MemberInfo member)
        {

            ConditionEntity condition = null;//new ConditionEntity(eConditionType.Constant);
            if (member.ToString() == CommonConst.SystemDateTimeNow)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;

            }
            else if (member.ToString() == CommonConst.SystemDateTimeMaxValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MaxValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemDateTimeMinValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemGuidEmpty)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = Guid.Empty.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemintMaxValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = int.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemintMinValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = int.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdoubleMaxValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = double.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdoubleMinValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = double.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemfloatMinValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = float.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemfloatMaxValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = float.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdecimalMaxValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = Decimal.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString() == CommonConst.SystemdecimalMinValue)
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = Decimal.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            return condition;
        }

        public static ConditionEntity GetCondition(eConditionType conditionType, eSignType signType)
        {
            ConditionEntity condition = GetCondition(conditionType);
            condition.SignType = signType;
            return condition;
        }


    }
}
