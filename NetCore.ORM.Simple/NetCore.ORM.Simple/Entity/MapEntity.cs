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

        }
        /// <summary>
        /// 字段属性名称
        /// </summary>
        public string PropName { get { return propName; } set { propName = value; } }
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
        


        private string propName;
        private string tableName;
        private string columnName;
        private string asColumnName;

    }
}
