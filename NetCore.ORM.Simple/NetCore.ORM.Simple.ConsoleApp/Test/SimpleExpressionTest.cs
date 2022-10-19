﻿using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp.Test
 * 接口名称 SimpleExpressionTest
 * 开发人员：-nhy
 * 创建时间：2022/10/17 10:41:08
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.ConsoleApp
{
    internal class SimpleExpressionTest
    {
        public SimpleExpressionTest()
        {
        }

        public void anonymity()
        {
        }
        public void Select()
        {
            ExpressionTest<UserEntity> u = new ExpressionTest<UserEntity>();
            u.Select(u=>new CompanyEntity{CompanyName=u.Name,Id=u.Id});
        }
        public void Where()
        {
            
                Console.WriteLine(typeof(Guid).Name);
                Console.WriteLine(typeof(float).Name);
                Console.WriteLine(typeof(Double).Name);
                Console.WriteLine(typeof(decimal).Name);
            ExpressionTest<UserEntity> u = new ExpressionTest<UserEntity>();
            int[] ids=new int[3] {1,2,3};
            string str = "";
            List<int> lids = new List<int>();
            IEnumerable<int> ints = new List<int>();
            
            lids.Add(1);
            lids.Add(2);
            lids.Add(3);
            u.Where(u => lids[2].Equals(u.Id));
        }

    }
    public class ExpressionTest<T>
    {
        MyExpressionAnalysis Visitor;
        public ExpressionTest()
        {
            Visitor=new MyExpressionAnalysis();
        }
        public void Select<TResult>(Expression<Func<T,TResult>> exp)
        {
            Visitor.Start(exp);
        }
        public void Where(Expression<Func<T,bool>> exp)
        {
            Visitor.Start(exp);
        }
    }
}