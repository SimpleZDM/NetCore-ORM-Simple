using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;

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
    public class JoinVisitor : ExpressionVisitor, IExpressionVisitor
    {
        /// <summary>
        /// 存放Expression表达式树的内容
        /// </summary>

        private string[] tableNames;

        private Dictionary<string, int> currentTables;

        private Dictionary<string, JoinTableEntity> JoinTables;
        private JoinTableEntity CurrentJoinTable;

        private List<string> JoinInfos;

        public JoinVisitor(params string[] _tableNames)
        {
            if (!Check.IsNull(_tableNames))
            {
                tableNames = _tableNames;
            }
            currentTables = new Dictionary<string, int>();
            JoinTables = new Dictionary<string, JoinTableEntity>();
            JoinTables.Add(tableNames[0], new JoinTableEntity() { DisplayName = tableNames[0], TableType = eTableType.Master });
            JoinInfos = new List<string>();
            // qValues = new List<Queue<string>>();
        }


        public string GetValue()
        {
            StringBuilder value = new StringBuilder();
            foreach (var jTable in JoinTables)
            {
                if (jTable.Value.TableType.Equals(eTableType.Master))
                {
                    continue;
                }
                int length = jTable.Value.QValue.Count;
                value.Append(jTable.Value.JoinType);
                value.Append($" {jTable.Key} ON ");
                for (int i = 0; i < length; i++)
                {
                    value.Append($"{jTable.Value.QValue.Dequeue()}");
                }
            }
            Console.WriteLine(value);
            return value.ToString();
        }
        /// <summary>
        /// 返回解析信息-不生成sql语句
        /// </summary>
        /// <returns></returns>
        public List<JoinTableEntity> GetJoinInfos()
        {
            if (Check.IsNull(JoinTables))
            {
                return null;
            }
            return JoinTables.Values.ToList();
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

            if (currentTables.Count() > SimpleConst.minTableCount)
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
            if (currentTables.Count() > SimpleConst.minTableCount)
            {
                //JoinTables.Add("");
                //qValue.Enqueue($"{node.Value} JOIN ");
                //JoinInfos.Add($"{node.Value} JOIN ");
                CurrentJoinTable = new JoinTableEntity() { TableType = eTableType.Slave };
                // CurrentJoinTable.JoinType;
                switch (node.Value)
                {
                    case eJoinType.Inner:
                        CurrentJoinTable.JoinType = eJoinType.Inner;
                        break;
                    case eJoinType.Left:
                        CurrentJoinTable.JoinType = eJoinType.Left;
                        break;
                    case eJoinType.Right:
                        CurrentJoinTable.JoinType = eJoinType.Right;
                        break;
                    default:
                        break;
                }
            }
            return node;
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

        /// <summary>
        /// 用于解析值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
            //entity_key.Add(node.Member.Name);
            //if (currentTables.Contains(node.Exp))
            //{
            //    map_data.Add(node.Member.Name);
            //}
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
        protected override Expression VisitUnary(UnaryExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitUnary(node);
        }



        protected override Expression VisitConditional(ConditionalExpression node)
        {
            Console.WriteLine(node.ToString());
            return base.VisitConditional(node);
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
                //data_key.Add($"{tableNames[currentTables[node.Expression.ToString()]]}_{node.Member.Name}");
                //qValue.Enqueue($"{tableNames[currentTables[node.Expression.ToString()]]}.{node.Member.Name} AS {data_key[data_key.Count() - 1]}");
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

            if (currentTables.Count > SimpleConst.minTableCount)
            {
                base.VisitNew(node);
                Console.WriteLine(node.ToString());
            }
            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            base.VisitNewArray(node);
            Console.WriteLine(node.ToString());
            return node;
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
            var lmember = node.Left as MemberExpression;
            var lvalue = node.Left as ConstantExpression;
            var rmember = node.Right as MemberExpression;
            var rvalue = node.Right as ConstantExpression;


            string lstrValue = GetMemberValue(lmember);
            if (Check.IsNullOrEmpty(lstrValue))
            {
                CurrentJoinTable.QValue.Enqueue(GetConstantValue(lvalue));
            }
            else
            {
                CurrentJoinTable.QValue.Enqueue(lstrValue);
            }
            action.Invoke(CurrentJoinTable.QValue);


            string rstrValue = GetMemberValue(rmember);

            if (Check.IsNullOrEmpty(rstrValue))
            {
                CurrentJoinTable.QValue.Enqueue(GetConstantValue(rvalue));
            }
            else
            {
                CurrentJoinTable.QValue.Enqueue(rstrValue);
            }

            //if (node.Left is MemberExpression member)
            //{
            //    string mName = string.Empty;
            //    if (tableNames.Length > SimpleConst.minTableCount)
            //    {
            //        if (currentTables.ContainsKey(member.Expression.ToString()))
            //        {
            //            if (!JoinTables.ContainsKey(tableNames[currentTables[member.Expression.ToString()]]))
            //            {
            //                JoinTableEntity joinEntity=new  JoinTableEntity()
            //                {
            //                    DisplayName = tableNames[currentTables[member.Expression.ToString()]],
            //                    JoinStr = JoinInfos.Last(),
            //                };
            //                qValue = joinEntity.QValue;
            //                JoinTables.Add(tableNames[currentTables[member.Expression.ToString()]],joinEntity);
            //            }
            //            mName = $"{tableNames[currentTables[member.Expression.ToString()]]}.{member.Member.Name}";
            //        }

            //    }
            //    else
            //    {
            //        mName = member.Member.Name;
            //    }
            //    qValue.Enqueue(mName);

            //}
            //if (!Check.IsNull(action))
            //{
            //    action.Invoke(qValue);
            //}
            //if (node.Right is Const constant)
            //{
            //    qValue.Enqueue(constant.Value.ToString());
            //}
        }

        /// <summary>
        /// 或与非
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleLogicBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            CurrentJoinTable.QValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.LeftBracket]);
            base.Visit(node.Left);
            CurrentJoinTable.QValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.RightBracket]);
            if (!Check.IsNull(action))
            {
                action.Invoke(CurrentJoinTable.QValue);
            }
            CurrentJoinTable.QValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.LeftBracket]);
            base.Visit(node.Right);
            CurrentJoinTable.QValue.Enqueue(SimpleConst.cStrSign[(int)eSignType.RightBracket]);
        }

        private string GetMemberValue(MemberExpression member)
        {
            string mName = string.Empty;
            if (!Check.IsNull(member))
            {

                if (tableNames.Length > SimpleConst.minTableCount)
                {
                    if (currentTables.ContainsKey(member.Expression.ToString()))
                    {
                        if (!JoinTables.ContainsKey(tableNames[currentTables[member.Expression.ToString()]]))
                        {
                            CreateJoinTable(member);
                        }
                        mName = $"{tableNames[currentTables[member.Expression.ToString()]]}.{member.Member.Name}";
                    }

                }
            }
            return mName;
        }

        private string GetConstantValue(ConstantExpression constant)
        {
            string mName = string.Empty;
            if (!Check.IsNull(constant))
            {

                if (constant.Type == typeof(string))
                {
                    CurrentJoinTable.QValue.Enqueue($"'{constant.Value}'");
                }
                else
                {
                    CurrentJoinTable.QValue.Enqueue($"{constant.Value}");
                }
            }
            return mName;
        }

        private void CreateJoinTable(MemberExpression member)
        {
            if (!Check.IsNull(member))
            {
                string mName = string.Empty;
                if (tableNames.Length > SimpleConst.minTableCount)
                {
                    if (currentTables.ContainsKey(member.Expression.ToString()))
                    {
                        if (!JoinTables.ContainsKey(tableNames[currentTables[member.Expression.ToString()]]))
                        {
                            if (Check.IsNull(CurrentJoinTable))
                            {
                                CurrentJoinTable = new JoinTableEntity()
                                {
                                    DisplayName = tableNames[currentTables[member.Expression.ToString()]],
                                    //JoinStr = JoinInfos.Last(),

                                };
                            }
                            else
                            {
                                CurrentJoinTable.DisplayName = tableNames[currentTables[member.Expression.ToString()]];
                            }
                            JoinTables.Add(CurrentJoinTable.DisplayName, CurrentJoinTable);
                        }
                    }

                }
            }
        }
    }
}
