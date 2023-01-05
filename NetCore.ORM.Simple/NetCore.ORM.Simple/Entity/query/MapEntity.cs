using NetCore.ORM.Simple.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 MapEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/20 11:58:50
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 收集查询返回的字段信息
    /// </summary>
    public class MapEntity
    {

        public MapEntity()
        {
            IsNeed = true;
            Methods = new List<MethodEntity>();
        }
        /// <summary>
        /// 字段属性名称
        /// </summary>
        public string PropName { get { return propName; } set { propName = value; } }
        /// <summary>
        /// 最后出现非匿名对象的属性名称
        /// </summary>
        //public string LastPropName { get { return lastPropName; } set { lastPropName = value; } }
        /// <summary>
        /// 对应数据库表名称
        /// </summary>
        public string TableName { get { return tableName; } set { tableName = value; } }
        /// <summary>
        /// 数据库列名称
        /// </summary>
        public string ColumnName { get { return columnName; } set { columnName = value; } }

        /// <summary>
        /// 列名称对应的属性名称
        /// </summary>
        public string ColumnPropertyName { get { return columnPropertyName; } set { columnPropertyName = value; } }
        /// <summary>
        /// 别名
        /// </summary>
        public string AsColumnName { get { return asColumnName; } set { asColumnName = value; } }
        [Obsolete]
        /// <summary>
        /// 调用方法
        /// </summary>
        public string MethodName { get { return methodName; } set { methodName = value; } }
        /// <summary>
        /// --标记是否需要查询字段
        /// --只标记最后一次映射的字段
        /// </summary>
        public bool IsNeed { get { return isNeed; } set { isNeed = value; } }

        public string ClassName { get { return className; } set { className = value; } }
        /// <summary>
        /// 实体的类型
        /// </summary>
        public Type EntityType { get { return entityType; } set { entityType = value; } }

        /// <summary>
        /// 属性的类型
        /// </summary>
        public PropertyInfo PropertyType { get { return propertyType; } set { propertyType = value; } }
        public bool IsKey { get { return isKey; } set { isKey = value; } }
        public int Soft { get { return soft; } set { soft = value; } }

        public List<MethodEntity> Methods { get { return methods; } set { methods = value; } }

        /// <summary>
        /// 深拷贝自己
        /// </summary>
        /// <returns></returns>
      

        private string propName;
        private string tableName;
        private string columnName;
        private string asColumnName;
        private bool isNeed;
        private bool isKey;
        private string methodName;
        private string className;
        private Type entityType;
        private PropertyInfo propertyType;
        private int soft;
        private string columnPropertyName;
        private List<MethodEntity> methods;
        #region method
        public MapEntity Clon()
        {
            MapEntity entity = new MapEntity();
            entity.TableName = this.tableName;
            entity.PropName = this.PropName;
            //entity.LastPropName = this.LastPropName;
            entity.ColumnName = this.ColumnName;
            entity.ColumnPropertyName = this.ColumnPropertyName;
            entity.IsNeed = this.IsNeed;
            entity.IsKey = this.IsKey;
            entity.ClassName = this.ClassName;
            entity.EntityType = this.EntityType;
            entity.PropertyType = this.PropertyType;
            entity.Methods = this.Methods;
            return entity;
        }

        public void SetAsColumnName(int index=0)
        {
            if (Check.IsNullOrEmpty(TableName) && Check.IsNullOrEmpty(ColumnName))
            {
                AsColumnName = $"{PropName}";
                if (Check.IsNullOrEmpty(PropName))
                {
                    if (!Check.IsNullOrEmpty(Methods))
                    {
                        AsColumnName = $"{Methods[0].Name}{index}";
                    }
                }
            }
            else
            {
                AsColumnName = $"{TableName}{DBMDConst.DownLine}{ColumnName}{index}";
            }
        }

        public void CheckPropertyName()
        {
            if (Check.IsNullOrEmpty(this.PropName))
            {
                PropName = PropertyType.GetColName();
            }
        }
        #endregion



    }
}
