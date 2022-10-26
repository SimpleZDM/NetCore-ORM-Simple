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
        private bool IsMultipleMap { get; set; }

        /// <summary>
        /// 当前表达式目录树中表的别称
        /// </summary>
        private Dictionary<string, int> currentTables;

        private bool IsCompleteMember;

        private NetCore.ORM.Simple.Entity.MemberEntity currentMember;

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
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:
                    select.MultipleBinary(node,ref currentTree, eSignType.And,ref IsComplete, (Node) =>
                    { 
                        base.Visit(Node);
                    }
                    );
                    break;
                case ExpressionType.Call:
                    break;
                case ExpressionType.GreaterThan:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.GrantThan);

                    break;
                case ExpressionType.GreaterThanOrEqual:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.GreatThanOrEqual);
                    break;
                case ExpressionType.LessThan:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.LessThan);
                    break;
                case ExpressionType.LessThanOrEqual:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref  currentTree,ref IsComplete, eSignType.LessThanOrEqual);
                    break;
                case ExpressionType.Equal:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.Equal);
                    break;
                case ExpressionType.NotEqual:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete, eSignType.NotEqual);
                    break;
                case ExpressionType.OrElse:
                    select.MultipleBinary(node,ref currentTree, eSignType.Or,ref IsComplete, (Node) =>
                    {
                     base.Visit(Node); 
                    });
                    break;
                case ExpressionType.ArrayIndex:
                    if (Check.IsNull(currentMember))
                    {
                        if (IsCompleteMember)
                        {
                            currentMember = new Simple.Entity.MemberEntity();
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
            select.VisitConstant(currentTree, node);
            base.VisitConstant(node);
            return node;

        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);
            select.VisitMethod(ref currentTree, node,ref IsCompleteMember,ref currentMember);
            IsComplete = true;
            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
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
            return node;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            string PropName = node.Member.Name;
            if ((node.Expression is ParameterExpression Parameter) && currentTables.ContainsKey(Parameter.Name))
            {
                int Index = currentTables[Parameter.Name];
                currentTree.LeftCondition=select.GetCondition(PropName, Index, node.Type);
            }
            else
            {
                select.VisitMember(currentTree, node.Member, IsCompleteMember, currentMember);
            }
            IsCompleteMember = true;
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

    }
}
