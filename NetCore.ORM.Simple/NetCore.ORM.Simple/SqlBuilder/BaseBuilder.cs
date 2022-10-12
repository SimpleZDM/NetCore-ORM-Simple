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
        private const char charConnectSign = '_';
        protected eDBType DbType;
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

            sql.StrSqlValue.Append($"INSERT INTO `{type.GetClassName()}` ");
            sql.StrSqlValue.Append("(");
            sql.StrSqlValue.Append(string.Join(',', Props.Select(p => $"`{p.GetColName()}`")));
            sql.StrSqlValue.Append(") ");
            sql.StrSqlValue.Append(" VALUE(");
            sql.StrSqlValue.Append(string.Join(',',
                Props.Select(p =>
                {
                    string key = $"@{random}{p.GetColName()}";

                    sql.AddParameter(DbType, key, p.GetValue(data));
                    return key;
                })));
            sql.StrSqlValue.Append(");");

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
            var Props = type.GetNotKeyAndIgnore();
            var pKey = type.GetKey();
            if (Check.IsNull(pKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            string keyName = $"@{pKey.GetColName()}";
            Update(sql, keyName, type.GetClassName(), pKey, data, Props, random);
            sql.DbCommandType = eDbCommandType.Update;
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
            var Props = type.GetNotKeyAndIgnore();
            var pKey = type.GetKey();
            string tableName = type.GetClassName();
            int Index = offset;
            if (Check.IsNull(pKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            string keyName = $"@{pKey.GetColName()}";
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
                    sql.StrSqlValue.Append($"INSERT INTO `{type.GetClassName()}` ");
                    sql.StrSqlValue.Append("(");
                    sql.StrSqlValue.Append(string.Join(',', Props.Select(p => $"`{p.GetColName()}`")));
                    sql.StrSqlValue.Append(") ");
                    sql.StrSqlValue.Append(" VALUE");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(" (");
                sql.StrSqlValue.Append(string.Join(',',
                  Props.Select(p =>
                  {
                      string key = $"@{Index + offset}{charConnectSign}{p.GetColName()}";
                      sql.AddParameter(DbType, key, p.GetValue(data));
                      return key;
                  })));
                sql.StrSqlValue.Append(" )");
                if (count == MysqlConst.INSERTMAX)
                {
                    sql.StrSqlValue.Append(";");
                    count = 0;
                }
                else
                {
                    if (Index == datas.Count())
                    {
                        sql.StrSqlValue.Append(";");
                    }
                    else
                    {
                        sql.StrSqlValue.Append(",");
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
            sql.StrSqlValue.Append($"SELECT " +
                $"{string.Join(',', type.GetNoIgnore())} " +
                $"FROM {type.GetClassName()} ");
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
            sql.StrSqlValue.Append($"SELECT " +
                $"{string.Join(',', type.GetNoIgnore().Select(p => p.GetColName()))} " +
                $"FROM {type.GetClassName()} ");
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
            sql.StrSqlValue.Append($" Where {Key.GetColName()}=LAST_INSERT_ID();");
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
            if (Check.IsNull(select.MapInfos))
            {
                select.MapInfos = new List<MapEntity>();
            }
            if (select.MapInfos.Count.Equals(CommonConst.ZeroOrNull))
            {
                Type type = typeof(TData);
                string TableName = type.GetClassName();
                foreach (var prop in type.GetNoIgnore())
                {
                    select.MapInfos.Add(new MapEntity()
                    {
                        PropName = prop.GetColName(),
                        ColumnName = prop.GetColName(),
                        TableName = TableName,
                    });
                }
            }
            //视图
            entity.StrSqlValue.Append("SELECT ");

            entity.MapInfos = select.MapInfos.ToArray();

            LinkMapInfos(select.MapInfos, entity);

            //连接
            LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
            //条件
            if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
            {
                entity.StrSqlValue.Append(" where");
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
            entity.StrSqlValue.Append("SELECT COUNT(*) As Number");

            //连接
            LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
            //条件
            if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
            {
                entity.StrSqlValue.Append(" where");
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
            sqlCommand.StrSqlValue.Append($"DELETE  FROM `{type.GetClassName()}` ");
            if (Check.IsNull(treeConditions) || treeConditions.Count.Equals(CommonConst.ZeroOrNull))
            {
                throw new Exception(ErrorType.DeleteNotMatch.GetErrorInfo());
            }
            sqlCommand.StrSqlValue.Append(" Where ");
            LinkConditions(conditions, treeConditions, sqlCommand);
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
            var key = $"@{PropKey.GetColName()}{random}";
            sqlCommand.StrSqlValue.Append($"DELETE FROM `{type.GetClassName()}` WHERE `{PropKey.GetColName()}`={key}");
            sqlCommand.AddParameter(DbType, key, PropKey.GetValue(data));
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
                            sqlEntity.StrSqlValue.Append(",");
                        }
                        if (Check.IsNullOrEmpty(mapInfos[i].TableName) && Check.IsNullOrEmpty(mapInfos[i].ColumnName))
                        {
                            mapInfos[i].AsColumnName = $"{mapInfos[i].PropName}";
                        }
                        else
                        {
                            mapInfos[i].AsColumnName = $"{mapInfos[i].TableName}{charConnectSign}{mapInfos[i].ColumnName}";
                        }
                        if (Check.IsNullOrEmpty(mapInfos[i].MethodName))
                        {
                            sqlEntity.StrSqlValue.Append($" { mapInfos[i].TableName}.{mapInfos[i].ColumnName} AS {mapInfos[i].AsColumnName} ");
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


                            sqlEntity.StrSqlValue.Append($" {vaule} AS {mapInfos[i].AsColumnName} ");
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
                        sqlEntity.StrSqlValue.Append($" FROM {join.DisplayName} ");
                    }
                    else
                    {
                        sqlEntity.StrSqlValue.Append($" {MysqlConst.StrJoins[(int)join.JoinType]} {join.DisplayName} ON ");
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

                switch (treeConditions[i].LeftCondition.ConditionType)
                {
                    case eConditionType.ColumnName:
                        leftValue = $" {treeConditions[i].LeftCondition.DisplayName} ";
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
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
                            leftValue = $"{treeConditions[i].LeftCondition.DisplayName}";
                            // sqlEntity.DbParams.Add(new MySqlParameter(leftValue, treeConditions[i].LeftCondition.DisplayName));
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            leftValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, leftValue, treeConditions[i].LeftCondition.DisplayName);

                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName} ";
                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].LeftCondition.DisplayName);
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
                    StrValue.Append(MysqlConst.MapMethod(treeConditions[i].RelationCondition.DisplayName, leftValue, rightValue));
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
                switch (treeConditions[i].LeftCondition.ConditionType)
                {
                    case eConditionType.ColumnName:
                        leftValue = $" {treeConditions[i].LeftCondition.DisplayName} ";
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
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
                            leftValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, leftValue, treeConditions[i].LeftCondition.DisplayName);

                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName}";
                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].LeftCondition.DisplayName);
                        }
                        break;
                    default:
                        break;
                }
                if (treeConditions[i].RelationCondition.ConditionType.Equals(eConditionType.Method))
                {
                    sqlEntity.StrSqlValue.Append(MysqlConst.MapMethod(treeConditions[i].RelationCondition.DisplayName, leftValue, rightValue));
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

            if (sqlEntity.PageNumber < 0 || sqlEntity.PageSize <= 0)
            {
                return;
            }
            sqlEntity.StrSqlValue.Append(" Limit @SkipNumber,@TakeNumber");
            sqlEntity.AddParameter(DbType, "@SkipNumber", (sqlEntity.PageNumber - 1) * sqlEntity.PageSize);
            sqlEntity.AddParameter(DbType, "@TakeNumber", sqlEntity.PageSize);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
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
        protected virtual void GroupBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(g => g.IsGroupBy).Any())
            {
                entity.StrSqlValue.Append(" Group By ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected virtual void Update<TEntity>(SqlCommandEntity sql, string keyName, string tableName, PropertyInfo pKey, TEntity data, IEnumerable<PropertyInfo> Props, int index)
        {
            sql.AddParameter(DbType, $"{keyName}{index}", pKey.GetValue(data));
            sql.StrSqlValue.Append($"UPDATE `{tableName}` SET ");
            sql.StrSqlValue.Append(string.Join(',',
            Props.Select(p =>
            {
                string colName = $"{p.GetColName()}";
                sql.AddParameter(DbType, $"@{colName}{index}", p.GetValue(data));
                return $"`{colName}`=@{colName}{index}";
            })));
            sql.StrSqlValue.Append(" Where ");
            sql.StrSqlValue.Append($"{pKey.GetColName()}={keyName}{index}");
            sql.StrSqlValue.Append(";");
        }
    }
}
