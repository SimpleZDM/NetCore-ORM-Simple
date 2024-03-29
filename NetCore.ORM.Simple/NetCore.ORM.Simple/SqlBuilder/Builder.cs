﻿using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;


/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 Builder
 * 开发人员：-nhy
 * 创建时间：2022/9/20 11:15:47
 * 描述说明：构造sql语句
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    internal class Builder : ISqlBuilder
    {
        ISqlBuilder sqlBuilder { get; set; }
        private eDBType dbType;
        public Builder(eDBType DBType)
        { 
            dbType = DBType;
            switch (dbType)
            {
                case eDBType.Mysql:
                    sqlBuilder = new MysqlBuilder(dbType);
                    break;
                case eDBType.SqlService:
                    sqlBuilder = new SqlServiceBuilder(dbType);
                    break;
                case eDBType.Sqlite:
                    sqlBuilder = new SqliteBuilder(dbType);
                    break;
                default:
                    break;
            }
        }

        public void SetAttr(Type Table = null, Type Column = null)
        {
               // sqlBuilder.SetAttr(Table, Column); 
        }
        public SqlCommandEntity GetInsert<TData>(TData data, int random = 0)
        {
            return sqlBuilder.GetInsert(data, random);
        }
        

        public SqlCommandEntity GetUpdate<TData>(TData data,int random=0)
        {
            return sqlBuilder.GetUpdate(data, random);
        }
        public SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset = 0)
        {
            return sqlBuilder.GetUpdate(datas, offset);
               
        }

        public SqlCommandEntity GetInsert<TData>(List<TData> datas,int offset=0)
        {
            return sqlBuilder.GetInsert(datas, offset);
        }
        public  SqlCommandEntity GetInsert(string sql, Dictionary<string, object> Params)
        {
            return sqlBuilder.GetInsert(sql, Params);
        }
        public  SqlCommandEntity GetUpdate(string sql, Dictionary<string, object> Params)
        {
            return sqlBuilder.GetUpdate(sql, Params);
        }
        public  SqlCommandEntity GetDelete(string sql, Dictionary<string, object> Params)
        {
            return sqlBuilder.GetDelete(sql, Params);
        }

        public  QueryEntity GetSelect(string sql, Dictionary<string, object> Params)
        {
            return sqlBuilder.GetSelect(sql, Params);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="select"></param>
        /// <param name="entity"></param>
        public void GetSelect<TData>(ContextSelect select,QueryEntity entity)
        {
             entity.LastAnonymity=select.LastAnonymity;
             select.MapInfos = select.MapInfos.OrderBy(m=>m.Soft).ToList();
            sqlBuilder.GetSelect<TData>(select, entity);

        }

       

        public void GetLastInsert<TData>(QueryEntity sql)
        {
            sqlBuilder.GetLastInsert<TData>(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDate"></typeparam>
        /// <param name="type"></param>
        /// <param name="conditions"></param>
        /// <param name="treeConditions"></param>
        /// <returns></returns>
        public SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions,List<TreeConditionEntity> treeConditions)
        {
            return sqlBuilder.GetDelete(type, conditions, treeConditions);
                
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public SqlCommandEntity GetDelete<TData>(TData data,int random)
        {
            return sqlBuilder.GetDelete(data, random);
        }

        public void GetCount(ContextSelect select, QueryEntity entity)
        {
            sqlBuilder.GetCount(select, entity);
        }

        public void GetAync(ContextSelect select, QueryEntity entity)
        {
            sqlBuilder.GetCount(select, entity);
        }
    }
}
