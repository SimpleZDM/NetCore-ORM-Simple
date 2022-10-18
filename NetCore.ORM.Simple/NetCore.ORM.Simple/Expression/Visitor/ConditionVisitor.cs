using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;


/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 MatchConditionExpress
 * 开发人员：-nhy
 * 创建时间：2022/9/19 14:08:26
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public class ConditionVisitor : ExpressionVisitor, IExpressionVisitor
    {
     
       
        /// <summary>
        /// 当前等式
        /// </summary>
        private TreeConditionEntity currentTree;
        /// <summary>
        /// 单个等式是否解析完成
        /// </summary>
        private bool IsComplete;

        /// <summary>
        /// 是否经过多次映射- 根据最后一次映射数据
        /// </summary>
        private bool IsMultipleMap;

        /// <summary>
        /// 当前表达式目录树中表的别称
        /// </summary>
        private Dictionary<string, int> currentTables;

        /// <summary>
        /// 多重条件的时候
        /// </summary>
        /// 
        private int firstConditionIndex;

        private SelectEntity select;
        public ConditionVisitor(SelectEntity select)
        {
            this.select=select;
            currentTables = new Dictionary<string, int>();
            IsComplete = true;
            IsMultipleMap = true;
            firstConditionIndex = 0;
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
           return String.Empty;
        }
      
       
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression)
        {
            currentTables.Clear();
            currentTree = null;
            select.TreeConditionInit(ref firstConditionIndex);
            IsMultipleMap = true;

            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                currentTables.Add(item.Name, currentTables.Count);
            }
            Visit(expression);

            return expression;
        }

        /// <summary>
        /// 表达式树的二元操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (currentTables.Count() > CommonConst.ZeroOrNull)
            {
                #region
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        SingleLogicBinary(node, (queue) =>
                        {
                            select.CreateCondition(new 
                            ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.And
                            });
                        });
                        break;
                    case ExpressionType.Call:

                        break;
                    case ExpressionType.GreaterThan:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition =
                               new ConditionEntity(eConditionType.Sign)
                               {
                                   SignType = eSignType.GrantThan
                               };
                        });
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition =
                              new ConditionEntity(eConditionType.Sign)
                              {
                                  SignType = eSignType.GreatThanOrEqual
                              };
                        });
                        break;
                    case ExpressionType.LessThan:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition =
                              new ConditionEntity(eConditionType.Sign)
                              {
                                  SignType = eSignType.LessThan
                              };
                        });
                        break;
                    case ExpressionType.LessThanOrEqual:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition =
                             new ConditionEntity(eConditionType.Sign)
                             {
                                 SignType = eSignType.LessThanOrEqual
                             };
                        });
                        break;
                    case ExpressionType.Equal:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition =
                             new ConditionEntity(eConditionType.Sign)
                             {
                                 SignType = eSignType.Equal
                             };
                        });
                        break;
                    case ExpressionType.NotEqual:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition =
                            new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.NotEqual
                            };
                        });
                        break;
                    case ExpressionType.OrElse:
                        SingleLogicBinary(node, (queue) =>
                        {
                            select.CreateCondition(
                            new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.Or
                            });
                           
                        });
                        break;
                    case ExpressionType.ArrayIndex:
                        int index = 0;
                        int.TryParse(node.Right.ToString(), out index);
                        if (!Check.IsNull(currentTree.LeftCondition))
                        {
                            currentTree.LeftCondition.Index = index;
                        }
                        base.Visit(node.Left);
                        break;
                    default:
                        break;
                }
                #endregion
            }
            return node;
        }
        /// <summary>
        /// 表达式树的常量操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = select.CreateTreeConditon();
                currentTree.LeftCondition = new ConditionEntity(eConditionType.Constant);
                currentTree.LeftCondition.DisplayName = $"{node.Value}";
                return node;
            }
            if (Check.IsNull(currentTree.RightCondition))
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);
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
                return base.VisitConstant(node);
            }
            if (!Check.IsNull(currentTree.LeftCondition.ConstPropType))
            {
                if (!Check.IsNull(currentTree.LeftCondition.ConstFieldType))
                {
                    var obj = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);

                    if (currentTree.LeftCondition.Index >= 0)
                    {
                        currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.LeftCondition.Index, obj, currentTree.LeftCondition.ConstPropType);
                        if (Check.IsNull(currentTree.RightCondition.DisplayName))
                        {
                            var currentobj = ((dynamic)obj)[currentTree.LeftCondition.Index];
                            currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstPropType.GetValue(currentobj).ToString();
                        }
                    }
                    else if (!Check.IsNullOrEmpty(currentTree.LeftCondition.Key))
                    {
                        currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.LeftCondition.Key, obj, currentTree.LeftCondition.ConstPropType);
                    }
                    else
                    {
                        currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstPropType.GetValue(obj).ToString();
                    }
                }

            }
            else if (!Check.IsNull(currentTree.LeftCondition.ConstFieldType) && currentTree.LeftCondition.ConstFieldType.Count > 0)
            {
                if (!Check.IsNullOrEmpty(currentTree.RelationCondition.DisplayName)&&currentTree.RelationCondition.DisplayName.Equals("Contains"))
                {

                    currentTree.RightCondition.Value= currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                    currentTree.RightCondition.PropertyType = currentTree.RightCondition.Value.GetType();
                }
                else if (currentTree.LeftCondition.ConstFieldType.Count >= 2)
                {
                    var obj = currentTree.LeftCondition.ConstFieldType[1].GetValue(node.Value);
                    if (currentTree.LeftCondition.Index >= 0)
                    {
                        currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstFieldType[0].GetValue(((dynamic)obj)[currentTree.LeftCondition.Index]).ToString();
                    }
                    else
                    {

                        currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstFieldType[0].GetValue(obj).ToString();
                    }

                }
                else if (currentTree.LeftCondition.Index >= 0)
                {
                    var obj = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                    currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.LeftCondition.Index, obj);
                }
                else if (!Check.IsNullOrEmpty(currentTree.LeftCondition.Key))
                {
                    var obj = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                    currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.LeftCondition.Key, obj);
                }
                else
                {
                    currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value).ToString();
                }

            }
            else
            {
                currentTree.RightCondition.DisplayName = node.Value.ToString();
            }
            base.VisitConstant(node);
            IsComplete = true;
            return node;
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (currentTables.Count > CommonConst.ZeroOrNull)
            {
                if (Check.IsNull(currentTree))
                {
                    currentTree =select.CreateTreeConditon();
                }
                else
                {
                    if (!Check.IsNull(currentTree.LeftCondition))
                    {
                        if (node.Arguments.Count() >= 1)
                        {
                            int value = 0;
                            if (int.TryParse(node.Arguments[0].ToString(), out value))
                            {
                                currentTree.LeftCondition.Index = value;
                            }
                            else
                            {
                                if (node.Arguments[0] is ConstantExpression content)
                                {
                                    currentTree.LeftCondition.Key = content.Value.ToString();
                                }
                            }

                        }

                    }
                }
                if (Check.IsNull(currentTree.RelationCondition))
                {
                    currentTree.RelationCondition = new ConditionEntity(eConditionType.Method);
                    currentTree.RelationCondition.DisplayName = node.Method.Name;
                }

            }
            return base.VisitMethodCall(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (!currentTables.ContainsKey(node.Name))
            {
                currentTables.Add(node.Name, currentTables.Count());
            }
            return base.VisitParameter(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }
        protected override Expression VisitMember(MemberExpression node)
        {

            PropertyInfo Prop = null;
            string PropName = node.Member.Name;
            string TableName;
            if (currentTables.Count > CommonConst.ZeroOrNull)
            {

                if (select.MapInfos.Where(m => m.PropName.Equals(PropName)).Any() &&select.MapInfos.Where(m=>m.PropName.Equals(PropName)).Count()==1)
                {
                    var Map = select.MapFirstOrDefault(m => m.PropName.Equals(PropName));
                    CreateCondition(Map, node.Type, eConditionType.ColumnName);
                }
                else if (currentTables.ContainsKey(node.Expression.ToString()))
                {
                    int index=currentTables[node.Expression.ToString()];
                    Prop = select.GetPropertyType(index,node.Member.Name);
                    TableName = select.GetTableName(index);
                    if (!Check.IsNull(Prop))
                    {
                        PropName = select.GetColumnName(index,node.Member.Name);
                        CreateCondition(TableName,PropName,node.Type,eConditionType.ColumnName);
                    }
                }
                else
                {
                    if (node.Member is FieldInfo field)
                    {
                        if (Check.IsNull(currentTree.LeftCondition))
                        {
                            currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                        }
                        currentTree.LeftCondition.ConstFieldType.Add(field);
                    }
                    else if (node.Member is PropertyInfo prop)
                    {
                        if (Check.IsNull(currentTree.LeftCondition))
                        {
                            currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                        }
                        currentTree.LeftCondition.ConstPropType = prop;
                    }
                }
                base.VisitMember(node);
            }

            return node;
        }


        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return node;
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return node;
        }
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return node;
        }
        /// <summary>
        /// 解析条件表达式大于小于等于
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree=select.CreateTreeConditon();
            }
            if (node.Left is MemberExpression leftMember)
            {
                currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                currentTree.LeftCondition.PropertyType = leftMember.Type;
                GetMemberValue(leftMember, currentTree.LeftCondition);
            }
            else if (node.Left is ConstantExpression leftConstant)
            {
                currentTree.LeftCondition = new ConditionEntity(eConditionType.Constant);
                GetConstantValue(leftConstant, currentTree.LeftCondition);
            }
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            if (node.Right is ConstantExpression rightConstant)
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);
                GetConstantValue(rightConstant, currentTree.RightCondition);

            }
            else if (node.Right is MemberExpression rightMember)
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.ColumnName);
                GetMemberValue(rightMember, currentTree.RightCondition);
            }
            IsComplete = true;
        }

        /// <summary>
        /// 解析 或与非
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleLogicBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            if (IsComplete)
            {
                currentTree=select.CreateTreeConditon();
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            base.Visit(node.Left);
            currentTree.RightBracket.Add(eSignType.RightBracket);
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            if (IsComplete)
            {
                currentTree = select.CreateTreeConditon();
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            base.Visit(node.Right);
            currentTree.RightBracket.Add(eSignType.RightBracket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetMemberValue(MemberExpression member, ConditionEntity condition)
        {
            string mName = string.Empty;
            if (!Check.IsNull(member))
            {

                if (IsMultipleMap)
                {
                    var mapInfo = select.MapFirstOrDefault(map => map.PropName.Equals(member.Member.Name));
                    if (!Check.IsNull(mapInfo))
                    {
                        condition.DisplayName = $"{mapInfo.TableName}.{mapInfo.ColumnName}";
                    }
                    else
                    {
                        VisitMember(member);
                    }
                }
                else
                {
                    if (tableNames.TableNames.Length > SimpleConst.minTableCount)
                    {
                        if (!Check.IsNull(member.Expression))
                        {
                            if (currentTables.ContainsKey(member.Expression.ToString()))
                            {
                                mName = $"{tableNames.TableNames[currentTables[member.Expression.ToString()]]}.{member.Member.Name}";
                                condition.DisplayName = mName;
                            }
                            else
                            {
                                VisitMember(member);
                            }
                        }
                        else
                        {
                            if (member.ToString().Equals("DateTime.Now"))
                            {
                                condition.DisplayName = DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                                condition.ConditionType = eConditionType.Constant;

                            }
                            else if (member.ToString().Equals("DateTime.Now"))
                            {

                            }
                        }

                    }
                    else
                    {
                        mName = member.Member.Name;
                    }
                }
            }
            return condition.DisplayName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private string GetConstantValue(ConstantExpression constant, ConditionEntity condition)
        {
            string mName = string.Empty;
            if (!Check.IsNull(constant))
            {

                if (constant.Type == typeof(string))
                {
                    condition.DisplayName = $"'{constant.Value}'";
                }
                else
                {
                    condition.DisplayName = $"{constant.Value}";
                }
            }
            return condition.DisplayName;
        }

        private void CreateCondition(string TableName, string ColumnName, Type type,eConditionType conditionType)
        {
            if (!Check.IsNull(currentTree))
            {
                currentTree.LeftCondition = new ConditionEntity(conditionType);
                currentTree.LeftCondition.DisplayName = $"{TableName}.{ColumnName}";
                currentTree.LeftCondition.PropertyType = type;
            }
        }
        private void CreateCondition(MapEntity Map, Type type, eConditionType conditionType)
        {
            if (!Check.IsNull(currentTree))
            {
                currentTree.LeftCondition = new ConditionEntity(conditionType);
                currentTree.LeftCondition.DisplayName = $"{Map.TableName}.{Map.ColumnName}";
                currentTree.LeftCondition.PropertyType = type;
            }
        }
    }
}
