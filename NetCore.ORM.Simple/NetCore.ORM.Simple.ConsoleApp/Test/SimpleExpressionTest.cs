using NetCore.ORM.Simple.ConsoleApp.Entity;
using NetCore.ORM.Simple.Visitor;
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
            
            ExpressionTest<UserEntity> u = new ExpressionTest<UserEntity>();
            TestEntity entity = new TestEntity();
            entity.Test = new TestEntity();
            entity.dictionary.Add("1",1);
            entity.dictionaryEntity.Add("1",new TestEntity() { Age=100});
            var key = new TestEntity();
            entity.dictionaryKeyEntity.Add(key, new TestEntity() { Age=200});
            entity.array = new int[] { 19,22}; 
            entity.Test.Age = 10;
            u.Where(u => entity.dictionaryKeyEntity[key].Age>u.Age && entity.dictionaryEntity["1"].Age>u.Age&&entity.dictionary["1"] >u.Id&&entity.array[1]>u.Id&&entity.Test.Age>u.Id);
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
