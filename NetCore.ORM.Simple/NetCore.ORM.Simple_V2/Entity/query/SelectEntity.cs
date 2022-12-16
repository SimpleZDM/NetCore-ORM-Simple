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
    public class SelectEntity
    {
        public SelectEntity(params Type[] types)
        {
            MapInfos = new List<MapEntity>(15);
            JoinInfos = new Dictionary<string, JoinTableEntity>();
            Conditions = new List<ConditionEntity>(15);
            TreeConditions = new List<TreeConditionEntity>(15);
            OrderInfos = new List<OrderByEntity>(5);
            LastAnonymity = false;
            Table = new TableEntity(types);
        }

        /// <summary>
        /// 获取表名称
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetTableName(int index)
        {
            if (Table.TableNames.Length > index)
            {
                //Table.GetTableName(Table.DicTable[Table.TableNames[index]].ClassType);
                var nameEntity =Table.GetTableName(index);
                return nameEntity.DisplayNmae;
            }
            throw new Exception("表不存在!");
        }
        public string GetAsTableName(int index)
        {
            if (Table.TableNames.Length > index)
            {
                //Table.GetTableName(Table.DicTable[Table.TableNames[index]].ClassType);
                var nameEntity = Table.GetTableName(index);
                return nameEntity.AsName;
            }
            throw new Exception("表不存在!");
        }

        public PropertyInfo GetProperty(int index, string PropName)
        {
            if (Table.TableNames.Length > index)
            {
                return Table.DicTable[Table.TableNames[index]].ClassType.GetProperty(PropName);
            }
            throw new Exception("表不存在!");
        }

        public void AddMapInfo(MapEntity map)
        {
            if (Check.IsNull(map))
            {
                throw new ArgumentNullException("map is null!");
            }
            MapInfos.Add(map);
        }


       




        public void CreateOrder(string PropName, int Index, eOrderOrGroupType OrderOrGroup, eOrderType OrderType)
        {
            if (OrderInfos.Any(info =>
            info.PropName.Equals(PropName) ||
            (PropName.Contains("key") && PropName.Replace("key", "").Equals(info.PropName))
            ))
            {
                var order = OrderInfos.Where(info => info.PropName.Equals(PropName) ||
                           (PropName.Contains("key") && PropName.Replace("key", "")
                           .Equals(info.PropName))).FirstOrDefault();
                switch (OrderOrGroup)
                {
                    case eOrderOrGroupType.OrderBy:
                        order.IsOrderBy = true;
                        order.OrderType = OrderType;
                        break;
                    case eOrderOrGroupType.GroupBy:
                        order.IsGroupBy = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                OrderByEntity order = new OrderByEntity();
                PropertyInfo Prop = null;
                switch (OrderOrGroup)
                {
                    case eOrderOrGroupType.OrderBy:
                        order.IsOrderBy = true;
                        order.OrderType = OrderType;
                        order.OrderSoft = OrderInfos.Where(o => o.IsOrderBy).Count() > 0 ? OrderInfos.Where(o => o.IsOrderBy).Max(o => o.OrderSoft) + 1 : 0;
                        break;
                    case eOrderOrGroupType.GroupBy:
                        order.IsGroupBy = true;
                        order.GroupSoft = OrderInfos.Where(g => g.IsGroupBy).Count() > 0 ? OrderInfos.Where(o => o.IsGroupBy).Max(o => o.GroupSoft) + 1 : 0;
                        break;
                    default:
                        break;
                }
                if (MapInfos.Where(m => m.PropName.Equals(PropName)).Count() == 1)
                {
                    var map = MapInfos.FirstOrDefault(m => m.PropName.Equals(PropName));
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
                        Prop = GetProperty(Index,PropName);
                        order.TableName =GetAsTableName(Index);
                        order.PropName = Prop.Name;
                        order.ColumnName = Prop.GetColName();
                    }

                }
                OrderInfos.Add(order);
            }



        }

        public void CreateLastType<TResult>(int start, int end)
        {
            LastAnonymity = CommonConst.IsAnonymityObject<TResult>();
        }




        #region Expression Visitor

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public List<MapEntity> MapInfos { get { return mapInfos; }  set { mapInfos = value; } }
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



        public TableEntity Table { get { return table; } set { table = value; } }

        private List<MapEntity> mapInfos;
        private Dictionary<string, JoinTableEntity> joinInfos;
        private List<ConditionEntity> conditions;
        private List<TreeConditionEntity> treeConditions;
        private List<OrderByEntity> orderInfos;
        private bool lastAnonymity;
        private TableEntity table;
    }
}
