using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// 存放Expression表达式树的内容
        /// </summary>
        private Queue<string> qWhere;

        private const int minTableCount = 1;



        private string[] tableNames;

        private Dictionary<string, int> currentTables;
        private int currentTableLength;

        private Dictionary<string, string> map_Method;

        private List<string> entity_key;
        private List<string> data_key;

        private string[]
            cStrSign =
            new string[] { "(", ")", "=", ">=", "<=", ">", "<", "AND", "OR", "<>" };


        public MyExpressionAnalysis(params string[] _tableNames)
        {
            if (!Check.IsNull(_tableNames))
            {
                tableNames = _tableNames;
            }
            map_Method = new Dictionary<string, string>();
            currentTables = new Dictionary<string, int>();
            map_Method.Add("Equal", "=");
            map_Method.Add("IsNullOrEmpty", "IS NULL");
            map_Method.Add("Contains", "Like");
            qWhere = new Queue<string>();
            currentTableLength = 0;
            entity_key = new List<string>();
            data_key = new List<string>();

        }


        public string GetWhereSql()
        {
            int length = qWhere.Count();
            for (int i = 0; i < length; i++)
            {
                Console.Write($" `{qWhere.Dequeue()}` ");
            }

            return "";
        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression)
        {
            currentTables.Clear();
            Visit(expression);
            return Visit(expression);
        }

        /// <summary>
        /// 表达式树的二元操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            //base.VisitBinary(node);
            //SingleBinary(node,null);
            //SingleBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.Or]); });
            if (currentTables.Count()>0)
            {
                #region
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        SingleLogicBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.And]); });
                        break;
                    case ExpressionType.Call:
                        Console.WriteLine("call");
                        break;
                    case ExpressionType.GreaterThan:
                        SingleBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.GrantThan]); });
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        SingleBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.GreatThanOrEqual]); });
                        break;
                    case ExpressionType.LessThan:
                        SingleBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.LessThan]); });
                        break;
                    case ExpressionType.LessThanOrEqual:
                        SingleBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.LessThanOrEqual]); });
                        break;
                    case ExpressionType.Equal:
                        Console.WriteLine("Equal");
                        break;
                    case ExpressionType.NotEqual:
                        SingleBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.NotEqual]); });
                        break;
                    case ExpressionType.OrElse:
                        SingleLogicBinary(node, (queue) => { queue.Enqueue(cStrSign[(int)eSignType.Or]); });
                        break;
                    case ExpressionType.Parameter:
                        break;
                    case ExpressionType.Power:
                        break;
                    case ExpressionType.Quote:
                        break;
                    case ExpressionType.RightShift:
                        break;
                    case ExpressionType.Subtract:
                        break;
                    case ExpressionType.SubtractChecked:
                        break;
                    case ExpressionType.TypeAs:
                        break;
                    case ExpressionType.TypeIs:
                        break;
                    case ExpressionType.Assign:
                        break;
                    case ExpressionType.Block:
                        break;
                    case ExpressionType.DebugInfo:
                        break;
                    case ExpressionType.Decrement:
                        break;
                    case ExpressionType.Dynamic:
                        break;
                    case ExpressionType.Default:
                        break;
                    case ExpressionType.Extension:
                        break;
                    case ExpressionType.Goto:
                        break;
                    case ExpressionType.Increment:
                        break;
                    case ExpressionType.Index:
                        break;
                    case ExpressionType.Label:
                        break;
                    case ExpressionType.RuntimeVariables:
                        break;
                    case ExpressionType.Loop:
                        break;
                    case ExpressionType.Switch:
                        break;
                    case ExpressionType.Throw:
                        break;
                    case ExpressionType.Try:
                        break;
                    case ExpressionType.Unbox:
                        break;
                    case ExpressionType.AddAssign:
                        break;
                    case ExpressionType.AndAssign:
                        break;
                    case ExpressionType.DivideAssign:
                        break;
                    case ExpressionType.ExclusiveOrAssign:
                        break;
                    case ExpressionType.LeftShiftAssign:
                        break;
                    case ExpressionType.ModuloAssign:
                        break;
                    case ExpressionType.MultiplyAssign:
                        break;
                    case ExpressionType.OrAssign:
                        break;
                    case ExpressionType.PowerAssign:
                        break;
                    case ExpressionType.RightShiftAssign:
                        break;
                    case ExpressionType.MultiplyAssignChecked:
                        break;
                    case ExpressionType.SubtractAssignChecked:
                        break;
                    case ExpressionType.PreIncrementAssign:
                        break;
                    case ExpressionType.PreDecrementAssign:
                        break;
                    case ExpressionType.PostIncrementAssign:
                        break;
                    case ExpressionType.PostDecrementAssign:
                        break;
                    case ExpressionType.TypeEqual:
                        break;
                    case ExpressionType.UnaryPlus:

                        break;
                    case ExpressionType.OnesComplement:
                        break;
                    case ExpressionType.IsTrue:
                        break;
                    case ExpressionType.IsFalse:
                        break;
                    default:
                        break;
                }
                #endregion
                #region
                //switch (node.NodeType)
                //{
                //    case ExpressionType.Add:
                //        break;
                //    case ExpressionType.AddChecked:
                //        break;
                //    case ExpressionType.And:
                //        Console.WriteLine("and");
                //        break;
                //    case ExpressionType.AndAlso:
                //        Console.WriteLine("and");
                //        break;
                //    case ExpressionType.ArrayLength:
                //        break;
                //    case ExpressionType.ArrayIndex:
                //        break;
                //    case ExpressionType.Call:
                //        Console.WriteLine("call");
                //        break;
                //    case ExpressionType.Coalesce:
                //        break;
                //    case ExpressionType.Conditional:
                //        break;
                //    case ExpressionType.Constant:
                //        break;
                //    case ExpressionType.Convert:
                //        break;
                //    case ExpressionType.ConvertChecked:
                //        break;
                //    case ExpressionType.Divide:
                //        break;
                //    case ExpressionType.Equal:
                //        break;
                //    case ExpressionType.ExclusiveOr:
                //        break;
                //    case ExpressionType.GreaterThan:
                //        break;
                //    case ExpressionType.GreaterThanOrEqual:
                //        break;
                //    case ExpressionType.Invoke:
                //        break;
                //    case ExpressionType.Lambda:
                //        break;
                //    case ExpressionType.LeftShift:
                //        break;
                //    case ExpressionType.LessThan:
                //        break;
                //    case ExpressionType.LessThanOrEqual:
                //        break;
                //    case ExpressionType.ListInit:
                //        break;
                //    case ExpressionType.MemberAccess:
                //        break;
                //    case ExpressionType.MemberInit:
                //        break;
                //    case ExpressionType.Modulo:
                //        break;
                //    case ExpressionType.Multiply:
                //        break;
                //    case ExpressionType.MultiplyChecked:
                //        break;
                //    case ExpressionType.Negate:
                //        break;
                //    case ExpressionType.UnaryPlus:
                //        break;
                //    case ExpressionType.NegateChecked:
                //        break;
                //    case ExpressionType.New:
                //        break;
                //    case ExpressionType.NewArrayInit:
                //        break;
                //    case ExpressionType.NewArrayBounds:
                //        break;
                //    case ExpressionType.Not:
                //        break;
                //    case ExpressionType.NotEqual:
                //        break;
                //    case ExpressionType.Or:
                //        break;
                //    case ExpressionType.OrElse:
                //        break;
                //    case ExpressionType.Parameter:
                //        break;
                //    case ExpressionType.Power:
                //        break;
                //    case ExpressionType.Quote:
                //        break;
                //    case ExpressionType.RightShift:
                //        break;
                //    case ExpressionType.Subtract:
                //        break;
                //    case ExpressionType.SubtractChecked:
                //        break;
                //    case ExpressionType.TypeAs:
                //        break;
                //    case ExpressionType.TypeIs:
                //        break;
                //    case ExpressionType.Assign:
                //        break;
                //    case ExpressionType.Block:
                //        break;
                //    case ExpressionType.DebugInfo:
                //        break;
                //    case ExpressionType.Decrement:
                //        break;
                //    case ExpressionType.Dynamic:
                //        break;
                //    case ExpressionType.Default:
                //        break;
                //    case ExpressionType.Extension:
                //        break;
                //    case ExpressionType.Goto:
                //        break;
                //    case ExpressionType.Increment:
                //        break;
                //    case ExpressionType.Index:
                //        break;
                //    case ExpressionType.Label:
                //        break;
                //    case ExpressionType.RuntimeVariables:
                //        break;
                //    case ExpressionType.Loop:
                //        break;
                //    case ExpressionType.Switch:
                //        break;
                //    case ExpressionType.Throw:
                //        break;
                //    case ExpressionType.Try:
                //        break;
                //    case ExpressionType.Unbox:
                //        break;
                //    case ExpressionType.AddAssign:
                //        break;
                //    case ExpressionType.AndAssign:
                //        break;
                //    case ExpressionType.DivideAssign:
                //        break;
                //    case ExpressionType.ExclusiveOrAssign:
                //        break;
                //    case ExpressionType.LeftShiftAssign:
                //        break;
                //    case ExpressionType.ModuloAssign:
                //        break;
                //    case ExpressionType.MultiplyAssign:
                //        break;
                //    case ExpressionType.OrAssign:
                //        break;
                //    case ExpressionType.PowerAssign:
                //        break;
                //    case ExpressionType.RightShiftAssign:
                //        break;
                //    case ExpressionType.SubtractAssign:
                //        break;
                //    case ExpressionType.AddAssignChecked:
                //        break;
                //    case ExpressionType.MultiplyAssignChecked:
                //        break;
                //    case ExpressionType.SubtractAssignChecked:
                //        break;
                //    case ExpressionType.PreIncrementAssign:
                //        break;
                //    case ExpressionType.PreDecrementAssign:
                //        break;
                //    case ExpressionType.PostIncrementAssign:
                //        break;
                //    case ExpressionType.PostDecrementAssign:
                //        break;
                //    case ExpressionType.TypeEqual:
                //        break;
                //    case ExpressionType.OnesComplement:
                //        break;
                //    case ExpressionType.IsTrue:
                //        break;
                //    case ExpressionType.IsFalse:
                //        break;
                //    default:
                //        break;
                //}
                //StackSet.Push(node.Value.ToString());
                #endregion
            }
            return node;
        }

        /// <summary>
        /// 表达式树的常量操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            qWhere.Enqueue(node.Value.ToString());
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Console.WriteLine(node.Method.Name);
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitRuntimeVariables(node);
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitMemberMemberBinding(node);
        }
        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitLabelTarget(node);
        }
        /// <summary>
        /// 用于解析值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
            entity_key.Add(node.Member.Name);
            //if (currentTables.Contains(node.Exp))
            //{
            //    map_data.Add(node.Member.Name);
            //}
            return node;
        }


        protected override Expression VisitLabel(LabelExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitLabel(node);
        }

        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitSwitchCase(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitExtension(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (!currentTables.ContainsKey(node.Name))
            {
                currentTables.Add(node.Name,currentTables.Count());
            }
            return base.VisitParameter(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitUnary(node);
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitBlock(node);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitConditional(node);
        }


        protected override Expression VisitDefault(DefaultExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitDefault(node);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitDynamic(node);
        }


        protected override Expression VisitGoto(GotoExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitGoto(node);
        }


        protected override Expression VisitListInit(ListInitExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitListInit(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitLoop(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            base.VisitMember(node);
            Console.WriteLine(node.ToString());
            if (currentTables.ContainsKey(node.Expression.ToString()))
            {
                data_key.Add($"{tableNames[currentTables[node.Expression.ToString()]]}_{node.Member.Name}");
                qWhere.Enqueue($"{tableNames[currentTables[node.Expression.ToString()]]}.{node.Member.Name} AS {data_key[data_key.Count() - 1]}");
            }
            return node;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitNew(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitNewArray(node);
        }

        protected override Expression VisitSwitch(SwitchExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitSwitch(node);
        }

        protected override Expression VisitTry(TryExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitTry(node);
        }

        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitTypeBinary(node);
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitCatchBlock(node);
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitElementInit(node);
        }

        /// <summary>
        /// 条件表达式大于小于等于
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            if (node.Left is MemberExpression member)
            {
                string mName = string.Empty;
                if (tableNames.Length > minTableCount)
                {
                    if (currentTables.ContainsKey(member.Expression.ToString()))
                    {

                        mName = $"{tableNames[currentTables[member.Expression.ToString()]]}.{member.Member.Name}";
                    }

                }
                else
                {
                    mName = member.Member.Name;
                }
                qWhere.Enqueue(mName);

            }
            if (!Check.IsNull(action))
            {
                action.Invoke(qWhere);
            }
            if (node.Right is ConstantExpression constant)
            {
                qWhere.Enqueue(constant.Value.ToString());
            }
        }

        /// <summary>
        /// 或与非
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleLogicBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            qWhere.Enqueue(cStrSign[(int)eSignType.LeftBracket]);
            base.Visit(node.Left);
            qWhere.Enqueue(cStrSign[(int)eSignType.RightBracket]);
            if (!Check.IsNull(action))
            {
                action.Invoke(qWhere);
            }
            qWhere.Enqueue(cStrSign[(int)eSignType.LeftBracket]);
            base.Visit(node.Right);
            qWhere.Enqueue(cStrSign[(int)eSignType.RightBracket]);
        }
    }
}
