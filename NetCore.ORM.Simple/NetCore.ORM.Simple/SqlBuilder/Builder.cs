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
        MysqlBuilder mysqlBuilder { get; set; }
        SqlServiceBuilder sqlServiceBuilder { get; set; }
        private eDBType dbType;
        public Builder(eDBType DBType)
        {
            dbType = DBType;
            mysqlBuilder = new MysqlBuilder();
            sqlServiceBuilder = new SqlServiceBuilder();
        }

        public SqlCommandEntity GetInsert<TData>(TData data, int random = 0)
        {
            return MatchDBType(() => mysqlBuilder.GetInsert(data,random));
        }

        public SqlCommandEntity GetUpdate<TData>(TData data,int random=0)
        {
            return MatchDBType(() => mysqlBuilder.GetUpdate(data,random));
        }

        public SqlCommandEntity GetInsert<TData>(IEnumerable<TData> datas)
        {
            return MatchDBType(() => mysqlBuilder.GetInsert(datas));
        }

        //public SqlEntity GetSelect<TData>()
        //{
        //    return MatchDBType(() =>mysqlBuilder.GetSelect<TData>());
        //}

        public SqlCommandEntity GetWhereSql<TData>(Expression<Func<TData, bool>> matchCondition)
        {
            return MatchDBType(() => mysqlBuilder.GetWhereSql(matchCondition));
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
             MatchDBType(() => mysqlBuilder.GetSelect<TData>(select,entity));
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
      

        public SqlCommandEntity GetSelect(List<MapEntity> mapInfos,string condition)
        {
            throw new NotImplementedException();
        }

        void ISqlBuilder.GetSelect<TData>()
        {
            throw new NotImplementedException();
        }

        public void GetLastInsert<TData>(QueryEntity sql)
        {
             MatchDBType(() => mysqlBuilder.GetLastInsert<TData>(sql));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDate"></typeparam>
        /// <param name="type"></param>
        /// <param name="conditions"></param>
        /// <param name="treeConditions"></param>
        /// <returns></returns>
        public SqlCommandEntity GetDelete<TDate>(Type type, List<ConditionEntity> conditions,List<TreeConditionEntity> treeConditions)
        {
            return MatchDBType(() => mysqlBuilder.GetDelete<TDate>(type,conditions,treeConditions));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public SqlCommandEntity GetDelete<TData>(TData data)
        {
            return MatchDBType(() => mysqlBuilder.GetDelete(data));
        }

        public void GetCount(SelectEntity select, QueryEntity entity)
        {
             MatchDBType(() => mysqlBuilder.GetCount(select,entity));
        }

        public void GetAync(SelectEntity select, QueryEntity entity)
        {
            MatchDBType(() => mysqlBuilder.GetCount(select, entity));
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
