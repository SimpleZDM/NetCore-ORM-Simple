using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    public class BaseEntity<T>
    {
        public T ID { get { return id; } set { id = value; } }

        private T id;

        public bool IsGuid()
        {
            if (ID is Guid)
            {
                return true;
            }
            return false;
        }

        public bool IsInt()
        {
            if (ID is Int32)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <returns>
        /// 当前 <see cref="T:System.Object"/> 的哈希代码。<br/>
        /// 如果<c>Id</c>为<c>null</c>则返回0，
        /// 如果不为<c>null</c>则返回<c>Id</c>对应的哈希值
        /// </returns>
        public override int GetHashCode()
        {
            if (ID == null)
            {
                return 0;
            }
            return ID.ToString().GetHashCode();
        }
    }
}
