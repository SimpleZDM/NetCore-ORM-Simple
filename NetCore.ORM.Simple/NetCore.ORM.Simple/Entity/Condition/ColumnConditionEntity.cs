using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Text;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple_V2.Entity.Condition
 * 接口名称 ColumnConditionEntity
 * 开发人员：11920
 * 创建时间：2022/12/29 13:43:01
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    internal class ColumnConditionEntity : BaseCondition, ICondition
    {
        public ColumnConditionEntity(eConditionType ConditionType) :base(ConditionType)
        {

        }

        public string AsTableName { get { return asTableName; } set { asTableName = value; } }
        public string ColumnName { get { return columnName; } set { columnName = value; } }
        public string TableName { get { return tableName; } set { tableName = value; } }
        public string GetConditionName()
        {
            if (Check.IsNullOrEmpty(ColumnName))
            {
                throw new Exception(nameof(ColumnName));
            }
            if (Check.IsNullOrEmpty(ColumnName))
            {
                throw new Exception(nameof(AsTableName));
            }
            return $"{AsTableName}{DBMDConst.Dot}{ColumnName}";
        }

        private string asTableName;
        private string tableName;
        private string columnName;
    }
}
