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
 * 接口名称 MapVisitor
 * 开发人员：-nhy
 * 创建时间：2022/9/19 14:09:37
 * 描述说明：解析sql映射成需要名称
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    public class MapVisitor : ExpressionVisitor
    {
      
        /// <summary>
        /// 所有的表
        /// </summary>

        private TableEntity Table;
        /// <summary>
        /// 当前传入的参数表
        /// </summary>

        private Dictionary<string, int> currentTables;

        /// <summary>
        /// 映射的信息
        /// </summary>

        private List<MapEntity> mapInfos;

        private MapEntity currentmapInfo;
        private bool IsAgain;
        public MapVisitor(TableEntity table,List<MapEntity> MapInfos)
        {
            if (Check.IsNull(table))
            {
                throw new ArgumentException("not table names!");
            }
            Table = table;
            currentTables = new Dictionary<string, int>();
            mapInfos = new List<MapEntity>();
        }

        public List<MapEntity> GetMapInfos()
        {
            return mapInfos;
        }
        public string GetValue()
        {
            StringBuilder value = new StringBuilder();
            //int length = qValue.Count();
            //for (int i = 0; i < length; i++)
            //{
            //    value.Append($" `{qValue.Dequeue()}` ");
            //}
            //Console.WriteLine(value.ToString());
            return value.ToString();
        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression)
        {
            currentTables.Clear();
            if (Check.IsNull(mapInfos)||mapInfos.Count==0)
            {
                mapInfos = new List<MapEntity>();
                IsAgain = false;
            }
            else
            {
                if (IsAgain)
                {
                    for (int i = 0; i < mapInfos.Count; i++)
                    {
                        mapInfos[i].IsNeed = false;
                    }
                }
            }

            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                currentTables.Add(item.Name,currentTables.Count);
            }
            Visit(expression);
            
            IsAgain = true;
            return expression;
        }

        /// <summary>
        /// 表达式树的二元操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {

            if (currentTables.Count() > 0)
            {
                #region
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        SingleLogicBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.And]); });
                        break;
                    case ExpressionType.Call:
                        Console.WriteLine("call");
                        break;
                    case ExpressionType.GreaterThan:
                        SingleBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.GrantThan]); });
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        SingleBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.GreatThanOrEqual]); });
                        break;
                    case ExpressionType.LessThan:
                        SingleBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.LessThan]); });
                        break;
                    case ExpressionType.LessThanOrEqual:
                        SingleBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.LessThanOrEqual]); });
                        break;
                    case ExpressionType.Equal:
                        SingleBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.Equal]); });
                        break;
                    case ExpressionType.NotEqual:
                        SingleBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.NotEqual]); });
                        break;
                    case ExpressionType.OrElse:
                        SingleLogicBinary(node, (queue) => { queue.Enqueue(SimpleConst.cStrSign[(int)eSignType.Or]); });
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
            //qValue.Enqueue(node.Value.ToString());
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // Console.WriteLine(node.Method.Name);
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
            currentmapInfo.PropName = node.Member.Name;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            base.VisitMember(node);
            Console.WriteLine(node.ToString());
            if (currentTables.ContainsKey(node.Expression.ToString()))
            {
                if (IsAgain)
                {

                     currentmapInfo=mapInfos.FirstOrDefault(m=>m.PropName.Equals(node.Member.Name));
                     currentmapInfo.IsNeed = true;
                }
                else
                {
                    currentmapInfo = new MapEntity();
                    mapInfos.Add(currentmapInfo);
                    currentmapInfo.ColumnName = node.Member.Name;
                    currentmapInfo.TableName = Table.TableNames[currentTables[node.Expression.ToString()]];
                }

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
                if (Table.TableNames.Length > SimpleConst.minTableCount)
                {
                    if (currentTables.ContainsKey(member.Expression.ToString()))
                    {

                        mName = $"{Table.TableNames[currentTables[member.Expression.ToString()]]}.{member.Member.Name}";
                    }

                }
                else
                {
                    mName = member.Member.Name;
                }

            }
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            if (node.Right is ConstantExpression constant)
            {
               // qValue.Enqueue(constant.Value.ToString());
            }
        }

        /// <summary>
        /// 或与非
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleLogicBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            //qValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.LeftBracket]);
            base.Visit(node.Left);
           // qValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.RightBracket]);
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            // qValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.LeftBracket]);
            base.Visit(node.Right);
            // qValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.RightBracket]);
        }
    }
}
