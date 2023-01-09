using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

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
        private bool IsCompleteMember;

        private ContextSelect contextSelect;

        private ConditionVisitor conditionVisitor;
        public MethodVisitor(ContextSelect _contextSelect)
        {
            contextSelect= _contextSelect;
            IsComplate = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void InitConditionVisitor()
        {
           
            if (Check.IsNull(conditionVisitor))
            {
                conditionVisitor = new ConditionVisitor(contextSelect);
                //conditionVisitor.InitMethodVisitor();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="_methods"></param>
        /// <param name="_tables"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression, List<MethodEntity> _methods, Dictionary<string, int> _tables)
        {
            IsComplate = true;
            TableParams = _tables;
            methods = _methods;
            IsCompleteMember = true;
            Visit(expression);
            return expression;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.ArrayIndex:
                        if (IsCompleteMember)
                        {
                            currentMember = new MemberEntity();
                            IsCompleteMember = false;
                        }

                    if (node.Right is ConstantExpression constant)
                    {
                        if (!Check.IsNull(currentMember))
                        {
                            currentMember.OParams.Add(constant.Value);
                        }
                    }
                    base.VisitBinary(node);
                    break;
                default:
                    conditionVisitor.Modify(node, currentMethod.TreeConditions, currentMethod.Conditions, TableParams);
                    break;
            }
            return node;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            CustomerVisitConstant(node);
            return base.VisitConstant(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (IsComplate)
            {
                currentMethod = new MethodEntity();
                IsComplate = false;
            }
            AddMemberParameter(node);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            CustomerVisitMember(node);
            return base.VisitMember(node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            return node;
        }
        #region extension method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
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
                currentMethod.Parameters.Add(condition);
            }
            else
            {
                if (Check.IsNullOrEmpty(currentMethod.Parameters))
                {
                    condition = contextSelect.GetCondition(eConditionType.Constant);
                    currentMethod.Parameters.Add(condition);
                }
                else
                {
                    condition = currentMethod.Parameters[currentMethod.Parameters.Count() - 1];
                    if (condition.ConditionType==eConditionType.ColumnName)
                    {
                        condition = contextSelect.GetCondition(eConditionType.Constant);
                        currentMethod.Parameters.Add(condition);

                    }
                }
                VisitMember(condition, node.Member);
            }
            IsCompleteMember= true;
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="member"></param>
        public  void VisitMember(ConditionEntity condition, MemberInfo member)
        {
            var memberCondition = ConditionsExtension.SetConstMember(contextSelect,member);
            if (Check.IsNull(memberCondition))
            {


                if (IsCompleteMember|| Check.IsNull(currentMember))
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void CustomerVisitMethod(MethodCallExpression node)
        {
            if (!Check.IsNullOrEmpty(currentMethod.Name)&&currentMethod.Name!=node.Method.Name)
            {
                currentMember.OParams.Clear();
            }
            currentMethod.Name = node.Method.Name;
            if (Check.IsNull(currentMethod))
            {
                currentMethod = new MethodEntity();
            }
           
            if (Check.IsNullOrEmpty(methods) || !object.ReferenceEquals(methods[methods.Count - 1], currentMethod))
            {
                methods.Add(currentMethod);
            }
        }

        public void AddMemberParameter(MethodCallExpression node)
        {

            currentMethod.Name = node.Method.Name;
            if (!MysqlConst.dicMethods.ContainsKey(node.Method.Name))
            {
                if (!Check.IsNullOrEmpty(node.Arguments))
                {
                    if (node.Arguments[0] is MethodCallExpression call)
                    {
                        currentMember = new MemberEntity();
                        currentMember.OParams.Add(call.Arguments[0]);
                        IsCompleteMember = false;
                    }
                    if (node.Arguments[0] is ConstantExpression constant)
                    {
                        currentMember = new MemberEntity();
                        currentMember.OParams.Add(constant.Value);
                        IsCompleteMember = false;
                    }
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
                condition.Value = node.Value;
                currentMethod.Parameters.Add(condition);
            }
            else
            {
                if (Check.IsNullOrEmpty(currentMethod.Parameters))
                {
                    right = new ConditionEntity(eConditionType.Constant);
                    currentMethod.Parameters.Add(right);
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
