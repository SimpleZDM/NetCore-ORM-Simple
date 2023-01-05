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
    internal class OrderByVisitor : ExpressionVisitor
    {
        private OrderByEntity currentOrder;
        private Dictionary<string, int> TableParams;
        private eOrderOrGroupType OrderOrGroup;
        private eOrderType OrderType;
        private ContextSelect select;
        public OrderByVisitor(ContextSelect select)
        {
            this.select = select;
            TableParams = new Dictionary<string, int>();
        }
        public Expression Modify(Expression expression,eOrderOrGroupType orderOrGroup, eOrderType orderType)
        {
            TableParams.Clear();
            OrderOrGroup = orderOrGroup;
            OrderType = orderType;
            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                TableParams.Add(item.Name,TableParams.Count);
            }
            Visit(expression);
            return expression;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
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
           
            return base.VisitParameter(node);
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            int Index = -1;
            if (TableParams.ContainsKey(node.Expression.ToString()))
            {
                Index = TableParams[node.Expression.ToString()];
            }
            string PropName = node.Member.Name;
            CreateOrder(PropName, Index);
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

        #region
        public void CreateOrder(string PropName, int Index)
        {
            if (select.OrderInfos.Any(info =>
            info.PropName.Equals(PropName) ||
            (PropName.Contains("key") && PropName.Replace("key", "").Equals(info.PropName))
            ))
            {
                var order = select.OrderInfos.Where(info => info.PropName.Equals(PropName) ||
                           (PropName.Contains("key") && PropName.Replace("key", "")
                           .Equals(info.PropName))).FirstOrDefault();
                switch (OrderOrGroup)
                {
                    case eOrderOrGroupType.OrderBy:
                        order.IsOrderBy = true;
                        order.OrderType = OrderType;
                        break;
                    case eOrderOrGroupType.GroupBy:
                        order.IsGroupBy = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                OrderByEntity order = new OrderByEntity();
                switch (OrderOrGroup)
                {
                    case eOrderOrGroupType.OrderBy:
                        order.IsOrderBy = true;
                        order.OrderType = OrderType;
                        order.OrderSoft = select.GetOrderBySoft(); //select.OrderInfos.Where(o => o.IsOrderBy).Count() > 0 ? select.OrderInfos.Where(o => o.IsOrderBy).Max(o => o.OrderSoft) + 1 : 0;
                        break;
                    case eOrderOrGroupType.GroupBy:
                        order.IsGroupBy = true;
                        order.GroupSoft = select.GetGroupBySoft(); ;//select.OrderInfos.Where(g => g.IsGroupBy).Count() > 0 ? select.OrderInfos.Where(o => o.IsGroupBy).Max(o => o.GroupSoft) + 1 : 0;
                        break;
                    default:
                        break;
                        
                }

                select.SetOrder(order,PropName,Index);

                select.OrderInfos.Add(order);
            }



        }
        #endregion
    }
}
