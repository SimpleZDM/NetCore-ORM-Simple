using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Visitor;
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

        public bool CreateJoin(int index,ref JoinTableEntity Join,ConditionEntity condition,string memberName)
        {
            if (Check.IsNull(Join))
            {
                Join = new JoinTableEntity();
            }
            if (Check.IsNull(condition))
            {
                condition = new ConditionEntity(eConditionType.ColumnName);
            }
            string mName = string.Empty;
            if (Table.TableNames.Length > index)
            {
                condition.DisplayName = $"{GetTableName(index)}.{memberName}";
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
                if (MapInfos.Where(m => m.PropName.Equals(PropName)).Count() == 1)
                {
                    var map = MapFirstOrDefault(m => m.PropName.Equals(PropName));
                    if (!Check.IsNull(map))
                    {
                        order.TableName = map.TableName;
                        order.ColumnName = map.ColumnName;
                    }

                }
                else
                {
                    if (Index >= 0)
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

        public void CreateLastType<TResult>(int start, int end)
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
                    for (int i = start; i <= end; i++)
                    {
                        LastType.Add(Table.TableNames[i], Table.DicTable[Table.TableNames[i]].ClassType);
                    }

                }

            }
        }




        #region Expression Visitor


        /// <summary>
        /// 解析 或与非
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        public void SingleLogicBinary(BinaryExpression node,ref TreeConditionEntity currentTree, eSignType signType,ref bool IsComplete,Action<Expression,TreeConditionEntity> Visitor)
        {
            if (Check.IsNull(Visitor))
            {
                throw new Exception("is null visitor!");
            }
            if (IsComplete)
            {
                currentTree = CreateTreeConditon();
                //bool res = object.ReferenceEquals(currentTree,TreeConditions[TreeConditions.Count-1]);
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Left,currentTree);
            currentTree.RightBracket.Add(eSignType.RightBracket);

            CreateCondition(new ConditionEntity(eConditionType.Sign)
            {
                SignType = signType
            });
            if (IsComplete)
            {
                currentTree = CreateTreeConditon();
                //bool res = object.ReferenceEquals(currentTree, TreeConditions[TreeConditions.Count - 1]);
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Right,currentTree);
            currentTree.RightBracket.Add(eSignType.RightBracket);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool GetMemberValue(MemberExpression member,ref ConditionEntity condition, int Index, bool IsMultipleMap)
        {
            string mName = string.Empty;
            if (!Check.IsNull(member))
            {
                if(SetConstMember(member,ref condition))
                {
                    return true;
                }
                if (IsMultipleMap)
                {
                    var map = MapInfos.Where((map) => map.PropName.Equals(member.Member.Name)).ToArray();

                    if (!Check.IsNull(map) && map.Count() == 1)
                    {
                        condition.DisplayName = $"{map[0].TableName}.{map[0].ColumnName}";
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (Table.TableNames.Length > CommonConst.ZeroOrNull)
                    {
                        if (!Check.IsNull(member.Expression))
                        {
                            condition.DisplayName = $"{GetTableName(Index)}.{member.Member.Name}";
                        }
                        else
                        {
                            SetConstMember(member,ref condition);
                        }

                    }
                    else
                    {
                        condition.DisplayName= member.Member.Name;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constant"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public void GetConstantValue(ConstantExpression constant,ConditionEntity condition)
        {
            string mName = string.Empty;
            if (!Check.IsNull(constant))
            {

                if (constant.Type == typeof(string))
                {
                    condition.DisplayName = $"'{constant.Value}'";
                }
                else
                {
                    condition.DisplayName = $"{constant.Value}";
                }
            }
        }

        public bool VisitConstant(ref TreeConditionEntity currentTree,ConstantExpression node)
        {
           
            if (Check.IsNull(currentTree.RightCondition))
            {
                currentTree.RightCondition = new ConditionEntity(eConditionType.Constant);
            }
            else
            {
                if (!currentTree.RightCondition.ConditionType.Equals(eConditionType.Constant))
                {
                    currentTree.RightCondition.ConditionType = eConditionType.Constant;
                }
            }
            if (!Check.IsNullOrEmpty(currentTree.RightCondition.DisplayName))
            {
                return false;
            }
            if (!Check.IsNull(currentTree.LeftCondition.ConstPropType))
            {
                if (!Check.IsNull(currentTree.LeftCondition.ConstFieldType) 
                    && currentTree.LeftCondition.ConstFieldType.Count() > 0
                    )
                {
                    if (node.Value.GetType().GetField(currentTree.LeftCondition.ConstFieldType[0].Name) == null)
                    {
                        SetField(ref currentTree,node);
                    }
                    else
                    {
                        var obj = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                        SetProperty(ref currentTree, node, obj);
                    }
                  

                }
                else { currentTree.RightCondition.DisplayName = node.Value.ToString(); }

            }
            else if (!Check.IsNull(currentTree.LeftCondition.ConstFieldType) && currentTree.LeftCondition.ConstFieldType.Count > 0)
            {
                SetField(ref currentTree,node);
            }
            else
            {
                currentTree.RightCondition.DisplayName = node.Value.ToString();
            }
            return true;
        }

        public void SetField(ref TreeConditionEntity currentTree, ConstantExpression node)
        {
            if (!Check.IsNullOrEmpty(currentTree.RelationCondition.DisplayName) && currentTree.RelationCondition.DisplayName.Equals("Contains"))
            {

                currentTree.RightCondition.Value = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                currentTree.RightCondition.PropertyType = currentTree.RightCondition.Value.GetType();
            }
            else if (currentTree.LeftCondition.ConstFieldType.Count >= 2)
            {
                var obj = currentTree.LeftCondition.ConstFieldType[1].GetValue(node.Value);
                if (currentTree.Index >= 0)
                {
                    currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstFieldType[0].GetValue(((dynamic)obj)[currentTree.Index]).ToString();
                }
                else
                {

                    currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstFieldType[0].GetValue(obj).ToString();
                }

            }
            else if (currentTree.Index >= 0)
            {
                var obj = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.Index, obj);
            }
            else if (!Check.IsNullOrEmpty(currentTree.Key))
            {
                var obj = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value);
                currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.Key, obj);
            }
            else
            {
                currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstFieldType[0].GetValue(node.Value).ToString();
            }
        }

        public void SetProperty(ref TreeConditionEntity currentTree, ConstantExpression node,object obj)
        {
            if (currentTree.Index >= 0)
            {
                currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.Index, obj, currentTree.LeftCondition.ConstPropType);
                if (Check.IsNull(currentTree.RightCondition.DisplayName))
                {
                    var currentobj = ((dynamic)obj)[currentTree.Index];

                    if (!Check.IsNull(currentobj.GetType().GetProperty(currentTree.LeftCondition.ConstPropType.Name)))
                    {
                        currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstPropType.GetValue(currentobj).ToString();
                    }
                    else
                    {
                        SetField(ref currentTree, node);
                    }
                        
                }
            }
            else if (!Check.IsNullOrEmpty(currentTree.Key))
            {
                currentTree.RightCondition.DisplayName = ArrayExtension.GetValue(currentTree.Key, obj, currentTree.LeftCondition.ConstPropType);
            }
            else
            {
                if (!Check.IsNull(obj.GetType().GetProperty(currentTree.LeftCondition.ConstPropType.Name)))
                {
                    currentTree.RightCondition.DisplayName = currentTree.LeftCondition.ConstPropType.GetValue(obj).ToString();
                }
                else
                {
                    currentTree.RightCondition.DisplayName = obj.ToString();
                }
            }
        }
        public void VisitMember(ref TreeConditionEntity currentTree,MemberInfo member)
        {
            if (member is FieldInfo field)
            {
                if (Check.IsNull(currentTree.LeftCondition))
                {
                    currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                }
                currentTree.LeftCondition.ConstFieldType.Add(field);
            }
            else if (member is PropertyInfo prop)
            {
                if (Check.IsNull(currentTree.LeftCondition))
                {
                    currentTree.LeftCondition = new ConditionEntity(eConditionType.ColumnName);
                }
                currentTree.LeftCondition.ConstPropType = prop;
            }

            //SetConstMember(member,currentTree.RightCondition);
        }

        public bool SetConstMember(MemberExpression member,ref ConditionEntity condition)
        {
            bool value = false;
            if (member.ToString().Equals("DateTime.Now"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;

                value = true;
            }
            else if (member.ToString().Equals("DateTime.MaxValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.MaxValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("DateTime.MinValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("DateTime.MinValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("Guid.Empty"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = Guid.Empty.ToString();
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("int.MaxValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = int.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("int.MinValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = int.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            return value;
        }
        public bool SetConstMember(MemberInfo member, ConditionEntity condition)
        {
           
            bool value = false;
            if (member.ToString().Equals("DateTime.Now"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;

                value = true;
            }
            else if (member.ToString().Equals("DateTime.MaxValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.MaxValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("DateTime.MinValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("DateTime.MinValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("Guid.Empty"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName =Guid.Empty.ToString();
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("int.MaxValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = int.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            else if (member.ToString().Equals("int.MinValue"))
            {
                if (Check.IsNull(condition))
                {
                    condition = new ConditionEntity(eConditionType.Constant);
                }
                condition.DisplayName = int.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
                value = true;
            }
            return value;
        }

        public void VisitMethod(ref TreeConditionEntity currentTree,MethodCallExpression node)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = CreateTreeConditon();
            }

            if (Check.IsNull(currentTree.RelationCondition))
            {
                currentTree.RelationCondition = new ConditionEntity(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;

            }
            if (node.Arguments.Count() >= 1)
            {
                int value = 0;
                if (node.Arguments[0] is MethodCallExpression call)
                {
                    if (call.Arguments[0].GetType() == typeof(int) && int.TryParse(call.Arguments[0].ToString(),out value))
                    {
                        currentTree.Index = value;
                    }
                    else
                    {
                        currentTree.Key = call.Arguments[0].ToString();
                    }

                }
                if (node.Arguments[0] is ConstantExpression content)
                {
                    if (content.Value.GetType()==typeof(int)&&int.TryParse(content.Value.ToString(),out value))
                    {
                        currentTree.Index = value;
                    }
                    else
                    {
                        currentTree.Key = content.Value.ToString();
                    }
                }

            }
        }

        #endregion

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
    }
}
