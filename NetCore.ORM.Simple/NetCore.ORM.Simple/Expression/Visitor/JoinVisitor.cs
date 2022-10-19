﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        private TreeConditionEntity currentTree;
        /// <summary>
        /// true-表示解析完成一个二元条件 false-表示正在解析当中
        /// current single equtaion Is or no final
        /// </summary>
        private bool IsComplete;

        private SelectEntity select;

        public JoinVisitor(SelectEntity Select)
        {
            select = Select;
            currentTables = new Dictionary<string, int>();
            select.InitJoin();
            IsComplete = true;
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

            if (currentTables.Count() > SimpleConst.minTableCount)
            {
                #region
                IsComplete = false;
                switch (node.NodeType)
                {
                    case ExpressionType.AndAlso:
                        select.SingleLogicBinary(node, ref currentTree, eSignType.And, ref IsComplete, (mynode, tree) => { base.Visit(mynode); });
                        break;
                    case ExpressionType.Call:
                        break;
                    case ExpressionType.GreaterThan:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition = new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.GrantThan
                            };
                        });
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition = new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.GreatThanOrEqual
                            };
                        });
                        break;
                    case ExpressionType.LessThan:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition = new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.LessThan
                            };
                        });
                        break;
                    case ExpressionType.LessThanOrEqual:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition = new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.LessThanOrEqual
                            };
                        });
                        break;
                    case ExpressionType.Equal:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition = new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.Equal
                            };
                        });
                        break;
                    case ExpressionType.NotEqual:
                        SingleBinary(node, (queue) =>
                        {
                            currentTree.RelationCondition = new ConditionEntity(eConditionType.Sign)
                            {
                                SignType = eSignType.NotEqual
                            };
                        });
                        break;
                    case ExpressionType.OrElse:
                        select.SingleLogicBinary(node, ref currentTree, eSignType.Or, ref IsComplete, (mynode, tree) => { base.Visit(mynode); });
                        break;
                    case ExpressionType.ArrayIndex:
                        //currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);  
                        int index = 0;
                        int.TryParse(node.Right.ToString(), out index);
                        if (!Check.IsNull(currentTree.LeftCondition))
                        {
                            currentTree.Index = index;
                        }
                        base.Visit(node.Left);
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
                if (IsComplete)
                {

                    CurrentJoinTable = new JoinTableEntity() { TableType = eTableType.Slave };
                    currentTree = new TreeConditionEntity();
                    // JoinTables.Add(CurrentJoinTable);
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
                else
                {
                    if (select.VisitConstant(ref currentTree, node))
                    {
                        base.VisitConstant(node);
                        //IsComplete = true;
                    }

                }

            }
            return node;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);
            select.VisitMethod(ref currentTree, node);
            //currentTree.RelationCondition = new ConditionEntity(eConditionType.Method);
            //currentTree.RelationCondition.DisplayName = node.Method.Name;
            //if (node.Arguments.Count() >= 1)
            //{
            //    int value = 0;
            //    if (node.Arguments[0] is MethodCallExpression call)
            //    {
            //        if (int.TryParse(call.Arguments[0].ToString(), out value))
            //        {
            //            currentTree.Index = value;
            //        }
            //        else
            //        {
            //            currentTree.Key = call.Arguments[0].ToString();
            //        }

            //    }
            //    if (node.Arguments[0] is ConstantExpression content)
            //    {
            //        if (int.TryParse(content.Value.ToString(), out value))
            //        {
            //            currentTree.Index = value;
            //        }
            //        else
            //        {
            //            currentTree.Key = content.Value.ToString();
            //        }
            //    }

            //}
            IsComplete = true;
            CurrentJoinTable.TreeConditions.Add(currentTree);
            return node;
        }


        /// <summary>
        /// 用于解析值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            base.VisitMemberBinding(node);
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
            return base.VisitUnary(node);
        }
        protected override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }



        protected override Expression VisitMember(MemberExpression node)
        {
           
            if (!Check.IsNull(node.Expression)&&currentTables.ContainsKey(node.Expression.ToString()))
            {
                    string StrP=node.Expression.ToString();
                    if (Check.IsNull(currentTree.LeftCondition))
                    {
                        currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                        //GetMemberValue(node, currentTree.LeftCondition);
                        currentTree.LeftCondition.PropertyType = node.Type;
                        select.CreateJoin(currentTables[StrP], ref CurrentJoinTable,currentTree.LeftCondition,node.Member.Name);
                        //string Params;
                        //if (!Check.IsNull(member.Expression))
                        //{
                        //    Params = member.Expression.ToString();
                        //    if (currentTables.ContainsKey(Params))
                        //    {

                        //        select.CreateJoin(currentTables[Params], ref CurrentJoinTable);
                        //        condition.DisplayName = $"{select.GetTableName(currentTables[Params])}.{member.Member.Name}";
                        //    }
                        //    else
                        //    {
                        //        VisitMember(member);
                        //    }

                        //}

                    }
                    else if (Check.IsNull(currentTree.RightCondition))
                    {
                        currentTree.RightCondition = new ConditionEntity(eConditionType.ColumnName);
                        select.CreateJoin(currentTables[StrP], ref CurrentJoinTable, currentTree.RightCondition,node.Member.Name);
                }

            }
            else
            {
                select.VisitMember(ref currentTree, node.Member);
            }
            base.VisitMember(node);
            return node;
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {


            base.VisitNew(node);
            return node;
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            base.VisitNewArray(node);
            return node;
        }

        /// <summary>
        /// 条件表达式大于小于等于
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleBinary(BinaryExpression node, Action<Queue<string>> action)
        {

            if (node.Left is ConstantExpression leftConstant)
            {
                currentTree.LeftCondition = new ConditionEntity(eConditionType.Constant);
                currentTree.LeftCondition.DisplayName = GetConstantValue(leftConstant);
            }
            else if (node.Left is MemberExpression leftMember)
            {
                currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                GetMemberValue(leftMember, currentTree.LeftCondition);
                currentTree.LeftCondition.PropertyType = leftMember.Type;

            }
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            if (node.Right is ConstantExpression rightConstant)
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);
                currentTree.RightCondition.DisplayName = GetConstantValue(rightConstant);

            }
            else if (node.Right is MemberExpression rightMember)
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.ColumnName);
                GetMemberValue(rightMember, currentTree.RightCondition);
                currentTree.LeftCondition.PropertyType = rightMember.Type;
            }
            CurrentJoinTable.TreeConditions.Add(currentTree);
            IsComplete = true;

        }

        /// <summary>
        /// 或与非
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        private void SingleLogicBinary(BinaryExpression node, Action<Queue<string>> action)
        {
            if (IsComplete)
            {

                currentTree = new TreeConditionEntity();
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            base.Visit(node.Left);
            currentTree.RightBracket.Add(eSignType.RightBracket);
            if (!Check.IsNull(action))
            {
                action.Invoke(null);
            }
            if (IsComplete)
            {
                currentTree = new TreeConditionEntity();
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            base.Visit(node.Right);
            currentTree.RightBracket.Add(eSignType.RightBracket);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        private void GetMemberValue(MemberExpression member, ConditionEntity condition)
        {
            //string Params;
            //if (!Check.IsNull(member.Expression))
            //{
            //    Params = member.Expression.ToString();
            //    if (currentTables.ContainsKey(Params))
            //    {

            //        select.CreateJoin(currentTables[Params], ref CurrentJoinTable);
            //        condition.DisplayName = $"{select.GetTableName(currentTables[Params])}.{member.Member.Name}";
            //    }
            //    else
            //    {
            //        VisitMember(member);
            //    }

            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constant"></param>
        /// <returns></returns>

        private string GetConstantValue(ConstantExpression constant)
        {
            string mName = string.Empty;
            if (!Check.IsNull(constant))
            {
                mName = constant.Value.ToString();
            }
            return mName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>

        public string GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
