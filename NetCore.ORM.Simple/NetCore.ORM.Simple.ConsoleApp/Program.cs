using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using System.Linq.Expressions;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Queryable;
using NetCore.ORM.Simple.Client;

namespace NetCore.ORM.Simple.ConsoleApp;
public static class Program
{
    public static int Main(string []args)
    {
        //eJoinType join=eJoinType.Inner;
        //Console.WriteLine(join.ToString());
        //MapVisitor mapVisitor = new MapVisitor("UserTable","CompanyTable","RoleTable");
        //JoinVisitor joinVisitor = new JoinVisitor("UserTable", "CompanyTable", "RoleTable"); 
        //ConditionVisitor conditionVisitor = new ConditionVisitor("UserTable", "CompanyTable", "RoleTable");
        ////条件解析
        //Expression<Func<UserEntity,CompanyEntity, RoleEntity, bool>> whereExpression = 
        //    (u, c, r) =>u.Id>100;

        //conditionVisitor.Modify(whereExpression);
        ////映射解析
        //Expression<Func<UserEntity, CompanyEntity, RoleEntity, ViewView>> mapExpression =
        //    (u, c, r) => new ViewView()
        //    {
        //        UserId= u.Id,
        //        DisplayName=u.Name,
        //        CompanyName=c.Name,
        //        RoleName=r.Name,
        //    };
        //mapVisitor.Modify(mapExpression);
        //string strMap=mapVisitor.GetValue();
        /////连接解析
        //Expression<Func<UserEntity, CompanyEntity, RoleEntity, JoinInfoEntity>> joinExpression =
        //    (u, c, r) => new JoinInfoEntity(
        //        new JoinMapEntity(eJoinType.Inner,u.Role==r.Id),
        //        new JoinMapEntity(eJoinType.Inner,u.CompanyId==c.Id)
        //        );

        //joinVisitor.Modify(joinExpression);

        //var mpaInfos=mapVisitor.GetMapInfos();
        //var joinInfos=joinVisitor.GetJoinInfos();
        //var condition=conditionVisitor.GetValue();
        //Builder builder = new Builder(eDBType.Mysql);

        ////var sql=builder.GetSelect<TRe>(mpaInfos,joinInfos,condition);

        //Console.WriteLine("Simple");

        //ISimpleQueryable<UserEntity,RoleEntity> query =new SimpleQueryable<UserEntity,RoleEntity>(eDBType.Mysql);
        //query.Where((u,r)=>true);
        ISimpleClient simpleClient = new SimpleClient(
            new DataBaseConfiguration(false,
            new ConnectionEntity() 
            { 
                IsAutoClose = true,
                DBType=eDBType.Mysql
            }));
        simpleClient.In
        return 0;
    }
}
public class UserEntity
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Role { get; set; }
    public Guid gIdColumn { get; set; }
}

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CompanyEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class MissionDetailEntity
{
    public MissionDetailEntity()
    {
        //ID = Guid.NewGuid();
        //GroupID = Guid.NewGuid();
        //MissionRole = -1;
    }

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
    public Guid MissionTimeId { get; set; }

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

public class RegionEntity
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int ParentID { get; set; }
    public int RegionTypeID { get; set; }
}

public class ViewView
{
    public int UserId { get; set; }
    public string DisplayName { get; set; }
    public string RoleName { get; set; }
    public string CompanyName { get; set; }
}
