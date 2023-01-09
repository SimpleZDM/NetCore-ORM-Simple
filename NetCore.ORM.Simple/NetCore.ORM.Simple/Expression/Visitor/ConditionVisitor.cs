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
        private ContextSelect contextSelect;
        /// <summary>
        /// 单个等式是否解析完成
        /// </summary>
        private bool IsComplete;
        private bool IsMultipleMap;
        /// <summary>
        /// 是否经过多次映射- 根据最后一次映射数据
        /// </summary>
        /// <summary>
        /// 当前表达式目录树中表的别称
        /// </summary>
        private Dictionary<string, int> TableParams;
        private bool IsCompleteMember;
        private MemberEntity currentMember;
        /// <summary>
        /// 多重条件的时候
        /// </summary>
        /// 
        private int firstConditionIndex;
        private MethodVisitor MethodVisitor;
        /// <summary>
        /// 
        /// </summary>
        public ConditionVisitor()
        {
            Init();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_select"></param>
        public ConditionVisitor(ContextSelect _select)
        {
            contextSelect = _select;
            treeConditions = _select.TreeConditions;
            conditions = _select.Conditions;
            Init();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            IsComplete = true;
            IsMultipleMap = true;
            IsCompleteMember = true;
            firstConditionIndex = 0;
            TableParams = new Dictionary<string, int>();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitModify()
        {
            IsComplete = true;
            currentTree = null;
            ConditionsExtension.TreeConditionInit(treeConditions, conditions, ref firstConditionIndex);
            IsMultipleMap = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitMethodVisitor()
        {

            if (Check.IsNull(MethodVisitor))
            {
                MethodVisitor = new MethodVisitor(contextSelect);
                MethodVisitor.InitConditionVisitor();
            }
        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression)
        {
            TableParams.Clear();
            InitModify();
            Type expType = expression.GetType();
            if (!Check.IsNull(expType.GetProperty("Parameters")))
            {
                foreach (ParameterExpression item in ((dynamic)expression).Parameters)
                {
                    TableParams.Add(item.Name, TableParams.Count);
                }
            }
            Visit(expression);
            return expression;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="_treeConditions"></param>
        /// <param name="_conditions"></param>
        /// <param name="_tables"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression, List<TreeConditionEntity> _treeConditions, List<ConditionEntity> _conditions, Dictionary<string, int> _tables)
        {
            if (Check.IsNullOrEmpty(_tables))
            {
                if (!Check.IsNull(expression.GetType().GetProperty("Parameters")))
                {
                    foreach (ParameterExpression item in ((dynamic)expression).Parameters)
                    {
                        TableParams.Add(item.Name, TableParams.Count);
                    }
                }
            }
            TableParams = _tables;
            conditions = _conditions;
            treeConditions = _treeConditions;
            InitModify();
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
            CustomerVisitBinary(node);
            return node;
        }
        /// <summary>
        /// 表达式树的常量操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            CustomerVisitConstant(node);
            base.VisitConstant(node);
            currentMember = null;
            return node;

        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = contextSelect.GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            if (Check.IsNull(currentTree.RelationCondition))
            {
                currentTree.RelationCondition = contextSelect.GetCondition(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;
            }


            if (MysqlConst.dicMethods.ContainsKey(node.Method.Name))
            {
                var condition=contextSelect.GetCondition(eConditionType.Method);

                MethodVisitor.Modify(node,condition.Methods, TableParams);
                if (Check.IsNull(currentTree.LeftCondition))
                {
                    currentTree.LeftCondition = condition;
                    MethodParamsSetTreeCondition(condition,true);
                }
                else if (currentTree.RelationCondition.ConditionType==eConditionType.Sign)
                {
                    currentTree.RightCondition = condition;
                    MethodParamsSetTreeCondition(condition,false);
                }
            }
            else
            {
                CustomerMethod(node);
                base.VisitMethodCall(node);
            }
            return node;
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            node.VisitUnary(ref currentTree, ref IsComplete);
            return node;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            CustomerVisitMember(node);
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

        #region extension method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void CustomerVisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:
                    MultipleBinary(node, eSignType.And, (Node) =>
                    {
                        base.Visit(Node);
                    });
                    break;
                case ExpressionType.Call:
                    break;
                case ExpressionType.GreaterThan:
                    SingleBinary(node, (Node) => base.Visit(Node), eSignType.GrantThan);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    SingleBinary(node, (Node) => base.Visit(Node), eSignType.GreatThanOrEqual);
                    break;
                case ExpressionType.LessThan:
                    SingleBinary(node, (Node) => base.Visit(Node), eSignType.LessThan);
                    break;
                case ExpressionType.LessThanOrEqual:
                    SingleBinary(node, (Node) => base.Visit(Node), eSignType.LessThanOrEqual);
                    break;
                case ExpressionType.Equal:
                    SingleBinary(node, (Node) => base.Visit(Node), eSignType.Equal);
                    break;
                case ExpressionType.NotEqual:
                    SingleBinary(node, (Node) => base.Visit(Node), eSignType.NotEqual);
                    break;
                case ExpressionType.OrElse:
                    MultipleBinary(node, eSignType.Or, (Node) =>
                    {
                        base.Visit(Node);
                    });
                    break;
                case ExpressionType.ArrayIndex:
                        if (IsCompleteMember)
                        {
                            currentMember = new MemberEntity();
                            IsCompleteMember = false;
                        }
                    if (node.Right is ConstantExpression constant)
                    {
                        if (!Check.IsNull(currentMember))
                        {
                            currentMember.OParams.Add(constant.Value);
                        }
                    }
                    base.VisitBinary(node);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void CustomerVisitMember(MemberExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = contextSelect.GetTreeConditon();
            }
            string PropName = node.Member.Name;
            if ((node.Expression is ParameterExpression Parameter) && TableParams.ContainsKey(Parameter.Name))
            {
                int Index = TableParams[Parameter.Name];
                ConditionEntity condition= contextSelect.GetCondition(PropName, Index);

                if (!Check.IsNull(currentTree.RightCondition)||Check.IsNull(currentTree.LeftCondition))
                {
                   currentTree.LeftCondition = condition;
                }
                else
                {
                    currentTree.RightCondition = condition;
                }
            }
            else
            {
                CustomerVisitMember(node.Member, currentMember);
            }
            IsCompleteMember = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void CustomerMethod(MethodCallExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = contextSelect.GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            if (Check.IsNull(currentTree.RelationCondition))
            {
                currentTree.RelationCondition = contextSelect.GetCondition(eConditionType.Method);// new ConditionEntity(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;

            }
            if (!Check.IsNullOrEmpty(node.Arguments))
            {
                if (node.Arguments[0] is MethodCallExpression call)
                {

                    if (IsCompleteMember)
                    {
                        currentMember = new MemberEntity();
                        IsCompleteMember = false;
                    }
                    currentMember.OParams.Add(call.Arguments[0]);

                }
                if (node.Arguments[0] is ConstantExpression constant)
                {

                    if (IsCompleteMember)
                    {
                        currentMember = new MemberEntity();
                        IsCompleteMember = false;
                    }
                    currentMember.OParams.Add(constant.Value);
                }

            }
            IsComplete = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="currentMember"></param>
        public void CustomerVisitMember(MemberInfo member, MemberEntity currentMember)
        {
            if (Check.IsNull(currentTree.LeftCondition))
            {
                currentTree.LeftCondition = contextSelect.GetCondition(eConditionType.ColumnName);
            }
            currentTree.RightCondition = ConditionsExtension.SetConstMember(contextSelect,member);
            if (Check.IsNull(currentTree.RightCondition))
            {


                if (Check.IsNull(currentMember)||IsCompleteMember)
                {
                    currentMember = new MemberEntity();
                }
                currentMember.Member = member;
                currentTree.LeftCondition.Members.Push(currentMember);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public  bool CustomerVisitConstant(ConstantExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = contextSelect.GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            if (Check.IsNull(currentTree.RightCondition))
            {
                currentTree.RightCondition = contextSelect.GetCondition(eConditionType.Constant);
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
                    currentTree.RightCondition.Value = node.Value;
            }
            else
            {
                ConditionsExtension.SetConstantContValue(currentTree, node);
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="signType"></param>
        /// <param name="Visitor"></param>
        /// <exception cref="Exception"></exception>
        public void MultipleBinary(BinaryExpression node, eSignType signType, Action<Expression> Visitor)
        {
            if (Check.IsNull(Visitor))
            {
                throw new Exception("visitor is not null!");
            }
            if (IsComplete)
            {
                currentTree = contextSelect.GetTreeConditon();

                treeConditions.Add(currentTree);

                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Left);
            currentTree.RightBracket.Add(eSignType.RightBracket);
            IsComplete = true;

            conditions.Add(contextSelect.GetCondition(eConditionType.Sign, signType));


            if (IsComplete)
            {
                currentTree = contextSelect.GetTreeConditon();
                treeConditions.Add(currentTree);
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Right);
            currentTree.RightBracket.Add(eSignType.RightBracket);
            IsComplete = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="Visitor"></param>
        /// <param name="signType"></param>
        public void SingleBinary(BinaryExpression node, Action<Expression> Visitor, eSignType signType)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = contextSelect.GetTreeConditon();
                treeConditions.Add(currentTree);
            }
            Visitor(node.Left);

            currentTree.RelationCondition = contextSelect.GetCondition(eConditionType.Sign, signType);

            Visitor(node.Right);

            IsComplete = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="IsLeft"></param>
        private void MethodParamsSetTreeCondition(ConditionEntity condition,bool IsLeft)
        {
            bool IsInit = false;
            if (!Check.IsNullOrEmpty(condition.Methods))
            {
                foreach (var method in condition.Methods)
                {

                    if (!Check.IsNullOrEmpty(method.Parameters))
                    {
                        foreach (var item in method.Parameters)
                        {

                            if (!IsInit && Check.IsNullOrEmpty(condition.DisplayName) && item.ConditionType == eConditionType.ColumnName)
                            {

                                item.CopyColumnProperty(condition);
                                IsInit = true;
                            }
                            else if (IsLeft && Check.IsNull(currentTree.RightCondition) && item.ConditionType == eConditionType.Constant)
                            {
                                if (currentTree.RelationCondition.ConditionType == eConditionType.Method)
                                {
                                    currentTree.RightCondition = contextSelect.GetCondition(eConditionType.Method);
                                    item.CopyColumnProperty(currentTree.RightCondition);
                                }

                            }
                        }
                    }

                }

            }
        }
        #endregion
    }
}
