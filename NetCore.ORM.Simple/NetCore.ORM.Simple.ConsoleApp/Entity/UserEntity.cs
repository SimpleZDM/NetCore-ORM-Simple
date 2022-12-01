using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp
 * 接口名称 UserEntity
 * 开发人员：-nhy
 * 创建时间：2022/10/8 16:34:32
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    [MyTableAttrbute("usertable")]
    public class UserEntity
    {
        [MyColumnAttrbute(name:"Id",key:true,autoIncrease:true)]
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public Guid gIdColumn { get; set; }
        public float Age { get; set; }
        public DateTime Time1 { get; set; }
        public DateTime Time2 { get; set; }

        public int IDDD;
    }
    [MyTableAttrbute("RoleTable")]
    public class RoleEntity
    {
        [MyColumnAttrbute(key: true, autoIncrease: true)]
        public int Id { get; set; }
        public string DisplayName { get; set; }
    }
    [MyTableAttrbute("CompanyTable")]
    public class CompanyEntity
    {
        [MyColumnAttrbute(key:true,autoIncrease: true)]
        public int Id { get; set; }
        [MyColumnAttrbute("CompanyName")]
        public string CompanyName { get; set; }
    }

    public class ViewEntity
    {
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
    }
    public class GroupEntity
    {
        public int Count { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public decimal Grade { get; set; }
    }
    [TableName("missiondetailtable")]
    public class MissionDetailEntity
    {
        public MissionDetailEntity()
        {
            //ID = Guid.NewGuid();
            //GroupID = Guid.NewGuid();
            //MissionRole = -1;
        }
        [Key(false)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductDeviceId { get; set; }
        public Guid MissionId { get; set; }
        public Guid UserId { get; set; }

        public int MissionRole { get; set; }

        public int StatusId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Score { get; set; }
        public string OperationRecord { get; set; }
        public Guid GroupID { get; set; }

        private bool isDelete = false;
        private string concurrencyStamp;
        private DateTime creationTime;
        private DateTime deletionTime;
        private Guid creatorID;
        private DateTime lastModificationTime;
        private Guid lastModifierID;
        private Guid deleterID;

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
        public Guid CreatorID { get { return creatorID; } set { creatorID = value; } }
        /// <summary>
        /// 最后跟新时间
        /// </summary>
        public DateTime LastModificationTime { get { return lastModificationTime; } set { lastModificationTime = value; } }

        /// <summary>
        /// 最后修改的id
        /// </summary>
        public Guid LastModifierID { get { return lastModifierID; } set { lastModifierID = value; } }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get { return isDelete; } set { isDelete = value; } }

        /// <summary>
        /// 删除者id
        /// </summary>
        public Guid DeleterID { get { return deleterID; } set { deleterID = value; } }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeletionTime { get { return deletionTime; } set { deletionTime = value; } }
    }
}
