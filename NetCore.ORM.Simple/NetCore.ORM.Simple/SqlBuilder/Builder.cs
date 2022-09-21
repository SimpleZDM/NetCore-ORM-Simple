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

        public SqlEntity GetInsert<TData>(TData data, int random = 0)
        {
            return MatchDBType(() => mysqlBuilder.GetInsert(data,random));
        }

        public SqlEntity GetUpdate<TData>(TData data,int random=0)
        {
            return MatchDBType(() => mysqlBuilder.GetUpdate(data,random));
        }

        public SqlEntity GetInsert<TData>(IEnumerable<TData> datas)
        {
            return MatchDBType(() => mysqlBuilder.GetInsert(datas));
        }

        //public SqlEntity GetSelect<TData>()
        //{
        //    return MatchDBType(() =>mysqlBuilder.GetSelect<TData>());
        //}

        public SqlEntity GetWhereSql<TData>(Expression<Func<TData, bool>> matchCondition)
        {
            return MatchDBType(() => mysqlBuilder.GetWhereSql(matchCondition));
        }

        public SqlEntity GetSelect<TData>(List<MapEntity> mapInfos, List<JoinTableEntity> joinInfos, string condition)
        {
            return MatchDBType(() => mysqlBuilder.GetSelect<TData>(mapInfos, joinInfos, condition));
        }

        public SqlEntity MatchDBType(params Func<SqlEntity>[] funcs)
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

        public SqlEntity GetSelect(List<MapEntity> mapInfos,string condition)
        {
            throw new NotImplementedException();
        }

        void ISqlBuilder.GetSelect<TData>()
        {
            throw new NotImplementedException();
        }

        public void GetLastInsert<TData>(SqlEntity sql)
        {
             MatchDBType(() => mysqlBuilder.GetLastInsert<TData>(sql));
        }
    }
}
