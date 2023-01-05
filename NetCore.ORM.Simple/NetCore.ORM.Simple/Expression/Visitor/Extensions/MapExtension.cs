using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Expression.Visitor
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
        public static void InitMap(ContextTableEntity table, List<MapEntity> maps)
        {
            Type type = table.DicTables[table.TableNames[0]].ClassType;
            PropertyInfo PropKey = type.GetKey();
            maps.Add(new MapEntity()
            {
                TableName = table.TableNames[0],
                ColumnName = PropKey.GetColName(),
                //PropName = PropKey.Name,
                IsKey = true,
               // LastPropName = PropKey.Name,//原始名称
                ClassName = type.GetTableName(),
                EntityType = type,
                PropertyType=PropKey
            });
            foreach (var item in type.GetNotKeyAndIgnore())
            {
                maps.Add(new MapEntity()
                {
                    TableName = table.TableNames[0],
                    ColumnName = item.GetColName(),
                    //PropName = item.GetColName(),
                    IsKey = false,
                    //LastPropName = item.Name,
                    ClassName = type.GetTableName(),
                    EntityType = type,
                    PropertyType=item
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
    }
}
