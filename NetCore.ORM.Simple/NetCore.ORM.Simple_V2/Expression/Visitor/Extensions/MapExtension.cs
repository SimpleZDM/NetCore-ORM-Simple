using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple_V2.Expression.Visitor.Extensions
 * 接口名称 MapExtension
 * 开发人员：11920
 * 创建时间：2022/12/14 15:42:20
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal static class MapExtension
    {
        public static void InitMap(TableEntity table,List<MapEntity> maps)
        {
            Type type = table.DicTable[table.TableNames[0]].ClassType;
            PropertyInfo PropKey = type.GetKey();
            maps.Add(new MapEntity()
            {
                TableName = table.TableNames[0],
                ColumnName = PropKey.GetColName(),
                PropName = PropKey.Name,
                IsKey = true,
                LastPropName = PropKey.Name,//原始名称
                ClassName = type.GetTableName(),
                EntityType = type,
            });
            foreach (var item in type.GetNotKeyAndIgnore())
            {
                maps.Add(new MapEntity()
                {
                    TableName = table.TableNames[0],
                    ColumnName = item.GetColName(),
                    PropName = item.Name,
                    IsKey = false,
                    LastPropName = item.Name,
                    ClassName = type.GetTableName(),
                    EntityType = type,
                });
            }
        }

        public static void AllMapNotNeed(List<MapEntity> maps)
        {
            foreach (var item in maps)
            {
                item.IsNeed = false;
                item.Soft = 0;
            }
        }

        public static MapEntity CreateMapInfo(TableEntity table,int Index, string PropName, bool IsAnonymity)
        {
            var Prop = table.GetProperty(Index, PropName);
            MapEntity map = new MapEntity();
            if (!Check.IsNull(Prop))
            {
                map.TableName = table.TableNames[Index];
                map.ColumnName = Prop.GetColName();
                map.PropertyType = Prop.PropertyType;
                if (!IsAnonymity)
                {
                    map.LastPropName = Prop.Name;
                }
                map.ClassName = table.DicTable[map.TableName].ClassType.GetTableName();
                map.EntityType = table.DicTable[map.TableName].ClassType;
            }
            return map;
        }

        public static MapEntity CloneMapInfo(ref MapEntity map)
        {
            MapEntity cmap= map.Clon();
            return cmap;
        }

        public static void VisitMember(this MemberExpression node,
            ref SelectEntity select,Dictionary<string,int> currentTables,ref MapEntity currentmapInfo
            , ref bool IsAgain,ref int soft, ref int MemberCurrent, MemberInfo[] Members,ref bool IsAnonymity)
        {
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
                prop = select.Table.GetProperty(currentTables[Parameter.Name], PropName);
                if (!Check.IsNull(prop))
                {
                    PropName = prop.Name;
                    TableName = select.Table.GetAsTableName(currentTables[Parameter.Name]);

                }
            }

            if (IsAgain)
            {

                MapEntity map = null;
                if (!Check.IsNull(TableName))
                {
                    map = select.MapInfos.FirstOrDefault(m =>
                       m.PropName.Equals(PropName) && m.TableName == TableName);
                    if (map == null)
                    {
                        map = select.MapInfos.FirstOrDefault(m =>
                        m.ColumnName.Equals(PropName) && m.TableName == TableName);
                    }
                }
                else
                {
                    map = select.MapInfos.FirstOrDefault(m =>
                    m.PropName.Equals(PropName));
                }

                currentmapInfo = map;

                if (!Check.IsNull(currentmapInfo))
                {

                    currentmapInfo.IsNeed = true;
                    currentmapInfo.Soft = soft;
                    SetPropName(IsAnonymity,ref MemberCurrent,ref currentmapInfo,Members);
                    soft++;
                    return;
                }
            }
            if (!Check.IsNull(prop))
            {
                CreateMap(node.Expression.ToString(), PropName,ref select,IsAnonymity,ref currentmapInfo,ref soft,currentTables,ref MemberCurrent,Members);
            }
        }


        public static void SetPropName(bool isAnonymity,ref int MemberCurrent,ref MapEntity currentmapInfo, MemberInfo[] Members)
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

        public static void CreateMap(string Params, string PropName,ref SelectEntity select,
            bool isAnonymity,ref MapEntity currentmapInfo,
            ref int soft,Dictionary<string,int> currentTables,ref int MemberCurrent,MemberInfo[] Members)
        {
            var index = currentTables[Params];
            currentmapInfo = MapExtension.CreateMapInfo(select.Table, index, PropName, isAnonymity);
            select.MapInfos.Add(currentmapInfo);
            currentmapInfo.Soft = soft;
            soft++;
            SetPropName(isAnonymity,ref MemberCurrent,ref currentmapInfo,Members);

        }

        public static void CreateMap(MethodEntity method,ref SelectEntity select,
           bool isAnonymity, ref MapEntity currentmapInfo,
           ref int soft, ref int MemberCurrent, MemberInfo[] Members)
        {

            
            //currentmapInfo = new MapEntity();//MapExtension.CreateMapInfo(select.Table, index, PropName, isAnonymity);
            if (!Check.IsNullOrEmpty(method.Parameters))
            {
                currentmapInfo.ClassName = method.Parameters[0].ColumnName;
                currentmapInfo.TableName = method.Parameters[0].TableName;
                currentmapInfo.ClassName = method.Parameters[0].AsTableName;
                currentmapInfo.PropertyType = method.Parameters[0].PropertyType;
            }
            select.MapInfos.Add(currentmapInfo);
            currentmapInfo.Soft = soft;
            soft++;
            SetPropName(isAnonymity, ref MemberCurrent, ref currentmapInfo, Members);

        }
    }
}
