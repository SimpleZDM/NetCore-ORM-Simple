﻿using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp.Test
 * 接口名称 SimpleTest
 * 开发人员：-nhy
 * 创建时间：2022/10/8 16:38:58
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    public class SimpleMysqlTest
    {
        ISimpleClient client;
        public SimpleMysqlTest()
        {
            string MastStr = "server=localhost;database=testdb;user=root;pwd=123456;Allow Zero Datetime=true;Convert Zero Datetime=True;";
            string db1 = "server=localhost;database=testdb1;user=root;pwd=123456;Allow Zero Datetime=true;Convert Zero Datetime=True;";
            string db2 = "server=localhost;database=testdb2;user=root;pwd=123456;Allow Zero Datetime=true;Convert Zero Datetime=True;";
            string db3 = "server=localhost;database=testdb3;user=root;pwd=123456;Allow Zero Datetime=true;Convert Zero Datetime=True;";
            string StrService = "server=49.233.33.36;database=virtualsoftplatformdb;user=root;pwd=[Txy*!14@msql*^];SSL Mode=None";

            DataBaseConfiguration.DBDrives = new Dictionary<eDBType, Tuple<Type, Type>>();
            DataBaseConfiguration.DBDrives.Add(eDBType.Mysql, Tuple.Create(typeof(MySqlConnection), typeof(MySqlParameter)));
            client = new SimpleClient(
          new DataBaseConfiguration(true,
          new(MastStr)
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "master",
              ReadWeight=1,
              WriteReadType = eWriteOrReadType.Write
          }, new(db1)
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "db1",
              ReadWeight = 3,
              WriteReadType = eWriteOrReadType.Read
          },
          new(db2)
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "db2",
              ReadWeight = 2,
              WriteReadType = eWriteOrReadType.Read
          },
          new(db3)
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "db3",
              ReadWeight = 5,
              WriteReadType = eWriteOrReadType.Read
          }));

            client.SetAOPLog((sql, Params) =>
            {
                //Console.WriteLine(sql);
            });
            client.SetAttr(typeof(MyTableAttrbute), typeof(MyColumnAttrbute));
            // var data=client.Queryable<MatchLog>().Take(10).ToList();
        }
        /// <summary>
        /// 添加测试
        /// </summary>
        public void InsertTest()
        {
            try
            {
                Console.WriteLine("****************新增测试*****************");
                Console.WriteLine("****************1.单条插入*****************");
                UserEntity user = new UserEntity();
                user.Name = "test1-小明";
                var result = client.Insert(user).SaveChange();
                Console.WriteLine($"*****************受影响行数:{result}****************");

                Console.WriteLine("****************2.多条插入*****************");
                Console.WriteLine($"*****************插入100条****************");
                List<UserEntity> users = new List<UserEntity>();
                const int Lenght = 100;
                for (int i = 0; i < Lenght; i++)
                {
                    users.Add(new UserEntity() { Name = $"测试添加{i}", Age = i, RoleId = 1, CompanyId = 1, Description = "测试" });
                }
                var result1 = client.Insert(users).SaveChange();
                Console.WriteLine($"*****************受影响行数:{result1}****************");
                Console.WriteLine("****************测试结束*****************");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        /// <summary>
        /// 更新测试
        /// </summary>
        public void UpdateTest()
        {
            try
            {
                Console.WriteLine("****************更新测试*****************");
                Console.WriteLine("****************1.单条更新*****************");
                UserEntity user = client.Queryable<UserEntity>().First();
                user.Name = "测试更新1111";
                var result = client.Update(user).SaveChange();
                Console.WriteLine($"*****************受影响行数:{result}****************");

                Console.WriteLine("****************2.多条更新*****************");
                Console.WriteLine($"*****************更新100条****************");
                List<UserEntity> users = client.Queryable<UserEntity>().Take(100).ToList();
                const int Lenght = 100;
                for (int i = 0; i < users.Count(); i++)
                {
                    users[i].Name = $"更新用户名称{i}";
                }
                var result1 = client.Update(users).SaveChange();
                Console.WriteLine($"*****************受影响行数:{result1}****************");
                Console.WriteLine("****************测试结束*****************");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 删除测试
        /// </summary>
        public void DeleteTest()
        {
            try
            {
                Console.WriteLine("****************删除测试*****************");
                Console.WriteLine("****************1.单条删除*****************");
                var users = client.Queryable<UserEntity>().Take(100).ToList().ToArray();
                var result = client.Delete(users[0]).SaveChange();
                int id = 1503;
                UserEntity user = users[3];
                user.IDDD = user.Id;
                Dictionary<string, UserEntity> duser = new Dictionary<string, UserEntity>();
                duser.Add("1", users[4]);
                //var result0 = client.Delete<UserEntity>(d => d.Id.Equals(1503)).SaveChange();
                //var result1 = client.Delete<UserEntity>(d => d.Id.Equals(id)).SaveChange();
                //var result2 = client.Delete<UserEntity>(d => d.Id.Equals(user.Id)).SaveChange();
                // var result14 = client.Delete<UserEntity>(d => d.Id.Equals(duser["1"].Id)).SaveChange();
                var result13 = client.Delete<UserEntity>(d => d.Id.Equals(users[3].IDDD)).SaveChange();
                var result15 = client.Delete<UserEntity>(d => d.Id.Equals(user.IDDD)).SaveChange();

                client.SaveChange();

                Console.WriteLine($"*****************受影响行数:{result}****************");
                //Console.WriteLine($"*****************受影响行数:{result1}****************");

                Console.WriteLine("****************2.多条删除*****************");
                Console.WriteLine($"*****************删除100条****************");

                //删除多个数据
                var result12 = client.Delete(users[0]).SaveChange();
                var result32 = client.Delete<UserEntity>(d => d.Id.Equals(10)).SaveChange();
                //Console.WriteLine($"*****************受影响行数:{result1}****************");
                //Console.WriteLine($"*****************受影响行数:{result2}****************");
                Console.WriteLine("****************测试结束*****************");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void QueryTest()
        {

            // var myobj = new { Name = "sd", Age = 10 };
            // var type= myobj.GetType();
            // var arr=type.GetFields();
            // var arr1=type.GetProperties();
            // var o=Activator.CreateInstance(type,"d",10);
            //var fdata=client.Queryable<UserEntity,RoleEntity>
            //     ((u,r)=>new JoinInfoEntity(eJoinType.Inner,u.RoleId==r.Id))
            //     .GroupBy(u=>u.RoleId).Select(u=> new 
            //     { Name = u.FirstOrDefault(f => f.Name),
            //         RoleId = u.Key,
            //       GID=u.FirstOrDefault(f=>f.gIdColumn)}).ToList();

            int i = 1000;
            int j = 2000;
            //var JoinData = client.Queryable<UserEntity, RoleEntity, CompanyEntity>(
            //        (u, r, c) => new JoinInfoEntity(
            //        new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
            //        new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
            //        )).GroupBy((u,r,c)=>new {u.Id}).Where(u=>u.Id>=i&&u.Id<=j).Where(u=>u.Id>1500).ToList();
            try
            {

                //var datasss = client.Queryable<MissionDetailEntity>().Where(m => !m.IsDelete || 
                //(m.EndTime < DateTime.Now && m.StartTime >DateTime.MinValue)||!m.Id.Equals(Guid.Empty)).Take(500).ToList();
                Console.WriteLine("****************查询测试*****************");
                Console.WriteLine("****************1.简单单表查询*****************");
                ///返回所有
                int[] ids = new int[] { 1811, 1813, 1814, 1815, 18116 };
                Dictionary<int, int> dic = new Dictionary<int, int>();
                List<int> lids = new List<int>();
                lids.Add(1811);
                lids.Add(1816);
                dic.Add(1816, 1816);
                dic.Add(1817, 1817);
                string[] names = new string[] { "111", "222", "333" };
                string str = "111";

                List<UserEntity> users = client.Queryable<UserEntity>().Where(u => ids.Contains(u.Id) && names.Contains(u.Name)).ToList();
                List<ViewEntity> c = new List<ViewEntity>();
                c.Count();
                ids.Count();
                c.Add(new ViewEntity() { RoleId = 1816 });
                UserEntity user = client.Queryable<UserEntity>().Where(u => c[0].RoleId.Equals(u.Id) && u.Name.Contains(str)).FirstOrDefault();
                List<UserEntity> left = client.Queryable<UserEntity>().Where(u => u.Name.LeftContains(str)).ToList();
                List<UserEntity> user1 = client.Queryable<UserEntity>().Where(u => u.Id.Equals(ids[0])).ToList();
                List<UserEntity> user2 = client.Queryable<UserEntity>().Where(u => lids[1].Equals(u.Id)).ToList();
                List<UserEntity> user3 = client.Queryable<UserEntity>().Where(u => ids.Contains(u.Id)).ToList();
                List<UserEntity> user4 = client.Queryable<UserEntity>().Where(u => lids.Contains(u.Id)).ToList();
                ///数据的条数
                int count = client.Queryable<UserEntity>().Count();
                ////是否存在
                bool any = client.Queryable<UserEntity>().Any();

                var first = client.Queryable<UserEntity>().First();

                var firstordefault = client.Queryable<UserEntity>().FirstOrDefault();

                //返回匿名对象
                var data0 = client.Queryable<UserEntity>().Select(u => new { Name = u.Name, Id = u.Id }).ToList();
                var data = client.Queryable<UserEntity>().Select(u => new UserEntity { Name = u.Name, Id = u.Id }).
                  Select(u => new { Name = u.Name, Id = u.Id }).ToList();
                //加条件
                int min = 1746;
                int max = 19999;
                var data1 = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).ToList();
                //分组
                var group = client.Queryable<UserEntity>().
                    Where(user => user.Id > min && user.Id <= max).
                    GroupBy(u => new { u.CompanyId }).Where(u => u.Id > 100).ToList();
                //排序
                var order = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).OrderBy(u => new { u.Id }).ToList();
                var orderDesce = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).OrderByDescending(u => new { u.Id }).ToList();
                //返回匿名对象
                var data01 = client.Queryable<UserEntity>().Select(u => new { Name = u.Name, Id = u.Id }).ToList();
                var data111 = client.Queryable<UserEntity>().Select(u => new UserEntity { Name = u.Name, Id = u.Id }).
                  Select(u => new { Name = u.Name, Id = u.Id }).ToList();
                //加条件
                int min1 = 1746;
                int max1 = 19999;
                var data11 = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).ToList();
                //分组
                var group1 = client.Queryable<UserEntity>().
                    Where(user => user.Id > min && user.Id <= max).
                    GroupBy(u => new { u.CompanyId }).Where(u => u.Id > 100).ToList();
                //排序
                var order1 = client.Queryable<UserEntity>().Where(user => user.Id >
                min && user.Id <= max).OrderBy(u => new { u.Id }).ToList();

                var orderDesc1e = client.Queryable<UserEntity>().Where(user => user.Id > min && user.Id <= max).OrderByDescending(u => new { u.Id }).ToList();

                //Console.WriteLine($"*****************是否有数据:{any}****************");
                //Console.WriteLine($"*****************总行数:{count}****************");

                //Console.WriteLine("****************2.多表连接查询*****************");
                //Console.WriteLine($"*****************连接查询****************");

                //var JoinData = client.Queryable<UserEntity, RoleEntity, CompanyEntity>(
                //    (u, r, c) => new JoinInfoEntity(
                //    new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                //    new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                //    )).ToList();

                //var JoinData1 = client.Queryable<
                //    UserEntity, RoleEntity, CompanyEntity>(
                //    (u, r, c) => new JoinInfoEntity(
                //   new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                //   new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                //   )).
                //   Where((u, r, c) => u.Id > 10).OrderByDescending((u, r) => u.Id).
                //   Select((u, r, c) => new
                //   {
                //       UserName = u.Name,
                //       CompanyName = c.CompanyName,
                //       RoleName = r.DisplayName,
                //       Id = u.Id
                //   });

                //UserEntity u = new UserEntity();
                //RoleEntity r = new RoleEntity();
                //CompanyEntity cc = new CompanyEntity();
                //new JoinInfoEntity(
                //   (eJoinType.Inner, u.RoleId.Equals(r.Id)),
                //   (eJoinType.Inner, u.CompanyId.Equals(cc.Id))
                //   );
                var JoinData1 = client.Queryable<
                    UserEntity, RoleEntity, CompanyEntity>(
                    (u, r, c) => new JoinInfoEntity(
                   eJoinType.Inner, u.RoleId.Equals(r.Id),
                   eJoinType.Inner, u.CompanyId.Equals(c.Id)
                   )).
                   Where((u, r, c) => u.Id > 10).OrderByDescending((u, r) => u.Id).
                   Select((u, r, c) => new
                   {
                       UserName = u.Name,
                       CompanyName = c.CompanyName,
                       RoleName = r.DisplayName,
                       Id = u.Id
                   });

                //JoinData1.Where(s => s.Id > 10);

                //JoinData1.Where(s => s.Id > 100 && s.Id > 1000);

                //JoinData1.Where(s => true);

                //var data111 = JoinData1.ToList();
                ///////连接查询分组
                //var JoinData2 = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
                //  new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                //  new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                //  )).
                //  Where((u, r, c) => u.Id > 10).Select((u, r, c) =>
                //  new ViewEntity
                //  {
                //      UserName = u.Name,
                //      CompanyId = c.Id,
                //      CompanyName = c.CompanyName,
                //      RoleId = r.Id
                //  }
                //  ).
                //  GroupBy(v => v.RoleId).
                //  Select((v) => new GroupEntity()
                //  {
                //      Count = v.Count(),
                //      FirstOrDefaultName = v.FirstOrDefault(s => s.UserName),
                //      Max = v.Max(s => s.RoleId)
                //  }).ToList();
                var data1111 = JoinData1.ToList();
                /////连接查询分组
                var JoinData2 = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) =>
                new JoinInfoEntity(
                  eJoinType.Inner, u.RoleId.Equals(r.Id),
                  eJoinType.Inner, u.CompanyId.Equals(c.Id))).
                  Where((u, r, c) => u.Id > 10).Select((u, r, c) =>
                  new ViewEntity
                  {
                      UserName = u.Name,
                      CompanyId = c.Id,
                      CompanyName = c.CompanyName,
                      RoleId = r.Id
                  }
                  ).
                  GroupBy(v => v.RoleId).
                  Select((v) => new GroupEntity()
                  {
                      Count = v.Count(),
                      //FirstOrDefaultName = v.FirstOrDefault(s => s.UserName),
                      //Max = v.Max(s => s.RoleId)
                  }).ToList();

                //var orderBy = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
                //   new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                //   new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                //   )).
                //   Where((u, r, c) => u.Id > 10).
                //   Select((u, r, c) => new { UserName = u.Name, CompanyName = c.CompanyName, RoleName = r.DisplayName, Id = u.Id }).OrderBy(u => u.Id).ToList();
                //Console.WriteLine($"*****************受影响行数:{result1}****************");
                // Console.WriteLine($"*****************受影响行数:{result2}****************");
                Console.WriteLine("****************测试结束*****************");
                var obj = new { sdfs = 1 };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// 读写分离 负载均衡
        /// </summary>
        public void MoreQuerTest()
        {
            var master = 0;
            var db1 = 0;
            var db2 = 0;
            var db3 = 0;
            var othder = 0;

            UserEntity iuser = new UserEntity();
            iuser.Name = "插入小明";
            iuser = client.Insert(iuser).ReturnEntity();
            iuser.Name = "更新小明";


            var result = client.Update(iuser).SaveChange();

            for (int i = 0; i < 100; i++)
            {
                var user = client.Queryable<UserEntity>().Where(u => u.Id.Equals(1)).First();
                if (user != null)
                {
                    if (user.RoleId == 1)
                    {
                        db1++;
                    }
                    else if (user.RoleId == 2)
                    {
                        db2++;
                    }
                    else if (user.RoleId == 3)
                    {
                        db3++;
                    }
                    else if (user.RoleId == 0)
                    {
                        master++;
                    }
                    else
                    {
                        othder++;
                        Console.WriteLine(user.Id);
                    }
                }
            }
            Console.WriteLine($"db1={db1}\n db2={db2}\n db3={db3} \n master={master} \nothder={othder}");
        }

        public void sqlTest()
        {
            Dictionary<string, object> p = new Dictionary<string, object>();
            p.Add("@CompanyId_0", 1);
            p.Add("@Name_0", "sdf");
            p.Add("@Description_0", "111");
            p.Add("@RoleId_0", 1);
            p.Add("@gIdColumn_0", Guid.Empty);
            p.Add("@Age_0", 1);

            var res = client.Insert<UserEntity>("INSERT  INTO  `usertable` (`CompanyId`,`Name`,`Description`,`RoleId`,`gIdColumn`,`Age`) " +
                 "VALUE(@CompanyId_0,@Name_0,@Description_0,@RoleId_0,@gIdColumn_0,@Age_0);", p).SaveChange();
        }
        /// <summary>
        /// 测试事务
        /// </summary>
        public void Transaction()
        {
            try
            {
                client.BeginTransaction();
                UserEntity u= client.Queryable<UserEntity>().Where(u=>u.Id==3192).FirstOrDefault();
                //u.Name = "bbbbsdsdfsb";
                //client.Update(u).SaveChange();
                int zero= 0;
               // int res=1 / zero;
                client.Commit();
            }
            catch (Exception e)
            {
                client.RollBack();
                Console.WriteLine(e.Message);
            }


        }
    }
}
