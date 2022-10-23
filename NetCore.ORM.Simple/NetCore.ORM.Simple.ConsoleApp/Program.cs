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
        //StartTast("server=49.233.33.36;database=virtualsoftplatformdb;user=root;pwd=[Txy*!14@msql*^];SSL Mode=None");
        Console.WriteLine();
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

        SimpleExpressionTest test = new SimpleExpressionTest();
        ////test.Select();
        test.Where();
       // SimpleMysqlTest MysqlTest = new SimpleMysqlTest();
        //MysqlTest.InsertTest();
        //MysqlTest.UpdateTest();
       // MysqlTest.DeleteTest();
        //MysqlTest.QueryTest();

        //SimpleSqliteTest sqliteTest = new SimpleSqliteTest();
        ////sqliteTest.InsertTest();
        //sqliteTest.UpdateTest();
        //sqliteTest.DeleteTest();
        //sqliteTest.QueryTest();

        SimpleSqlServiceTest sqlServcie = new SimpleSqlServiceTest();
        //sqlServcie.InsertTest();
        //sqlServcie.UpdateTest();
        //sqlServcie.DeleteTest();
        sqlServcie.QueryTest();
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
        client.SetAPOLog((sql, pars) =>
        {
            //Console.WriteLine(sql);
            //Console.WriteLine($"Date:{DateTime.Now}\t \t sql:{sql}");
        });
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

