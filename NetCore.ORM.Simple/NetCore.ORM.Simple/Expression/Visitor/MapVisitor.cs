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

        private int soft;
        private bool IsAgain;
        private bool IsAnonymity;
        private int MemberCurrent;

        private ContextSelect contextSelect;
        private MapEntity CurrentMapInfo;
        private MethodVisitor methodVisitor;
        private Dictionary<string, int> TableParams;
        private MemberInfo[] Members { get; set; }


        public MapVisitor(ContextSelect _contextSlect)
        {
            TableParams = new Dictionary<string, int>();
            contextSelect = _contextSlect;
            IsAgain = true;
            MapExtension.InitMap(contextSelect.Table, contextSelect.MapInfos);
            methodVisitor = new MethodVisitor(contextSelect);
            methodVisitor.InitConditionVisitor();

        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression,bool IsAnonymity = false)
        {
            this.IsAnonymity = IsAnonymity;
            if (IsAgain)
            {
                MapExtension.AllMapNotNeed(contextSelect.MapInfos);
                TableParams.Clear();
            }
            foreach (ParameterExpression item in ((dynamic)expression).Parameters)
            {
                TableParams.Add(item.Name,TableParams.Count);
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
            CurrentMapInfo = new MapEntity();
            methodVisitor.Modify(node, CurrentMapInfo.Methods,TableParams);
            AddMapInfo();
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
            CurrentMapInfo = new MapEntity();
            base.VisitMemberBinding(node);
            if (!IsAnonymity)
            {
                //非匿名对象属性名称
                //CurrentMapInfo.LastPropName = node.Member.Name;
            }
            CurrentMapInfo.PropName = node.Member.Name;
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
            CustomVisitMember(node);
            return node;
        }

        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (IsAnonymity)
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


        #region method extensions


        public  void CustomVisitMember(MemberExpression node)
        {
            string PropName = node.Member.Name;
            if (node.Member.Name.Equals("Key"))
            {
                if (contextSelect.OrderInfos.Where(g => g.IsGroupBy).Count() == 1)
                {
                    var group = contextSelect.OrderInfos.Where(g => g.IsGroupBy).FirstOrDefault();
                    PropName = group.PropName;
                }
                else
                {
                    throw new Exception("不能将key直接赋值给一个对象的成员!");
                }
            }

            PropertyInfo Property = null;
            string TableName = null;
            if ((node.Expression is ParameterExpression Parameter) && TableParams.ContainsKey(Parameter.Name))
            {
                Property = contextSelect.Table.GetProperty(TableParams[Parameter.Name], PropName);
                if (!Check.IsNull(Property))
                {
                    PropName = Property.GetColName();
                    TableName = contextSelect.Table.GetAsTableName(TableParams[Parameter.Name]);
                }
            }
            if (IsAgain)
            {

                CurrentMapInfo = contextSelect.GetMapInfoByName(TableName,PropName);

                if (!Check.IsNull(CurrentMapInfo))
                {
                    Activate();
                    return;
                }
            }
            if (!Check.IsNull(Property))
            {
                AddMapInfo(node.Expression.ToString(), PropName);
            }
        }
        private void SetAnonymityProperty()
        {
            if (IsAnonymity)
            {
                if (Members.Count() > MemberCurrent)
                {
                    CurrentMapInfo.PropName = Members[MemberCurrent].Name;
                    CurrentMapInfo.PropertyType =(PropertyInfo)Members[MemberCurrent];
                    MemberCurrent++;
                }
            }
        }
        public  void AddMapInfo(string Params, string PropName)
        {
            var index = TableParams[Params];
            CurrentMapInfo =contextSelect.GetMapInfo(index,PropName);
            contextSelect.MapInfos.Add(CurrentMapInfo);
            Activate();
        }
        private void AddMapInfo()
        {
            ConditionEntity condition=null;
            if (!Check.IsNullOrEmpty(CurrentMapInfo.Methods))
            {
                foreach (var method in CurrentMapInfo.Methods)
                {
                    if (!Check.IsNullOrEmpty(method.Parameters)&&method.Parameters.Any(m=>m.ConditionType==eConditionType.ColumnName))
                    {
                        condition = method.Parameters.FirstOrDefault(m => m.ConditionType == eConditionType.ColumnName);
                        break;
                    }
                }
            }

            if (Check.IsNull(condition))
            {
                if (!Check.IsNullOrEmpty(CurrentMapInfo.Methods))
                {
                    foreach (var method in CurrentMapInfo.Methods)
                    {
                        if (Check.IsNullOrEmpty(method.TreeConditions))
                        {
                            if (method.TreeConditions.Any(t=>!Check.IsNull(t.LeftCondition)&&t.LeftCondition.ConditionType==eConditionType.ColumnName))
                            {
                                condition = method.TreeConditions.FirstOrDefault(t => !Check.IsNull(t.LeftCondition) && t.LeftCondition.ConditionType == eConditionType.ColumnName).LeftCondition;
                                break;
                            }
                            else if (method.TreeConditions.Any(t => !Check.IsNull(t.RightCondition) && t.RightCondition.ConditionType == eConditionType.ColumnName))
                            {
                                condition = method.TreeConditions.FirstOrDefault(t => !Check.IsNull(t.LeftCondition) && t.LeftCondition.ConditionType == eConditionType.ColumnName).RightCondition;
                                break;
                            }
                        }
                    }
                }
            }
            if (!Check.IsNull(condition))
            {
                var map = contextSelect.GetMapInfoByName(condition.TableName, condition.ColumnName);
                if (!Check.IsNull(map))
                {
                    if (map.IsNeed)
                    {
                        map = map.Clon();
                        contextSelect.MapInfos.Add(map);
                    }
                    map.Methods = CurrentMapInfo.Methods;
                    CurrentMapInfo = map;
                }
                else
                {
                    condition.CopyColumnProperty(CurrentMapInfo);
                    contextSelect.MapInfos.Add(CurrentMapInfo);
                }
            }
            else
            {
                contextSelect.MapInfos.Add(CurrentMapInfo);
            }
            Activate();
        }
        private void Activate()
        {
            CurrentMapInfo.IsNeed = true;
            CurrentMapInfo.Soft = soft;
            SetAnonymityProperty();
            soft++;
        }
        #endregion

    }
}
