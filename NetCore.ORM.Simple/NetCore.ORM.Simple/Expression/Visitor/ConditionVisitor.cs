using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        /// 逻辑条件关联符号
        /// </summary>
        private List<ConditionEntity> conditions;
        /// <summary>
        /// 单个表达式集合
        /// </summary>
        private List<TreeConditionEntity> treeConditions;
        /// <summary>
        /// 当前等式
        /// </summary>
        private TreeConditionEntity currentTree;
        /// <summary>
        /// 单个等式是否解析完成
        /// </summary>
        private bool IsComplete;

        /// </summary>
        private List<MapEntity> mapInfos;
        /// <summary>
        /// 是否经过多次映射- 根据最后一次映射数据
        /// </summary>
        private bool IsMultipleMap;
        /// <summary>
        /// 初始化是包含的所有表
        /// </summary>
        private TableEntity tableNames;

        /// <summary>
        /// 当前表达式目录树中表的别称
        /// </summary>
        private Dictionary<string, int> currentTables;

        private int firstConditionIndex;
        public ConditionVisitor(params string[] _tableNames)
        {
            if (Check.IsNull(_tableNames))
            {
                throw new ArgumentException("not table names!");
            }
            tableNames = new TableEntity(_tableNames);
            currentTables = new Dictionary<string, int>();
            conditions = new List<ConditionEntity>();
            treeConditions = new List<TreeConditionEntity>();
            IsComplete = true;
            IsMultipleMap = false;
            firstConditionIndex = 0;
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            StringBuilder values = new StringBuilder();
            for (int i = 0; i < treeConditions.Count; i++)
            {
                foreach (var item in treeConditions[i].LeftBracket)
                {
                    values.Append(SimpleConst.cStrSign[(int)item]);
                }

                values.Append(treeConditions[i].LeftCondition.DisplayName);

                values.Append(SimpleConst.cStrSign[(int)treeConditions[i].RelationCondition.SignType]);

                values.Append(treeConditions[i].RightCondition.DisplayName);

                foreach (var item in treeConditions[i].RightBracket)
                {
                    values.Append(SimpleConst.cStrSign[(int)item]);
                }
                if (conditions.Count > i)
                {
                    values.Append(SimpleConst.cStrSign[(int)conditions[i].SignType]);
                }
            }
            return values.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tuple<List<ConditionEntity>, List<TreeConditionEntity>> GetCondition()
        {
            return Tuple.Create(conditions, treeConditions);
        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression, List<MapEntity> mapInfos = null)
        {
            currentTables.Clear();
            currentTree = null;


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
            if (!Check.IsNull(mapInfos) && mapInfos.Count > 0)
            {
                this.mapInfos = mapInfos;
                IsMultipleMap = true;
            }
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
                            conditions.Add(new ConditionEntity(eConditionType.Sign)
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
                            conditions.Add(new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.Or
                            });
                        });
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
            currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);
            currentTree.RightCondition.DisplayName = node.Value.ToString();
            return base.VisitConstant(node);
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (currentTables.Count>CommonConst.ZeroOrNull)
            {
                if (Check.IsNull(currentTree))
                {
                    currentTree = new TreeConditionEntity();
                }
                base.VisitMethodCall(node);
                currentTree.RelationCondition = new ConditionEntity(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;
                treeConditions.Add(currentTree);
            }
            return node;
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
            Console.WriteLine(node.ToString());
            return base.VisitUnary(node);
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            if (currentTables.Count > CommonConst.ZeroOrNull)
            {
                base.VisitMember(node);
                if (currentTables.ContainsKey(node.Expression.ToString()))
                {
                    currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                    currentTree.LeftCondition.DisplayName = $"{tableNames.TableNames[currentTables[node.Expression.ToString()]]}.{node.Member.Name}";
                    currentTree.LeftCondition.PropertyType = node.Type;
                }
            }

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
                currentTree = new TreeConditionEntity();
            }
            if (node.Left is MemberExpression leftMember)
            {
                currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
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
            treeConditions.Add(currentTree);
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
                currentTree = new TreeConditionEntity();
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
                currentTree = new TreeConditionEntity();
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
                    var mapInfo = mapInfos.FirstOrDefault();
                    if (!Check.IsNull(mapInfo))
                    {
                        condition.DisplayName = $"{mapInfo.TableName}.{mapInfo.ColumnName}";
                    }
                }
                else
                {
                    if (tableNames.TableNames.Length > SimpleConst.minTableCount)
                    {
                        if (currentTables.ContainsKey(member.Expression.ToString()))
                        {
                            mName = $"{tableNames.TableNames[currentTables[member.Expression.ToString()]]}.{member.Member.Name}";
                            condition.DisplayName = mName;
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
    }
}
