using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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


        public MapVisitor(SelectEntity Select)
        {

            select = Select;
            currentTables = new Dictionary<string, int>();
            select.InitMap();
            IsAgain = true;
        }
        /// <summary>
        /// 修改表达式树的形式
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Expression Modify(Expression expression, bool IsAnonymity = false)
        {
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
        protected override LabelTarget? VisitLabelTarget(LabelTarget? node)
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
            base.VisitMemberBinding(node);

            if (!isAnonymity)
            {
                //非匿名对象属性名称
                if (Check.IsNull(currentmapInfo))
                {
                    currentmapInfo = new MapEntity();
                    select.AddMapInfo(currentmapInfo);
                }
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
            string PropName = node.Member.Name;
            if (node.Member.Name.Equals("Key"))
            {
                return node;
            }
            
            PropertyInfo prop = null;
            string TableName=null;
            if ((node.Expression is ParameterExpression Parameter) && currentTables.ContainsKey(Parameter.Name))
            {
                prop = select.GetPropertyType(currentTables[Parameter.Name],PropName); 
                if (!Check.IsNull(prop))
                {
                    PropName = prop.Name;
                    TableName = select.GetTableName(currentTables[Parameter.Name]);
                }
            }
            if (IsAgain)
            {
                currentmapInfo = select.MapFirstOrDefault(m =>
                 m.PropName.Equals(PropName));
                if (!Check.IsNull(currentmapInfo))
                {
                    currentmapInfo.IsNeed = true;
                    SetPropName();
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
            SetPropName();

        }

        public void SetPropName()
        {
            if (isAnonymity)
            {
                if (Members.Count() > MemberCurrent)
                {
                    currentmapInfo.PropName = Members[MemberCurrent].Name;
                    MemberCurrent++;
                }
            }
        }
    }
}
