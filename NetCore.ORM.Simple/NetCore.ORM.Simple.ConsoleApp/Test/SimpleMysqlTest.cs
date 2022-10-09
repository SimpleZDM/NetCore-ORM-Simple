using NetCore.ORM.Simple.Client;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            client = new SimpleClient(
          new DataBaseConfiguration(false,
          new ConnectionEntity("server=localhost;database=testdb;user=root;pwd=123456;SSL Mode=None")
          {
              IsAutoClose = true,
              DBType = eDBType.Mysql,
              Name = "test1",
              ReadWeight = 5,
              WriteReadType = eWriteOrReadType.ReadOrWrite
          }));

            client.SetAPOLog((sql, Params) =>
            {
                Console.WriteLine(sql);
            });
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
                    users.Add(new UserEntity() { Name=$"测试添加{i}",Age=100,RoleId=1,CompanyId=1,Description="测试"});
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
                for (int i = 0; i < Lenght; i++)
                {
                    users[i].Name = "更新用户名称{i}";
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
                List<UserEntity> users = client.Queryable<UserEntity>().Take(100).ToList();
                var result = client.Delete(users[0]).SaveChange();
                var result1 = client.Delete<UserEntity>(d => d.Id.Equals(10)).SaveChange();
                Console.WriteLine($"*****************受影响行数:{result}****************");
                Console.WriteLine($"*****************受影响行数:{result1}****************");

                Console.WriteLine("****************2.多条删除*****************");
                Console.WriteLine($"*****************删除100条****************");
                
                //删除多个数据
                //var result2 = client.Delete(users[0]).SaveChange();
               // var result3 = client.Delete<UserEntity>(d=>d.Id.Equals(10)).SaveChange();
                //Console.WriteLine($"*****************受影响行数:{result1}****************");
               // Console.WriteLine($"*****************受影响行数:{result2}****************");
                Console.WriteLine("****************测试结束*****************");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void QueryTest()
        {
            try
            {
                Console.WriteLine("****************查询测试*****************");
                Console.WriteLine("****************1.简单单表查询*****************");
                ///返回所有
                List<UserEntity> users = client.Queryable<UserEntity>().ToList();
                UserEntity user = client.Queryable<UserEntity>().FirstOrDefault();
                ///数据的条数
                int count = client.Queryable<UserEntity>().Count();
                //是否存在
                bool any = client.Queryable<UserEntity>().Any();

                //返回匿名对象
                var data = client.Queryable<UserEntity>().Select(u => new { Name = u.Name, Id = u.Id }).ToList();
                //加条件
                var data1 = client.Queryable<UserEntity>().Where(user=>user.Id>20&&user.Id<=30).ToList();
                //分组
                var group = client.Queryable<UserEntity>().Where(user=>user.Id>20&&user.Id<=30).GroupBy(u=>new { u.CompanyId}).ToList();
                //排序
                var order = client.Queryable<UserEntity>().Where(user => user.Id > 20 && user.Id <= 30).OrderBy(u=>new { u.Id }).ToList();

                Console.WriteLine($"*****************是否有数据:{any}****************");
                Console.WriteLine($"*****************总行数:{count}****************");

                Console.WriteLine("****************2.多表连接查询*****************");
                Console.WriteLine($"*****************连接查询****************");

                var JoinData = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u,r,c)=>new JoinInfoEntity(
                    new JoinMapEntity(eJoinType.Inner,u.RoleId.Equals(r.Id)),
                    new JoinMapEntity(eJoinType.Inner,u.CompanyId.Equals(c.Id))
                    )).ToList();

                var JoinData1 = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
                   new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                   new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                   )).
                   Where((u,r,c)=>u.Id>10).
                   Select((u,r,c)=>new {UserName=u.Name,CompanyName=c.Name,RoleName=r.Name});

                ///连接查询分组
                var JoinData2 = client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
                  new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                  new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                  )).
                  Where((u, r, c) => u.Id > 10).Select((u,r,c)=>
                  new ViewEntity {
                      UserName=u.Name,
                      CompanyId=c.Id,
                      CompanyName=c.Name,
                      RoleId=r.Id
                  }
                  ).
                  GroupBy(v=>v.RoleId).
                  Select((v) =>new GroupEntity()
                  {
                      Count=v.Count()
                  }).ToList();

                var orderBy= client.Queryable<UserEntity, RoleEntity, CompanyEntity>((u, r, c) => new JoinInfoEntity(
                   new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)),
                   new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                   )).
                   Where((u, r, c) => u.Id > 10).
                   Select((u, r, c) => new { UserName = u.Name, CompanyName = c.Name, RoleName = r.Name,Id=u.Id }).OrderBy(u=>u.Id);
                //Console.WriteLine($"*****************受影响行数:{result1}****************");
                // Console.WriteLine($"*****************受影响行数:{result2}****************");
                Console.WriteLine("****************测试结束*****************");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
