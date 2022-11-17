using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    public class RecordEntity<T>:BaseEntity<T>
    {
        public RecordEntity()
        {
            DeleterID = Guid.Empty;
            CreationTime = DateTime.Now;
            LastModificationTime = DateTime.Now;
            concurrencyStamp = Guid.NewGuid().ToString();
        }
        private bool isDelete = false;
        private string concurrencyStamp;
        private DateTime creationTime;
        private DateTime deletionTime;
        private Guid? creatorID;
        private DateTime lastModificationTime;
        private Guid? lastModifierID;
        private Guid? deleterID;

        /// <summary>
        /// 修改标记--防止并发冲突
        /// </summary>
        public string ConcurrencyStamp { get { return concurrencyStamp; } set { concurrencyStamp = value; } }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get { return creationTime; } set { creationTime = value; } }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public Guid? CreatorID { get { return creatorID; } set { creatorID = value; } }
        /// <summary>
        /// 最后跟新时间
        /// </summary>
        public DateTime LastModificationTime { get { return lastModificationTime; } set { lastModificationTime = value; } }

        /// <summary>
        /// 最后修改的id
        /// </summary>
        public Guid? LastModifierID { get { return lastModifierID; } set { lastModifierID = value; } }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get { return isDelete; } set { isDelete = value; } }

        /// <summary>
        /// 删除者id
        /// </summary>
        public Guid? DeleterID { get { return deleterID; } set { deleterID = value; } }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeletionTime { get { return deletionTime; } set { deletionTime = value; } }
    }
}
