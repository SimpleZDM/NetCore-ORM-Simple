using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
//using MemberEntity= NetCore.ORM.Simple.Entity.MemberEntity;
/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 JoinVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/19 14:11:42
 * 描述说明：解析多表查询连接部分（Left Join,Innor Join）
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public class JoinVisitor : ExpressionVisitor, IExpressionVisitor
    {
        /// <summary>
        /// 表结合
        /// all tables
        /// </summary>


        private Dictionary<string, int> currentTables;
        /// <summary>
        /// 表的连接信息
        /// for table join info
        /// </summary>

        /// <summary>
        /// 正在解析连接条件
        /// </summary>
        private JoinTableEntity CurrentJoinTable;
        /// <summary>
        /// 当前解析的单个等式
        /// current single equtaion
        /// </summary>
        private TreeConditionEntity currentTree;
        /// <summary>
        /// true-表示解析完成一个二元条件 false-表示正在解析当中
        /// current single equtaion Is or no final
        /// </summary>
        private bool IsComplete;
        private bool IsCompleteMember;

        private SelectEntity select;

        private NetCore.ORM.Simple.Entity.MemberEntity currentMember;

        public JoinVisitor(SelectEntity Select)
        {
            select = Select;
            currentTables = new Dictionary<string, int>();
            select.InitJoin();
            IsComplete = true;
            IsCompleteMember = true;
        }




        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression)
        {
            currentTables.Clear();
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
                    },CurrentJoinTable);
                    break;
                case ExpressionType.Call:
                    break;
                case ExpressionType.GreaterThan:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref currentTree,ref IsComplete,eSignType.GrantThan,ref CurrentJoinTable);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref  currentTree,ref  IsComplete, eSignType.GreatThanOrEqual,ref CurrentJoinTable);
                    break;
                case ExpressionType.LessThan:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref  currentTree,ref  IsComplete, eSignType.LessThan,ref CurrentJoinTable);
                    break;
                case ExpressionType.LessThanOrEqual:
                    select.SingleBinary(node, (Node) => base.Visit(Node),ref  currentTree,ref  IsComplete, eSignType.LessThanOrEqual,ref CurrentJoinTable);
                    break;
                case ExpressionType.Equal:
                    select.SingleBinary(node, (Node) => base.Visit(Node), ref currentTree, ref IsComplete, eSignType.Equal,ref CurrentJoinTable);
                    break;
                case ExpressionType.NotEqual:
                    select.SingleBinary(node, (Node) => base.Visit(Node), ref currentTree,ref  IsComplete, eSignType.NotEqual,ref CurrentJoinTable);
                    break;
                case ExpressionType.OrElse:
                    select.MultipleBinary(node,ref  currentTree, eSignType.Or,ref  IsComplete, (Node) => 
                    { 
                        base.Visit(Node);
                    }, CurrentJoinTable);
                    break;
                case ExpressionType.ArrayIndex:
                    if (Check.IsNull(currentMember))
                    {
                        if (IsCompleteMember)
                        {
                            currentMember = new NetCore.ORM.Simple.Entity.MemberEntity();
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

            if (IsComplete && node.Type.IsEnum && node.Type.Equals(typeof(eJoinType)))
            {

                CurrentJoinTable = select.GetJoinEntity(eTableType.Slave);
                currentTree = select.GetTreeConditon();
                switch (node.Value)
                {
                    case eJoinType.Inner:
                        CurrentJoinTable.JoinType = eJoinType.Inner;
                        break;
                    case eJoinType.Left:
                        CurrentJoinTable.JoinType = eJoinType.Left;
                        break;
                    case eJoinType.Right:
                        CurrentJoinTable.JoinType = eJoinType.Right;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                select.VisitConstant(ref currentTree,node,ref CurrentJoinTable);
            }
            base.VisitConstant(node);
            return node;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = select.GetTreeConditon();
            }
            select.VisitMethod(ref currentTree,node,ref IsCompleteMember,ref currentMember);
            IsComplete = true;
            CurrentJoinTable.TreeConditions.Add(currentTree);
            base.VisitMethodCall(node);
            return node;
        }


        /// <summary>
        /// 用于解析值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }
        protected override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }



        protected override Expression VisitMember(MemberExpression node)
        {
            // 
            if ((node.Expression is ParameterExpression Parameter) && currentTables.ContainsKey(Parameter.Name))
            {
                if (Check.IsNull(currentTree.LeftCondition))
                {
                    currentTree.LeftCondition = select.GetCondition(eConditionType.ColumnName);
                    select.CreateJoin(currentTables[Parameter.Name],CurrentJoinTable,currentTree.LeftCondition, node.Member.Name);
                    currentTree.LeftCondition.PropertyType = node.Type;

                }
                else if (Check.IsNull(currentTree.RightCondition))
                {
                    currentTree.RightCondition= select.GetCondition(eConditionType.ColumnName);
                    select.CreateJoin(currentTables[Parameter.Name],CurrentJoinTable,currentTree.RightCondition, node.Member.Name);
                }
            }
            else
            {
                select.VisitMember(ref currentTree,node.Member,ref IsCompleteMember,currentMember);
            }
            IsCompleteMember = true;
            base.VisitMember(node);
            return node;
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {


            base.VisitNew(node);
            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            base.VisitNewArray(node);
            return node;
        }
        public string GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
