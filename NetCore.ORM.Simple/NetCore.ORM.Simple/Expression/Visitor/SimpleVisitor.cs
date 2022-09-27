using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Common;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Expression.Visitor
 * 接口名称 SimpleVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/21 17:28:28
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public class SimpleVisitor
    {
        private TableEntity Table;
        private MapVisitor mapVisitor;
        private JoinVisitor joinVisitor;
        private ConditionVisitor conditionVisitor;
        private SelectEntity Select;

        /// <summary>
        /// 
        /// </summary>
        public SimpleVisitor(params string[]tableNames)
        {
            if (Check.IsNull(tableNames))
            {

            }
            Table = new TableEntity(tableNames);
            mapVisitor=new MapVisitor(Table,Select.MapInfos);
            joinVisitor=new JoinVisitor(Table,Select.JoinInfos);
            conditionVisitor=new ConditionVisitor(Table,Select.Conditions,Select.TreeConditions);
        }

        public List<JoinTableEntity> GetJoinInfos()
        {
            return joinVisitor.GetJoinInfos();
        }
        public List<MapEntity> GetMapInfos()
        {
            return mapVisitor.GetMapInfos();
        }

        public Tuple<List<ConditionEntity>,List<TreeConditionEntity>> GetCondition()
        {
            return conditionVisitor.GetCondition();
        }

        public SelectEntity GetSelectInfo()
        {
            return Select;
        }

        public void VisitMap<T1,TResult>(Expression<Func<T1,TResult>> expression)
        {
            mapVisitor.Modify(expression);
        }
        public void VisitMap<T1,T2,TResult>(Expression<Func<T1,T2,TResult>>expression)
        {
            mapVisitor.Modify(expression);
        }
        public void VisitMap<T1,T2,T3,TResult>(Expression<Func<T1,T2,T3,TResult>> expression)
        {
            mapVisitor.Modify(expression);
        }
        public void VisitMap<T1,T2,T3,T4,TResult>(Expression<Func<T1,T2,T3,T4,TResult>> expression)
        {
            mapVisitor.Modify(expression);
        }
        public void VisitMap<T1, T2, T3, T4,T5,TResult>(Expression<Func<T1, T2, T3, T4,T5,TResult>> expression)
        {
            mapVisitor.Modify(expression);
        }


        public void VisitJoin<T1,T2>(Expression<Func<T1,T2,JoinInfoEntity>>expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1,T2,T3>(Expression<Func<T1,T2,T3, JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1,T2,T3,T4>(Expression<Func<T1,T2,T3,T4,JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }
        public void VisitJoin<T1,T2,T3,T4,T5>(Expression<Func<T1,T2,T3,T4,T5,JoinInfoEntity>> expression)
        {
            joinVisitor.Modify(expression);
        }


        public void VisitorCondition<T1>(Expression<Func<T1,bool>>expression)
        {
            conditionVisitor.Modify(expression,GetMapInfos());
        }
        public void VisitorCondition<T1,T2>(Expression<Func<T1,T2,bool>> expression)
        {
            conditionVisitor.Modify(expression,GetMapInfos());
        }
        public void VisitorCondition<T1,T2,T3>(Expression<Func<T1,T2,T3,bool>> expression)
        {
            conditionVisitor.Modify(expression,GetMapInfos());
        }
        public void VisitorCondition<T1,T2,T3,T4>(Expression<Func<T1,T2,T3,T4,bool>> expression)
        {
            conditionVisitor.Modify(expression,GetMapInfos());
        }
        public void VisitorCondition<T1,T2,T3,T4,T5>(Expression<Func<T1,T2,T3,T4,T5,bool>> expression)
        {
            conditionVisitor.Modify(expression,GetMapInfos());
        }
    }
}
