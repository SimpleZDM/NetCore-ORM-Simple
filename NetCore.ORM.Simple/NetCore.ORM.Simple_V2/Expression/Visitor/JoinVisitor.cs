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
    internal class JoinVisitor : ExpressionVisitor, IExpressionVisitor
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
        private ConditionVisitor conditionVisitor;
        /// <summary>
        /// true-表示解析完成一个二元条件 false-表示正在解析当中
        /// current single equtaion Is or no final
        /// </summary>

        private Dictionary<string, JoinTableEntity> joinInfos;
        private TableEntity table;


        public JoinVisitor(Dictionary<string,JoinTableEntity>_joinInfos,TableEntity _table)
        {
            if (Check.IsNull(_joinInfos))
            {
                throw new Exception();
            }
            joinInfos = _joinInfos;
            table = _table;
            currentTables = new Dictionary<string, int>();
            JoinExtension.InitJoin(table,joinInfos,ref CurrentJoinTable);
            conditionVisitor = new ConditionVisitor(CurrentJoinTable.TreeConditions,CurrentJoinTable.Conditions,table);
            conditionVisitor.InitMethodVisitor();
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

            conditionVisitor.Modify(node,CurrentJoinTable.TreeConditions, CurrentJoinTable.Conditions, currentTables);
            JoinExtension.InitJoinTable(ref CurrentJoinTable, joinInfos);
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
            conditionVisitor.Modify(node,CurrentJoinTable.TreeConditions,CurrentJoinTable.Conditions,currentTables);
            JoinExtension.InitJoinTable(ref CurrentJoinTable, joinInfos);
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
            conditionVisitor.Modify(node, CurrentJoinTable.TreeConditions,CurrentJoinTable.Conditions,currentTables);
            JoinExtension.InitJoinTable(ref CurrentJoinTable,joinInfos);
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
            return joinInfos;
        }
    }
}
