using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Expression.Visitor
 * 接口名称 OrderByVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/27 15:20:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public class OrderByVisitor : ExpressionVisitor
    {
        private OrderByEntity currentOrder;
        private Dictionary<string, int> currentTables;
        private eOrderOrGroupType OrderOrGroup;
        private eOrderType OrderType;
        private SelectEntity select;
        public OrderByVisitor(SelectEntity select)
        {
            this.select = select;
            currentTables = new Dictionary<string, int>();

        }
        public Expression Modify(Expression expression,eOrderOrGroupType orderOrGroup, eOrderType orderType)
        {
            currentTables.Clear();
            OrderOrGroup = orderOrGroup;
            OrderType = orderType;
            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                currentTables.Add(item.Name, currentTables.Count);
            }
            Visit(expression);
            return expression;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            Console.WriteLine(node.Value.ToString());
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return base.VisitMethodCall(node);
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (!currentTables.ContainsKey(node.Name))
            {
                currentTables.Add(node.Name, currentTables.Count());
            }
            return base.VisitParameter(node);
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            int Index = -1;
            if (currentTables.ContainsKey(node.Expression.ToString()))
            {
                Index = currentTables[node.Expression.ToString()];
            }
            string PropName = node.Member.Name;
            select.CreateOrder(PropName, Index, OrderOrGroup, OrderType);
            return node;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
    }
}
