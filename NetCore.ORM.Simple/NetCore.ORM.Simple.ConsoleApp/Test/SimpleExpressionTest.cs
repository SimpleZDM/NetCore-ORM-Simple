﻿using NetCore.ORM.Simple.ConsoleApp.Entity;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.ConsoleApp
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
            ExpressionTest<UserEntity, CompanyEntity, RoleEntity> u = new ExpressionTest<UserEntity,CompanyEntity,RoleEntity>();
            u.Select((u,c,r)=> new JoinInfoEntity(
                    new JoinMapEntity(eJoinType.Inner, u.RoleId.Equals(r.Id)&&u.Id.Equals(eConditionType.Sign)),
                    new JoinMapEntity(eJoinType.Inner, u.CompanyId.Equals(c.Id))
                    ));
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
            int[] array = new int[] { 1,2,34};
            u.Where(u => u.Id.Equals(array[0]) &&entity.dictionaryKeyEntity[key].Age>u.Age && entity.dictionaryEntity["1"].Age>u.Age&&entity.dictionary["1"] >u.Id&&entity.array[1]>u.Id&&entity.Test.Age>u.Id);
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
    public class ExpressionTest<T,T2,T3>
    {
        MyExpressionAnalysis Visitor;
        public ExpressionTest()
        {
            Visitor = new MyExpressionAnalysis();
        }
        public void Select<TResult>(Expression<Func<T,T2,T3, TResult>> exp)
        {
            Visitor.Start(exp);
        }
        public void Where(Expression<Func<T, bool>> exp)
        {
            Visitor.Start(exp);
        }
    }
}