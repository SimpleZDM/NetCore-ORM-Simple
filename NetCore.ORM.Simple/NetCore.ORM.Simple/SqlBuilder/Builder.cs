using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
    public class Builder : ISqlBuilder
    {
        ISqlBuilder mysqlBuilder { get; set; }
        ISqlBuilder sqlServiceBuilder { get; set; }
        ISqlBuilder sqliteBuilder { get; set; }
        private eDBType dbType;
        public Builder(eDBType DBType)
        { 
            dbType = DBType;
            switch (dbType)
            {
                case eDBType.Mysql:
                    mysqlBuilder = new MysqlBuilder(dbType);
                    break;
                case eDBType.SqlService:
                    sqlServiceBuilder = new SqlServiceBuilder(dbType);
                    break;
                case eDBType.Sqlite:
                    sqliteBuilder = new SqliteBuilder(dbType);
                    break;
                default:
                    break;
            }
        }

        public SqlCommandEntity GetInsert<TData>(TData data, int random = 0)
        {
            return MatchDBType(
                () => mysqlBuilder.GetInsert(data,random),
                () => sqlServiceBuilder.GetInsert(data,random),
                () => sqliteBuilder.GetInsert(data,random)
               );
        }
        

        public SqlCommandEntity GetUpdate<TData>(TData data,int random=0)
        {
            return MatchDBType(
                () => mysqlBuilder.GetUpdate(data,random),
                () => sqlServiceBuilder.GetUpdate(data,random),
                () => sqliteBuilder.GetUpdate(data,random)
               );
        }
        public SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset = 0)
        {
            return MatchDBType(
                () => mysqlBuilder.GetUpdate(datas,offset),
                () => sqlServiceBuilder.GetUpdate(datas,offset),
                () => sqliteBuilder.GetUpdate(datas,offset)
               );
        }

        public SqlCommandEntity GetInsert<TData>(List<TData> datas,int offset=0)
        {
            return MatchDBType(
                () => mysqlBuilder.GetInsert(datas,offset),
                () => sqlServiceBuilder.GetInsert(datas,offset),
                () => sqliteBuilder.GetInsert(datas,offset)
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="select"></param>
        /// <param name="entity"></param>
        public void GetSelect<TData>(SelectEntity select,QueryEntity entity)
        {
             entity.LastAnonymity=select.LastAnonymity;
             entity.LastType=select.LastType;
             entity.DyToMap = select.DyToMap;
             MatchDBType(
                 () => mysqlBuilder.GetSelect<TData>(select,entity),
                 () => sqlServiceBuilder.GetSelect<TData>(select,entity),
                 () => sqliteBuilder.GetSelect<TData>(select,entity)
                 );
        }

        public SqlCommandEntity MatchDBType(params Func<SqlCommandEntity>[] funcs)
        {
            if (Check.IsNull(funcs))
            {
                throw new ArgumentException(nameof(funcs));
            }
            if ((int)dbType < funcs.Length)
            {
                return funcs[(int)dbType].Invoke();
            }
            return null;
        }

        public void GetLastInsert<TData>(QueryEntity sql)
        {
             MatchDBType(
                 () => mysqlBuilder.GetLastInsert<TData>(sql),
                 () => sqlServiceBuilder.GetLastInsert<TData>(sql),
                 () => sqliteBuilder.GetLastInsert<TData>(sql)
                 );
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
            return MatchDBType(
                () => mysqlBuilder.GetDelete(type,conditions,treeConditions),
                () => sqlServiceBuilder.GetDelete(type,conditions,treeConditions),
                () => sqliteBuilder.GetDelete(type,conditions,treeConditions)
                );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public SqlCommandEntity GetDelete<TData>(TData data,int random)
        {
            return MatchDBType(
                () => mysqlBuilder.GetDelete(data,random),
                () => sqlServiceBuilder.GetDelete(data,random),
                () => sqliteBuilder.GetDelete(data,random)
                );
        }

        public void GetCount(SelectEntity select, QueryEntity entity)
        {
             
             MatchDBType(
                 () => mysqlBuilder.GetCount(select,entity),
                 () => sqlServiceBuilder.GetCount(select,entity),
                 () => sqliteBuilder.GetCount(select,entity)
             );
        }

        public void GetAync(SelectEntity select, QueryEntity entity)
        {
            MatchDBType(
                () => mysqlBuilder.GetCount(select, entity),
                () => sqlServiceBuilder.GetCount(select, entity),
                () => sqliteBuilder.GetCount(select, entity)
                );
        }

        public void MatchDBType(params Action[] actions)
        {
            if (Check.IsNull(actions))
            {
                throw new ArgumentException(nameof(actions));
            }
            if ((int)dbType < actions.Length)
            {
                actions[(int)dbType].Invoke();
            }
        }
    }
}
