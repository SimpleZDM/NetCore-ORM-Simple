using MySqlConnector;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 MysqlBuilder
 * 开发人员：-nhy
 * 创建时间：2022/9/20 11:16:54
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public class MysqlBuilder :BaseBuilder,ISqlBuilder
    {

        
        public MysqlBuilder(eDBType dbType):base(dbType)
        {
        }
        public override SqlCommandEntity GetInsert<TData>(TData data, int random)
        {
            return base.GetInsert(data,random);
        }

        public override SqlCommandEntity GetUpdate<TData>(TData data, int random)
        {
            return base.GetUpdate(data,random);
        }
        public override SqlCommandEntity GetUpdate<TData>(List<TData> datas,int offset)
        {
            return base.GetUpdate(datas,offset);
        }
      
        public override SqlCommandEntity GetInsert<TData>(List<TData> datas,int offset)
        {
            return base.GetUpdate(datas,offset);
        }
        public override void GetSelect<TData>(QueryEntity sql)
        {
             base.GetSelect<TData>(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        public override void GetSelect(QueryEntity sql, Type type)
        {
            base.GetSelect(sql,type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="sql"></param>
        public override void GetLastInsert<TData>(QueryEntity sql)
        {
            base.GetLastInsert<TData>(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
        public override void GetSelect<TData>(SelectEntity select, QueryEntity entity)
        {
            base.GetSelect<TData>(select,entity);

            GroupBy(select.OrderInfos, entity);

            OrderBy(select.OrderInfos, entity);

            SetPageList(entity);
        }

        public override void GetCount(SelectEntity select, QueryEntity entity)
        {
             base.GetCount(select,entity);

            GroupBy(select.OrderInfos, entity);

            SetPageList(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDate"></typeparam>
        /// <param name="entity"></param>
        public override SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions)
        {
           
           return base.GetDelete(type, conditions, treeConditions);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        public override SqlCommandEntity GetDelete<TData>(TData data,int random)
        {
           return base.GetDelete(data,random);
        }


       
        /// <summary>
        /// 拼接分页
        /// </summary>
        /// <param name="sqlEntity"></param>
        protected override void SetPageList(QueryEntity sqlEntity)
        {
            base.SetPageList(sqlEntity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append(" Order By ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(o => o.IsOrderBy).OrderBy(o => o.OrderSoft).Select(o => $"{o.TableName}.{o.ColumnName} {MysqlConst.AscendOrDescend(o.OrderType)}")));
                entity.StrSqlValue.Append(" ");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void GroupBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(g => g.IsGroupBy).Any())
            {
                entity.StrSqlValue.Append(" Group By ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected override void Update<TEntity>(SqlCommandEntity sql,string keyName,string tableName,PropertyInfo pKey,TEntity data,IEnumerable<PropertyInfo> Props,int index)
        {
            sql.AddParameter(DbType,$"{keyName}{index}", pKey.GetValue(data));
            sql.StrSqlValue.Append($"UPDATE `{tableName}` SET ");
            sql.StrSqlValue.Append(string.Join(',',
            Props.Select(p =>
            {
                string colName = $"{GetColName(p)}";
                sql.AddParameter(DbType,$"@{colName}{index}", p.GetValue(data));
                return $"`{colName}`=@{colName}{index}";
            })));
            sql.StrSqlValue.Append(" Where ");
            sql.StrSqlValue.Append($"{GetColName(pKey)}={keyName}{index}");
            sql.StrSqlValue.Append(";");
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table,Column);
        }
    }
}
