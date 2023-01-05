using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 OrderByEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/27 15:08:08
 * 描述说明：排序实体
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 收集分组与排序信息
    /// </summary>
    public class OrderByEntity
    {
        public OrderByEntity()
        {
            IsGroupBy = false;
            IsOrderBy = false;
        }
        
        public bool IsOrderBy { get { return isOrderBy; } set { isOrderBy = value; } }
        public eOrderType OrderType { get { return orderType; } set { orderType = value; } }

        public int OrderSoft { get { return orderSoft; } set { orderSoft = value; } }
        public string TableName { get { return tableName; } set { tableName = value; } }
        public string ColumnName { get { return columnName; } set { columnName = value; } }
        public string PropName { get { return propName; } set { propName = value; } }
        /// <summary>
        /// 是否分组
        /// </summary>
        public bool IsGroupBy { get { return isGroupBy; } set { isGroupBy = value; } }

        /// <summary>
        /// 分组序号
        /// </summary>
        /// 
        public int GroupSoft { get { return groupSoft; } set { groupSoft = value; } }

       


        private bool isOrderBy;
        private eOrderType orderType;
        private int orderSoft;
        private bool isGroupBy;
        private int groupSoft;
        private string tableName;
        private string columnName;
        private string propName;

    }
}
