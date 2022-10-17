using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 MyExpressionAnalysis
 * 开发人员：-nhy
 * 创建时间：2022/9/14 17:11:25
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public class MyExpressionAnalysis : ExpressionVisitor
    {

        public MyExpressionAnalysis()
        {

        }

        public void Start(Expression expression)
        {
            base.Visit(expression);
        }
        
     
        public  ReadOnlyCollection<T> VisitAndConvert<T>(ReadOnlyCollection<T> nodes, string? callerName) where T : Expression
        {
            return default(ReadOnlyCollection<T>);
        }
       
        public T? VisitAndConvert<T>(T? node, string? callerName) where T : Expression
        {
            return default(T);
        }
        
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            base.VisitCatchBlock(node);
            return node;
        }
      
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            base.VisitElementInit(node);
            return node;
        }
     
        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        {
            base.VisitLabelTarget(node);
            return node;
        }
       
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            base.VisitMemberAssignment(node);
            return node;
        }
       
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
            return node;
        }
        
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            base.VisitMemberListBinding(node);
            return node;
        }
         
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            base.VisitMemberMemberBinding(node);
            return node;
        }
        
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            base.VisitSwitchCase(node);
            return node;
        }
        
        protected override Expression VisitBinary(BinaryExpression node)
        {
            base.VisitBinary(node);
            return node;
        }
      
        protected  override Expression VisitBlock(BlockExpression node)
        {
            {
                base.VisitBlock(node);
                return node;
            }
        }
       
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            base.VisitConditional(node);
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            base.VisitConstant(node);
            return node;
        }
       
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            base.VisitDebugInfo(node);
            return node;
        }
        
        protected override Expression VisitDefault(DefaultExpression node)
        {
            base.VisitDefault(node);
            return node;
        }
       
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            base.VisitDynamic(node);
            return node;
        }
        
        protected override Expression VisitExtension(Expression node)
        {
            base.VisitExtension(node);
            return node;
        }
       
        protected override Expression VisitGoto(GotoExpression node)
        {
            base.VisitGoto(node);
            return node;
        }
        
        protected override Expression VisitIndex(IndexExpression node)
        {
            base.VisitIndex(node);
            return node;
        }
        
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            {
                base.VisitInvocation(node);
                return node;
            }
        }
       
        protected override Expression VisitLabel(LabelExpression node)
        {
            base.VisitLabel(node);
            return node;
        }
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            base.VisitLambda(node);
            return node;
        }
       
        protected override Expression VisitListInit(ListInitExpression node)
        {
            base.VisitListInit(node);
            return node;
        }
        
        protected override Expression VisitLoop(LoopExpression node)
        {
            base.VisitLoop(node);
            return node;
        }
       
        protected override Expression VisitMember(MemberExpression node)
        {
            base.VisitMember(node);
            return node;
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            base.VisitMemberInit(node);
            return node;
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);
            return node;
        }
      
        protected  override Expression VisitNew(NewExpression node)
        {
            base.VisitNew(node);
            //var m=node.Members[1];
            //foreach (var item in node.Members)
            //{
            //    Console.WriteLine(item.Name);
            //}
            return node;
        }
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            base.VisitNewArray(node);
            return node;
        }
     
        protected override Expression VisitParameter(ParameterExpression node)
        {
            base.VisitParameter(node);
            return node;
        }
       
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            base.VisitRuntimeVariables(node);
            return node;
        }
      
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            base.VisitSwitch(node);
            return node;
        }
      
        protected override Expression VisitTry(TryExpression node)
        {
            base.VisitTry(node);
            return node;
        }
       
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            base.VisitTypeBinary(node);
            return node;
        }
       
        protected  override Expression VisitUnary(UnaryExpression node)
        {
            base.VisitUnary(node);
            return node;
        }
    }
}
