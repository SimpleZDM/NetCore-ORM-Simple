using MDT.VirtualSoftPlatform.Entity;
using NetCore.ORM.Simple.ConsoleApp.构造匿名对象;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NetCore.ORM.Simple.ConsoleApp;
public static class Program
{
    public static int Main(string []args)
    {
        List<object> objs = new List<object>();
        objs.Add(new object());
        objs.Add(new object());
        object o= objs[0];
        Console.WriteLine(object.ReferenceEquals(o, objs[0]));
        //object o1 = new { UserName = 11 };
        //var b =Activator.CreateInstance(o1.GetType());

        //Anonymity anonymity = new Anonymity();

        //string[] namelist = new string[] { "UserName", "UserId" };
        //Dictionary<string, Type> dic = new Dictionary<string, Type>();
        //dic.Add("UserName", typeof(string));
        //dic.Add("UserId", typeof(int));
        //var o =anonymity.Test1(dic);
        //UserEntity u = new UserEntity();
        //ProductTest productTest = new ProductTest();
        //productTest.StartTest1(new MissionDetailParameter
        //{
        //    ClassID = Guid.Parse("08da0008-e513-4291-8e5d-abbcce797617"),
        //    UserID = Guid.Parse("f906b8ad-2f57-4236-90bd-08b7acc428d2")
        //});
        //productTest.StartTest(new MissionDetailParameter
        //{
        //    ClassID = Guid.Parse("08da0008-e513-4291-8e5d-abbcce797617"),
        //    UserID = Guid.Parse("f906b8ad-2f57-4236-90bd-08b7acc428d2")
        //});
        //Console.WriteLine(typeof(List<int>).IsArray);
        // Console.WriteLine(typeof(Dictionary<int,int>).IsD);
        //Console.WriteLine(typeof(int[]).IsArray);
        // StartTast("server=49.233.33.36;database=virtualsoftplatformdb;user=root;pwd=[Txy*!14@msql*^];SSL Mode=None");

        //Console.WriteLine();
        //object o = 1;
        //object o2 = "232";
        //object o3 = 1.1;
        //Console.WriteLine(o);
        //Console.WriteLine(o2);
        //Console.WriteLine($"{o3}");
        //Console.WriteLine($"{o}");
        //Console.WriteLine($"{o2}");

        //Console.WriteLine(typeof(List<object>).FullName);
        //Console.WriteLine(typeof(object[]).FullName);
        //Console.WriteLine(typeof(List<object>).Name);
        //Console.WriteLine(typeof(object[]).Name);

        //SimpleExpressionTest test = new SimpleExpressionTest();
        ////test.Select();
        //test.Where();
        // SimpleMysqlTest MysqlTest = new SimpleMysqlTest();
        // SimpleExpressionTest test = new SimpleExpressionTest();
        //test.Select();
        // test.Where();
        UserEntity u = new UserEntity();
        List<UserEntity> users = new List<UserEntity>();
        users.Add(u);
        Console.WriteLine(object.ReferenceEquals(users[0],u));
        SimpleMysqlTest MysqlTest = new SimpleMysqlTest();
       // MysqlTest.MoreQuerTest();
        //MysqlTest.sqlTest();
        //MysqlTest.InsertTest();
        //MysqlTest.UpdateTest();
        MysqlTest.DeleteTest();
        //Console.WriteLine();
         MysqlTest.QueryTest();
        //MysqlTest.Transaction();
        MysqlTest.TestCallMethod();
        //SimpleSqliteTest sqliteTest = new SimpleSqliteTest();
        //sqliteTest.InsertTest();

        //sqliteTest.UpdateTest();

        //sqliteTest.UpdateTest();
        //sqliteTest.DeleteTest();
        //sqliteTest.QueryTest();

        //SimpleSqlServiceTest sqlServcie = new SimpleSqlServiceTest();
        //sqlServcie.InsertTest();
        //sqlServcie.UpdateTest();
        //sqlServcie.DeleteTest();
        //sqlServcie.QueryTest();
        return 0;
    }

    public static void StartTast(string strConnection)
    {
        Console.WriteLine("*****************Simple***************");
        Stopwatch watch = new Stopwatch();
        watch.Start();
        ISimpleClient client = new SimpleClient(new DataBaseConfiguration(false, new ConnectionEntity(strConnection)
        {
            DBType = eDBType.Mysql,
            IsAutoClose = false,
            Name = "Test"
        })
            );
        client.SetAOPLog((sql, pars) =>
        {
            //Console.WriteLine(sql);
            //Console.WriteLine($"Date:{DateTime.Now}\t \t sql:{sql}");
        });
        int b=0;
        JoinInfoEntity j = new JoinInfoEntity(eJoinType.Inner,b.Equals(1));
        int DataLength = 20;
        int count = 100;
        long l = 0;
        long sum = 0;
        long sum2 = 0;
        long l2 = 0;
        for (int i = 0; i < count; i++)
        {
            l = watch.ElapsedMilliseconds;
            var data = client.Queryable<MissionDetailEntity>().Where(m => !m.IsDelete || (m.EndTime < DateTime.Now && m.StartTime > DateTime.MinValue)).Take(DataLength);
            //Console.WriteLine(i);
            sum += watch.ElapsedMilliseconds - l;
            l2 = watch.ElapsedMilliseconds;
            data.ToList();
            sum2 += watch.ElapsedMilliseconds - l2;
        }
        watch.Stop();
        Console.WriteLine($"耗时{watch.ElapsedMilliseconds}ms");
        Console.WriteLine($"平均每次耗时：{watch.ElapsedMilliseconds / count}ms");

        Console.WriteLine($"拼接语句耗时{sum}ms");
        Console.WriteLine($"拼接语句平均每次耗时：{sum / count}ms");

        Console.WriteLine($"读取数据耗时{sum2}ms");
        Console.WriteLine($"读取数据平均每次耗时：{sum2 / count}ms");
    }

}

