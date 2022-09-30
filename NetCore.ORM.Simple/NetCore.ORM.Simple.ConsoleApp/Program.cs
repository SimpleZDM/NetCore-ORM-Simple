using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using System.Linq.Expressions;
using NetCore.ORM.Simple.SqlBuilder;
using NetCore.ORM.Simple.Queryable;
using NetCore.ORM.Simple.Client;
using NetCore.ORM.Simple.Common;
using System.Diagnostics;

namespace NetCore.ORM.Simple.ConsoleApp;
public static class Program
{
    public static int Main(string []args)
    {
        string [] arry=new string[] { "sdfsd", "sdfsd", "sdfasd" };
        var i=arry.Where(i=>i.Equals("11")).First();
        //Console.WriteLine(DateTime.Now.ToString("yyyy-M-dd HH:mm"));
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
        //var o = new {Name=""};
        //var o1 = new {Name="",age=10};
        //var o2 = new {Name="",age=10,sex=1,row=1,sss=11};
        //Type type = o.GetType();
        //Type type2 = o1.GetType();
        //Type type3= o2.GetType();
        //Console.WriteLine(type.GetClassName());
        //Console.WriteLine(type2.GetClassName());
        //Console.WriteLine(type3.GetClassName());
        //var text=Activator.CreateInstance(type);
       
        
        ISimpleClient simpleClient = new SimpleClient(
            new DataBaseConfiguration(false,
            new ConnectionEntity("server=127.0.0.1;database=testdb;user=root;pwd=123456;") 
            { 
                IsAutoClose = true,
                DBType=eDBType.Mysql,
                Name="test1",
                ReadWeight=5,
                WriteReadType=eWriteOrReadType.ReadOrWrite
            }));
 //       Stopwatch watch = new Stopwatch();
 //       int lenght = 100;
 //       watch.Start();
 //       for (int i = 0; i < 100; i++)
 //       {
 //var data1=simpleClient.GetEntity<UserEntity,CompanyEntity,RoleEntity>(
 //           new UserEntity() { Id = 10, Name = "test" },
 //           (u)=>new CompanyEntity() { CompanyName=u.Name,ID=u.Id},
 //           (u)=>new RoleEntity() { DisplayName=u.CompanyName,Id=u.ID}
 //           );
 //       }

 //       watch.Stop();
 //       Console.WriteLine($"******************用时:{watch.ElapsedMilliseconds}********************");
 //       Console.WriteLine($"*********************单次时长:{watch.ElapsedMilliseconds/(lenght+0.0)}毫秒*****************");
 //       Dictionary<string,object> map = new Dictionary<string,object>();
 //       map.Add("Name","sadfasd");
 //       map.Add("Age",100);
 //       UserEntity user = new UserEntity();
 //       watch.Start();
 //       for (int i = 0; i < 100; i++)
 //       {
 //           var data1 = simpleClient.GetEntity<UserEntity>(map
 //                      );
 //       }

 //       watch.Stop();
 //       Console.WriteLine($"******************用时:{watch.ElapsedMilliseconds}********************");
 //       Console.WriteLine($"*********************单次时长:{watch.ElapsedMilliseconds / (lenght + 0.0)}毫秒*****************");

        //var command=simpleClient.Insert(
        //    new UserEntity() {
        //        CompanyId=1,
        //        gIdColumn=Guid.NewGuid(),
        //        Description="Test",
        //        Name="Name",RoleID=10});

        //  simpleClient.Update(
        //  new UserEntity()
        //  {
        //      CompanyId = 1,
        //      gIdColumn = Guid.NewGuid(),
        //      Description = "Test",
        //      Name = "Name",
        //      RoleID= 10
        //  });

        TestSimple();
        //Expression<Func<UserEntity, UserEntity>> expression = (u) => new UserEntity { Name=u.Name};
        //expression.Compile().Invoke(new UserEntity());

        return 0;
    }
    public static void TestSimple()
    {
        Console.WriteLine("*******************Simple*************************");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        #region
         ISimpleClient simpleClient = new SimpleClient(
          new DataBaseConfiguration(false,
          new ConnectionEntity("server=49.233.33.36;database=virtualsoftplatformdb;user=root;pwd=[Txy*!14@msql*^];SSL Mode=None")
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "test1",
              ReadWeight = 5,
              WriteReadType = eWriteOrReadType.ReadOrWrite
          }));
        simpleClient.SetAPOLog((sql, Params) =>
        {
            Console.WriteLine(sql);
        });
        for (int i = 0; i < 20; i++)
        {
            var data=simpleClient.Queryable<MissionDetailEntity>().
                Where(m=>m.Id.Equals("00073aec-630b-4d4f-b042-067ed92db178")).Take(500).ToListAsync().Result;
            Console.WriteLine(i);
        }
       
        #endregion
        watch.Stop();
        Console.WriteLine($"******************用时:{watch.ElapsedMilliseconds}********************");
        Console.WriteLine($"*********************单次时长:{watch.ElapsedMilliseconds / (20 + 0.0)}毫秒*****************");
    }

    public class TastClass
    {
        public int AVG { get; set; }
    }

    [ClassName("missiondetailtable")]
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

