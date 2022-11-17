using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Entity
 * 接口名称 ProductDeviceRecordView
 * 开发人员：-nhy
 * 创建时间：2022/6/24 18:04:37
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Entity
{
    public class ProductDeviceRecordView
    { /// <summary>
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
        public Guid ProductDeviceId { get { return productDeviceId; } set { productDeviceId = value; } }
        public int ID { get { return id; } set { id = value; } }

        /// <summary>
        /// 操作类型 0-设备操作开关机 1-转移记录
        /// </summary>
        public int OperationalType { get { return operationalType; } set { operationalType = value; } }
        public string OperationalTypeName { get { return operationalTypeName; } set { operationalTypeName = value; } }


        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperationalDescription { get { return operationalDescription; } set { operationalDescription = value; } }

        private string operationalDescription;
        private int operationalType;
        private Guid productDeviceId;
        private int id;
        private bool isDelete = false;
        private string concurrencyStamp;
        private DateTime creationTime;
        private DateTime deletionTime;
        private Guid? creatorID;
        private DateTime lastModificationTime;
        private Guid? lastModifierID;
        private Guid? deleterID;
        private string operationalTypeName;
    }
}
