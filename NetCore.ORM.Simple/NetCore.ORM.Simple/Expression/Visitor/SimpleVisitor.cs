using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Common;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 SimpleVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/21 17:28:28
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal class SimpleVisitor
    {
        private MapVisitor mapVisitor;
        private JoinVisitor joinVisitor;
        private OrderByVisitor OrderVistor;
        private ConditionVisitor conditionVisitor;
        private ContextSelect contextSelect;

        /// <summary>
        /// 
        /// </summary>
        public SimpleVisitor(params Type[] types)
        {
            if (Check.IsNull(types))
            {

            }
            contextSelect = new ContextSelect(types);
            mapVisitor = new MapVisitor(contextSelect);
            joinVisitor = new JoinVisitor(contextSelect);
            OrderVistor = new OrderByVisitor(contextSelect);
            conditionVisitor = new ConditionVisitor(contextSelect);
            conditionVisitor.InitMethodVisitor();
        }

        public List<JoinTableEntity> GetJoinInfos()
        {
            return contextSelect.JoinInfos.Values.ToList();
        }
        public List<MapEntity> GetMapInfos()
        {
            return contextSelect.MapInfos;
        }

        public Tuple<List<ConditionEntity>,List<TreeConditionEntity>> GetCondition()
        {
            return Tuple.Create(contextSelect.Conditions,contextSelect.TreeConditions);
        }

        public ContextSelect GetSelectInfo()
        {
            return contextSelect;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        public void VisitMap<T1, TResult>(Expression<Func<T1, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }
        public void VisitMap<T1, T2, TResult>(Expression<Func<T1, T2, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }
        public void VisitMap<T1, T2, T3, TResult>(Expression<Func<T1, T2, T3, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }
        public void VisitMap<T1, T2, T3, T4, TResult>(Expression<Func<T1, T2, T3, T4, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression,contextSelect.LastAnonymity);
        }
        public void VisitMap<T1, T2, T3, T4, T5, TResult>(Expression<Func<T1, T2, T3, T4, T5, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }
        public void VisitMap<T1, T2, T3, T4, T5,T6, TResult>(Expression<Func<T1, T2, T3, T4, T5,T6, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }

        public void VisitMap<T1, T2, T3, T4, T5, T6,T7, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6,T7, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }

        public void VisitMap<T1, T2, T3, T4, T5, T6, T7,T8, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }

        public void VisitMap<T1, T2, T3, T4, T5, T6, T7, T8,T9, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }

        public void VisitMap<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }
        public void VisitMap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }

        public void VisitMap<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, TResult>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, TResult>> expression)
        {
            contextSelect.IsAnonymity<TResult>();
            mapVisitor.Modify(expression, contextSelect.LastAnonymity);
        }



        public void VisitJoin<T1, T2>(Expression<Func<T1, T2, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3>(Expression<Func<T1, T2, T3, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4, T5,T6>(Expression<Func<T1, T2, T3, T4, T5,T6, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4, T5, T6,T7>(Expression<Func<T1, T2, T3, T4, T5, T6,T7, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }

        public void VisitJoin<T1, T2, T3, T4, T5, T6, T7,T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4, T5, T6, T7, T8,T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }

        public void VisitJoin<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }


        public void VisitorCondition<T1>(Expression<Func<T1, bool>> expression)
        {

            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2>(Expression<Func<T1, T2, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3>(Expression<Func<T1, T2, T3, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4>(Expression<Func<T1, T2, T3, T4, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5>(Expression<Func<T1, T2, T3, T4, T5, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5,T6>(Expression<Func<T1, T2, T3, T4, T5,T6, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5, T6,T7>(Expression<Func<T1, T2, T3, T4, T5, T6,T7, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5, T6, T7,T8>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5, T6, T7, T8,T9>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }
        public void VisitorCondition<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, bool>> expression)
        {
            conditionVisitor.Modify(expression) ;
        }
        public void VisitorCondition<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, bool>> expression)
        {
            conditionVisitor.Modify(expression);
        }

        public void OrderBy<T1, TOrder>(Expression<Func<T1, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, TOrder>(Expression<Func<T1, TOrder>> expression)
        {
            OrderVistor.Modify(expression,eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, TOrder>(Expression<Func<T1, T2, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, TOrder>(Expression<Func<T1, T2, T3, TOrder>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, TOrder>(Expression<Func<T1, T2, T3, T4, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression)
        {
            OrderVistor.Modify(expression,eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, TOrder>(Expression<Func<T1, T2, T3, T4, T5, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5,T6,TOrder>(Expression<Func<T1, T2, T3, T4, T5,T6, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, T6, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, TOrder>> expression)
        {
            OrderVistor.Modify(expression,eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, T6,T7, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6,T7, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, T6, T7, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, TOrder>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, T6, T7,T8, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, TOrder>> expression)
        {
            OrderVistor.Modify(expression,eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, T6, T7, T8,T9, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, TOrder>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, TOrder>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }
        public void OrderByDescending<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }
        public void OrderBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12, TOrder>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.OrderBy, eOrderType.Ascending);
        }

        public void OrderByDescending<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOrder>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TOrder>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.OrderBy, eOrderType.Descending);
        }

        public void GroupBy<T1, TGroup>(Expression<Func<T1, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, TGroup>(Expression<Func<T1, T2, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, TGroup>(Expression<Func<T1, T2, T3, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, TGroup>(Expression<Func<T1, T2, T3, T4, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, TGroup>(Expression<Func<T1, T2, T3, T4, T5, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5,T6, TGroup>(Expression<Func<T1, T2, T3, T4, T5,T6, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, T6,T7, TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6,T7,TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, T6, T7,T8, TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7,T8, TGroup>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, T6, T7, T8,T9, TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8,T9, TGroup>> expression)
        {
            OrderVistor.Modify(expression,  eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9,T10, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
        public void GroupBy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11, T12, TGroup>(Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11,T12, TGroup>> expression)
        {
            OrderVistor.Modify(expression, eOrderOrGroupType.GroupBy, eOrderType.Ascending);
        }
    }
}
