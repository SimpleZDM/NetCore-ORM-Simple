using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 ConditionEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/21 17:57:30
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class ConditionEntity
    {
        public ConditionEntity(eConditionType type)
        {
            ConditionType = type;
            Members = new Stack<MemberEntity>();
            Methods = new List<MethodEntity>();
        }
        public string DisplayName { get { return displayName; } set { displayName = value; } }

        public string AsTableName { get { return asTableName; } set{  asTableName=value; } }
        public string ColumnName { get { return columnName; } set{ columnName = value; } }
        public string TableName { get { return tableName; } set{  tableName=value; } }
        /// <summary>
        /// 常量-字段
        /// </summary>
        public eConditionType ConditionType { get { return conditionType; } set { conditionType = value; } }
        /// <summary>
        /// 符号类型
        /// </summary>
        public eSignType SignType { get { return signType; } set { signType = value; } }
        public eDataType DataType { get { return dataType; } set { dataType = value; } }

        public Stack<MemberEntity> Members { get { return members; } set { members = value; } }
        public PropertyInfo PropertyType { get { return propertyType; } set { propertyType = value; } }
        /// <summary>
        /// 将一些值进行保存
        /// </summary>
        public object Value { get { return val; } set { val = value; } }

        public List<MethodEntity> Methods { get { return methods; } set { methods = value; } }

        private string displayName;
        private eConditionType conditionType;
        private eSignType signType;
        private Stack<MemberEntity> members;
        private PropertyInfo propertyType;
        private object val;
        private eDataType dataType;
        private string asTableName;
        private string tableName;
        private string columnName;
        public List<MethodEntity> methods;

        public void CopyColumnProperty(ConditionEntity condition)
        {
            condition.Value = this.Value;
            condition.DisplayName = this.DisplayName;
            condition.TableName = this.TableName;
            condition.AsTableName = this.AsTableName;
        }

        public void CopyColumnProperty(MapEntity mapinfo)
        {

            mapinfo.ClassName = this.ColumnName;
            mapinfo.TableName =this.TableName;
            mapinfo.ClassName = this.AsTableName;
            mapinfo.PropertyType = this.PropertyType;
        }

    }
}
