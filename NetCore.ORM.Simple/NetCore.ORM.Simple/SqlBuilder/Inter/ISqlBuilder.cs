using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.ISimple.SqlBuilder
 * 接口名称 ISqlBuilder
 * 开发人员：-nhy
 * 创建时间：2022/9/20 11:26:50
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public interface ISqlBuilder
    {
        public SqlCommandEntity GetInsert<TData>(TData data, int random = 0);

        public SqlCommandEntity GetUpdate<TData>(TData data,int random=0);
        public SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset);


        /// <summary>
        /// 批量插入生成sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public SqlCommandEntity GetInsert<TData>(List<TData> datas,int offset);

        /// <summary>
        /// 拼装单挑查询语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public void GetSelect<TData>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public SqlCommandEntity GetWhereSql<TData>(Expression<Func<TData, bool>> matchCondition);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
        public void GetSelect<TData>(SelectEntity select,QueryEntity entity);

        public SqlCommandEntity GetDelete<TDate>(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions);

        public SqlCommandEntity GetDelete<TData>(TData data,int random);

        public void GetLastInsert<TData>(QueryEntity sql);
    }
}
