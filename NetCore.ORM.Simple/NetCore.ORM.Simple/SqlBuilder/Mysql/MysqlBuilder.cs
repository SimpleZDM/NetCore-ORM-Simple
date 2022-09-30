using MySql.Data.MySqlClient;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
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
    public class MysqlBuilder : ISqlBuilder
    {

        private const char charConnectSign = '_';
        public MysqlBuilder()
        {
        }
        /// <summary>
        /// 单条sql语句生成
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        public SqlCommandEntity GetInsert<TData>(TData data, int random = 0)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore().ToArray();

            sql.StrSqlValue.Append($"INSERT INTO {type.GetClassName()} ");
            sql.StrSqlValue.Append("(");
            sql.StrSqlValue.Append(string.Join(',', Props.Select(p => p.GetColName())));
            sql.StrSqlValue.Append(") ");
            sql.StrSqlValue.Append(" VALUE(");
            sql.StrSqlValue.Append(string.Join(',',
                Props.Select(p =>
                {
                    string key = $"@{random}{p.GetColName()}";
                    sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                    return key;
                })));
            sql.StrSqlValue.Append(");");

            sql.DbCommandType = eDbCommandType.Insert;

            return sql;
        }
        /// <summary>
        /// 更新单挑sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SqlCommandEntity GetUpdate<TData>(TData data, int random)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = type.GetNoIgnore().ToArray();
            var pKey = type.GetKey();
            string keyName = $"@{pKey.GetColName()}";
            sql.DbParams.Add(new MySqlParameter(keyName, pKey.GetValue(data)));


            sql.StrSqlValue.Append($"UPDATE {type.GetClassName()} SET ");

            sql.StrSqlValue.Append(string.Join(',',
                Props.Select(p =>
                {
                    string key = $"{p.GetColName()}";
                    sql.DbParams.Add(new MySqlParameter($"@{key}{random}", p.GetValue(data)));
                    return $"{key}=@{key}{random}";
                })));
            if (Check.IsNull(pKey))
            {
                throw new Exception("没有主键请为实体设置主键!");
            }
            sql.StrSqlValue.Append(" Where ");

            sql.StrSqlValue.Append($"{pKey.GetColName()}={keyName}");
            sql.StrSqlValue.Append(";");
            sql.DbCommandType = eDbCommandType.Update;
            return sql;
        }

        /// <summary>
        /// 批量插入生成sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public SqlCommandEntity GetInsert<TData>(IEnumerable<TData> datas)
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
                    sql.StrSqlValue.Append($"INSERT INTO {type.GetClassName()} ");
                    sql.StrSqlValue.Append("(");
                    sql.StrSqlValue.Append(string.Join(',', Props.Select(p => p.GetColName())));
                    sql.StrSqlValue.Append(") ");
                    sql.StrSqlValue.Append(" VALUE");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(" (");
                sql.StrSqlValue.Append(string.Join(',',
                  Props.Select(p =>
                  {
                      string key = $"@{Index}{charConnectSign}{p.GetColName()}";
                      sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                      return key;
                  })));
                sql.StrSqlValue.Append(" (");
                if (count == MysqlConst.INSERTMAX)
                {
                    sql.StrSqlValue.Append(";");
                    count = 0;
                }
                else
                {
                    sql.StrSqlValue.Append(",");
                }
            }

            return sql;
        }
        /// <summary>
        /// 拼装单挑查询语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public void GetSelect<TData>(QueryEntity sql)
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

        public void GetSelect(QueryEntity sql, Type type)
        {
            if (Check.IsNull(sql))
            {
                sql = new QueryEntity();
            }
            sql.StrSqlValue.Append($"SELECT " +
                $"{string.Join(',', type.GetNoIgnore())} " +
                $"FROM {type.GetClassName()} ");
        }

        public void GetLastInsert<TData>(QueryEntity sql)
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
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public SqlCommandEntity GetWhereSql<TData>(Expression<Func<TData, bool>> matchCondition)
        {
            SqlCommandEntity sqlEntity = new SqlCommandEntity();
            if (Check.IsNull(matchCondition))
            {
                throw new Exception();
            }
            return sqlEntity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos">映射成需要返回的实体部分</param>
        /// <param name="joinInfos">连接部分</param>
        /// <param name="condition">条件部分</param>
        /// <returns></returns>
        public void GetSelect<TData>(SelectEntity select, QueryEntity entity)
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



            GroupBy(select.OrderInfos,entity);

            OrderBy(select.OrderInfos,entity);
            //分页部分
            SetPageList(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos"></param>
        /// <param name="sbValue"></param>
        /// <exception cref="ArgumentException"></exception>
        private void LinkMapInfos(List<MapEntity> mapInfos, QueryEntity sqlEntity)
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
                        mapInfos[i].AsColumnName = $"{mapInfos[i].TableName}{charConnectSign}{mapInfos[i].ColumnName}";
                        if (Check.IsNullOrEmpty(mapInfos[i].MethodName))
                        {
                            sqlEntity.StrSqlValue.Append($" { mapInfos[i].TableName}.{mapInfos[i].ColumnName} AS {mapInfos[i].AsColumnName} ");
                        }
                        else
                        {
                            string vaule = MysqlConst.MapMethod(mapInfos[i].MethodName,$"{mapInfos[i].TableName}.{mapInfos[i].ColumnName}",string.Empty);
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
        private void LinkJoinInfos(JoinTableEntity[] joinInfos, QueryEntity sqlEntity)
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

        private void LinkConditions(List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions, QueryEntity sqlEntity)
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
                            sqlEntity.DbParams.Add(new MySqlParameter(rightValue, treeConditions[i].RightCondition.DisplayName));
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            //非常量
                            rightValue = $" {treeConditions[i].RightCondition.DisplayName}";
                        }
                        break;
                    case eConditionType.Constant:
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            leftValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.DbParams.Add(new MySqlParameter(leftValue, treeConditions[i].LeftCondition.DisplayName));

                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.DbParams.Add(new MySqlParameter(rightValue, treeConditions[i].RightCondition.DisplayName));
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName} ";
                            rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.DbParams.Add(new MySqlParameter(rightValue, treeConditions[i].LeftCondition.DisplayName));
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
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        public void GetSelect<TData>()
        {

        }
        /// <summary>
        /// 拼接分页
        /// </summary>
        /// <param name="sqlEntity"></param>
        private void SetPageList(QueryEntity sqlEntity)
        {
            if (sqlEntity.PageNumber < 0)
            {
                sqlEntity.PageNumber = 1;
            }
            if (sqlEntity.PageSize <= 0)
            {
                sqlEntity.PageSize = 100;
            }
            sqlEntity.StrSqlValue.Append(" Limit @SkipNumber,@TakeNumber");
            sqlEntity.DbParams.Add(new MySqlParameter("@SkipNumber", (sqlEntity.PageNumber - 1) * sqlEntity.PageSize));
            sqlEntity.DbParams.Add(new MySqlParameter("@TakeNumber", sqlEntity.PageSize));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        private void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
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
        private void GroupBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(g => g.IsGroupBy).Any())
            {
                entity.StrSqlValue.Append(" Group By ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }
    }
}
