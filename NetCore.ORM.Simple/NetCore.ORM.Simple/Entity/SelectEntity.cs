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
            JoinTableEntity joinTable = GetJoinEntity(eTableType.Master);
            joinTable.DisplayName = Table.TableNames[0];
            CreateJoinTable(Table.TableNames[0], ref joinTable);
        }

        public bool CreateJoin(int index, JoinTableEntity Join, ConditionEntity condition, string memberName)
        {
            if (Check.IsNull(Join))
            {
                Join = new JoinTableEntity();
            }
            if (Check.IsNull(condition))
            {
                condition = GetCondition(eConditionType.ColumnName);
            }
            string mName = string.Empty;
            if (Table.TableNames.Length > index)
            {
                condition.DisplayName = $"{GetTableName(index)}.{memberName}";
                if (!JoinInfos.ContainsKey(Table.TableNames[index]))
                {
                    Join.AsName = Table.TableNames[index];
                    Join.DisplayName = Table.DicTable[Join.AsName].DisplayNmae;//表的名称
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
        public ConditionEntity GetCondition(eConditionType conditionType, eSignType signType)
        {
            ConditionEntity condition = GetCondition(conditionType);
            condition.SignType = signType;
            return condition;
        }
        public ConditionEntity GetCondition(eConditionType conditionType)
        {
            ConditionEntity condition = new ConditionEntity(conditionType);
            return condition;
        }
        /// <summary>
        /// 列名称
        /// </summary>
        /// <param name="PropName"></param>
        /// <param name="Index"></param>
        /// <param name="type"></param>
        /// <param name="conditionType"></param>
        /// <returns></returns>
        public ConditionEntity GetCondition(string PropName,int Index,Type type)
        {
            ConditionEntity conditon = null;
            var maps = mapInfos.Where(m=>m.PropName.Equals(PropName)).ToArray();
            if (!Check.IsNullOrEmpty(maps)&&maps.Count()==1)
            {
                conditon = GetCondition(eConditionType.ColumnName); 
                conditon.DisplayName = $"{maps[0].TableName}.{maps[0].ColumnName}";
                conditon.PropertyType = type;
            }
            else  if (Index>=0 &&Index<table.TableNames.Length)
            {
                
                    var Prop = GetPropertyType(Index,PropName);
                    var TableName =GetTableName(Index);
                    if (!Check.IsNull(Prop))
                    {
                        PropName = GetColumnName(Index, PropName);
                        conditon.DisplayName = $"{TableName}{PropName}";
                    }
            }
            if (!Check.IsNull(conditon))
            {
                conditon.PropertyType=type;
            }
            return conditon;
        }
        public void CreateCondition(ConditionEntity condition)
        {
            if (!Check.IsNull(condition))
            {
                Conditions.Add(condition);
            }

        }

       


        public TreeConditionEntity GetTreeConditon()
        {
            TreeConditionEntity treeCondition = new TreeConditionEntity();
            return treeCondition;
        }
        public void AddTreeConditon(TreeConditionEntity treeCondition)
        {
            if (!Check.IsNull(treeCondition))
            {
                TreeConditions.Add(treeCondition);
            }
        }
        public JoinTableEntity GetJoinEntity(eTableType TableType)
        {
            JoinTableEntity joinTable = new JoinTableEntity();
            joinTable.TableType = TableType;
            return joinTable;
        }

        public void CreateJoinTable(string key, ref JoinTableEntity joinTable)
        {
            if (Check.IsNullOrEmpty(key) || Check.IsNull(joinTable))
            {
                return;
            }
            if (!JoinInfos.ContainsKey(key))
            {
                JoinInfos.Add(key, joinTable);
            }
        }

        //public void CreateOrder(string PropName, int Index, eOrderOrGroupType OrderOrGroup, eOrderType OrderType)
        //{
        //    if (OrderInfos.Any(info =>
        //    info.PropName.Equals(PropName) ||
        //    (PropName.Contains("key") && PropName.Replace("key", "").Equals(info.PropName))
        //    ))
        //    {
        //        var order = OrderInfos.Where(info => info.PropName.Equals(PropName) ||
        //                   (PropName.Contains("key") && PropName.Replace("key", "")
        //                   .Equals(info.PropName))).FirstOrDefault();
        //        switch (OrderOrGroup)
        //        {
        //            case eOrderOrGroupType.OrderBy:
        //                order.IsOrderBy = true;
        //                order.OrderType = OrderType;
        //                break;
        //            case eOrderOrGroupType.GroupBy:
        //                order.IsGroupBy = true;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        OrderByEntity order = new OrderByEntity();
        //        PropertyInfo Prop = null;
        //        switch (OrderOrGroup)
        //        {
        //            case eOrderOrGroupType.OrderBy:
        //                order.IsOrderBy = true;
        //                order.OrderType = OrderType;
        //                order.OrderSoft = OrderInfos.Where(o => o.IsOrderBy).Count() > 0 ? OrderInfos.Where(o => o.IsOrderBy).Max(o => o.OrderSoft) + 1 : 0;
        //                break;
        //            case eOrderOrGroupType.GroupBy:
        //                order.IsGroupBy = true;
        //                order.GroupSoft = OrderInfos.Where(g => g.IsGroupBy).Count() > 0 ? OrderInfos.Where(o => o.IsGroupBy).Max(o => o.GroupSoft) + 1 : 0;
        //                break;
        //            default:
        //                break;
        //        }
        //        if (MapInfos.Where(m => m.PropName.Equals(PropName)).Count() == 1)
        //        {
        //            var map = MapFirstOrDefault(m => m.PropName.Equals(PropName));
        //            if (!Check.IsNull(map))
        //            {
        //                order.TableName = map.TableName;
        //                order.ColumnName = map.ColumnName;
        //            }

        //        }
        //        else
        //        {
        //            if (Index >= 0)
        //            {
        //                Prop = GetPropertyType(Index, PropName);
        //                order.TableName = Table.GetTableName(GetEntityType(Index));
        //                order.PropName = Prop.Name;
        //                order.ColumnName = Table.GetColName(Prop);
        //            }


        //        }
        //        OrderInfos.Add(order);
        //    }



        //}

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
                if (LastType.Count() == CommonConst.Zero)
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
        /// 多个二元表达式
        /// </summary>
        /// <param name="node"></param>
        /// <param name="currentTree"></param>
        /// <param name="signType"></param>
        /// <param name="IsComplete"></param>
        /// <param name="Visitor"></param>
        /// <exception cref="Exception"></exception>
        public void MultipleBinary(BinaryExpression node,ref TreeConditionEntity currentTree,
            eSignType signType,ref bool IsComplete, Action<Expression> Visitor,
            JoinTableEntity currentJoin=null)
        {
            if (Check.IsNull(Visitor))
            {
                throw new Exception("visitor is not null!");
            }
            if (IsComplete)
            {
                currentTree = GetTreeConditon();
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Left);
            currentTree.RightBracket.Add(eSignType.RightBracket);

            if (Check.IsNull(currentJoin))
            {
                CreateCondition(GetCondition(eConditionType.Sign, signType));
            }
            else
            {
                currentJoin.Conditions.Add(GetCondition(eConditionType.Sign, signType));
            }

            if (IsComplete)
            {
                currentTree = GetTreeConditon();
                IsComplete = false;
            }
            currentTree.LeftBracket.Add(eSignType.LeftBracket);
            Visitor(node.Right);
            currentTree.RightBracket.Add(eSignType.RightBracket);
        }
        public void SingleBinary(BinaryExpression node, Action<Expression> Visitor,
            ref TreeConditionEntity currentTree,ref bool IsComplete,eSignType signType
            )
        {
            JoinTableEntity join = null;
            SingleBinary(node,Visitor,ref currentTree,ref IsComplete,signType,ref join);
        }

        public void SingleBinary(BinaryExpression node, Action<Expression> Visitor,
           ref TreeConditionEntity currentTree, ref bool IsComplete, eSignType signType,
           ref JoinTableEntity currentJoin
           )
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = GetTreeConditon();
            }
            Visitor(node.Left);

            currentTree.RelationCondition = GetCondition(eConditionType.Sign, signType);

            Visitor(node.Right);
            //区分连接的条件和
            //查询后面的条件
            if (!Check.IsNull(currentJoin))
            {

                currentJoin.TreeConditions.Add(currentTree);
            }
            else
            {
                TreeConditions.Add(currentTree);
            }
            IsComplete = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        //public bool GetMemberValue(MemberExpression member, ref ConditionEntity condition, int Index, bool IsMultipleMap)
        //{
        //    string mName = string.Empty;
        //    if (!Check.IsNull(member))
        //    {
        //        if (SetConstMember(member, ref condition))
        //        {
        //            return true;
        //        }
        //        if (IsMultipleMap)
        //        {
        //            var map = MapInfos.Where((map) => map.PropName.Equals(member.Member.Name)).ToArray();

        //            if (!Check.IsNull(map) && map.Count() == 1)
        //            {
        //                condition.DisplayName = $"{map[0].TableName}.{map[0].ColumnName}";
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            if (Table.TableNames.Length > CommonConst.Zero)
        //            {
        //                if (!Check.IsNull(member.Expression))
        //                {
        //                    condition.DisplayName = $"{GetTableName(Index)}.{member.Member.Name}";
        //                }
        //                else
        //                {
        //                    SetConstMember(member, ref condition);
        //                }

        //            }
        //            else
        //            {
        //                condition.DisplayName = member.Member.Name;
        //            }
        //        }
        //    }
        //    return true;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="constant"></param>
        ///// <param name="condition"></param>
        ///// <returns></returns>
        //public void GetConstantValue(ConstantExpression constant, ConditionEntity condition)
        //{
        //    string mName = string.Empty;
        //    if (!Check.IsNull(constant))
        //    {

        //        if (constant.Type == typeof(string))
        //        {
        //            condition.DisplayName = $"'{constant.Value}'";
        //        }
        //        else
        //        {
        //            condition.DisplayName = $"{constant.Value}";
        //        }
        //    }
        //}

        public bool VisitConstant(TreeConditionEntity currentTree, ConstantExpression node)
        {

            if (Check.IsNull(currentTree.RightCondition))
            {
                currentTree.RightCondition = GetCondition(eConditionType.Constant);
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
            if (node.Type.IsValueType)
            {
                if (node.Type.IsEnum)
                {
                    int.TryParse(node.Value.ToString(), out int value);
                    currentTree.RightCondition.DisplayName = value.ToString();
                }
                else
                {
                    currentTree.RightCondition.DisplayName = node.Value.ToString();
                }
            }
            else
            {
                SetValue(currentTree, node);
            }
            return true;
        }

        public void SetValue(TreeConditionEntity currentTree, ConstantExpression node)
        {
            if (!Check.IsNull(currentTree.LeftCondition))
            {
                if (!Check.IsNullOrEmpty<Stack<MemberEntity>, MemberEntity>(currentTree.LeftCondition.Members))
                {
                    object value = null;
                    MemberEntity meber = currentTree.LeftCondition.Members.Pop();
                    if (meber.Member is FieldInfo f)
                    {
                        value = f.GetValue(node.Value);
                    }
                    while (Check.IsNullOrEmpty<Stack<MemberEntity>, MemberEntity>(currentTree.LeftCondition.Members))
                    {
                        MemberEntity m = currentTree.LeftCondition.Members.Pop();
                        if (m.Member is FieldInfo field)
                        {
                            if (!Check.IsNull(m.OParams))
                            {
                                value = ((dynamic)field.GetValue(value))[m.OParams];
                            }
                            else if (!Check.IsNull(m.KeyMember))
                            {
                                if (m.KeyMember is PropertyInfo PropKey)
                                {
                                    var Key = PropKey.GetValue(value);
                                    value = ((dynamic)field.GetValue(value))[(dynamic)Key];
                                }
                                else if (m.KeyMember is FieldInfo fieldKey)
                                {
                                    var Key = fieldKey.GetValue(node.Value);
                                    value = ((dynamic)field.GetValue(value))[(dynamic)Key];
                                }
                            }
                            else
                            {
                                value = field.GetValue(value);
                            }
                        }
                        if (m.Member is PropertyInfo Property)
                        {
                            if (!Check.IsNull(m.OParams))
                            {
                                var o = Property.GetValue(value);
                                value = ((dynamic)o)[(dynamic)m.OParams];
                            }
                            else if (!Check.IsNull(m.KeyMember))
                            {
                                if (m.KeyMember is PropertyInfo PropKey)
                                {
                                    var Key = PropKey.GetValue(node.Value);
                                    value = ((dynamic)Property.GetValue(value))[(dynamic)Key];
                                }
                                else if (m.KeyMember is FieldInfo fieldKey)
                                {
                                    var Key = fieldKey.GetValue(node.Value);
                                    value = ((dynamic)Property.GetValue(value))[(dynamic)Key];
                                }
                            }
                            else
                            {
                                value = Property.GetValue(value);
                            }
                        }
                    }
                    currentTree.RightCondition.DisplayName = value.ToString();
                }

            }

        }

        public void VisitMember(TreeConditionEntity currentTree, MemberInfo member,bool IsCompleteMember, MemberEntity currentMember)
        {
            if (Check.IsNull(currentTree.LeftCondition))
            {
                currentTree.LeftCondition = GetCondition(eConditionType.ColumnName);
            }
            currentTree.RightCondition = SetConstMember(member);
            if (Check.IsNull(currentTree.RightCondition))
            {
                if (IsCompleteMember)
                {
                    IsCompleteMember = false;
                    currentMember = new MemberEntity();
                    currentMember.Member = member;
                    currentTree.LeftCondition.Members.Push(currentMember);
                }
            }
        }
        public ConditionEntity SetConstMember(MemberInfo member)
        {

            ConditionEntity condition=null;//new ConditionEntity(eConditionType.Constant);
            if (member.ToString().Equals("System.DateTime.Now"))
            {
                condition=GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.Now.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;

            }
            else if (member.ToString().Equals("System.DateTime.MaxValue"))
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MaxValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString().Equals("System.DateTime.MinValue"))
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString().Equals("System.DateTime.MinValue"))
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = DateTime.MinValue.ToString("yyyy-MM-dd H:m:s");
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString().Equals("System.Guid Empty"))
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = Guid.Empty.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString().Equals("System.int.MaxValue"))
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = int.MaxValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            else if (member.ToString().Equals("System.int.MinValue"))
            {
                condition = GetCondition(eConditionType.Constant);
                condition.DisplayName = int.MinValue.ToString();
                condition.ConditionType = eConditionType.Constant;
            }
            return condition;
        }

        public void VisitMethod(ref TreeConditionEntity currentTree,
            MethodCallExpression node,ref bool IsCompleteMember,
            ref MemberEntity currentMember)
        {
            if (Check.IsNull(currentTree))
            {
                currentTree = GetTreeConditon();
            }

            if (Check.IsNull(currentTree.RelationCondition))
            {
                currentTree.RelationCondition = GetCondition(eConditionType.Method);// new ConditionEntity(eConditionType.Method);
                currentTree.RelationCondition.DisplayName = node.Method.Name;

            }

            if (!Check.IsNullOrEmpty<IReadOnlyCollection<Expression>, Expression>(node.Arguments))
            {
                int value = 0;
                if (node.Arguments[0] is MethodCallExpression call)
                {

                    if (IsCompleteMember)
                    {
                        currentMember = new MemberEntity();
                        currentMember.OParams = call.Arguments[0];
                        IsCompleteMember = false;
                    }
                    //if (call.Arguments[0].GetType() == typeof(int) && int.TryParse(call.Arguments[0].ToString(),out value))
                    //{
                    //    currentTree.Index = value;
                    //}
                    //else
                    //{
                    //    currentTree.Key = call.Arguments[0].ToString();
                    //}

                }
                if (node.Arguments[0] is ConstantExpression constant)
                {
                    //if (content.Value.GetType()==typeof(int)&&int.TryParse(content.Value.ToString(),out value))
                    //{
                    //    currentTree.Index = value;
                    //}
                    //else
                    //{
                    //    currentTree.Key = content.Value.ToString();
                    //}
                    if (IsCompleteMember)
                    {
                        currentMember = new MemberEntity();
                        currentMember.OParams = constant.Value;
                        IsCompleteMember = false;
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
