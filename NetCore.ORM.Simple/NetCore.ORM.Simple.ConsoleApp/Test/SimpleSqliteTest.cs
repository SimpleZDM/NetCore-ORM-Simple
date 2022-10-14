//using NetCore.ORM.Simple.Client;
//using NetCore.ORM.Simple.Entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

///*********************************************************
// * 命名空间 NetCore.ORM.Simple.ConsoleApp
// * 接口名称 SimpleSqliteTest
// * 开发人员：-nhy
// * 创建时间：2022/10/13 10:10:36
// * 描述说明：
// * 更改历史：
// * 
// * *******************************************************/
//namespace NetCore.ORM.Simple.ConsoleApp
//{
//    public class SimpleSqliteTest
//    {
//        ISimpleClient client;
//        public SimpleSqliteTest()
//        {
//            client = new SimpleClient(
//          new DataBaseConfiguration(false,
//          new ConnectionEntity($"Data Source=C:\\Users\\11920\\Desktop\\sqlite.db;")
//          {
//              IsAutoClose = true,
//              DBType = eDBType.Sqlite,
//              Name = "test1",
//              ReadWeight = 5,
//              WriteReadType = eWriteOrReadType.ReadOrWrite
//          }));

//            client.SetAPOLog((sql, Params) =>
//            {
//                Console.WriteLine(sql);
//            });
//            client.SetAttr(typeof(MyTableAttrbute), typeof(MyColumnAttrbute));
//            // var data=client.Queryable<MatchLog>().Take(10).ToList();
//        }
//        /// <summary>
//        /// 添加测试
//        /// </summary>
//        public void InsertTest()
//        {
//            try
//            {
//                Console.WriteLine("****************新增测试*****************");
//                Console.WriteLine("****************1.单条插入*****************");
//                UserEntity user = new UserEntity();
//                user.Name = "test1-小明";
//                user.Description = "sdf";
//                user.gIdColumn = Guid.NewGuid();
//                var res=client.Insert(user).SaveChange();
//                Console.WriteLine($"*****************受影响行数:{res}****************");

//                Console.WriteLine("****************2.多条插入*****************");
//                Console.WriteLine($"*****************插入100条****************");
//                List<UserEntity> users = new List<UserEntity>();
//                const int Lenght = 100;
//                for (int i = 0; i < Lenght; i++)
//                {
//                    users.Add(new UserEntity() { Name = $"测试添加{i}", Age = 100, RoleId = 1, CompanyId = 1, Description = "测试" });
//                }
//                var result1 = client.Insert(users);
//                var result=client.SaveChange();
//                Console.WriteLine($"*****************受影响行数:{result}****************");
//                Console.WriteLine("****************测试结束*****************");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }

//        }
//        /// <summary>
//        /// 更新测试
//        /// </summary>
//        public void UpdateTest()
//        {
//            try
//            {
//                Console.WriteLine("****************更新测试*****************");
//                Console.WriteLine("****************1.单条更新*****************");
//                UserEntity user = client.Queryable<UserEntity>().First();
//                user.Name = "测试更新1111";
//                var result = client.Update(user).SaveChange();
//                Console.WriteLine($"*****************受影响行数:{result}****************");

//                Console.WriteLine("****************2.多条更新*****************");
//                Console.WriteLine($"*****************更新100条****************");
//                List<UserEntity> users = client.Queryable<UserEntity>().Take(100).ToList();
//                const int Lenght = 100;
//                for (int i = 0; i < Lenght; i++)
//                {
//                    users[i].Name = "更新用户名称{i}";
//                }
//                var result1 = client.Update(users).SaveChange();
//                Console.WriteLine($"*****************受影响行数:{result1}****************");
//                Console.WriteLine("****************测试结束*****************");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//        /// <summary>
//        /// 删除测试
//        /// </summary>
//        public void DeleteTest()
//        {
//            try
//            {
//                Console.WriteLine("****************删除测试*****************");
//                Console.WriteLine("****************1.单条删除*****************");
//                var users = client.Queryable<UserEntity>().Take(100).ToList().ToArray();
//                var result = client.Delete(users[0]).SaveChange();
//                int id = 1503;
//                UserEntity user = users[3];
//                user.IDDD = user.Id;
//                Dictionary<string, UserEntity> duser = new Dictionary<string, UserEntity>();
//                duser.Add("1", users[4]);
//                //var result0 = client.Delete<UserEntity>(d => d.Id.Equals(1503)).SaveChange();
//                // var result1 = client.Delete<UserEntity>(d => d.Id.Equals(id)).SaveChange();
//                //var result2 = client.Delete<UserEntity>(d => d.Id.Equals(user.Id)).SaveChange();
//                var result14 = client.Delete<UserEntity>(d => d.Id.Equals(duser["1"].Id)).SaveChange();
//                var result13 = client.Delete<UserEntity>(d => d.Id.Equals(users[3].IDDD)).SaveChange();
//                var result15 = client.Delete<UserEntity>(d => d.Id.Equals(user.IDDD)).SaveChange();

//                client.SaveChange();

//                Console.WriteLine($"*****************受影响行数:{result}****************");
//                // Console.WriteLine($"*****************受影响行数:{result1}****************");

//                Console.WriteLine("****************2.多条删除*****************");
//                Console.WriteLine($"*****************删除100条****************");

//                //删除多个数据
//                //var result2 = client.Delete(users[0]).SaveChange();
//                // var result3 = client.Delete<UserEntity>(d=>d.Id.Equals(10)).SaveChange();
//                //Console.WriteLine($"*****************受影响行数:{result1}****************");
//                // Console.WriteLine($"*****************受影响行数:{result2}****************");
//                Console.WriteLine("****************测试结束*****************");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }

//        public void QueryTest()
//        {
//            try
//            {
//                Console.WriteLine("****************查询测试*****************");
//                Console.WriteLine("****************1.简单单表查询*****************");
//                ///返回所有
//                List<UserEntity> users = client.Queryable<UserEntity>().ToList();
//                UserEntity user = client.Queryable<UserEntity>().FirstOrDefault();
//                ///数据的条数
//                int count = client.Queryable<UserEntity>().Count();
//                //是否存在
//                bool any = client.Queryable<UserEntity>().Any();

//                var first = client.Queryable<UserEntity>().First();

//                var firstordefault = client.Queryable<UserEntity>().FirstOrDefault();

//                //返回匿名对象
//                var data0 = client.Queryable<UserEntity>().Select(u => new { Name = u.Name, Id = u.Id }).ToList();
//                var data = client.Queryable<UserEntity>().Select(u => new UserEntity { Name = u.Name, Id = u.Id }).
//                    Select(u => new { Name = u.Name, Id = u.Id }).ToList();
//                //加条件
//                int min = 1746;
//                int max = 1756;
//                var data1 = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).ToList();
//                //分组
//                var group = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).GroupBy(u => new { u.CompanyId }).ToList();
//                //排序
//                var order = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).OrderBy(u => new { u.Id }).ToList();
//                var orderDesce = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).OrderByDescending(u => new { u.Id }).ToList();

//                Console.WriteLine($"*****************是否有数据:{any}****************");
//                Console.WriteLine($"*****************总行数:{count}****************");

//                Console.WriteLine("****************2.多表连接查询*****************");
//                Console.WriteLine($"*****************连接查询****************");

//                var JoinData = client.Queryable<UserEntity, RoleEntity, CompanyEntity>(
//                    (u, r, c) => new JoinInfoEntity(
//                    new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
//                    new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
//                    )).ToList();

//                var JoinData1 = client.Queryable<
//                    UserEntity, RoleEntity, CompanyEntity>(
//                    (u, r, c) => new JoinInfoEntity(
//                   new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
//                   new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
//                   )).
//                   Where((u, r, c) => u.Id > 10).OrderByDescending((u, r) => u.Id).
//                   Select((u, r, c) => new
//                   {
//                       UserName = u.Name,
//                       CompanyName = c.CompanyName,
//                       RoleName = r.DisplayName,
//                       Id = u.Id
//                   });

//                JoinData1.Where(s => s.Id > 10);

//                JoinData1.Where(s => s.Id > 100 && s.Id > 1000);

//                JoinData1.Where(s => true);

//                var data111 = JoinData1.ToList();
//                /////连接查询分组
//                var JoinData2 = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
//                  new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
//                  new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
//                  )).
//                  Where((u, r, c) => u.Id > 10).Select((u, r, c) =>
//                  new ViewEntity
//                  {
//                      UserName = u.Name,
//                      CompanyId = c.Id,
//                      CompanyName = c.CompanyName,
//                      RoleId = r.Id
//                  }
//                  ).
//                  GroupBy(v => v.RoleId).
//                  Select((v) => new GroupEntity()
//                  {
//                      Count = v.Count(),
//                      FirstOrDefaultName = v.FirstOrDefault(s => s.UserName),
//                      Max = v.Max(s => s.RoleId)
//                  }).ToList();

//                var orderBy = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
//                   new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
//                   new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
//                   )).
//                   Where((u, r, c) => u.Id > 10).
//                   Select((u, r, c) => new { UserName = u.Name, CompanyName = c.CompanyName, RoleName = r.DisplayName, Id = u.Id }).OrderBy(u => u.Id).ToList();
//                //Console.WriteLine($"*****************受影响行数:{result1}****************");
//                // Console.WriteLine($"*****************受影响行数:{result2}****************");
//                Console.WriteLine("****************测试结束*****************");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//    }
//}
