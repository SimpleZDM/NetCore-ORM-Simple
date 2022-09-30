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

        var command=simpleClient.Insert(
            new UserEntity() {
                CompanyId=1,
                gIdColumn=Guid.NewGuid(),
                Description="Test",
                Name="Name",RoleID=10});

          simpleClient.Update(
          new UserEntity()
          {
              CompanyId = 1,
              gIdColumn = Guid.NewGuid(),
              Description = "Test",
              Name = "Name",
              RoleID= 10
          });


        //Expression<Func<UserEntity, UserEntity>> expression = (u) => new UserEntity { Name=u.Name};
        //expression.Compile().Invoke(new UserEntity());
        Stopwatch watch = new Stopwatch();
        watch.Start();
        for (int i = 0; i < 5000; i++)
        {
            var query = simpleClient.Queryable<UserEntity, RoleEntity, CompanyEntity>(
            (u, r, c) =>
                new JoinInfoEntity(
                    new JoinMapEntity(eJoinType.Inner, u.RoleID == r.Id && u.RoleID > ((int)eConditionType.Sign)),
                    new JoinMapEntity(eJoinType.Inner, u.CompanyId == c.ID)
                )
             )
            .Select((user, r, c) => new ViewView
            {
                UserId = r.Id,
                DisplayName = user.Name,
                CompanyName = c.CompanyName,
                RoleName = r.DisplayName,
            }).Where(v => v.UserId > 0).Select(v => new ViewView2
            {
                RID = v.UserId,
                DisplayName = v.DisplayName
            }).GroupBy(v=> new{ v.RID}).
            OrderBy(v => new { Id =v.Key.RID }).Select(v => new
            {
                SUMAge=v.Sum(s=>s.RID),

            });
            var data = query.ToListAsync().Result;
            foreach (var item in data)
            {
                Console.WriteLine(item.SUMAge);
            }
            Console.WriteLine(i);
        }
        watch.Stop();
        Console.WriteLine($"******************用时:{watch.ElapsedMilliseconds}********************");
        Console.WriteLine($"*********************单次时长:{watch.ElapsedMilliseconds / (5000 + 0.0)}毫秒*****************");
        return 0;
    }
}

