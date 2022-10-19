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
            this.select = select;
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
                        
                        select.SingleLogicBinary(node,ref currentTree, eSignType.And, ref IsComplete, (mynode,tree) => { 
                            base.Visit(mynode); });
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
                        select.SingleLogicBinary(node,ref currentTree, eSignType.Or, ref IsComplete, (mynode,tree) => { base.Visit(mynode); });
                        break;
                    case ExpressionType.ArrayIndex:
                        int index = 0;
                        int.TryParse(node.Right.ToString(), out index);
                        if (!Check.IsNull(currentTree))
                        {
                            currentTree.Index = index;
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
                IsComplete = true;
                return node;
            }
            if (select.VisitConstant(ref currentTree,node))
            {
                base.VisitConstant(node);
                IsComplete = true;
                return node;
            }
            return base.VisitConstant(node);

        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            select.VisitMethod(ref currentTree,node);
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
                var Map = select.MapFirstOrDefault(m => m.PropName.Equals(PropName));
                if (!Check.IsNull(Map))
                {
                    CreateCondition(Map, node.Type, eConditionType.ColumnName);
                }
                else if (currentTables.ContainsKey(node.Expression.ToString()))
                {
                    int index = currentTables[node.Expression.ToString()];
                    Prop = select.GetPropertyType(index, node.Member.Name);
                    TableName = select.GetTableName(index);
                    if (!Check.IsNull(Prop))
                    {
                        PropName = select.GetColumnName(index, node.Member.Name);
                        CreateCondition(TableName, PropName, node.Type, eConditionType.ColumnName);
                    }
                }
                select.VisitMember(ref currentTree,node.Member);
                base.VisitMember(node);
            }
            IsComplete = true;
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
                currentTree = select.CreateTreeConditon();
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
                select.GetConstantValue(leftConstant,currentTree.LeftCondition);
            }
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            if (node.Right is ConstantExpression rightConstant)
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);
                select.GetConstantValue(rightConstant,currentTree.RightCondition);

            }
            else if (node.Right is MemberExpression rightMember)
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.ColumnName);
                GetMemberValue(rightMember, currentTree.RightCondition);
            }
            IsComplete = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private void GetMemberValue(MemberExpression member, ConditionEntity condition)
        {
            if (currentTables.ContainsKey(member.Expression.ToString()))
            {
                if (!select.GetMemberValue(member,ref condition, currentTables[member.Expression.ToString()], IsMultipleMap))
                {
                    VisitMember(member);
                }
            }
            else
            {
                VisitMember(member);
            }
        }
        private void CreateCondition(string TableName, string ColumnName, Type type, eConditionType conditionType)
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
