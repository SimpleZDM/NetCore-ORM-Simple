using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 MethodVisitor
 * 开发人员：11920
 * 创建时间：2022/12/13 17:41:22
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal class MethodVisitor : ExpressionVisitor
    {
        private List<MethodEntity> methods;
        private MethodEntity currentMethod;
        private MapEntity[] mapInfos;
        private TableEntity table;
        private MemberEntity currentMember;
        private bool IsComplate;

        private ConditionVisitor conditionVisitor;
        public MethodVisitor(TableEntity _table)
        {
            table= _table;
            IsComplate = true;
        }
        public void InitConditionVisitor()
        {
            if (Check.IsNull(table))
            {
                throw new Exception(nameof(table));
            }
            if (Check.IsNull(conditionVisitor))
            {
                conditionVisitor = new ConditionVisitor(table);
            }

        }
        private Dictionary<string, int> currentTables;
        public Expression Modify(Expression expression, List<MethodEntity> _methods, Dictionary<string, int> _tables, params MapEntity[] _mapInfos)
        {
            currentTables = _tables;
            methods = _methods;
            mapInfos = _mapInfos;
            Visit(expression);
            return expression;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            conditionVisitor.Modify(node, currentMethod.TreeConditions, currentMethod.Conditions, currentTables);
            return node;
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            node.VisitConstant(ref currentMethod);
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (IsComplate)
            {
                currentMethod = new MethodEntity();
                IsComplate = false;
            }
            node.VisitMethod(ref currentMethod,ref currentMember,methods);
            base.VisitMethodCall(node);
            IsComplate = true;
            if (IsComplate)
            {
                currentMethod = new MethodEntity();
                IsComplate = false;
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
           
            node.VisitMember(currentTables,table,mapInfos,ref currentMember,currentMethod.Parameters);
           
            return base.VisitMember(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            node.VisitUnary(ref currentMethod);
            return node;
        }
    }
}
