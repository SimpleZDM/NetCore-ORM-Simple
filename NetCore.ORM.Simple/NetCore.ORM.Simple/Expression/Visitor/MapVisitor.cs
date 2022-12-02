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
    public class MapVisitor : ExpressionVisitor
    {

        /// <summary>
        /// 所有的表
        /// </summary>

        private SelectEntity select;
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

        private string CurrentMethodName;
        /// <summary>
        /// 匿名类标记
        /// </summary>
        private bool isAnonymity;

        private bool isMethodMultiParams;
        private bool isMethodParamsStart;

        private int soft;


        public MapVisitor(SelectEntity Select)
        {

            select = Select;
            currentTables = new Dictionary<string, int>();
            select.InitMap();
            IsAgain = true;
            isMethodMultiParams = false;
            isMethodParamsStart = false;

        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression, bool IsAnonymity = false)
        {
            soft = 1;
            currentTables.Clear();
            isAnonymity = IsAnonymity;
            if (IsAgain)
            {

                select.AllMapNotNeed();
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
            ConditionEntity condition = null;
            switch (node.NodeType)
            {
                case ExpressionType.AndAlso:

                    break;
                case ExpressionType.Call:
                    break;
                case ExpressionType.GreaterThan:
                    if (!Check.IsNull(currentmapInfo))
                    {
                        condition = select.GetCondition(eConditionType.Sign, eSignType.GrantThan);
                        currentmapInfo.Conditions.Enqueue(condition);
                    }
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    if (!Check.IsNull(currentmapInfo))
                    {
                        condition = select.GetCondition(eConditionType.Sign, eSignType.GreatThanOrEqual);
                        currentmapInfo.Conditions.Enqueue(condition);
                    }
                    break;
                case ExpressionType.LessThan:
                    if (!Check.IsNull(currentmapInfo))
                    {
                        condition = select.GetCondition(eConditionType.Sign, eSignType.LessThan);
                        currentmapInfo.Conditions.Enqueue(condition);
                    }
                    break;
                case ExpressionType.LessThanOrEqual:
                    if (!Check.IsNull(currentmapInfo))
                    {
                        condition = select.GetCondition(eConditionType.Sign, eSignType.LessThanOrEqual);
                        currentmapInfo.Conditions.Enqueue(condition);
                    }

                    break;
                case ExpressionType.Equal:
                    if (!Check.IsNull(currentmapInfo))
                    {
                        condition = select.GetCondition(eConditionType.Sign, eSignType.Equal);
                        currentmapInfo.Conditions.Enqueue(condition);
                    }
                    break;
                case ExpressionType.NotEqual:
                    if (!Check.IsNull(currentmapInfo))
                    {
                        condition = select.GetCondition(eConditionType.Sign, eSignType.NotEqual);
                        currentmapInfo.Conditions.Enqueue(condition);
                    }
                    break;
                case ExpressionType.OrElse:

                    break;
                case ExpressionType.ArrayIndex:

                    base.VisitBinary(node);
                    break;
                default:
                    break;
            }
            return node;
            return node;
        }

        /// <summary>
        /// 表达式树的常量操作
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (!Check.IsNull(currentmapInfo))
            {
                var condition = select.GetCondition(eConditionType.Constant);
                condition.Value = node.Value;
                currentmapInfo.Conditions.Enqueue(condition);
            }
            return base.VisitConstant(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {

            isMethodMultiParams = Check.IsMethodMultiParams(node.Method.Name);

            base.VisitMethodCall(node);
            if (Check.IsNull(currentmapInfo))
            {
                currentmapInfo = new MapEntity();
                currentmapInfo.MethodName = node.Method.Name;
                select.AddMapInfo(currentmapInfo);
            }
            else
            {
                currentmapInfo.MethodName = node.Method.Name;
            }

            if (isAnonymity && string.IsNullOrEmpty(currentmapInfo.ColumnName))
            {
                currentmapInfo.Soft = soft;
                SetPropName();
                soft++;
            }
            if (isAnonymity && currentmapInfo.PropertyType != null &&
                select.MapInfos.Any(m => m.PropertyType != null && m.PropertyType.Equals(currentmapInfo.PropertyType)))
            {
                currentmapInfo = null;
            }
            isMethodMultiParams = false;
            isMethodParamsStart = false;
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
            if (!select.MapInfos.Any(m => m.PropName.Equals(currentmapInfo.PropName)))
            {
                select.AddMapInfo(currentmapInfo);
            }
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
            string PropName = node.Member.Name;
            if (node.Member.Name.Equals("Key"))
            {
                if (select.OrderInfos.Where(g => g.IsGroupBy).Count() == 1)
                {
                    var group = select.OrderInfos.Where(g => g.IsGroupBy).FirstOrDefault();
                    PropName = group.PropName;
                }
                else
                {
                    throw new Exception("不能将key直接赋值给一个对象的成员!");
                }
            }

            PropertyInfo prop = null;
            string TableName = null;
            if ((node.Expression is ParameterExpression Parameter) && currentTables.ContainsKey(Parameter.Name))
            {
                prop = select.GetPropertyType(currentTables[Parameter.Name], PropName);
                if (!Check.IsNull(prop))
                {
                    PropName = prop.Name;
                    TableName = select.GetAsTableName(currentTables[Parameter.Name]);
                }
            }


            if (IsAgain)
            {

                MapEntity map = null;
                if (!Check.IsNull(TableName))
                {
                    map = select.MapFirstOrDefault(m =>
                  m.PropName.Equals(PropName) && m.TableName == TableName);
                    if (map==null)
                    {
                        map=select.MapFirstOrDefault(m =>
                        m.ColumnName.Equals(PropName) && m.TableName == TableName);
                    }
                }
                else
                {
                    map = select.MapFirstOrDefault(m =>
                    m.PropName.Equals(PropName));
                }

                if (isMethodMultiParams && isMethodParamsStart)
                {
                    if (!Check.IsNull(map))
                    {
                        var condition = select.GetCondition(eConditionType.ColumnName);
                        condition.DisplayName = $"{map.TableName}{DBMDConst.Dot}{map.ColumnName}";
                        currentmapInfo.Conditions.Enqueue(condition);
                    }
                    return node;
                }
                if (isMethodMultiParams)
                {
                    isMethodParamsStart = true;
                }

                currentmapInfo = map;


                if (!Check.IsNull(currentmapInfo))
                {
                    if (currentmapInfo.IsNeed == true)
                    {
                        CreateMap();
                    }
                    else
                    {
                        currentmapInfo.IsNeed = true;
                        currentmapInfo.Soft = soft;
                        SetPropName();
                        soft++;
                    }
                    return node;
                }
                else
                {
                    if (!Check.IsNull(prop))
                    {
                        CreateMap(node.Expression.ToString(), PropName);
                    }
                }
            }
            else
            {
                if (!Check.IsNull(prop))
                {
                    CreateMap(node.Expression.ToString(), PropName);
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
        public void CreateMap(string Params, string PropName)
        {
            var index = currentTables[Params];
            currentmapInfo = select.CreateMapInfo(index, PropName, isAnonymity);
            select.AddMapInfo(currentmapInfo);
            currentmapInfo.Soft = soft;
            soft++;
            SetPropName();

        }
        public void CreateMap()
        {
            currentmapInfo = currentmapInfo.Clon();
            select.AddMapInfo(currentmapInfo);
            currentmapInfo.Soft = soft;
            soft++;
            SetPropName();
        }

        public void SetPropName()
        {
            if (isAnonymity)
            {
                if (Members.Count() > MemberCurrent)
                {
                    currentmapInfo.PropName = Members[MemberCurrent].Name;
                    currentmapInfo.PropertyType = ((PropertyInfo)Members[MemberCurrent]).PropertyType;
                    MemberCurrent++;
                }
            }
        }
    }
}
