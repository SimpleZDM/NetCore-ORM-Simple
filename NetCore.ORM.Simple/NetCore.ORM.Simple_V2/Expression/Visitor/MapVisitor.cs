using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
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
    internal class MapVisitor : ExpressionVisitor
    {


        /// <summary>
        /// 当前传入的参数表
        /// </summary>

        private Dictionary<string, int> currentTables;

        /// <summary>
        /// 映射的信息
        /// </summary>
        private MemberInfo[] Members { get; set; }

        private int MemberCurrent;

        private MapEntity currentmapInfo;
        private bool IsAgain;

        /// <summary>
        /// 匿名类标记
        /// </summary>
        private bool isAnonymity;


        private int soft;
        private SelectEntity select;
        private MethodVisitor methodVisitor;




        public MapVisitor(SelectEntity _select)
        {

            // select = Select;
            currentTables = new Dictionary<string, int>();
            //select.InitMap();
            select = _select;
            MapExtension.InitMap(select.Table, select.MapInfos);
            IsAgain = true;
            methodVisitor = new MethodVisitor(select.Table);
            methodVisitor.InitConditionVisitor();


        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression,bool IsAnonymity = false)
        {
            soft = 1;
            currentTables.Clear();
            isAnonymity = IsAnonymity;
            if (IsAgain)
            {
                MapExtension.AllMapNotNeed(select.MapInfos);
            }
            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                currentTables.Add(item.Name, currentTables.Count);
            }
            Visit(expression);
            currentmapInfo = null;
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
            return node;
        }

        /// <summary>
        /// 表达式树的常量操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            currentmapInfo = new MapEntity();
            methodVisitor.Modify(node, currentmapInfo.Methods, currentTables, select.MapInfos.ToArray());
            if (!Check.IsNullOrEmpty(currentmapInfo.Methods))
            {
                if (!Check.IsNullOrEmpty(currentmapInfo.Methods[0].Parameters))
                {
                    if (select.MapInfos.Any(p=>
                    p.TableName==currentmapInfo.Methods[0].Parameters[0].TableName&&
                    p.ColumnName == currentmapInfo.Methods[0].Parameters[0].ColumnName
                    ))
                    {
                        var map = select.MapInfos.Where(p=>p.TableName == currentmapInfo.Methods[0].Parameters[0].TableName &&
                    p.ColumnName == currentmapInfo.Methods[0].Parameters[0].ColumnName).FirstOrDefault();
                        map.IsNeed = true;
                        map.Soft = soft;
                        MapExtension.SetPropName(isAnonymity,ref MemberCurrent,ref currentmapInfo,Members);
                        soft++;
                        map.Methods = currentmapInfo.Methods;
                        currentmapInfo = map;
                    }
                }
                else
                {
                    MapExtension.CreateMap(currentmapInfo.Methods[0], ref select, isAnonymity, ref currentmapInfo, ref soft, ref MemberCurrent, Members);

                }
            }
            currentmapInfo.MethodName = node.Method.Name;
            return node;
        }

        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return base.VisitRuntimeVariables(node);
        }

        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return base.VisitMemberMemberBinding(node);
        }
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            return base.VisitLabelTarget(node);
        }
        /// <summary>
        /// 用于解析值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            currentmapInfo = new MapEntity();
            base.VisitMemberBinding(node);
            if (!isAnonymity)
            {
                //非匿名对象属性名称
                currentmapInfo.LastPropName = node.Member.Name;
            }
            currentmapInfo.PropName = node.Member.Name;
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {

            return base.VisitParameter(node);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
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
            node.VisitMember(ref select,currentTables,ref currentmapInfo,ref IsAgain,ref soft,ref MemberCurrent,Members,ref isAnonymity);
            return node;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (isAnonymity)
            {
                Members = node.Members.ToArray();
                MemberCurrent = 0;
            }
            return base.VisitNew(node);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
    }
}
