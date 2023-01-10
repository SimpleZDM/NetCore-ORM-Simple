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
 * 接口名称 JoinVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/19 14:11:42
 * 描述说明：解析多表查询连接部分（Left Join,Innor Join）
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal class JoinVisitor : ExpressionVisitor, IExpressionVisitor
    {
        private JoinTableEntity CurrentJoinTable;
        private ConditionVisitor conditionVisitor;
        private Dictionary<string, int> TableParams;
        private ContextSelect contextSelect;

        public JoinVisitor(ContextSelect _contextSelect)
        {
            TableParams = new Dictionary<string, int>();
            contextSelect = _contextSelect;
            if (Check.IsNull(_contextSelect.JoinInfos))
            {
                throw new Exception();
            }
            JoinExtension.InitJoin(_contextSelect.Table, _contextSelect.JoinInfos, ref CurrentJoinTable);
            conditionVisitor = new ConditionVisitor(contextSelect);
            conditionVisitor.InitMethodVisitor();
        }




        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression)
        {

            TableParams.Clear();
            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                TableParams.Add(item.Name, TableParams.Count);
            }
            Visit(expression);
            return expression;
        }

        public Expression Modify(Expression expression,eJoinType joinType)
        {
            CurrentJoinTable = JoinExtension.CreateJoinEntity(eTableType.Slave);
            CurrentJoinTable.JoinType = joinType;
            return Modify(expression);
        }

        /// <summary>
        /// 表达式树的二元操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            conditionVisitor.Modify(node,CurrentJoinTable.TreeConditions, CurrentJoinTable.Conditions,TableParams);
            JoinExtension.InitJoinTable(ref CurrentJoinTable,contextSelect.JoinInfos);
            return node;
        }

        /// <summary>
        /// 表达式树的常量操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            node.VisitConstant(ref CurrentJoinTable);
            return node;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            conditionVisitor.Modify(node, CurrentJoinTable.TreeConditions, CurrentJoinTable.Conditions,TableParams);
            JoinExtension.InitJoinTable(ref CurrentJoinTable,contextSelect.JoinInfos);
            return node;
        }


        /// <summary>
        /// 用于解析值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return node;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node;
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return node;
        }
        protected override Expression VisitListInit(ListInitExpression node)
        {
            return node;
        }



        protected override Expression VisitMember(MemberExpression node)
        {
            conditionVisitor.Modify(node, CurrentJoinTable.TreeConditions, CurrentJoinTable.Conditions,TableParams);
            JoinExtension.InitJoinTable(ref CurrentJoinTable,contextSelect.JoinInfos);
            return node;
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {


            return base.VisitNew(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
        public Dictionary<string,JoinTableEntity> GetValue()
        {
            return null;
        }
    }
}
