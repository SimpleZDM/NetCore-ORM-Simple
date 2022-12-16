using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 QueryEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/28 14:03:04
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    /// <summary>
    /// 收集查询信息
    /// </summary>
    public class QueryEntity : SqlBase
    {
        public QueryEntity()
        {
            PageSize = 0;
            PageNumber = 1;
            DbCommandType = eDbCommandType.Query;
        }
        public QueryEntity(string sql)
        {
            PageSize = 0;
            PageNumber = 1;
            DbCommandType = eDbCommandType.Query;
            StrSqlValue.Append(sql);
        }

        /// <summary>
        /// 取出的数据
        /// </summary>
        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("页码不能小于一!");
                }
                pageNumber = value;
            }
        }
        /// <summary>
        /// 跳过的数据
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (pageSize < 0)
                {
                    throw new ArgumentException("每页大小不能小于!");
                }
                pageSize = value;
            }
        }


        /// <summary>
        ///映射信息
        /// </summary>
        public MapEntity[] MapInfos { get { return mapInfos; } set { mapInfos = value; } }

      


        //public Type 
        /// <summary>
        /// 最后一个是否是匿名对象
        /// </summary>
        public bool LastAnonymity { get { return lastAnonymity; } set { lastAnonymity = value; } }

        private MapEntity[] mapInfos;
        private int pageNumber;
        private int pageSize;
        private bool lastAnonymity;


    }
}
