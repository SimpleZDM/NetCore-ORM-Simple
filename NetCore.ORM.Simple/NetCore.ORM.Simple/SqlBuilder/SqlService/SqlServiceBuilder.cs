using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 SqlServiceBuilder
 * 开发人员：-nhy
 * 创建时间：2022/9/20 14:59:18
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public class SqlServiceBuilder : ISqlBuilder
    {
        public void GetCount(SelectEntity select, QueryEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetDelete<TDate>(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetDelete<TData>(TData data, int random)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetInsert<TData>(TData data, int random = 0)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetInsert<TData>(List<TData> datas, int offset)
        {
            throw new NotImplementedException();
        }

        public void GetLastInsert<TData>(QueryEntity sql)
        {
            throw new NotImplementedException();
        }

        public void GetSelect<TData>()
        {
            throw new NotImplementedException();
        }

        public void GetSelect<TData>(SelectEntity select, QueryEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetUpdate<TData>(TData data, int random = 0)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset)
        {
            throw new NotImplementedException();
        }

        public SqlCommandEntity GetWhereSql<TData>(Expression<Func<TData, bool>> matchCondition)
        {
            throw new NotImplementedException();
        }
    }
}
