using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private MemberEntity currentMember;
        private Dictionary<string, int> TableParams;
        private bool IsComplate;
        private ContextSelect contextSelect;


        private ConditionVisitor conditionVisitor;
        public MethodVisitor(ContextSelect _contextSelect)
        {
            contextSelect= _contextSelect;
            IsComplate = true;
        }
        public void InitConditionVisitor()
        {
           
            if (Check.IsNull(conditionVisitor))
            {
                conditionVisitor = new ConditionVisitor(contextSelect);
                //conditionVisitor.InitMethodVisitor();
            }

        }

        public Expression Modify(Expression expression, List<MethodEntity> _methods, Dictionary<string, int> _tables)
        {
            IsComplate = true;
            TableParams = _tables;
            methods = _methods;
            Visit(expression);
            return expression;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            conditionVisitor.Modify(node, currentMethod.TreeConditions, currentMethod.Conditions, TableParams);
            return node;
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            CustomerVisitConstant(node);
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (IsComplate)
            {
                currentMethod = new MethodEntity();
                IsComplate = false;
            }
            base.VisitMethodCall(node);
            CustomerVisitMethod(node);
            IsComplate = true;
            if (IsComplate)
            {
                currentMethod = new MethodEntity();
                IsComplate = false;
            }
            IsComplate = true;
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            CustomerVisitMember(node);
            return base.VisitMember(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            return node;
        }



        #region extension method
        public  void CustomerVisitMember(MemberExpression node)
        {
            ConditionEntity condition = null;
            string PropName = node.Member.Name;
            ParameterExpression Parameter = node.Expression as ParameterExpression;
            int Index = -1;

            if ((!Check.IsNull(Parameter) && TableParams.ContainsKey(Parameter.Name)))
            {
                Index = TableParams[Parameter.Name];
            }

            if (Index >= 0 || contextSelect.MapInfos.Any(u => u.PropName == PropName))
            {
                condition = contextSelect.GetCondition(PropName, Index);
            }
            else
            {
                if (Check.IsNull(condition))
                {
                    condition = contextSelect.GetCondition(eConditionType.ColumnName);
                }
                VisitMember(condition, node.Member);
            }
            currentMethod.Parameters.Add(condition);
        }
        public  void VisitMember(ConditionEntity condition, MemberInfo member)
        {
            var memberCondition = ConditionsExtension.SetConstMember(contextSelect,member);
            if (Check.IsNull(memberCondition))
            {


                if (Check.IsNull(currentMember))
                {
                    currentMember = new MemberEntity();
                }
                currentMember.Member = member;
                condition.Members.Push(currentMember);
            }
            else
            {
                currentMethod.Parameters.Add(memberCondition);
            }
        }
        public void CustomerVisitMethod(MethodCallExpression node)
        {
            if (Check.IsNull(currentMethod))
            {
                currentMethod = new MethodEntity();
            }
            currentMethod.Name = node.Method.Name;

            if (!Check.IsNullOrEmpty(node.Arguments))
            {
                if (node.Arguments[0] is MethodCallExpression call)
                {
                    currentMember = new MemberEntity();
                    currentMember.OParams.Add(call.Arguments[0]);
                }
                if (node.Arguments[0] is ConstantExpression constant)
                {
                    currentMember = new MemberEntity();
                    currentMember.OParams.Add(constant.Value);
                }
            }
            if (Check.IsNullOrEmpty(methods) || !object.ReferenceEquals(methods[methods.Count - 1], currentMethod))
            {
                methods.Add(currentMethod);
            }
        }

        public  bool CustomerVisitConstant(ConstantExpression node)
        {
            if (Check.IsNull(currentMethod))
            {
                throw new ArgumentException(nameof(currentMethod));
            }
            ConditionEntity right;
            if (node.Type.IsValueType)
            {
                ConditionEntity condition = contextSelect.GetCondition(eConditionType.Constant);
                if (node.Type.IsEnum)
                {
                    int.TryParse(node.Value.ToString(), out int value);
                    condition.Value = node.Value;
                }
                else
                {
                    condition.DisplayName = node.Value.ToString();
                }
                currentMethod.Parameters.Add(condition);
            }
            else
            {
                if (Check.IsNullOrEmpty(currentMethod.Parameters))
                {
                    right = new ConditionEntity(eConditionType.Constant);
                }
                else
                {
                    right = currentMethod.Parameters[currentMethod.Parameters.Count - 1];
                    right.ConditionType = eConditionType.Constant;
                }
                right.SetConstantContValue(null, node);
            }
            return true;
        }
        #endregion
    }
}
