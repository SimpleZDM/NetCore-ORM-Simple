using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
    public class SelectEntity
    {
        public SelectEntity(Type tableAttr, Type columnAttr, params Type[] types)
        {
            MapInfos = new List<MapEntity>(15);
            JoinInfos = new Dictionary<string, JoinTableEntity>();
            Conditions = new List<ConditionEntity>(15);
            TreeConditions = new List<TreeConditionEntity>(15);
            OrderInfos = new List<OrderByEntity>(5);
            LastType = new Dictionary<string, Type>();
            DyToMap = new List<dynamic>();
            LastAnonymity = false;
            Table = new TableEntity(tableAttr, columnAttr, types);
        }

        public Type GetEntityType(int index)
        {
            if (Table.TableNames.Length > index)
            {
                return Table.DicTable[Table.TableNames[index]].ClassType;
            }
            throw new Exception("表不存在!");
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
                return Table.GetTableName(Table.DicTable[Table.TableNames[index]].ClassType);
            }
            throw new Exception("表不存在!");
        }

        public string GetColumnName(int index, string PropName)
        {
            if (Table.TableNames.Length > index)
            {
                return Table.GetColName(Table.DicTable[Table.TableNames[index]].ClassType.GetProperty(PropName));
            }
            throw new Exception("表不存在!");
        }

        public PropertyInfo GetPropertyType(int index, string PropName)
        {
            if (Check.IsNull(PropName))
            {
                throw new ArgumentException("PropName is not null!");
            }
            return GetEntityType(index).GetProperty(PropName);
        }
        public void AddMapInfo(MapEntity map)
        {
            if (Check.IsNull(map))
            {
                throw new ArgumentNullException("map is null!");
            }
            MapInfos.Add(map);
        }

        public MapEntity MapFirstOrDefault(Func<MapEntity, bool> exp)
        {
            return MapInfos.FirstOrDefault(exp);
        }

        /// <summary>
        /// 链接查询中默认左边第一张表为主表(初始化映射信息)
        /// </summary>
        public void InitMap()
        {
            Type type = Table.DicTable[Table.TableNames[0]].ClassType;
            PropertyInfo PropKey = Table.GetKey(type);
            AddMapInfo(new MapEntity()
            {
                TableName = table.TableNames[0],
                ColumnName = Table.GetColName(PropKey),
                PropName = PropKey.Name,
                IsKey = true,
                LastPropName = PropKey.Name,//原始名称
                ClassName = Table.GetTableName(type),
                EntityType = type,
            });
            foreach (var item in Table.GetNotKeyAndIgnore(type))
            {
                mapInfos.Add(new MapEntity()
                {
                    TableName = table.TableNames[0],
                    ColumnName = Table.GetColName(item),
                    PropName = item.Name,
                    IsKey = false,
                    LastPropName = item.Name,
                    ClassName = Table.GetTableName(type),
                    EntityType = type,
                });
            }
        }

        public MapEntity CreateMapInfo(int Index, string PropName, bool IsAnonymity)
        {
            var Prop = GetPropertyType(Index, PropName);
            MapEntity map = new MapEntity();
            map.TableName = Table.TableNames[Index];
            map.ColumnName = Table.GetColName(Prop);
            if (!IsAnonymity)
            {
                map.LastPropName = Prop.Name;
            }
            map.ClassName = Table.GetTableName(Table.DicTable[map.TableName].ClassType);
            map.EntityType = Table.DicTable[map.TableName].ClassType;
            return map;
        }
        /// <summary>
        /// 重新映射的时候将所有的列设置成不需要映射
        /// </summary>
        public void AllMapNotNeed()
        {
            foreach (var item in mapInfos)
            {
                item.IsNeed = false;
            }
        }

        /// <summary>
        /// 添加主表
        /// </summary>
        public void InitJoin()
        {
            JoinInfos.Add(Table.TableNames[0], new JoinTableEntity() { DisplayName = Table.TableNames[0], TableType = eTableType.Master });
        }

        public bool CreateJoin(int index, JoinTableEntity Join)
        {
            if (Check.IsNull(Join))
            {
                Join = new JoinTableEntity();
            }
            string mName = string.Empty;
            if (Table.TableNames.Length > index)
            {

                if (!JoinInfos.ContainsKey(Table.TableNames[index]))
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    Join.AsName = Table.TableNames[index];
                    Join.DisplayName = Table.DicTable[Join.AsName].DisplayNmae;
                    JoinInfos.Add(Join.DisplayName, Join);
                    return true;
                }
            }
            return false;

        }

        public void TreeConditionInit(ref int firstConditionIndex)
        {
            if (TreeConditions.Count > 0)
            {
                TreeConditions[firstConditionIndex].LeftBracket.Add(eSignType.LeftBracket);
                firstConditionIndex = treeConditions.Count - 1;
                TreeConditions[firstConditionIndex].RightBracket.Add(eSignType.RightBracket);
                Conditions.Add(new ConditionEntity(eConditionType.Sign)
                {
                    SignType = eSignType.And

                });
            }
        }
        public void CreateCondition(ConditionEntity condition)
        {
            Conditions.Add(condition);
        }
        public TreeConditionEntity CreateTreeConditon()
        {
            TreeConditionEntity treeCondition = new TreeConditionEntity();
            TreeConditions.Add(treeCondition);
            return treeCondition;
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
                if (MapInfos.Where(m => m.PropName.Equals(PropName)).Any() && MapInfos.Where(m => m.PropName.Equals(PropName)).Count() == 1)
                {
                    var map = MapFirstOrDefault(m => m.PropName.Equals(m.PropName));
                    if (!Check.IsNull(map))
                    {
                        order.TableName = map.TableName;
                        order.ColumnName = map.ColumnName;
                    }

                }
                else
                {
                    if (Index >=0)
                    {
                        Prop = GetPropertyType(Index, PropName);
                        order.TableName = Table.GetTableName(GetEntityType(Index));
                        order.PropName = Prop.Name;
                        order.ColumnName = Table.GetColName(Prop);
                    }


                }
                OrderInfos.Add(order);
            }



        }

        public void CreateLastType<TResult>(int start,int end)
        {
            LastAnonymity = CommonConst.IsAnonymityObject<TResult>(Table.TableAtrr);
            Type type = typeof(TResult);
            if (!LastAnonymity)
            {
                DyToMap.Clear();
                LastType.Clear();
                LastType.Add(type.Name, type);
            }
            else
            {
                if (LastType.Count() == CommonConst.ZeroOrNull)
                {
                    for (int i = start; i <=end; i++)
                    {
                        LastType.Add(Table.TableNames[i], Table.DicTable[Table.TableNames[i]].ClassType);
                    }
                   
                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<MapEntity> MapInfos { get { return mapInfos; } private set { mapInfos = value; } }
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

        public List<dynamic> DyToMap { get { return dyToMap; } set { dyToMap = value; } }

        public Dictionary<string, Type> LastType { get { return lastType; } set { lastType = value; } }

        public TableEntity Table { get { return table; } set { table = value; } }

        private List<MapEntity> mapInfos;
        private Dictionary<string, JoinTableEntity> joinInfos;
        private List<ConditionEntity> conditions;
        private List<TreeConditionEntity> treeConditions;
        private List<OrderByEntity> orderInfos;
        private bool lastAnonymity;
        private List<dynamic> dyToMap;
        private Dictionary<string, Type> lastType;
        private TableEntity table;
        private Type tableAtrr;
        private Type columnAttr;

        public string GetTableName(Type type)
        {
            return type.GetTableName(tableAtrr);
        }
        public string GetColName(PropertyInfo Prop)
        {
            return Prop.GetColName(columnAttr);
        }

        public IEnumerable<PropertyInfo> GetNotKeyAndIgnore(Type type)
        {
            return type.GetNotKeyAndIgnore(columnAttr);
        }
        public IEnumerable<PropertyInfo> GetNoIgnore(Type type)
        {
            return type.GetNoIgnore(columnAttr);
        }
        public PropertyInfo GetKey(Type type)
        {
            return type.GetKey(columnAttr);
        }
        public PropertyInfo GetAutoKey(Type type)
        {
            return type.GetAutoKey(columnAttr);
        }
    }
}
