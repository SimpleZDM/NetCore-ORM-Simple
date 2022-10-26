//using NetCore.ORM.Simple.Common;
//using NetCore.ORM.Simple.Entity;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

///*********************************************************
// * 命名空间 NetCore.ORM.Simple.Visitor
// * 接口名称 MyExpressionAnalysis
// * 开发人员：-nhy
// * 创建时间：2022/9/14 17:11:25
// * 描述说明：
// * 更改历史：
// * 
// * *******************************************************/
//namespace NetCore.ORM.Simple.Visitor
//{
//    public class MemberEntity
//    {
//        public MemberInfo memberInfo { get; set; }
//        public object oParams { get; set; }
//        public MemberInfo Key { get; set; }
//    }
//    public class MyExpressionAnalysis : ExpressionVisitor
//    {
//        private MemberEntity currentMember;
//        Stack<MemberEntity> members;
//        public bool IsComplete { get; set; }
//        public MyExpressionAnalysis()
//        {
//            members = new Stack<MemberEntity>();
//            IsComplete = true;
//        }

//        public void Start(Expression expression)
//        {
//            base.Visit(expression);
//        }


//        public ReadOnlyCollection<T> VisitAndConvert<T>(ReadOnlyCollection<T> nodes, string? callerName) where T : Expression
//        {
//            return default(ReadOnlyCollection<T>);
//        }

//        public T? VisitAndConvert<T>(T? node, string? callerName) where T : Expression
//        {
//            return default(T);
//        }

//        protected override CatchBlock VisitCatchBlock(CatchBlock node)
//        {
//            base.VisitCatchBlock(node);
//            return node;
//        }

//        protected override ElementInit VisitElementInit(ElementInit node)
//        {
//            base.VisitElementInit(node);
//            return node;
//        }

//        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
//        {
//            base.VisitLabelTarget(node);
//            return node;
//        }

//        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
//        {
//            base.VisitMemberAssignment(node);
//            return node;
//        }

//        protected override MemberBinding VisitMemberBinding(MemberBinding node)
//        {
//            base.VisitMemberBinding(node);
//            return node;
//        }

//        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
//        {
//            base.VisitMemberListBinding(node);
//            return node;
//        }

//        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
//        {
//            base.VisitMemberMemberBinding(node);
//            return node;
//        }

//        protected override SwitchCase VisitSwitchCase(SwitchCase node)
//        {
//            base.VisitSwitchCase(node);
//            return node;
//        }

//        protected override Expression VisitBinary(BinaryExpression node)
//        {
//            switch (node.NodeType)
//            {
//                case ExpressionType.Add:
//                    break;
//                case ExpressionType.AddChecked:
//                    break;
//                case ExpressionType.And:
//                    break;
//                case ExpressionType.AndAlso:
//                    break;
//                case ExpressionType.ArrayLength:
//                    break;
//                case ExpressionType.ArrayIndex:
//                    if (Check.IsNull(currentMember))
//                    {
//                        if (IsComplete)
//                        {
//                            currentMember = new MemberEntity();
//                            IsComplete = false;
//                        }
//                        if (node.Right is ConstantExpression constant)
//                        {
//                            currentMember.oParams = constant.Value;

//                        }

//                    }
//                    break;
//                case ExpressionType.Call:
//                    break;
//                case ExpressionType.Coalesce:
//                    break;
//                case ExpressionType.Conditional:
//                    break;
//                case ExpressionType.Constant:
//                    break;
//                case ExpressionType.Convert:
//                    break;
//                case ExpressionType.ConvertChecked:
//                    break;
//                case ExpressionType.Divide:
//                    break;
//                case ExpressionType.Equal:
//                    break;
//                case ExpressionType.ExclusiveOr:
//                    break;
//                case ExpressionType.GreaterThan:
//                    break;
//                case ExpressionType.GreaterThanOrEqual:
//                    break;
//                case ExpressionType.Invoke:
//                    break;
//                case ExpressionType.Lambda:
//                    break;
//                case ExpressionType.LeftShift:
//                    break;
//                case ExpressionType.LessThan:
//                    break;
//                case ExpressionType.LessThanOrEqual:
//                    break;
//                case ExpressionType.ListInit:
//                    break;
//                case ExpressionType.MemberAccess:
//                    break;
//                case ExpressionType.MemberInit:
//                    break;
//                case ExpressionType.Modulo:
//                    break;
//                case ExpressionType.Multiply:
//                    break;
//                case ExpressionType.MultiplyChecked:
//                    break;
//                case ExpressionType.Negate:
//                    break;
//                case ExpressionType.UnaryPlus:
//                    break;
//                case ExpressionType.NegateChecked:
//                    break;
//                case ExpressionType.New:
//                    break;
//                case ExpressionType.NewArrayInit:
//                    break;
//                case ExpressionType.NewArrayBounds:
//                    break;
//                case ExpressionType.Not:
//                    break;
//                case ExpressionType.NotEqual:
//                    break;
//                case ExpressionType.Or:
//                    break;
//                case ExpressionType.OrElse:
//                    break;
//                case ExpressionType.Parameter:
//                    break;
//                case ExpressionType.Power:
//                    break;
//                case ExpressionType.Quote:
//                    break;
//                case ExpressionType.RightShift:
//                    break;
//                case ExpressionType.Subtract:
//                    break;
//                case ExpressionType.SubtractChecked:
//                    break;
//                case ExpressionType.TypeAs:
//                    break;
//                case ExpressionType.TypeIs:
//                    break;
//                case ExpressionType.Assign:
//                    break;
//                case ExpressionType.Block:
//                    break;
//                case ExpressionType.DebugInfo:
//                    break;
//                case ExpressionType.Decrement:
//                    break;
//                case ExpressionType.Dynamic:
//                    break;
//                case ExpressionType.Default:
//                    break;
//                case ExpressionType.Extension:
//                    break;
//                case ExpressionType.Goto:
//                    break;
//                case ExpressionType.Increment:
//                    break;
//                case ExpressionType.Index:
//                    break;
//                case ExpressionType.Label:
//                    break;
//                case ExpressionType.RuntimeVariables:
//                    break;
//                case ExpressionType.Loop:
//                    break;
//                case ExpressionType.Switch:
//                    break;
//                case ExpressionType.Throw:
//                    break;
//                case ExpressionType.Try:
//                    break;
//                case ExpressionType.Unbox:
//                    break;
//                case ExpressionType.AddAssign:
//                    break;
//                case ExpressionType.AndAssign:
//                    break;
//                case ExpressionType.DivideAssign:
//                    break;
//                case ExpressionType.ExclusiveOrAssign:
//                    break;
//                case ExpressionType.LeftShiftAssign:
//                    break;
//                case ExpressionType.ModuloAssign:
//                    break;
//                case ExpressionType.MultiplyAssign:
//                    break;
//                case ExpressionType.OrAssign:
//                    break;
//                case ExpressionType.PowerAssign:
//                    break;
//                case ExpressionType.RightShiftAssign:
//                    break;
//                case ExpressionType.SubtractAssign:
//                    break;
//                case ExpressionType.AddAssignChecked:
//                    break;
//                case ExpressionType.MultiplyAssignChecked:
//                    break;
//                case ExpressionType.SubtractAssignChecked:
//                    break;
//                case ExpressionType.PreIncrementAssign:
//                    break;
//                case ExpressionType.PreDecrementAssign:
//                    break;
//                case ExpressionType.PostIncrementAssign:
//                    break;
//                case ExpressionType.PostDecrementAssign:
//                    break;
//                case ExpressionType.TypeEqual:
//                    break;
//                case ExpressionType.OnesComplement:
//                    break;
//                case ExpressionType.IsTrue:
//                    break;
//                case ExpressionType.IsFalse:
//                    break;
//                default:
//                    break;
//            }
//            base.VisitBinary(node);
//            return node;
//        }

//        protected override Expression VisitBlock(BlockExpression node)
//        {
//            {
//                base.VisitBlock(node);
//                return node;
//            }
//        }

//        protected override Expression VisitConditional(ConditionalExpression node)
//        {
//            base.VisitConditional(node);
//            return node;
//        }

//        protected override Expression VisitConstant(ConstantExpression node)
//        {
//            //Type type = node.Value.GetType();
//            //var obj1=type.GetField("ids").GetValue(node.Value); //ids
//            //var obj2=type.GetField("lids").GetValue(node.Value); //lids
//            //var obj3=type.GetField("str").GetValue(node.Value); //lids
//            //Console.WriteLine(type.Name);
//            //Console.WriteLine(obj1.GetType());
//            //Console.WriteLine(obj2.GetType());
//            //Console.WriteLine(obj3.GetType());
//            if (node.Type.IsEnum)
//            {
//                Console.WriteLine(node.Value);
//                Console.WriteLine((int)node.Value);
//            }
//            if (IsComplete)
//            {
//                switch (node.Value)
//                {
//                    case eJoinType.Inner:
//                        break;
//                    case eJoinType.Left:
//                        break;
//                    case eJoinType.Right:
//                        break;
//                    default:
//                        break;
//                }
//            }
//            else
//            {
//                if (node.Type.IsValueType) 
//                {
//                    Console.WriteLine(node.Value);
//                    int.TryParse(node.Value.ToString(),out int val);
//                    Console.WriteLine(val);
//                }else if (node.Type==typeof(string))
//                {
//                    Console.WriteLine(node.Value.ToString());
//                }
//                else
//                {
//                    object value = null;
//                    if (members.Count != 0)
//                    {
//                        MemberEntity meber = members.Pop();
//                        if (meber.memberInfo is FieldInfo f)
//                        {
//                            value = f.GetValue(node.Value);
//                        }
//                    }

//                    while (members.Count != 0)
//                    {
//                        MemberEntity m = members.Pop();
//                        if (m.memberInfo is FieldInfo field)
//                        {
//                            if (m.oParams != null)
//                            {
//                                value = ((dynamic)field.GetValue(value))[m.oParams];
//                            }
//                            else if (m.Key != null)
//                            {
//                                if (m.Key is PropertyInfo PropKey)
//                                {
//                                    var Key = PropKey.GetValue(value);
//                                    value = ((dynamic)field.GetValue(value))[(dynamic)Key];
//                                }
//                                else if (m.Key is FieldInfo fieldKey)
//                                {
//                                    var Key = fieldKey.GetValue(node.Value);
//                                    value = ((dynamic)field.GetValue(value))[(dynamic)Key];
//                                }
//                            }
//                            else
//                            {
//                                value = field.GetValue(value);
//                            }
//                        }
//                        if (m.memberInfo is PropertyInfo Property)
//                        {
//                            if (m.oParams != null)
//                            {
//                                var o = Property.GetValue(value);
//                                value = ((dynamic)o)[(dynamic)m.oParams];
//                            }
//                            else if (m.Key != null)
//                            {
//                                if (m.Key is PropertyInfo PropKey)
//                                {
//                                    var Key = PropKey.GetValue(node.Value);
//                                    value = ((dynamic)Property.GetValue(value))[(dynamic)Key];
//                                }
//                                else if (m.Key is FieldInfo fieldKey)
//                                {
//                                    var Key = fieldKey.GetValue(node.Value);
//                                    value = ((dynamic)Property.GetValue(value))[(dynamic)Key];
//                                }
//                            }
//                            else
//                            {
//                                value = Property.GetValue(value);
//                            }
//                        }
//                    }
//                }
//            }

//            base.VisitConstant(node);
//            return node;
//        }

//        protected override Expression VisitDebugInfo(DebugInfoExpression node)
//        {
//            base.VisitDebugInfo(node);
//            return node;
//        }

//        protected override Expression VisitDefault(DefaultExpression node)
//        {
//            base.VisitDefault(node);
//            return node;
//        }

//        protected override Expression VisitDynamic(DynamicExpression node)
//        {
//            base.VisitDynamic(node);
//            return node;
//        }

//        protected override Expression VisitExtension(Expression node)
//        {
//            base.VisitExtension(node);
//            return node;
//        }

//        protected override Expression VisitGoto(GotoExpression node)
//        {
//            base.VisitGoto(node);
//            return node;
//        }

//        protected override Expression VisitIndex(IndexExpression node)
//        {
//            base.VisitIndex(node);
//            return node;
//        }

//        protected override Expression VisitInvocation(InvocationExpression node)
//        {
//            {
//                base.VisitInvocation(node);
//                return node;
//            }
//        }

//        protected override Expression VisitLabel(LabelExpression node)
//        {
//            base.VisitLabel(node);
//            return node;
//        }
//        protected override Expression VisitLambda<T>(Expression<T> node)
//        {
//            base.VisitLambda(node);
//            return node;
//        }

//        protected override Expression VisitListInit(ListInitExpression node)
//        {
//            base.VisitListInit(node);
//            return node;
//        }

//        protected override Expression VisitLoop(LoopExpression node)
//        {
//            base.VisitLoop(node);
//            return node;
//        }

//        protected override Expression VisitMember(MemberExpression node)
//        {
//            if (node.Expression.NodeType == ExpressionType.Parameter)
//            {

//            }
//            else
//            {
//                if (IsComplete)
//                {
//                    currentMember = new MemberEntity();
//                }
//                currentMember.memberInfo = node.Member;
//                members.Push(currentMember);
//            }
//            IsComplete = true;
//            base.VisitMember(node);
//            return node;
//        }
//        protected override Expression VisitMemberInit(MemberInitExpression node)
//        {
//            base.VisitMemberInit(node);
//            return node;
//        }
//        protected override Expression VisitMethodCall(MethodCallExpression node)
//        {

//            if (!Check.IsNull(node.Arguments) && node.Arguments.Count > 0)
//            {
//                if (node.Arguments[0] is ConstantExpression constant)
//                {
//                    if (IsComplete)
//                    {
//                        currentMember = new MemberEntity();
//                        currentMember.oParams = constant.Value;
//                        IsComplete = false;
//                    }
//                }
//                if (node.Arguments[0] is MemberExpression member)
//                {
//                    if (IsComplete)
//                    {
//                        currentMember = new MemberEntity();
//                        currentMember.Key = member.Member;
//                        IsComplete = false;
//                    }
//                }
//            }
//            base.VisitMethodCall(node);
//            return node;
//        }

//        protected override Expression VisitNew(NewExpression node)
//        {
//            base.VisitNew(node);
//            //var m=node.Members[1];
//            //foreach (var item in node.Members)
//            //{
//            //    Console.WriteLine(item.Name);
//            //}
//            return node;
//        }
//        protected override Expression VisitNewArray(NewArrayExpression node)
//        {
//            base.VisitNewArray(node);
//            return node;
//        }

//        protected override Expression VisitParameter(ParameterExpression node)
//        {
//            base.VisitParameter(node);
//            return node;
//        }

//        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
//        {
//            base.VisitRuntimeVariables(node);
//            return node;
//        }

//        protected override Expression VisitSwitch(SwitchExpression node)
//        {
//            base.VisitSwitch(node);
//            return node;
//        }

//        protected override Expression VisitTry(TryExpression node)
//        {
//            base.VisitTry(node);
//            return node;
//        }

//        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
//        {
//            base.VisitTypeBinary(node);
//            return node;
//        }

//        protected override Expression VisitUnary(UnaryExpression node)
//        {
//            base.VisitUnary(node);
//            return node;
//        }
//    }
//}