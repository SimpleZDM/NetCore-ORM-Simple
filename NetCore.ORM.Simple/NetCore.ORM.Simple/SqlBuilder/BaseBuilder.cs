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
 * 接口名称 BaseBuilder
 * 开发人员：-nhy
 * 创建时间：2022/10/11 15:14:12
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public class BaseBuilder
    {
        protected const char charConnectSign = '_';
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
            var Props = GetNotKeyAndIgnore(type).ToArray();

            sql.StrSqlValue.Append($"{DBMDConst.Insert}{DBMDConst.Into} {DBMDConst.UnSingleQuotes}{GetTableName(type)}{DBMDConst.UnSingleQuotes} ");
            sql.StrSqlValue.Append(DBMDConst.LeftBracket);
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma, Props.Select(p => $"{DBMDConst.UnSingleQuotes}{GetColName(p)}{DBMDConst.UnSingleQuotes}")));
            sql.StrSqlValue.Append(DBMDConst.RightBracket);
            sql.StrSqlValue.Append($" {MainWordType.Value.GetMainWordStr()}{DBMDConst.LeftBracket}");
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma,
                Props.Select(p =>
                {
                    string key = $"{DBMDConst.AT}{random}{GetColName(p)}";

                    sql.AddParameter(DbType, key, p.GetValue(data));
                    return key;
                })));
            sql.StrSqlValue.Append($"{DBMDConst.RightBracket}{DBMDConst.Semicolon}");

            sql.DbCommandType = eDbCommandType.Insert;
            return sql;
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
            var Props = GetNotKeyAndIgnore(type);
            var pKey = GetKey(type);
            if (Check.IsNull(pKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            string keyName = $"{DBMDConst.AT}{GetColName(pKey)}";
            Update(sql, keyName, GetTableName(type), pKey, data, Props, random);
            sql.DbCommandType = eDbCommandType.Update;
            sql.StrSqlValue.Append(DBMDConst.Semicolon);
            return sql;
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
            var Props = GetNotKeyAndIgnore(type);
            var pKey = GetKey(type);
            string tableName = GetTableName(type);
            int Index = offset;
            if (Check.IsNull(pKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            string keyName = $"{DBMDConst.AT}{GetColName(pKey)}";
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
                    sql.StrSqlValue.Append($" {DBMDConst.Value}");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                sql.StrSqlValue.Append(string.Join(',',
                  Props.Select(p =>
                  {
                      string key = $"{DBMDConst.AT}{GetColName(p)}{DBMDConst.DownLine}{Index + offset}";
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
                $"{string.Join(DBMDConst.Comma, GetNoIgnore(type))} " +
                $"{DBMDConst.From} {GetTableName(type)} ");
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
                $"{string.Join(DBMDConst.Comma, GetNoIgnore(type).Select(p => GetColName(p)))} " +
                $"{DBMDConst.From} {GetTableName(type)} ");
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
            var Key = GetKey(type);
            sql.StrSqlValue.Append($" {DBMDConst.Where} {GetColName(Key)}{DBMDConst.Equal}{DBMDConst.LAST_INSERT_ID}{DBMDConst.Semicolon}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
        public virtual void GetSelect<TData>(SelectEntity select, QueryEntity entity)
        {
            if (Check.IsNull(entity))
            {
                entity = new QueryEntity();
            }
            if (Check.IsNull(select))
            {
                throw new ArgumentNullException(nameof(select));
            }

            if (select.MapInfos.Count.Equals(CommonConst.ZeroOrNull))
            {
                Type type = typeof(TData);
                string TableName = GetTableName(type);
                foreach (var prop in GetNoIgnore(type))
                {
                    select.MapInfos.Add(new MapEntity()
                    {
                        PropName = GetColName(prop),
                        ColumnName = GetColName(prop),
                        TableName = TableName,
                    });
                }
            }
            //视图
            entity.StrSqlValue.Append($"{DBMDConst.Select} ");

            entity.MapInfos = select.MapInfos.ToArray();

            LinkMapInfos(select.MapInfos, entity);

            //连接
            LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
            //条件
            if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
            {
                entity.StrSqlValue.Append($" {DBMDConst.Where}");
                LinkConditions(select.Conditions, select.TreeConditions, entity);
            }

        }

        public virtual void GetCount(SelectEntity select, QueryEntity entity)
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
            entity.StrSqlValue.Append($"{DBMDConst.Select} {DBMDConst.Count}{DBMDConst.LeftBracket}{DBMDConst.Asterisk}{DBMDConst.RightBracket} {DBMDConst.As} {DBMDConst.SimpleNumber}");

            //连接
            LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
            //条件
            if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
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
            sqlCommand.StrSqlValue.Append($"{DBMDConst.Delete}  {DBMDConst.From} {DBMDConst.UnSingleQuotes}{GetTableName(type)}{DBMDConst.UnSingleQuotes} ");
            if (Check.IsNull(treeConditions) || treeConditions.Count.Equals(CommonConst.ZeroOrNull))
            {
                throw new Exception(ErrorType.DeleteNotMatch.GetErrorInfo());
            }
            sqlCommand.StrSqlValue.Append($" {DBMDConst.Where} ");
            LinkConditions(conditions, treeConditions, sqlCommand);
            sqlCommand.StrSqlValue.Append(DBMDConst.Semicolon);
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
            var PropKey = GetKey(type);
            if (Check.IsNull(PropKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            SqlCommandEntity sqlCommand = new SqlCommandEntity();
            var key = $"{DBMDConst.AT}{GetColName(PropKey)}{random}";
            sqlCommand.StrSqlValue.Append($"{DBMDConst.Delete} {DBMDConst.From} `{GetTableName(type)}` {DBMDConst.Where} {DBMDConst.UnSingleQuotes}{GetColName(PropKey)}{DBMDConst.UnSingleQuotes}{DBMDConst.Equal}{key}");
            sqlCommand.AddParameter(DbType, key, PropKey.GetValue(data));
            sqlCommand.StrSqlValue.Append(DBMDConst.Semicolon);
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
            bool IsFirst = true;
            if (!Check.IsNull(mapInfos))
            {
                for (int i = 0; i < mapInfos.Count; i++)
                {
                    if (mapInfos[i].IsNeed)
                    {
                        if (IsFirst)
                        {
                            IsFirst = false;
                        }
                        else
                        {
                            sqlEntity.StrSqlValue.Append(DBMDConst.Comma);
                        }
                        if (Check.IsNullOrEmpty(mapInfos[i].TableName) && Check.IsNullOrEmpty(mapInfos[i].ColumnName))
                        {
                            mapInfos[i].AsColumnName = $"{mapInfos[i].PropName}";
                        }
                        else
                        {
                            mapInfos[i].AsColumnName = $"{mapInfos[i].TableName}{DBMDConst.DownLine}{mapInfos[i].ColumnName}";
                        }
                        if (Check.IsNullOrEmpty(mapInfos[i].MethodName))
                        {
                            sqlEntity.StrSqlValue.Append($" { mapInfos[i].TableName}.{mapInfos[i].ColumnName} {DBMDConst.As} {mapInfos[i].AsColumnName} ");
                        }
                        else
                        {
                            string vaule = string.Empty;
                            if (Check.IsNullOrEmpty(mapInfos[i].TableName) && Check.IsNullOrEmpty(mapInfos[i].ColumnName))
                            {
                                vaule = MysqlConst.MapMethod(mapInfos[i].MethodName, String.Empty, string.Empty);
                            }
                            else
                            {
                                vaule = MysqlConst.MapMethod(mapInfos[i].MethodName, $"{mapInfos[i].TableName}.{mapInfos[i].ColumnName}", string.Empty);
                            }


                            sqlEntity.StrSqlValue.Append($" {vaule} {DBMDConst.As} {mapInfos[i].AsColumnName} ");
                        }


                    }
                }
            }

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
            if (!Check.IsNull(joinInfos))
            {
                foreach (var join in joinInfos)
                {
                    if (join.TableType.Equals(eTableType.Master))
                    {
                        sqlEntity.StrSqlValue.Append($" {DBMDConst.From} {join.DisplayName} ");
                    }
                    else
                    {
                        sqlEntity.StrSqlValue.Append($" {MysqlConst.StrJoins[(int)join.JoinType]} {join.DisplayName} {DBMDConst.On} ");
                        LinkConditions(join.Conditions, join.TreeConditions, sqlEntity);
                    }
                }
            }
        }

        protected virtual void LinkConditions(List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions, QueryEntity sqlEntity)
        {
            if (Check.IsNull(treeConditions))
            {
                return;
            }
            if (Check.IsNull(conditions))
            {
                return;

            }
            if (treeConditions.Count > 0 && conditions.Count != treeConditions.Count - 1)
            {
                throw new Exception("sql 语句条件部分解析有误!");
            }
            StringBuilder StrValue = new StringBuilder();
            for (int i = 0; i < treeConditions.Count(); i++)
            {
                foreach (var sign in treeConditions[i].LeftBracket)
                {
                    StrValue.Append(MysqlConst.cStrSign[(int)sign]);
                }

                string leftValue = string.Empty;
                string rightValue = string.Empty;
                ConditionEntity currentConditon = null;
                switch (treeConditions[i].LeftCondition.ConditionType)
                {
                    case eConditionType.ColumnName:
                        leftValue = $" {treeConditions[i].LeftCondition.DisplayName} ";
                        if (Check.IsNull(treeConditions[i].RightCondition))
                        {
                            break;
                        }
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            GetConditionValue(treeConditions[i].RightCondition, sqlEntity, rightValue);
                            currentConditon = treeConditions[i].RightCondition;
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            //非常量
                            rightValue = $" {treeConditions[i].RightCondition.DisplayName}";
                        }
                        break;
                    case eConditionType.Constant:
                        if (Check.IsNull(treeConditions[i].RelationCondition))
                        {
                            if (treeConditions[i].IsNot)
                            {
                                leftValue = $"{DBMDConst.Not} {treeConditions[i].LeftCondition.DisplayName}";
                            }
                            else
                            {
                                leftValue = $"{treeConditions[i].LeftCondition.DisplayName}";
                            }
                            // sqlEntity.DbParams.Add(new MySqlParameter(leftValue, treeConditions[i].LeftCondition.DisplayName));
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            leftValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, leftValue, treeConditions[i].LeftCondition.DisplayName);

                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            GetConditionValue(treeConditions[i].LeftCondition, sqlEntity, rightValue);
                            currentConditon = treeConditions[i].LeftCondition;
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName} ";
                        }
                        break;
                    default:
                        break;
                }
                if (Check.IsNull(treeConditions[i].RelationCondition))
                {
                    StrValue.Append($" ({leftValue}) ");
                }
                else if (treeConditions[i].RelationCondition.ConditionType.Equals(eConditionType.Method))
                {
                    StrValue.Append(MapMethod(treeConditions[i].RelationCondition.DisplayName, leftValue, rightValue, currentConditon, treeConditions[i].IsNot));
                }
                else
                {
                    StrValue.Append($"{leftValue}{MysqlConst.cStrSign[(int)treeConditions[i].RelationCondition.SignType]}{rightValue}");
                }
                foreach (var sign in treeConditions[i].RightBracket)
                {
                    StrValue.Append(MysqlConst.cStrSign[(int)sign]);
                }

                if (conditions.Count > i)
                {
                    StrValue.Append(MysqlConst.cStrSign[(int)conditions[i].SignType]);

                }

            }
            sqlEntity.StrSqlValue.Append(StrValue.ToString());
        }

        protected virtual void LinkConditions(List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions, SqlCommandEntity sqlEntity)
        {
            if (Check.IsNull(treeConditions))
            {
                return;
            }
            if (Check.IsNull(conditions))
            {
                return;

            }
            if (treeConditions.Count > 0 && conditions.Count != treeConditions.Count - 1)
            {
                throw new Exception(ErrorType.SqlAnalysis.GetErrorInfo());
            }
            for (int i = 0; i < treeConditions.Count(); i++)
            {
                foreach (var sign in treeConditions[i].LeftBracket)
                {
                    sqlEntity.StrSqlValue.Append(MysqlConst.cStrSign[(int)sign]);
                }

                string leftValue = string.Empty;
                string rightValue = string.Empty;
                ConditionEntity currentConditon = null;
                switch (treeConditions[i].LeftCondition.ConditionType)
                {
                    case eConditionType.ColumnName:
                        leftValue = $" {treeConditions[i].LeftCondition.DisplayName} ";
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            GetConditionValue(treeConditions[i].RightCondition, sqlEntity, rightValue);
                            currentConditon = treeConditions[i].RightCondition;
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            //非常量
                            rightValue = $"{treeConditions[i].RightCondition.DisplayName}";
                        }
                        break;
                    case eConditionType.Constant:
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            leftValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, leftValue, treeConditions[i].LeftCondition.DisplayName);

                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName}";
                            GetConditionValue(treeConditions[i].LeftCondition, sqlEntity, rightValue);
                            currentConditon = treeConditions[i].LeftCondition;
                        }
                        break;
                    default:
                        break;
                }
                if (treeConditions[i].RelationCondition.ConditionType.Equals(eConditionType.Method))
                {
                    sqlEntity.StrSqlValue.Append(MapMethod(treeConditions[i].RelationCondition.DisplayName, leftValue, rightValue, currentConditon,treeConditions[i].IsNot));
                }
                else
                {
                    sqlEntity.StrSqlValue.Append($"{leftValue}{MysqlConst.cStrSign[(int)treeConditions[i].RelationCondition.SignType]}{rightValue}");
                }
                foreach (var sign in treeConditions[i].RightBracket)
                {
                    sqlEntity.StrSqlValue.Append(MysqlConst.cStrSign[(int)sign]);
                }

                if (conditions.Count > i)
                {
                    sqlEntity.StrSqlValue.Append(MysqlConst.cStrSign[(int)conditions[i].SignType]);
                }

            }
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
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(o => o.IsOrderBy).OrderBy(o => o.OrderSoft).Select(o => $"{o.TableName}.{o.ColumnName} {MysqlConst.AscendOrDescend(o.OrderType)}")));
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
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected virtual void Update<TEntity>(SqlCommandEntity sql, string keyName, string tableName, PropertyInfo pKey, TEntity data, IEnumerable<PropertyInfo> Props, int index)
        {
            sql.AddParameter(DbType, $"{DBMDConst.AT}{keyName}{DBMDConst.DownLine}{index}", pKey.GetValue(data));
            sql.StrSqlValue.Append($"{DBMDConst.Update} {DBMDConst.UnSingleQuotes}{tableName}{DBMDConst.UnSingleQuotes} {DBMDConst.Set} ");
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma,
            Props.Select(p =>
            {
                string colName = $"{GetColName(p)}";
                sql.AddParameter(DbType, $"{DBMDConst.AT}{colName}{DBMDConst.DownLine}{index}", p.GetValue(data));
                return $"{DBMDConst.UnSingleQuotes}{colName}{DBMDConst.UnSingleQuotes}{DBMDConst.Equal}{DBMDConst.AT}{colName}{DBMDConst.DownLine}{index}";
            })));
            sql.StrSqlValue.Append($" {DBMDConst.Where} ");
            sql.StrSqlValue.Append($"{GetColName(pKey)}{DBMDConst.Equal}{keyName}{DBMDConst.DownLine}{index}");
            sql.StrSqlValue.Append(DBMDConst.Semicolon);
        }

        protected string GetTableName(Type type)
        {
            return type.GetTableName(tableAtrr);
        }
        protected string GetColName(PropertyInfo Prop)
        {
            return Prop.GetColName(columnAttr);
        }

        protected IEnumerable<PropertyInfo> GetNotKeyAndIgnore(Type type)
        {
            return type.GetNotKeyAndIgnore(columnAttr);
        }
        protected IEnumerable<PropertyInfo> GetNoIgnore(Type type)
        {
            return type.GetNoIgnore(columnAttr);
        }
        protected PropertyInfo GetKey(Type type)
        {
            return type.GetKey(columnAttr);
        }
        protected PropertyInfo GetAutoKey(Type type)
        {
            return type.GetAutoKey(columnAttr);
        }

        public virtual void SetAttr(Type Table = null, Type Column = null)
        {
            if (!Check.IsNull(Table))
            {
                tableAtrr = Table;
            }
            if (!Check.IsNull(Column))
            {
                columnAttr = Column;
            }

        }

        protected bool IsGroup(SelectEntity entity)
        {
            if (!Check.IsNull(entity) && !Check.IsNull(entity.OrderInfos) && entity.OrderInfos.Any(u => u.IsGroupBy))
            {
                return true;
            }
            return false;
        }
        protected bool IsOrder(SelectEntity entity)
        {
            if (!Check.IsNull(entity) && !Check.IsNull(entity.OrderInfos) && entity.OrderInfos.Any(u => u.IsOrderBy))
            {
                return true;
            }
            return false;
        }


        protected virtual void GetConditionValue(ConditionEntity condition, SqlBase entity, string key)
        {
            StringBuilder sValue = new StringBuilder();
            if (Check.IsNull(condition))
            {
                throw new ArgumentNullException(nameof(condition));
            }
            if (!Check.IsNullOrEmpty(condition.DisplayName))
            {
                entity.AddParameter(DbType, key, condition.DisplayName);
                return;
            }
            if (Check.IsNull(condition.Value))
            {
                return;
            }
            condition.DataType = CommonConst.GetType(condition.PropertyType);
            switch (condition.DataType)
            {
                case eDataType.SimpleString:
                case eDataType.SimpleInt:
                case eDataType.SimpleGuid:
                case eDataType.SimpleTime:
                case eDataType.SimpleFloat:
                case eDataType.SimpleDouble:
                case eDataType.SimpleDecimal:
                    sValue.Append(condition.Value);

                    break;
                case eDataType.SimpleArrayString:
                case eDataType.SimpleListString:
                case eDataType.SimpleArrayGuid:
                case eDataType.SimpleListGuid:
                    foreach (var item in (dynamic)condition.Value)
                    {
                        sValue.Append($"'{item}',");
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
                        sValue.Append($"{item},");
                    }
                    sValue.Remove(sValue.Length - 1, 1);
                    break;
                default:
                    break;
            }
            condition.DisplayName = sValue.ToString();
        }

        protected virtual string MapMethod(string methodName, string leftValue, string rightValue, ConditionEntity condition, bool IsNot)
        {
            string value = MysqlConst.EqualSign.ToString();
            if (Check.IsNullOrEmpty(methodName))
            {
                return value;
            }
            switch (methodName)
            {
                case "ToString":
                    break;
                case "Equals":
                    if (IsNot)
                    {
                        value = $"{leftValue}<>{rightValue}";
                    }
                    else
                    {
                        value = $"{leftValue}={rightValue}";
                    }
                    break;
                case "IsNullOrEmpty":
                case "IsNull":
                    if (IsNot)
                    {
                        if (!Check.IsNullOrEmpty(leftValue))
                        {
                            value = $"{leftValue} IS NOT NULL";
                        }
                        else if (!Check.IsNullOrEmpty(rightValue))
                        {
                            value = $"{rightValue} IS NOT NULL";
                        }
                    }
                    else {
                        if (!Check.IsNullOrEmpty(leftValue))
                        {
                            value = $"{leftValue} IS  NULL";
                        }
                        else if (!Check.IsNullOrEmpty(rightValue))
                        {
                            value = $"{rightValue} IS  NULL";
                        }
                    }

                    break;
                case "Sum":
                    value = $" SUM({leftValue}) ";
                    break;
                case "Min":
                    value = $" Min({leftValue}) ";
                    break;
                case "Max":
                    value = $" Max({leftValue}) ";
                    break;
                case "Count":
                    var star = "*";
                    leftValue = Check.IsNullOrEmpty(leftValue) ? star : leftValue;
                    value = $" COUNT({leftValue}) ";
                    break;
                case "Average":
                    value = $" AVG({leftValue}) ";
                    break;
                case "FirstOrDefault":
                    value = $" {leftValue}";
                    break;
                case "Contains":
                    if (eDataType.SimpleString == condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} NOT Like '%{condition.DisplayName}%' ";
                        }
                        else
                        {
                            value = $"{leftValue}  Like '%{condition.DisplayName}%' ";

                        }
                    }
                    else if ((int)eDataType.SimpleArrayInt <= (int)condition.DataType
                        && (int)eDataType.SimpleListDecimal >= (int)condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} NOT in ({condition.DisplayName}) ";
                        }
                        else
                        {
                            value = $"{leftValue}  in ({condition.DisplayName}) ";
                        }
                    }
                    break;
                case "LeftContains":
                    if (eDataType.SimpleString == condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} NOT Like '%{condition.DisplayName}' ";
                        }
                        else
                        {
                            value = $"{leftValue} Like '%{condition.DisplayName}' ";
                        }
                       
                    }
                    break;
                case "RightContains":
                    if (eDataType.SimpleString == condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} NOT Like '{condition.DisplayName}%' ";
                        }
                        else
                        {
                            value = $"{leftValue} Like '{condition.DisplayName}%' ";
                        }
                       
                    }
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}
