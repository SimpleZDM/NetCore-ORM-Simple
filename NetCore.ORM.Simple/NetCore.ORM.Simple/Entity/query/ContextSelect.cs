using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 SelectEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/27 13:32:46
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 记录select语句的信息和解析
    /// </summary>
    internal class ContextSelect
    {
        public ContextSelect(params Type[] types)
        {
            MapInfos = new List<MapEntity>(15);
            JoinInfos = new Dictionary<string, JoinTableEntity>();
            Conditions = new List<ConditionEntity>(15);
            TreeConditions = new List<TreeConditionEntity>(15);
            OrderInfos = new List<OrderByEntity>(5);
            LastAnonymity = false;
            Table = new ContextTableEntity(types);
        }

        /// <summary>
        /// 获取表名称
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>





        public void IsAnonymity<TResult>()
        {
            LastAnonymity = CommonConst.IsAnonymityObject<TResult>();
        }




        #region Expression Visitor

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public List<MapEntity> MapInfos { get { return mapInfos; } set { mapInfos = value; } }
        public List<OrderByEntity> OrderInfos { get { return orderInfos; } set { orderInfos = value; } }
        /// <summary>
        /// 链接信息
        /// </summary>
        public Dictionary<string, JoinTableEntity> JoinInfos { get { return joinInfos; } set { joinInfos = value; } }
        /// <summary>
        /// 多个二元表达式中的连接
        /// </summary>
        public List<ConditionEntity> Conditions { get { return conditions; } set { conditions = value; } }
        /// <summary>
        /// 等式集合
        /// </summary>
        public List<TreeConditionEntity> TreeConditions { get { return treeConditions; } set { treeConditions = value; } }

        public bool LastAnonymity { get { return lastAnonymity; } set { lastAnonymity = value; } }



        public ContextTableEntity Table { get { return table; } set { table = value; } }

        private List<MapEntity> mapInfos;
        private Dictionary<string, JoinTableEntity> joinInfos;
        private List<ConditionEntity> conditions;
        private List<TreeConditionEntity> treeConditions;
        private List<OrderByEntity> orderInfos;
        private bool lastAnonymity;
        private ContextTableEntity table;


        #region method order
        public int GetOrderBySoft()
        {

            return OrderInfos.Where(o => o.IsOrderBy).Count() > 0 ? OrderInfos.Where(o => o.IsOrderBy).Max(o => o.OrderSoft) + 1 : 0;

        }
        public int GetGroupBySoft()
        {
            return OrderInfos.Where(g => g.IsGroupBy).Count() > 0 ? OrderInfos.Where(o => o.IsGroupBy).Max(o => o.GroupSoft) + 1 : 0;
        }

        public void SetOrder(OrderByEntity order, string PropName, int Index)
        {
            if (MapInfos.Where(m => !Check.IsNullOrEmpty(m.PropName) && m.PropName.Equals(PropName)).Count() == 1)
            {
                var map = MapInfos.FirstOrDefault(m => !Check.IsNullOrEmpty(m.PropName) && m.PropName.Equals(PropName));
                if (!Check.IsNull(map))
                {
                    order.TableName = map.TableName;
                    order.ColumnName = map.ColumnName;
                    order.PropName = map.PropName;
                }
            }
            else
            {
                if (Index >= 0)
                {
                    var Prop = Table.GetProperty(Index, PropName);
                    order.TableName = Table.GetAsTableName(Index);
                    order.PropName = Prop.Name;
                    order.ColumnName = Prop.GetColName();
                }

            }
        }

        #endregion

        #region condition

        public ConditionEntity GetCondition(eConditionType conditionType)
        {
            ConditionEntity condition = new ConditionEntity(conditionType);
            return condition;
        }

        public  TreeConditionEntity GetTreeConditon()
        {
            TreeConditionEntity treeCondition = new TreeConditionEntity();
            return treeCondition;
        }
        public ConditionEntity GetCondition(eConditionType conditionType, eSignType signType)
        {
            ConditionEntity condition = GetCondition(conditionType);
            condition.SignType = signType;
            return condition;
        }

        public ConditionEntity GetCondition(string PropName, int Index)
        {
            ConditionEntity condition = GetCondition(eConditionType.ColumnName);


            if (Index >= 0 && Index < table.TableNames.Length)
            {
                var Prop = table.GetProperty(Index, PropName);
                var NameEntity = table.GetName(Index);
                condition.AsTableName = NameEntity.AsName;
                condition.TableName = NameEntity.DisplayNmae;
                if (!Check.IsNull(Prop))
                {
                    condition.ColumnName = Prop.GetColName();
                    condition.PropertyType = Prop;
                }
            }
            else if (MapInfos.Where(m => !Check.IsNull(m.PropName) && m.PropName.Equals(PropName)).Any())
            {
                var map = MapInfos.Where(m => !Check.IsNull(m.PropName) && m.PropName.Equals(PropName)).FirstOrDefault();
                condition.ColumnName = map.ColumnName;
                condition.TableName = map.TableName;
                condition.AsTableName = map.TableName;
                condition.PropertyType = map.PropertyType;
            }
            condition.DisplayName = GetDisplayName(condition.AsTableName, condition.ColumnName);
            return condition;
        }

        public string GetDisplayName(string TableName, string PropName)
        {
            if (Check.IsNullOrEmpty(TableName) || Check.IsNullOrEmpty(PropName))
            {
                throw new Exception("未找到属性名称或者表名称!");
            }
            return $"{TableName}{DBMDConst.Dot}{PropName}";
        }
        #endregion

        #region map
        public MapEntity GetMapInfo(int Index, string PropName)
        {
            var Prop = Table.GetProperty(Index, PropName);
            MapEntity map = new MapEntity();
            if (!Check.IsNull(Prop))
            {
                map.TableName = Table.TableNames[Index];
                map.ColumnName = Prop.GetColName();
                map.PropertyType = Prop;
                map.PropName = Prop.GetColName();
      
                map.ClassName = Table.DicTables[map.TableName].ClassType.GetTableName();
                map.EntityType = Table.DicTables[map.TableName].ClassType;
            }
            return map;
        }

        public MapEntity GetMapInfoByName(string TableName, string PropName)
        {
            if (Check.IsNull(PropName))
            {
                throw new ArgumentException(nameof(PropName));
            }
            MapEntity map = null;
            if (!Check.IsNull(TableName))
            {

                if (MapInfos.Any(m =>!Check.IsNullOrEmpty(m.PropName)&&m.PropName.Equals(PropName) && m.TableName == TableName))
                {
                    map = MapInfos.FirstOrDefault(m => !Check.IsNullOrEmpty(m.PropName) &&m.PropName.Equals(PropName) && m.TableName == TableName);
                }
                else if (MapInfos.Any(m => !Check.IsNullOrEmpty(m.ColumnName) &&m.ColumnName.Equals(PropName) && m.TableName == TableName))
                {
                     map = MapInfos.FirstOrDefault(m =>m.ColumnName.Equals(PropName) && m.TableName == TableName);
                }
            }
            else
            {
                if (MapInfos.Any(m =>!Check.IsNullOrEmpty(m.PropName)&& m.PropName.Equals(PropName)))
                {
                    map = MapInfos.FirstOrDefault(m => !Check.IsNullOrEmpty(m.PropName) && m.PropName.Equals(PropName));
                }
            }
            return map;
        }
        #endregion
    }
}
