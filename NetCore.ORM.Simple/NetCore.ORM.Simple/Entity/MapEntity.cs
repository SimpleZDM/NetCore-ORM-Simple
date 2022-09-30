using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 映射数据
    /// </summary>
    public class MapEntity
    {

        public MapEntity()
        {
            IsNeed = true;
        }
        /// <summary>
        /// 字段属性名称
        /// </summary>
        public string PropName { get { return propName; } set { propName = value; } }
        /// <summary>
        /// 最后出现非匿名对象的属性名称
        /// </summary>
        public string LastPropName { get { return lastPropName; } set { lastPropName = value; } }
        /// <summary>
        /// 对应数据库表名称
        /// </summary>
        public string TableName { get { return tableName; } set { tableName = value; } }
        /// <summary>
        /// 数据库列名称
        /// </summary>
        public string ColumnName { get { return columnName; } set { columnName = value; } }
        /// <summary>
        /// 别名
        /// </summary>
        public string AsColumnName { get { return asColumnName; } set { asColumnName = value; } }
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




        private string propName;
        private string tableName;
        private string columnName;
        private string asColumnName;
        private bool isNeed;
        private string methodName;
        private string lastPropName;
        private string className;
       

    }
}
