using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Visitor
 * 接口名称 JoinExtension
 * 开发人员：11920
 * 创建时间：2022/12/13 13:12:49
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Visitor
{
    internal static class JoinExtension
    {
        public static void JoinCondition(ConditionVisitor visitor, Expression exp)
        {
            visitor.Modify(exp);
        }
        public static void VisitConstant(this ConstantExpression node,ref JoinTableEntity CurrentJoinTable)
        {
            CurrentJoinTable = CreateJoinEntity(eTableType.Slave);
            switch (node.Value)
            {
                case eJoinType.Inner:
                    CurrentJoinTable.JoinType = eJoinType.Inner;
                    break;
                case eJoinType.Left:
                    CurrentJoinTable.JoinType = eJoinType.Left;
                    break;
                case eJoinType.Right:
                    CurrentJoinTable.JoinType = eJoinType.Right;
                    break;
                default:
                    break;
            }
        }
        public static void InitJoinTable(ref JoinTableEntity CurrentJoinTable,Dictionary<string,JoinTableEntity>joinInfos)
        {
            foreach (var item in CurrentJoinTable.TreeConditions)
            {
                if (ByConditionAddJoin(item.LeftCondition,joinInfos,ref CurrentJoinTable))
                {
                    break;
                }
                if (ByConditionAddJoin(item.RightCondition, joinInfos, ref CurrentJoinTable))
                {
                    break;
                }

            }
        }
        public static bool ByConditionAddJoin(ConditionEntity condition, Dictionary<string, JoinTableEntity> joinInfos, ref JoinTableEntity CurrentJoinTable)
        {
            if (!Check.IsNull(condition) && (condition.ConditionType == eConditionType.ColumnName||condition.ConditionType==eConditionType.Method))
            {
                if (!Check.IsNullOrEmpty(condition.AsTableName) && !joinInfos.ContainsKey(condition.AsTableName))
                {
                    CurrentJoinTable.AsName = condition.AsTableName;
                    CurrentJoinTable.DisplayName = condition.TableName;
                    joinInfos.Add(condition.AsTableName,CurrentJoinTable);
                    return true;
                }
            }
            return false;
        }
        public static void InitJoin(TableEntity table,Dictionary<string,JoinTableEntity> joinInfos,ref JoinTableEntity mastertable)
        {
             mastertable = CreateJoinEntity(eTableType.Master);
            mastertable.DisplayName = table.TableNames[0];

            if (Check.IsNullOrEmpty(mastertable.DisplayName))
            {
                return;
            }
            if (!joinInfos.ContainsKey(mastertable.DisplayName))
            {
                joinInfos.Add(mastertable.DisplayName,mastertable);
            }
        }
        public static JoinTableEntity CreateJoinEntity(eTableType TableType)
        {
            JoinTableEntity joinTable = new JoinTableEntity();
            joinTable.TableType = TableType;
            return joinTable;
        }

    }
}
