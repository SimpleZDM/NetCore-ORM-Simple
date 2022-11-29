using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 SqliteBuilder
 * 开发人员：-nhy
 * 创建时间：2022/10/11 15:10:21
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public class SqliteBuilder : BaseBuilder,ISqlBuilder
    {
        public SqliteBuilder(eDBType dbtype):base(dbtype)
        {

        }
        public override SqlCommandEntity GetInsert<TData>(TData data, int random)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = GetNotKeyAndIgnore(type).ToArray();

            sql.StrSqlValue.Append($"{DBMDConst.Insert} {DBMDConst.Into} {DBMDConst.UnSingleQuotes}{GetTableName(type)}{DBMDConst.UnSingleQuotes} ");
            sql.StrSqlValue.Append(DBMDConst.LeftBracket);
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma, Props.Select(p => $"{DBMDConst.UnSingleQuotes}{GetColName(p)}{DBMDConst.UnSingleQuotes}")));
            sql.StrSqlValue.Append(DBMDConst.RightBracket);
            sql.StrSqlValue.Append($" {DBMDConst.Values}{DBMDConst.LeftBracket}");
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma,
                Props.Select(p =>
                {
                    string key = $"{DBMDConst.AT}{GetColName(p)}{DBMDConst.DownLine}{random}";

                    sql.AddParameter(DbType, key, p.GetValue(data));
                    return key;
                })));
            sql.StrSqlValue.Append($"{DBMDConst.RightBracket}{DBMDConst.Semicolon}");
            return sql;
        }

        public override SqlCommandEntity GetUpdate<TData>(TData data, int random)
        {
            return base.GetUpdate(data, random);
        }
        public override SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset)
        {
            return base.GetUpdate(datas, offset);
        }

        public override SqlCommandEntity GetInsert<TData>(List<TData> datas, int offset)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = GetNotKeyAndIgnore(type);
            int count = 0;
            int Index = 0;
            foreach (var data in datas)
            {
                if (count == 0)
                {
                    sql.StrSqlValue.Append($"{DBMDConst.Insert} {DBMDConst.Into} {DBMDConst.UnSingleQuotes}{GetTableName(type)}{DBMDConst.UnSingleQuotes} ");
                    sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                    sql.StrSqlValue.Append(string.Join(DBMDConst.Comma, Props.Select(p => $"{DBMDConst.UnSingleQuotes}{GetColName(p)}{DBMDConst.UnSingleQuotes}")));
                    sql.StrSqlValue.Append(DBMDConst.RightBracket);
                    sql.StrSqlValue.Append($" {DBMDConst.Values}");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                sql.StrSqlValue.Append(string.Join(DBMDConst.Comma,
                  Props.Select(p =>
                  {
                      string key = GetParameterName(Index+offset,GetColName(p));
                      sql.AddParameter(DbType, key, p.GetValue(data));
                      return key;
                  })));
                sql.StrSqlValue.Append(DBMDConst.RightBracket);
                if (count == MysqlConst.INSERTMAX)
                {
                    sql.StrSqlValue.Append(DBMDConst.Semicolon);
                    count = 0;
                }
                else
                {
                    if (Index == datas.Count())
                    {
                        sql.StrSqlValue.Append(DBMDConst.Semicolon);
                    }
                    else
                    {
                        sql.StrSqlValue.Append(DBMDConst.Comma);
                    }
                }
            }
            return sql;
        }


        public override SqlCommandEntity GetInsert(string sql, Dictionary<string, object> Params)
        {
            return base.GetInsert(sql, Params);
        }
        public override SqlCommandEntity GetUpdate(string sql, Dictionary<string, object> Params)
        {
            return base.GetUpdate(sql, Params);
        }
        public override SqlCommandEntity GetDelete(string sql, Dictionary<string, object> Params)
        {
            return base.GetDelete(sql, Params);
        }

        public override QueryEntity GetSelect(string sql, Dictionary<string, object> Params)
        {
            return base.GetSelect(sql, Params);
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
            base.GetSelect(sql, type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="sql"></param>
        public override void GetLastInsert<TData>(QueryEntity sql)
        {
            if (Check.IsNull(sql))
            {
                sql = new QueryEntity();
            }
            Type type = typeof(TData);
            GetSelect(sql, type);
            var Key =GetAutoKey(type);
            if (Check.IsNull(Key))
            {
                throw new Exception("");
            }
            string TableName = GetTableName(type);
            sql.StrSqlValue.Append($" {DBMDConst.Where} {GetColName(Key)}" +
                $"{DBMDConst.Equal}{DBMDConst.LeftBracket}{DBMDConst.Select}" +
                $" Id {DBMDConst.From} sqlite_sequence {DBMDConst.Where}" +
                $" name{DBMDConst.Equal}{DBMDConst.SingleQuotes}" +
                $"{TableName}{DBMDConst.SingleQuotes}{DBMDConst.RightBracket}{DBMDConst.Semicolon}");
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
            base.GetSelect<TData>(select, entity);

            GroupBy(select.OrderInfos, entity);

            OrderBy(select.OrderInfos, entity);

            SetPageList(entity);

            entity.StrSqlValue.Append(DBMDConst.Semicolon);
        }

        public override void GetCount(SelectEntity select, QueryEntity entity)
        {
            base.GetCount(select, entity);

            GroupBy(select.OrderInfos, entity);

            SetPageList(entity);

            entity.StrSqlValue.Append(DBMDConst.Semicolon);
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
        public override SqlCommandEntity GetDelete<TData>(TData data, int random)
        {
            return base.GetDelete(data, random);
        }



        /// <summary>
        /// 拼接分页
        /// </summary>
        /// <param name="sqlEntity"></param>
        protected override void SetPageList(QueryEntity sqlEntity)
        {

            if (sqlEntity.PageNumber < 0 || sqlEntity.PageSize <= 0)
            {
                return;
            }
            sqlEntity.StrSqlValue.Append($" {DBMDConst.Limit} {DBMDConst.AT}{DBMDConst.TakeNumber}" +
                $" {DBMDConst.Offset} {DBMDConst.AT}{DBMDConst.SkipNumber}");
            sqlEntity.AddParameter(DbType, $"{DBMDConst.AT}{DBMDConst.SkipNumber}", (sqlEntity.PageNumber - 1) * sqlEntity.PageSize);
            sqlEntity.AddParameter(DbType, $"{DBMDConst.AT}{DBMDConst.TakeNumber}", sqlEntity.PageSize);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append($" {DBMDConst.Order} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma,
                    OrderByInfos.Where(o => o.IsOrderBy).OrderBy(o => 
                    o.OrderSoft).Select(o => $"{o.TableName}.{o.ColumnName} " +
                    $"{MysqlConst.AscendOrDescend(o.OrderType)}")));
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
                entity.StrSqlValue.Append($" {DBMDConst.Group} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma, 
                    OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).
                    Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected override void Update<TEntity>(SqlCommandEntity sql, string keyName, string tableName, PropertyInfo pKey, TEntity data, IEnumerable<PropertyInfo> Props, int index)
        {
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }
    }
}
