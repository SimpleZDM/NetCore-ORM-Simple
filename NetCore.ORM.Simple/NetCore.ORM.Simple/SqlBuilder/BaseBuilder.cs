using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.SqlBuilder
 * 接口名称 BaseBuilder
 * 开发人员：-nhy
 * 创建时间：2022/10/11 15:14:12
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    internal class BaseBuilder
    {
        protected eDBType DbType;
        protected Type tableAtrr;
        protected Type columnAttr;
        public BaseBuilder(eDBType dbtype)
        {
            DbType = dbtype;
        }

        /// <summary>
        /// 单条sql语句生成
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        public virtual SqlCommandEntity GetInsert<TData>(TData data, int random)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore().ToArray();

            sql.StrSqlValue.Append($"{DBMDConst.Insert}{DBMDConst.Into} " +
                $"{DBMDConst.UnSingleQuotes}{type.GetTableName()}{DBMDConst.UnSingleQuotes} ");
            sql.StrSqlValue.Append(DBMDConst.LeftBracket);
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(), Props.Select(p =>
            $"{DBMDConst.UnSingleQuotes}{p.GetColName()}{DBMDConst.UnSingleQuotes}")));
            sql.StrSqlValue.Append(DBMDConst.RightBracket);

            sql.StrSqlValue.AppendLine();
            sql.AddHorizontalTAB();
            sql.StrSqlValue.Append($" {DBMDConst.Value}");
            sql.StrSqlValue.AppendLine();
            sql.StrSqlValue.Append($"{DBMDConst.LeftBracket}");
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(),
                Props.Select(p =>
                {
                    string key = GetParameterName(random, p.GetColName());

                    sql.AddParameter(DbType, key, p.GetValue(data));
                    return key;
                })));
            sql.StrSqlValue.Append($"{DBMDConst.RightBracket}{DBMDConst.Semicolon}");
            sql.DbCommandType = eDbCommandType.Insert;
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public virtual SqlCommandEntity GetInsert(string sql, Dictionary<string, object> Params)
        {
            SqlCommandEntity sqlCommand = new SqlCommandEntity(sql);
            if (!Check.IsNull(Params))
            {
                foreach (var item in Params)
                {
                    sqlCommand.AddParameter(DbType, item.Key, item.Value);
                }
            }
            sqlCommand.DbCommandType = eDbCommandType.Insert;
            return sqlCommand;
        }

        /// <summary>
        /// 更新单条sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual SqlCommandEntity GetUpdate<TData>(TData data, int random)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore();
            var pKey = type.GetKey();
            if (Check.IsNull(pKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            string keyName = pKey.GetColName();
            Update(sql, keyName, type.GetTableName(), pKey, data, Props, random);
            sql.DbCommandType = eDbCommandType.Update;
            sql.StrSqlValue.Append(DBMDConst.Semicolon);
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public virtual SqlCommandEntity GetUpdate(string sql, Dictionary<string, object> Params)
        {
            SqlCommandEntity sqlCommand = new SqlCommandEntity(sql);
            if (!Check.IsNull(Params))
            {
                foreach (var item in Params)
                {
                    sqlCommand.AddParameter(DbType, item.Key, item.Value);
                }
            }
            sqlCommand.DbCommandType = eDbCommandType.Update;
            return sqlCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore();
            var pKey = type.GetKey();
            string tableName = type.GetTableName();
            int Index = offset;
            if (Check.IsNull(pKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            string keyName = pKey.GetColName();
            foreach (var data in datas)
            {
                Update(sql, keyName, tableName, pKey, data, Props, Index);
                Index++;
            }
            sql.DbCommandType = eDbCommandType.Update;
            return sql;
        }

        /// <summary>
        /// 批量插入生成sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public virtual SqlCommandEntity GetInsert<TData>(List<TData> datas, int offset)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore();
            int count = 0;
            int Index = 0;
            foreach (var data in datas)
            {
                if (count == 0)
                {
                    sql.StrSqlValue.Append($"{DBMDConst.Insert} {DBMDConst.Into}" +
                        $" {DBMDConst.UnSingleQuotes}{type.GetTableName()}" +
                        $"{DBMDConst.UnSingleQuotes} ");
                    sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                    sql.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(), Props.Select(p =>
                    $"{DBMDConst.UnSingleQuotes}{p.GetColName()}{DBMDConst.UnSingleQuotes}")));
                    sql.StrSqlValue.Append(DBMDConst.RightBracket);
                    sql.AddLineFeed();
                    sql.AddHorizontalTAB();
                    sql.StrSqlValue.Append($" {DBMDConst.Value}");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                sql.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(),
                  Props.Select(p =>
                  {
                      string key = GetParameterName(Index + offset, p.GetColName());//$"{DBMDConst.AT}{GetColName(p)}{DBMDConst.DownLine}{Index + offset}";
                      sql.AddParameter(DbType, key, p.GetValue(data));
                      return key;
                  })));
                sql.StrSqlValue.Append(DBMDConst.RightBracket);
                if (count == DataBaseConfiguration.INSERTMAX)
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
                        sql.AddLineFeed();
                    }
                }
            }
            sql.DbCommandType = eDbCommandType.Insert;
            return sql;
        }
        /// <summary>
        /// 拼装单挑查询语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public virtual void GetSelect<TData>(QueryEntity sql)
        {
            if (Check.IsNull(sql))
            {
                sql = new QueryEntity();
            }
            Type type = typeof(TData);
            sql.StrSqlValue.Append($"{DBMDConst.Select} " +
                $"{string.Join(DBMDConst.Comma.ToString(), type.GetNoIgnore())} " +
                $"{DBMDConst.From} {type.GetTableName()} ");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public virtual QueryEntity GetSelect(string sql, Dictionary<string, object> Params)
        {
            QueryEntity sqlCommand = new QueryEntity(sql);
            if (!Check.IsNull(Params))
            {
                foreach (var item in Params)
                {
                    sqlCommand.AddParameter(DbType, item.Key, item.Value);
                }
            }
            sqlCommand.DbCommandType = eDbCommandType.Query;
            return sqlCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        public virtual void GetSelect(QueryEntity sql, Type type)
        {
            if (Check.IsNull(sql))
            {
                sql = new QueryEntity();
            }
            sql.StrSqlValue.Append($"{DBMDConst.Select} " +
                $"{string.Join(DBMDConst.Comma.ToString(), type.GetNoIgnore().Select(p => p.GetColName()))} " +
                $"{DBMDConst.From} {type.GetTableName()} ");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="sql"></param>
        public virtual void GetLastInsert<TData>(QueryEntity sql)
        {
            if (Check.IsNull(sql))
            {
                sql = new QueryEntity();
            }
            Type type = typeof(TData);
            GetSelect(sql, type);
            var Key = type.GetKey();
            sql.StrSqlValue.Append($" {DBMDConst.Where} {Key.GetColName()}" +
                $"{DBMDConst.Equal}{DBMDConst.LAST_INSERT_ID}" +
                $"{DBMDConst.Semicolon}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
        public virtual void GetSelect<TData>(ContextSelect select, QueryEntity entity)
        {
            if (Check.IsNull(entity))
            {
                entity = new QueryEntity();
            }
            if (Check.IsNull(select))
            {
                throw new ArgumentNullException(nameof(select));
            }
            //视图
            entity.StrSqlValue.Append($"{DBMDConst.Select} ");
            entity.MapInfos = select.MapInfos.ToArray();
            LinkMapInfos(select.MapInfos, entity);
            //连接
            LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
            //条件
            if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.Zero)
            {
                entity.StrSqlValue.Append($" {DBMDConst.Where}");
                LinkConditions(select.Conditions, select.TreeConditions, entity);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="select"></param>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void GetCount(ContextSelect select, QueryEntity entity)
        {
            if (Check.IsNull(entity))
            {
                entity = new QueryEntity();
            }
            if (Check.IsNull(select))
            {
                throw new ArgumentNullException(nameof(select));
            }

            //视图
            entity.StrSqlValue.Append($"{DBMDConst.Select} {DBMDConst.Count}" +
                $"{DBMDConst.LeftBracket}{DBMDConst.Asterisk}{DBMDConst.RightBracket} " +
                $"{DBMDConst.As} {DBMDConst.SimpleNumber}");

            //连接
            LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
            //条件
            if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.Zero)
            {
                entity.StrSqlValue.Append($" {DBMDConst.Where}");
                LinkConditions(select.Conditions, select.TreeConditions, entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDate"></typeparam>
        /// <param name="entity"></param>
        public virtual SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions)
        {

            SqlCommandEntity sqlCommand = new SqlCommandEntity();
            sqlCommand.StrSqlValue.Append($"{DBMDConst.Delete}  {DBMDConst.From}" +
                $" {DBMDConst.UnSingleQuotes}{type.GetTableName()}{DBMDConst.UnSingleQuotes} ");
            if (Check.IsNull(treeConditions) || treeConditions.Count.Equals(CommonConst.Zero))
            {
                throw new Exception(ErrorType.DeleteNotMatch.GetErrorInfo());
            }
            sqlCommand.StrSqlValue.Append($" {DBMDConst.Where} ");
            LinkConditions(conditions, treeConditions, sqlCommand);
            sqlCommand.StrSqlValue.Append(DBMDConst.Semicolon);
            sqlCommand.DbCommandType = eDbCommandType.Delete;
            return sqlCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        public virtual SqlCommandEntity GetDelete<TData>(TData data, int random)
        {
            Type type = typeof(TData);
            var PropKey = type.GetKey();
            if (Check.IsNull(PropKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            SqlCommandEntity sqlCommand = new SqlCommandEntity();
            var key = $"{DBMDConst.AT}{PropKey.GetColName()}{random}";
            sqlCommand.StrSqlValue.Append($"{DBMDConst.Delete} {DBMDConst.From} " +
                $"{DBMDConst.UnSingleQuotes}{type.GetTableName()}{DBMDConst.UnSingleQuotes} " +
                $"{DBMDConst.Where} {DBMDConst.UnSingleQuotes}{PropKey.GetColName()}{DBMDConst.UnSingleQuotes}{DBMDConst.Equal}{key}");
            sqlCommand.AddParameter(DbType, key, PropKey.GetValue(data));
            sqlCommand.StrSqlValue.Append(DBMDConst.Semicolon);
            sqlCommand.DbCommandType = eDbCommandType.Delete;
            return sqlCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public virtual SqlCommandEntity GetDelete(string sql, Dictionary<string, object> Params)
        {
            SqlCommandEntity sqlCommand = new SqlCommandEntity(sql);
            if (!Check.IsNull(Params))
            {
                foreach (var item in Params)
                {
                    sqlCommand.AddParameter(DbType, item.Key, item.Value);
                }
            }
            return sqlCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos"></param>
        /// <param name="sbValue"></param>
        /// <exception cref="ArgumentException"></exception>
        protected virtual void LinkMapInfos(List<MapEntity> mapInfos, QueryEntity sqlEntity)
        {
            if (Check.IsNull(sqlEntity))
            {
                throw new ArgumentException("");
            }
            int index = 0;
            if (!Check.IsNull(mapInfos))
            {
                foreach (var map in mapInfos.Where(m => m.IsNeed))
                {
                    if (index > 0)
                    {
                        sqlEntity.StrSqlValue.Append(DBMDConst.Comma);
                    }
                    map.CheckPropertyName();
                    map.SetAsColumnName(index);

                    if (Check.IsNullOrEmpty(map.Methods))
                    {
                        sqlEntity.StrSqlValue.Append($" {DBMDConst.UnSingleQuotes}{map.TableName}{DBMDConst.UnSingleQuotes}{DBMDConst.Dot}{DBMDConst.UnSingleQuotes}{map.ColumnName}{DBMDConst.UnSingleQuotes} {DBMDConst.As} {map.AsColumnName}");
                    }
                    else
                    {
                        SetName(map.Methods, sqlEntity, index);
                        sqlEntity.StrSqlValue.Append($" {MapMethod(map.Methods, sqlEntity)} {DBMDConst.As} {map.AsColumnName} ");
                    }
                    index++;
                }

            }
            sqlEntity.StrSqlValue.Append("\n");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="joinInfos"></param>
        /// <param name="sbValue"></param>
        /// <exception cref="ArgumentException"></exception>
        protected virtual void LinkJoinInfos(JoinTableEntity[] joinInfos, QueryEntity sqlEntity)
        {
            if (Check.IsNull(sqlEntity))
            {
                throw new ArgumentException("");
            }
            if (!Check.IsNullOrEmpty(joinInfos))
            {
                foreach (var join in joinInfos)
                {
                    if (join.TableType.Equals(eTableType.Master))
                    {
                        sqlEntity.AddLineFeed();
                        sqlEntity.AddHorizontalTAB();
                        sqlEntity.StrSqlValue.Append($" {DBMDConst.From} {DBMDConst.UnSingleQuotes}{join.DisplayName}{DBMDConst.UnSingleQuotes} ");
                    }
                    else
                    {
                        sqlEntity.StrSqlValue.Append($" {MysqlConst.StrJoins[(int)join.JoinType]}" +
                            $"{DBMDConst.UnSingleQuotes}{join.DisplayName}{DBMDConst.UnSingleQuotes} {DBMDConst.As} {join.AsName} {DBMDConst.On} ");
                        LinkConditions(join.Conditions, join.TreeConditions, sqlEntity);
                    }
                }


            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="treeConditions"></param>
        /// <param name="sqlEntity"></param>
        /// <exception cref="Exception"></exception>
        protected virtual void LinkConditions(List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions, SqlBase sqlEntity)
        {
            if (Check.IsNullOrEmpty(treeConditions))
            {
                return;
            }
            if (Check.IsNull(conditions))
            {
                return;
            }
            if (conditions.Count != treeConditions.Count - 1)
            {
                throw new Exception("sql 语句条件部分解析有误!");
            }
            StringBuilder StrValue = new StringBuilder();
            for (int i = 0; i < treeConditions.Count(); i++)
            {
                AddBrackets(StrValue, treeConditions[i].LeftBracket);
                string leftValue = string.Empty;
                string rightValue = string.Empty;
                SetName(treeConditions[i].LeftCondition, sqlEntity, i);
                SetName(treeConditions[i].RightCondition, sqlEntity, i);
                leftValue = treeConditions[i].LeftCondition.DisplayName;
                if (Check.IsNullOrEmpty(leftValue))
                {
                    leftValue = treeConditions[i].RightCondition.DisplayName;
                }
                else if (!Check.IsNull(treeConditions[i].RightCondition))
                {
                    rightValue = treeConditions[i].RightCondition.DisplayName;
                }

                if (Check.IsNull(treeConditions[i].RelationCondition))
                {

                    if (!Check.IsNullOrEmpty(rightValue) && rightValue.ToLower().Contains(DBMDConst.True))
                    {
                        StrValue.Append($" {DBMDConst.LeftBracket}{CommonConst.One}{DBMDConst.Equal}{CommonConst.One}{DBMDConst.RightBracket} ");
                    }
                    else
                    {
                        if (treeConditions[i].IsNot)
                        {
                            StrValue.Append($" {DBMDConst.LeftBracket}{DBMDConst.ExclamationMark}{leftValue}{DBMDConst.RightBracket} ");
                        }
                        else
                        {
                            StrValue.Append($" {DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ");
                        }
                    }
                }
                else if (treeConditions[i].RelationCondition.ConditionType.Equals(eConditionType.Method))
                {
                    StrValue.Append(MapMethod(treeConditions[i].LeftCondition.Methods));
                }
                else
                {
                    MapMethod(treeConditions[i], leftValue, rightValue);
                    StrValue.Append($"{leftValue}{MysqlConst.cStrSign[(int)treeConditions[i].RelationCondition.SignType]}{rightValue}");
                }
                AddBrackets(StrValue, treeConditions[i].RightBracket);
                if (conditions.Count > i)
                {
                    AddBrackets(StrValue, conditions[i].SignType);
                }
            }

            sqlEntity.StrSqlValue.Append(StrValue.ToString());
        }
        /// <summary>
        /// 拼接分页
        /// </summary>
        /// <param name="sqlEntity"></param>
        protected virtual void SetPageList(QueryEntity sqlEntity)
        {
            if (IsPage(sqlEntity))
            {
                sqlEntity.StrSqlValue.Append($" {DBMDConst.Limit} {DBMDConst.AT}{DBMDConst.SkipNumber}{DBMDConst.Comma}{DBMDConst.AT}{DBMDConst.TakeNumber}");
                sqlEntity.AddParameter(DbType, $"{DBMDConst.AT}{DBMDConst.SkipNumber}", (sqlEntity.PageNumber - 1) * sqlEntity.PageSize);
                sqlEntity.AddParameter(DbType, $"{DBMDConst.AT}{DBMDConst.TakeNumber}", sqlEntity.PageSize);
            }
        }
        protected virtual bool IsPage(QueryEntity sqlEntity)
        {
            if (sqlEntity.PageSize > 0 && sqlEntity.PageNumber < 1)
            {
                sqlEntity.PageNumber = 1;
            }
            if (sqlEntity.PageNumber > 1 && sqlEntity.PageSize <= 0)
            {
                sqlEntity.PageSize = 50;
            }
            if (sqlEntity.PageNumber < 0 || sqlEntity.PageSize <= 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append($" {DBMDConst.Order} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(), OrderByInfos.Where(o => o.IsOrderBy).OrderBy(o => o.OrderSoft).Select(o => $"{o.TableName}.{o.ColumnName} {AscendOrDescend(o.OrderType)}")));
                entity.StrSqlValue.Append(" ");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void GroupBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(g => g.IsGroupBy).Any())
            {
                entity.StrSqlValue.Append($" {DBMDConst.Group} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(), OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="keyName"></param>
        /// <param name="tableName"></param>
        /// <param name="pKey"></param>
        /// <param name="data"></param>
        /// <param name="Props"></param>
        /// <param name="index"></param>
        protected virtual void Update<TEntity>(SqlCommandEntity sql, string keyName, string tableName, PropertyInfo pKey, TEntity data, IEnumerable<PropertyInfo> Props, int index)
        {
            string ParamName = GetParameterName(index, keyName);
            sql.AddParameter(DbType, ParamName, pKey.GetValue(data));
            sql.StrSqlValue.Append($"{DBMDConst.Update} {DBMDConst.UnSingleQuotes}{tableName}{DBMDConst.UnSingleQuotes} {DBMDConst.Set} ");
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(),
            Props.Select(p =>
            {
                string colName = $"{p.GetColName()}";
                sql.AddParameter(DbType, $"{GetParameterName(index, colName)}", p.GetValue(data));
                return $"{DBMDConst.UnSingleQuotes}{colName}{DBMDConst.UnSingleQuotes}{DBMDConst.Equal}{GetParameterName(index, colName)}";
            })));
            sql.StrSqlValue.Append($" {DBMDConst.Where} ");
            sql.StrSqlValue.Append($"{pKey.GetColName()}{DBMDConst.Equal}{ParamName}");
            sql.StrSqlValue.Append(DBMDConst.Semicolon);
        }
        /// <summary>
        /// 返回conditon name 或者 返回@参数
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="Index"></param>
        /// <param name=""></param>
        /// <returns></returns>
        protected string SetCondition(ConditionEntity condition, SqlBase sql, int Index, bool IsParams = false)
        {
            string value = null;
            if (Check.IsNull(condition))
            {
                return value;
            }
            switch (condition.ConditionType)
            {
                case eConditionType.Sign:
                    break;
                case eConditionType.Method:
                    break;
                case eConditionType.ColumnName:
                    value = condition.DisplayName;
                    break;
                case eConditionType.Constant:
                    if (!Check.IsNull(condition.Value))
                    {
                        if (IsParams)
                        {
                            value = GetRandomParaName(Index);
                            sql.AddParameter(DbType, value, condition.DisplayName);
                            condition.DisplayName = value;
                        }
                        else
                        {
                            GetConditionValue(condition, sql, value, Index);
                            value = condition.DisplayName;
                        }
                    }
                    break;
                case eConditionType.IgnoreSign:
                    break;
                default:
                    break;
            }
            return value;
        }
        protected bool IsGroup(ContextSelect entity)
        {
            if (!Check.IsNull(entity) && !Check.IsNull(entity.OrderInfos) && entity.OrderInfos.Any(u => u.IsGroupBy))
            {
                return true;
            }
            return false;
        }
        protected bool IsOrder(ContextSelect entity)
        {
            if (!Check.IsNull(entity) && !Check.IsNull(entity.OrderInfos) && entity.OrderInfos.Any(u => u.IsOrderBy))
            {
                return true;
            }
            return false;
        }
        protected virtual void GetConditionValue(ConditionEntity condition, SqlBase entity, string key, int Index)
        {
            StringBuilder sValue = new StringBuilder();

            if (Check.IsNull(condition))
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (Check.IsNull(condition.Value))
            {
                return;
            }
            condition.DataType = CommonConst.GetType(condition.Value.GetType());
            switch (condition.DataType)
            {
                case eDataType.SimpleTime:
                case eDataType.SimpleGuid:
                case eDataType.SimpleString:
                case eDataType.SimpleTimeSpan:
                    sValue.Append($"{ReplaceSingleQuotes(condition.Value.ToString())}");
                    break;
                case eDataType.SimpleInt:
                case eDataType.SimpleFloat:
                case eDataType.SimpleDouble:
                case eDataType.SimpleDecimal:
                    sValue.Append(condition.Value);
                    break;
                case eDataType.SimpleArrayString:
                case eDataType.SimpleListString:
                case eDataType.SimpleArrayGuid:
                case eDataType.SimpleListGuid:
                case eDataType.SimpleListTime:
                case eDataType.SimpleListTimeSpan:
                    foreach (var item in (dynamic)condition.Value)
                    {
                        if (item.ToString() is string strValue)
                        {
                            if (strValue.Contains(DBMDConst.SingleQuotes))
                            {
                                sValue.Append($"{DBMDConst.SingleQuotes}{ReplaceSingleQuotes(strValue)}{DBMDConst.SingleQuotes}{DBMDConst.Comma}");
                            }
                            else
                            {
                                sValue.Append($"{DBMDConst.SingleQuotes}{strValue}{DBMDConst.SingleQuotes}{DBMDConst.Comma}");
                            }
                        }
                    }
                    sValue.Remove(sValue.Length - 1, 1);
                    break;
                case eDataType.SimpleArrayInt:
                case eDataType.SimpleArrayDouble:
                case eDataType.SimpleArrayFloat:
                case eDataType.SimpleArrayDecimal:
                case eDataType.SimpleListInt:
                case eDataType.SimpleListFloat:
                case eDataType.SimpleListDouble:
                case eDataType.SimpleListDecimal:
                    foreach (var item in (dynamic)condition.Value)
                    {
                        sValue.Append($"{item}{DBMDConst.Comma}");
                    }
                    sValue.Remove(sValue.Length - 1, 1);
                    break;
                default:
                    break;
            }
            condition.DisplayName = sValue.ToString();
        }

        protected string ReplaceSingleQuotes(string value)
        {
            return value.Replace("\'", "\'\'");
        }

        public void MapMethod(TreeConditionEntity treeCondition, string left, string right)
        {
            if (!Check.IsNullOrEmpty(treeCondition.LeftCondition.methods))
            {
                left = MapMethod(treeCondition.LeftCondition.Methods);
            }
            if (!Check.IsNullOrEmpty(treeCondition.RightCondition.methods))
            {
                left = MapMethod(treeCondition.RightCondition.Methods);
            }
        }
        protected virtual string MapMethod(List<MethodEntity> methods, SqlBase sql = null)
        {
            StringBuilder sbValue = new StringBuilder();// DBMDConst.Equal.ToString();
            foreach (var m in methods)
            {
                if (m.MethodType == eMethodType.DataBase)
                {
                    if (MysqlConst.dicMethods.ContainsKey(m.Name))
                    {
                        if (!Check.IsNullOrEmpty(m.TreeConditions) && !Check.IsNull(sql))
                        {
                            QueryEntity qsql = new QueryEntity();
                            LinkConditions(m.Conditions, m.TreeConditions, sql);
                            if (!Check.IsNullOrEmpty(qsql.DbParams))
                            {
                                sql.DbParams.AddRange(qsql.DbParams);
                            }
                            m.Extensions = qsql.StrSqlValue.ToString();
                        }
                        sbValue.Append(MysqlConst.dicMethods[m.Name].Invoke(m));
                    }
                }
            }
            return sbValue.ToString();
        }
        protected string GetRandomParaName(int index, int length = 8)
        {
            return $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), length)}{index}";
        }
        protected string GetRandomName(int index, int length = 8)
        {
            return $"{MD5Encrypt.Encrypt(DateTime.Now.ToString(), length)}{index}";
        }

        protected string GetParameterName(int index, string name)
        {
            return $"{DBMDConst.AT}{name}{DBMDConst.DownLine}{index}";
        }
        protected string AscendOrDescend(eOrderType OrderType)
        {
            string value = string.Empty;
            switch (OrderType)
            {
                case eOrderType.Ascending:
                    value = DBMDConst.Ascending;
                    break;
                case eOrderType.Descending:
                    value = DBMDConst.Descending;
                    break;
                default:
                    break;
            }
            return value;
        }

        protected void AddBrackets(StringBuilder StrValue, List<eSignType> signTypes)
        {
            AddBrackets(StrValue, signTypes.ToArray());
        }
        protected void AddBrackets(StringBuilder StrValue, params eSignType[] signTypes)
        {
            foreach (var sign in signTypes)
            {
                StrValue.Append(MysqlConst.cStrSign[(int)sign]);
            }
        }

        protected void SetName(ConditionEntity condition, SqlBase sqlEntity, int i)
        {
            if (!Check.IsNull(condition))
            {
                if (Check.IsNullOrEmpty(condition.Methods))
                {
                    SetCondition(condition, sqlEntity, i,true);
                }
                else
                {
                    SetName(condition.Methods, sqlEntity, i);
                }
            }
        }
        protected void SetName(IEnumerable<MethodEntity> methods, SqlBase sqlEntity, int i)
        {

            foreach (var method in methods)
            {
                foreach (var Params in method.Parameters)
                {
                    bool IsParams = true;
                    switch (method.Name)
                    {
                        case MethodConst._Contains:
                        case MethodConst._RightContains:
                        case MethodConst._LeftContains:
                            IsParams = false;
                            break;
                        default:
                            break;
                    }
                    SetCondition(Params, sqlEntity,i,IsParams);
                }
            }

        }


    }
}
