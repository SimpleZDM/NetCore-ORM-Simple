using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private List<OrderByEntity> OrderInfos;
        private TableEntity Table;
        private OrderByEntity currentOrder;
        private Dictionary<string, int> currentTables;
        private eOrderOrGroupType OrderOrGroup;
        private eOrderType OrderType;
        private List<MapEntity> mapInfos;
        public OrderByVisitor(TableEntity table, List<OrderByEntity> orderInfos)
        {
            if (Check.IsNull(table))
            {
                throw new ArgumentException("not table names!");
            }
            Table = table;
            OrderInfos = orderInfos;
            currentTables = new Dictionary<string, int>();

        }
        public Expression Modify(Expression expression, List<MapEntity> MapInfos, eOrderOrGroupType orderOrGroup, eOrderType orderType)
        {
            currentTables.Clear();
            OrderOrGroup = orderOrGroup;
            OrderType = orderType;
            mapInfos = MapInfos;
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
            Console.WriteLine(node.Method.Name);
            return base.VisitMethodCall(node);
        }

        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
            Console.WriteLine(node.Member.Name);
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
            var Params = node.Expression as ParameterExpression;
            //base.VisitMember(node);
            if (OrderInfos.Any(info =>
            info.PropName.Equals(node.Member.Name) ||
            (node.Member.Name.Contains("key") && node.Member.Name.Replace("key", "").Equals(info.PropName))
            ))
            {
                var order = OrderInfos.Where(info => info.PropName.Equals(node.Member.Name) ||
             (node.Member.Name.Contains("key") && node.Member.Name.Replace("key", "")
             .Equals(info.PropName))).FirstOrDefault();
                order.PropName = node.Member.ToString();
                switch (OrderOrGroup)
                {
                    case eOrderOrGroupType.OrderBy:
                        order.IsOrderBy = true;
                        order.OrderType = OrderType;
                        // order.OrderSoft = OrderInfos.Where(o=>o.IsOrderBy).Max(o=>o.OrderSoft)+1;
                        break;
                    case eOrderOrGroupType.GroupBy:
                        order.IsGroupBy = true;
                        //order.GroupSoft = OrderInfos.Where(o => o.IsGroupBy).Max(o => o.GroupSoft)+1;
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
                        order.OrderSoft = OrderInfos.Where(o => o.IsOrderBy).Count() > 0 ? OrderInfos.Where(o => o.IsOrderBy).Max(o => o.OrderSoft) + 1 : 0;
                        break;
                    case eOrderOrGroupType.GroupBy:
                        order.IsGroupBy = true;
                        order.GroupSoft = OrderInfos.Where(g => g.IsGroupBy).Count() > 0 ? OrderInfos.Where(o => o.IsGroupBy).Max(o => o.GroupSoft) + 1 : 0;
                        break;
                    default:
                        break;
                }
                if (Check.IsNull(mapInfos) || mapInfos.Count <= 0)
                {
                    if (!Check.IsNull(Params))
                    {
                        order.TableName = Table.TableNames[currentTables[Params.Name]];
                        order.ColumnName = node.Member.Name;
                    }

                }
                else
                {
                    var map = mapInfos.Where(m => m.PropName.Equals(node.Member.Name)).FirstOrDefault();
                    order.TableName = map.TableName;
                    order.ColumnName = map.ColumnName;
                }
                order.PropName = node.Member.ToString();
                OrderInfos.Add(order);
            }
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
            Console.WriteLine(node.ToString());
            return base.VisitNewArray(node);
        }
    }
}
