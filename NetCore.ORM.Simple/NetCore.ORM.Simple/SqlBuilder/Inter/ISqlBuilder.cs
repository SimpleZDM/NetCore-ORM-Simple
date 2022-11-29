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
        public SqlCommandEntity GetInsert<TData>(TData data, int random );
        /// <summary>
        /// 批量插入生成sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public SqlCommandEntity GetInsert<TData>(List<TData> datas,int offset);
        public SqlCommandEntity GetUpdate<TData>(TData data,int random);
        public SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset);


        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
        public void GetSelect<TData>(SelectEntity select,QueryEntity entity);

        public SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions);

        public SqlCommandEntity GetDelete<TData>(TData data,int random);

        public SqlCommandEntity GetInsert(string sql, Dictionary<string, object> Params);

        public  SqlCommandEntity GetUpdate(string sql, Dictionary<string, object> Params);

        public SqlCommandEntity GetDelete(string sql, Dictionary<string, object> Params);


        public QueryEntity GetSelect(string sql, Dictionary<string, object> Params);
        

        public void GetLastInsert<TData>(QueryEntity sql);
        public  void GetCount(SelectEntity select, QueryEntity entity);
        public  void SetAttr(Type Table = null, Type Column = null);
    }
}
