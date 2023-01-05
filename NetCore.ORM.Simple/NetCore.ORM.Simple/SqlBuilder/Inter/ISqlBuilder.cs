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
    internal interface ISqlBuilder
    {
         SqlCommandEntity GetInsert<TData>(TData data, int random );
        /// <summary>
        /// 批量插入生成sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
         SqlCommandEntity GetInsert<TData>(List<TData> datas,int offset);
         SqlCommandEntity GetUpdate<TData>(TData data,int random);
         SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset);


        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
         void GetSelect<TData>(ContextSelect select,QueryEntity entity);

         SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions);

         SqlCommandEntity GetDelete<TData>(TData data,int random);

         SqlCommandEntity GetInsert(string sql, Dictionary<string, object> Params);

          SqlCommandEntity GetUpdate(string sql, Dictionary<string, object> Params);

         SqlCommandEntity GetDelete(string sql, Dictionary<string, object> Params);


         QueryEntity GetSelect(string sql, Dictionary<string, object> Params);
        

         void GetLastInsert<TData>(QueryEntity sql);
          void GetCount(ContextSelect select, QueryEntity entity);
    }
}
