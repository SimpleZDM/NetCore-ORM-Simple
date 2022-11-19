using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    /// <summary>
    /// 查询参数基类
    /// </summary>
    public class BaseParameter
    {
        public BaseParameter()
        {
            Current = 1;
            PageSize =20;
            IsPage = true;
        }
        /// <summary>
        ///属性
        /// </summary>
        public int Current { get { return current; } set { current = value; } }
        /// <summary>
        /// 属性
        /// </summary>
        public int PageSize { get { return pageSize; } set { pageSize = value; } }
        public bool IsPage { get { return isPage; } set { isPage = value; } }

        /// <summary>
        /// 字段
        /// </summary>

        private int current;
        private int pageSize;
        private bool isPage;
    }
}
