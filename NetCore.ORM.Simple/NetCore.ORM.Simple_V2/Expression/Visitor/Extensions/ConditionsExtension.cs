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
        public static bool VisitConstant(this ConstantExpression node,List<TreeConditionEntity>treeConditions,ref TreeConditionEntity currentTree)
        {
            if (Check.IsNull(currentTree))
            {
                 currentTree = GetTreeConditon();
                  treeConditions.Add(currentTree);
            }
            if (Check.IsNull(currentTree.RightCondition))
            {
                currentTree.RightCondition = GetCondition(eConditionType.Constant);
            }
            else
            {
                if (!currentTree.RightCondition.ConditionType.Equals(eConditionType.Constant))
                {
                    currentTree.RightCondition.ConditionType = eConditionType.Constant;
                }
            }

            if (!Check.IsNullOrEmpty(currentTree.RightCondition.DisplayName))
            {
                return false;
            }
            if (node.Type.IsValueType)
            {
                if (node.Type.IsEnum)
                {
                    int.TryParse(node.Value.ToString(), out int value);
                    currentTree.RightCondition.DisplayName = value.ToString();
                }
                else
                {
                    currentTree.RightCondition.DisplayName = node.Value.ToString();
                }
            }
            else
            {
                SetConstantContValue(currentTree, node);
            }
            return true;
        }

        public static void VisitMethod(this MethodCallExpression node, 
            ref TreeConditionEntity currentTree,ref bool IsComplete,
            ref bool IsCompleteMember, ref MemberEntity currentMember,List<TreeConditionEntity>treeConditions)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree =GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            VisitMethod(node,ref currentTree, ref IsCompleteMember, ref currentMember);
            IsComplete = true;
            IsCompleteMember = true;
        }

      
        public static void VisitMethod(this MethodCallExpression node,ref TreeConditionEntity currentTree,
           ref bool IsCompleteMember,
           ref MemberEntity currentMember)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = GetTreeConditon();
            }

            if (Check.IsNull(currentTree.RelationCondition))
            {
                currentTree.RelationCondition = GetCondition(eConditionType.Method);// new ConditionEntity(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;

            }

            if (!Check.IsNullOrEmpty(node.Arguments))
            {
                if (node.Arguments[0] is MethodCallExpression call)
                {

                    if (IsCompleteMember)
                    {
                        currentMember = new MemberEntity();
                        currentMember.OParams = call.Arguments[0];
                        IsCompleteMember = false;
                    }


                }
                if (node.Arguments[0] is ConstantExpression constant)
                {

                    if (IsCompleteMember)
                    {
                        currentMember = new MemberEntity();
                        currentMember.OParams = constant.Value;
                        IsCompleteMember = false;
                    }
                }

            }
        }

       

        public static void VisitMember(this MemberExpression node,
            Dictionary<string,int> currentTables,ref TreeConditionEntity currentTree,
            TableEntity table,List<MapEntity>mapInfos,ref MemberEntity currentMember,
            ref bool IsCompleteMember)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = GetTreeConditon();
            }
            string PropName = node.Member.Name;
            if ((node.Expression is ParameterExpression Parameter) && currentTables.ContainsKey(Parameter.Name))
            {
                int Index = currentTables[Parameter.Name];
                
                if (!Check.IsNull(currentTree.LeftCondition))
                {
                    if (!Check.IsNull(currentTree.RightCondition))
                    {
                        currentTree.LeftCondition = GetCondition(table, PropName, Index, node.Type, mapInfos);
                    }
                    else
                    {
                        currentTree.RightCondition = GetCondition(table, PropName, Index, node.Type, mapInfos);
                    }
                }
                else
                {
                    currentTree.LeftCondition = GetCondition(table, PropName, Index, node.Type, mapInfos);
                }
            }
            else
            {
                VisitMember(ref currentTree, node.Member, ref IsCompleteMember, currentMember);
            }
            IsCompleteMember = true;
        }
        public static void VisitMember(ref TreeConditionEntity currentTree, MemberInfo member, ref bool IsCompleteMember, MemberEntity currentMember)
        {
            if (Check.IsNull(currentTree.LeftCondition))
            {
                currentTree.LeftCondition = GetCondition(eConditionType.ColumnName);
            }
            currentTree.RightCondition = SetConstMember(member);
            if (Check.IsNull(currentTree.RightCondition))
            {


                if (Check.IsNull(currentMember))
                {
                    currentMember = new MemberEntity();
                }
                currentMember.Member = member;
                currentTree.LeftCondition.Members.Push(currentMember);
            }
        }

        public static void MultipleBinary(this BinaryExpression node, ref TreeConditionEntity currentTree,
           eSignType signType, ref bool IsComplete, Action<Expression> Visitor,
           List<TreeConditionEntity> treeConditions,List<ConditionEntity>conditions)
        {
            if (Check.IsNull(Visitor))
            {
                throw new Exception("visitor is not null!");
            }
            if (IsComplete)
            {
                currentTree = GetTreeConditon();
               
                treeConditions.Add(currentTree);
               
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Left);
            currentTree.RightBracket.Add(eSignType.RightBracket);
            IsComplete = true;

            conditions.Add(GetCondition(eConditionType.Sign, signType));
           

            if (IsComplete)
            {
                currentTree = GetTreeConditon();
                treeConditions.Add(currentTree);
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Right);
            currentTree.RightBracket.Add(eSignType.RightBracket);
            IsComplete = true;
        }

        public static void SingleBinary(this BinaryExpression node, Action<Expression> Visitor,
           ref TreeConditionEntity currentTree, ref bool IsComplete, eSignType signType,
            List<TreeConditionEntity> treeConditions
           )
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            Visitor(node.Left);

            currentTree.RelationCondition = GetCondition(eConditionType.Sign, signType);

            Visitor(node.Right);

            IsComplete = true;
        }

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
        public static void SetConstantContValue(this TreeConditionEntity currentTree, ConstantExpression node)
        {
            currentTree.LeftCondition.SetConstantContValue(currentTree.RightCondition,node);
        }
        public static void SetConstantContValue(this ConditionEntity left,ConditionEntity right,ConstantExpression node)
        {
            if (!Check.IsNull(left))
            {
                if (!Check.IsNullOrEmpty(left.Members))
                {
                    object value = null;
                    MemberEntity meber =left.Members.Pop();
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
                        right.DisplayName = value.ToString();
                    }
                    else
                    {
                        right.Value = value;
                    }
                }
                else
                {
                    right.Value = node.Value.ToString();
                }

            }

        }

        public static TreeConditionEntity GetTreeConditon()
        {
            TreeConditionEntity treeCondition = new TreeConditionEntity();
            return treeCondition;
        }

        public static ConditionEntity GetCondition(eConditionType conditionType)
        {
            ConditionEntity condition = new ConditionEntity(conditionType);
            return condition;
        }

        public static ConditionEntity GetCondition(TableEntity table, string PropName, int Index, Type type, List<MapEntity> mapInfos)
        {
            ConditionEntity conditon = GetCondition(eConditionType.ColumnName);

            var maps = mapInfos.Where(m => m.PropName.Equals(PropName)).ToArray();

            if (!Check.IsNullOrEmpty(maps))
            {
                conditon.DisplayName=GetDisplayName(maps[0].TableName, maps[0].ColumnName);
            }
            else if (Index >= 0 && Index < table.TableNames.Length)
            {

                var Prop = GetPropertyType(table, Index, PropName);
                var NameEntity= table.GetTableName(Index);
                conditon.AsTableName = NameEntity.AsName;
                conditon.TableName = NameEntity.DisplayNmae;
                if (!Check.IsNull(Prop))
                {
                    conditon.ColumnName = Prop.GetColName();
                    conditon.DisplayName =GetDisplayName(conditon.AsTableName,conditon.ColumnName);
                }
            }
            conditon.PropertyType = type;
            return conditon;
        }

        public static string GetDisplayName(string TableName, string PropName)
        {
            if (Check.IsNullOrEmpty(TableName) || Check.IsNullOrEmpty(PropName))
            {
                throw new Exception("未找到属性名称或者表名称!");
            }
            return $"{TableName}{DBMDConst.Dot}{PropName}";
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

        public static PropertyInfo GetPropertyType(TableEntity table,int index, string PropName)
        {
            if (Check.IsNull(PropName))
            {
                throw new ArgumentException("PropName is not null!");
            }
            return GetEntityType(table,index).GetProperty(PropName);
        }
        public static Type GetEntityType(TableEntity table,int index)
        {
            if (table.TableNames.Length > index)
            {
                return table.DicTable[table.TableNames[index]].ClassType;
            }
            throw new Exception("表不存在!");
        }

        public static void TreeConditionInit(List<TreeConditionEntity> treeConditions,List<ConditionEntity> conditions, ref int firstConditionIndex)
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


    }
}
