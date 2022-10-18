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
        object o = 1;
        object o2 = "232";
        object o3 = 1.1;
        Console.WriteLine(o);
        Console.WriteLine(o2);
        Console.WriteLine($"{o3}");
        Console.WriteLine($"{o}");
        Console.WriteLine($"{o2}");


        //SimpleExpressionTest test = new SimpleExpressionTest();
        ////test.Select();
        //test.Where();
        SimpleMysqlTest MysqlTest = new SimpleMysqlTest();
        //MysqlTest.InsertTest();
        //MysqlTest.UpdateTest();
        //MysqlTest.DeleteTest();
        MysqlTest.QueryTest();

        SimpleSqliteTest sqliteTest = new SimpleSqliteTest();
        //sqliteTest.InsertTest();
        //sqliteTest.UpdateTest();
        //sqliteTest.DeleteTest();
        sqliteTest.QueryTest();

        SimpleSqlServiceTest sqlServcie = new SimpleSqlServiceTest();
        //sqlServcie.InsertTest();
        //sqlServcie.UpdateTest();
        //sqlServcie.DeleteTest();
        sqlServcie.QueryTest();
        return 0;
    }
}

