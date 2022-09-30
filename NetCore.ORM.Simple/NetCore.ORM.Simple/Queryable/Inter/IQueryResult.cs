﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Queryable
 * 接口名称 IQueryResult
 * 开发人员：-nhy
 * 创建时间：2022/9/21 9:15:24
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Queryable
{
    public interface IQueryResult<TResult>
    { 
        public IQueryResult<TResult> Skip(int Number);
        public IQueryResult<TResult> Take(int Number);
        public IQueryResult<TResult> ToPage(int takeNumber, int skipNumber);
        public IEnumerable<TResult> ToList();
        public Task<IEnumerable<TResult>> ToListAsync();
        public IQueryResult<TNewResult> Select<TNewResult>(Expression<Func<TResult,TNewResult>> expression) where TNewResult : class;
        public IQueryResult<TResult> Select(Expression<Func<TResult,TResult>> expression);
        public IQueryResult<TResult> Where(Expression<Func<TResult,bool>>expression);
        public  ISimpleGroupByQueryable<TResult, TOrder> OrderBy<TOrder>(Expression<Func<TResult, TOrder>> expression) where TOrder : class;
        public ISimpleGroupByQueryable<TResult, TGroup> GroupBy<TGroup>(Expression<Func<TResult, TGroup>> expression)where TGroup :class;

        public int Count();
       
        public bool Any();
        
        public  Task<int> CountAsync();
       
        public  Task<bool> AnyAsync();
       
        public TResult First();
       
        public  Task<TResult> FirstAsync();
       
        public TResult FirstOrDefault();
       
        public  Task<TResult> FirstOrDefaultAsync();



    }
}
