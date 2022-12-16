using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;


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
    internal class ConditionVisitor : ExpressionVisitor, IExpressionVisitor
    {
        /// <summary>
        /// 当前等式
        /// </summary>
        private TreeConditionEntity currentTree;

        private List<TreeConditionEntity> treeConditions;
        private List<ConditionEntity> conditions;
        private List<MapEntity> mapInfos;
        private TableEntity table;
        /// <summary>
        /// 单个等式是否解析完成
        /// </summary>
        private bool IsComplete;

        /// <summary>
        /// 是否经过多次映射- 根据最后一次映射数据
        /// </summary>
        private bool IsMultipleMap { get; set; }

        /// <summary>
        /// 当前表达式目录树中表的别称
        /// </summary>
        private Dictionary<string, int> currentTables;

        private bool IsCompleteMember;

        private MemberEntity currentMember;

        /// <summary>
        /// 多重条件的时候
        /// </summary>
        /// 
        private int firstConditionIndex;

        private MethodVisitor MethodVisitor;


        public ConditionVisitor(List<TreeConditionEntity>_treeConditions,List<ConditionEntity>_conditions,TableEntity _table)
        {
            treeConditions = _treeConditions;
            conditions = _conditions;
            table= _table;
            Init();
        }
        public ConditionVisitor(TableEntity _table)
        {
            table = _table;
            Init();
        }
        public void Init()
        {
            IsComplete = true;
            IsMultipleMap = true;
            IsCompleteMember = true;
            firstConditionIndex = 0;
            currentTables = new Dictionary<string, int>();
        }

        public void InitMethodVisitor()
        {
            if (Check.IsNull(table))
            {
                throw new Exception(nameof(table));
            }
            if (Check.IsNull(MethodVisitor))
            {
                MethodVisitor = new MethodVisitor(table);
                MethodVisitor.InitConditionVisitor();
            }
        }
        private void InitModify(params MapEntity[] _mapInfos)
        {
            mapInfos = _mapInfos.ToList();
            IsComplete = true;
             currentTree = null;
            ConditionsExtension.TreeConditionInit(treeConditions, conditions, ref firstConditionIndex);
            IsMultipleMap = true;
        }

        /// <summary>
        /// get value
        /// </summary>
        /// <returns></returns>
        public Tuple<List<TreeConditionEntity>,List<ConditionEntity>> GetValue()
        {
            return Tuple.Create(treeConditions,conditions);
        }


        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression, params MapEntity[] _mapInfos)
        {
            currentTables.Clear();
            InitModify(_mapInfos);
            Type expType = expression.GetType();
            if (!Check.IsNull(expType.GetProperty("Parameters")))
            {
                foreach (ParameterExpression item in ((dynamic)expression).Parameters)
                {
                    currentTables.Add(item.Name, currentTables.Count);
                }
            }
            Visit(expression);
            return expression;
        }

        public Expression Modify(Expression expression,List<TreeConditionEntity>_treeConditions,List<ConditionEntity>_conditions,Dictionary<string, int>_tables, params MapEntity[] _mapInfos)
        {
            currentTables = _tables;
            conditions= _conditions;
            treeConditions  = _treeConditions;
            InitModify(_mapInfos);
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
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:
                    ConditionsExtension.MultipleBinary(node,ref currentTree, eSignType.And,ref IsComplete, (Node) =>
                    { 
                        base.Visit(Node);
                    }
                    ,treeConditions,conditions);
                    break;
                case ExpressionType.Call:
                    break;
                case ExpressionType.GreaterThan:
                    ConditionsExtension.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.GrantThan,treeConditions);

                    break;
                case ExpressionType.GreaterThanOrEqual:
                    ConditionsExtension.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.GreatThanOrEqual, treeConditions);
                    break;
                case ExpressionType.LessThan:
                    ConditionsExtension.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.LessThan, treeConditions);
                    break;
                case ExpressionType.LessThanOrEqual:
                    ConditionsExtension.SingleBinary(node, (Node) => base.Visit(Node),ref  currentTree,ref IsComplete, eSignType.LessThanOrEqual,treeConditions);
                    break;
                case ExpressionType.Equal:
                    ConditionsExtension.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.Equal,treeConditions);
                    break;
                case ExpressionType.NotEqual:
                    ConditionsExtension.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.NotEqual,treeConditions);
                    break;
                case ExpressionType.OrElse:
                    ConditionsExtension.MultipleBinary(node,ref currentTree, eSignType.Or,ref IsComplete, (Node) =>
                    {
                     base.Visit(Node); 
                    },treeConditions,conditions);
                    break;
                case ExpressionType.ArrayIndex:
                    if (Check.IsNull(currentMember))
                    {
                        if (IsCompleteMember)
                        {
                            currentMember = new MemberEntity();
                            IsCompleteMember = false;
                        }

                    }
                    if (node.Right is ConstantExpression constant)
                    {
                        if (!Check.IsNull(currentMember))
                        {
                            currentMember.OParams = constant.Value;
                        }
                    }
                    base.VisitBinary(node);
                    break;
                default:
                    break;
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
            node.VisitConstant(treeConditions,ref currentTree);
            base.VisitConstant(node);
            return node;

        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = ConditionsExtension.GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            if (Check.IsNull(currentTree.RelationCondition))
            {
                currentTree.RelationCondition = ConditionsExtension.GetCondition(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;
            }
            if (MysqlConst.dicMethods.ContainsKey(node.Method.Name))
            {
                if (Check.IsNull(currentTree.LeftCondition))
                {
                    currentTree.LeftCondition = ConditionsExtension.GetCondition(eConditionType.Method);
                    MethodVisitor.Modify(node, currentTree.LeftCondition.Methods, currentTables);
                    if (!Check.IsNullOrEmpty(currentTree.LeftCondition.Methods) && !Check.IsNullOrEmpty(currentTree.LeftCondition.Methods[0].Parameters))
                    {
                        MethodParamsSetTreeCondition(currentTree.LeftCondition.Methods[0].Parameters[0]);
                        if (currentTree.LeftCondition.Methods[0].Parameters.Count == 2)
                        {
                            currentTree.RightCondition = ConditionsExtension.GetCondition(eConditionType.Method);
                            MethodParamsSetTreeCondition(currentTree.LeftCondition.Methods[0].Parameters[1]);
                        }

                    }
                }
                else if (Check.IsNull(currentTree.RightCondition))
                {
                    currentTree.RightCondition = ConditionsExtension.GetCondition(eConditionType.Method);
                    MethodVisitor.Modify(node, currentTree.RightCondition.Methods, currentTables);
                    if (!Check.IsNullOrEmpty(currentTree.RightCondition.Methods) && !Check.IsNullOrEmpty(currentTree.RightCondition.Methods[0].Parameters))
                    {
                        currentTree.RightCondition.Value = currentTree.RelationCondition.Methods[0].Parameters[0].Value;
                        currentTree.RightCondition.DisplayName = currentTree.RightCondition.Methods[0].Parameters[0].DisplayName;
                        currentTree.RightCondition.TableName = currentTree.RightCondition.Methods[0].Parameters[0].TableName;
                        currentTree.RightCondition.AsTableName = currentTree.RightCondition.Methods[0].Parameters[0].AsTableName;
                    }
                }
            }

            else
            {
                node.VisitMethod(ref currentTree, ref IsComplete, ref IsCompleteMember, ref currentMember, treeConditions);
                base.VisitMethodCall(node);
            }
            return node;
        }

       
        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            node.VisitUnary(ref currentTree,ref IsComplete);
            return node;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            node.VisitMember(currentTables,ref currentTree,table,mapInfos,ref currentMember,ref IsCompleteMember);
            base.VisitMember(node);
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
        private void MethodParamsSetTreeCondition(ConditionEntity condition)
        {
            if (condition.ConditionType==eConditionType.Constant)
            {
                currentTree.LeftCondition.Value = condition.Value;
                currentTree.LeftCondition.DisplayName = condition.DisplayName;
                currentTree.LeftCondition.TableName = condition.TableName;
                currentTree.LeftCondition.AsTableName = condition.AsTableName;
            }
            else if (condition.ConditionType == eConditionType.ColumnName)
            {
                currentTree.RightCondition = ConditionsExtension.GetCondition(eConditionType.Method);
                currentTree.RightCondition.Value = condition.Value;
                currentTree.RightCondition.DisplayName = condition.DisplayName;
                currentTree.RightCondition.TableName = condition.TableName;
                currentTree.RightCondition.AsTableName = condition.AsTableName;
            }
        }
    }
}
