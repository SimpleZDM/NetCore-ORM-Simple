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
        public SqlEntity GetInsert<TData>(TData data, int random = 0)
        {
            SqlEntity sql = new SqlEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore().ToArray();

            sql.Sb_Sql.Append($"INSERT INTO {type.GetClassName()} ");
            sql.Sb_Sql.Append("(");
            sql.Sb_Sql.Append(string.Join(',', Props.Select(p => p.GetColName())));
            sql.Sb_Sql.Append(") ");
            sql.Sb_Sql.Append(" VALUE(");
            sql.Sb_Sql.Append(string.Join(',',
                Props.Select(p =>
                {
                    string key = $"@{random}{p.GetColName()}";
                    sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                    return key;
                })));
            sql.Sb_Sql.Append(");");

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
        public SqlEntity GetUpdate<TData>(TData data,int random)
        {
            SqlEntity sql = new SqlEntity();
            Type type = typeof(TData);
            var Props = type.GetNoIgnore().ToArray();
            var pKey = type.GetKey();
            string keyName = $"@{pKey.GetColName()}";
            sql.DbParams.Add(new MySqlParameter(keyName, pKey.GetValue(data)));


            sql.Sb_Sql.Append($"UPDATE {type.GetClassName()} SET ");

            sql.Sb_Sql.Append(string.Join(',',
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
            sql.Sb_Sql.Append(" Where ");

            sql.Sb_Sql.Append($"{pKey.GetColName()}={keyName}");
            sql.Sb_Sql.Append(";");
            sql.DbCommandType = eDbCommandType.Update;
            return sql;
        }

        /// <summary>
        /// 批量插入生成sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="datas"></param>
        /// <returns></returns>
        public SqlEntity GetInsert<TData>(IEnumerable<TData> datas)
        {
            SqlEntity sql = new SqlEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore();
            int count = 0;
            int Index = 0;
            foreach (var data in datas)
            {
                if (count == 0)
                {
                    sql.Sb_Sql.Append($"INSERT INTO {type.GetClassName()} ");
                    sql.Sb_Sql.Append("(");
                    sql.Sb_Sql.Append(string.Join(',', Props.Select(p => p.GetColName())));
                    sql.Sb_Sql.Append(") ");
                    sql.Sb_Sql.Append(" VALUE");
                }
                Index++;
                count++;
                sql.Sb_Sql.Append(" (");
                sql.Sb_Sql.Append(string.Join(',',
                  Props.Select(p =>
                  {
                      string key = $"@{Index}{charConnectSign}{p.GetColName()}";
                      sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                      return key;
                  })));
                sql.Sb_Sql.Append(" (");
                if (count == MysqlConst.INSERTMAX)
                {
                    sql.Sb_Sql.Append(";");
                    count = 0;
                }
                else
                {
                    sql.Sb_Sql.Append(",");
                }
            }

            return sql;
        }
        /// <summary>
        /// 拼装单挑查询语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public void GetSelect<TData>(SqlEntity sql)
        {
            if (Check.IsNull(sql))
            {
                sql = new SqlEntity();
            }   
            Type type = typeof(TData);
            sql.Sb_Sql.Append($"SELECT " +
                $"{string.Join(',', type.GetNoIgnore())} " +
                $"FROM {type.GetClassName()} ");
        }

        public void GetSelect(SqlEntity sql,Type type)
        {
            if (Check.IsNull(sql))
            {
                sql = new SqlEntity();
            }
            sql.Sb_Sql.Append($"SELECT " +
                $"{string.Join(',', type.GetNoIgnore())} " +
                $"FROM {type.GetClassName()} ");
        }

        public void GetLastInsert<TData>(SqlEntity sql)
        {
            if (Check.IsNull(sql))
            {
                sql = new SqlEntity();
            }
            Type type = typeof(TData);
            GetSelect(sql,type);
            var Key = type.GetKey();
            sql.Sb_Sql.Append($" Where {Key.GetColName()}=LAST_INSERT_ID();");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public SqlEntity GetWhereSql<TData>(Expression<Func<TData, bool>> matchCondition)
        {
            SqlEntity sqlEntity = new SqlEntity();
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
        public void GetSelect<TData>(List<MapEntity> mapInfos, List<JoinTableEntity> joinInfos, List<ConditionEntity> conditions,List<TreeConditionEntity> treeConditions,SqlEntity entity)
        {
            if (Check.IsNull(entity))
            {
                entity= new SqlEntity();
            }

            if (Check.IsNull(mapInfos))
            {
                mapInfos = new List<MapEntity>();
            }
            if (mapInfos.Count.Equals(CommonConst.ZeroOrNull))
            {
                Type type = typeof(TData);
                string TableName = type.GetClassName();
                foreach (var prop in type.GetNoIgnore())
                {
                    mapInfos.Add(new MapEntity()
                    {
                        PropName = prop.GetColName(),
                        ColumnName = prop.GetColName(),
                        TableName = TableName,
                    });
                }
            }
            //视图
            entity.Sb_Sql.Append("SELECT ");

            LinkMapInfos(mapInfos, entity);
            //连接
            LinkJoinInfos(joinInfos, entity);
            //条件
            if (!Check.IsNull(treeConditions) && treeConditions.Count>CommonConst.ZeroOrNull)
            {
                entity.Sb_Sql.Append(" where");
                LinkConditions(conditions,treeConditions, entity);
            }

            //分页部分
            SetPageList(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapInfos"></param>
        /// <param name="sbValue"></param>
        /// <exception cref="ArgumentException"></exception>
        private void LinkMapInfos(List<MapEntity> mapInfos, SqlEntity sqlEntity)
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
                            sqlEntity.Sb_Sql.Append(",");
                        }
                        mapInfos[i].AsColumnName = $"{mapInfos[i].TableName}{charConnectSign}{mapInfos[i].ColumnName}";
                        sqlEntity.Sb_Sql.Append($" {mapInfos[i].TableName}.{mapInfos[i].ColumnName} AS {mapInfos[i].AsColumnName} ");
                        
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
        private void LinkJoinInfos(List<JoinTableEntity> joinInfos,SqlEntity sqlEntity)
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
                        sqlEntity.Sb_Sql.Append($" FROM {join.DisplayName} ");
                    }
                    else
                    {
                        sqlEntity.Sb_Sql.Append($" {MysqlConst.StrJoins[(int)join.JoinType]} {join.DisplayName} ON ");
                        LinkConditions(join.Conditions,join.TreeConditions,sqlEntity);
                    }
                }
            }
        }

        private void LinkConditions(List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions,SqlEntity sqlEntity)
        {
            if (Check.IsNull(treeConditions))
            {
                return;
            }
            if (Check.IsNull(conditions))
            {
                return;
              
            }
            if (treeConditions.Count>0&&conditions.Count!=treeConditions.Count-1)
            {
                    throw new Exception("sql 语句条件部分解析有误!");
            }
            for (int i = 0; i <treeConditions.Count(); i++)
            {
                foreach (var sign in treeConditions[i].LeftBracket)
                {
                    sqlEntity.Sb_Sql.Append(MysqlConst.cStrSign[(int)sign]);
                }
               
                    string leftValue = string.Empty;
                    string rightValue = string.Empty;
                    switch (treeConditions[i].LeftCondition.ConditionType)
                    {
                        case eConditionType.ColumnName:
                            leftValue=$" {treeConditions[i].LeftCondition.DisplayName} ";
                            if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                            {
                                rightValue =$"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(),8)}{i}" ;
                                sqlEntity.DbParams.Add(new MySqlParameter(rightValue, treeConditions[i].RightCondition.DisplayName));
                            }
                            else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                            {
                                //非常量
                                rightValue=$" {treeConditions[i].RightCondition.DisplayName}";
                            }
                            break;
                        case eConditionType.Constant:
                            if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                            {
                                leftValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                                sqlEntity.DbParams.Add(new MySqlParameter(leftValue,treeConditions[i].LeftCondition.DisplayName));

                                rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                                sqlEntity.DbParams.Add(new MySqlParameter(rightValue,treeConditions[i].RightCondition.DisplayName));
                            }
                            else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                            {
                               leftValue=$" {treeConditions[i].RightCondition.DisplayName} ";
                                rightValue = $"@{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                                sqlEntity.DbParams.Add(new MySqlParameter(rightValue, treeConditions[i].LeftCondition.DisplayName));
                            }
                            break;
                        default:
                            break;
                    }
                    if (treeConditions[i].RelationCondition.ConditionType.Equals(eConditionType.Method))
                    {
                        sqlEntity.Sb_Sql.Append(MysqlConst.MapMethod(treeConditions[i].RelationCondition.DisplayName, leftValue, rightValue));
                    }
                    else
                    {
                        sqlEntity.Sb_Sql.Append($"{leftValue}{MysqlConst.cStrSign[(int)treeConditions[i].RelationCondition.SignType]}{rightValue}");
                    }
                foreach (var sign in treeConditions[i].RightBracket)
                {
                    sqlEntity.Sb_Sql.Append(MysqlConst.cStrSign[(int)sign]);
                }

                if (conditions.Count>i)
                {
                    sqlEntity.Sb_Sql.Append(MysqlConst.cStrSign[(int)conditions[i].SignType]);
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
        private void SetPageList(SqlEntity sqlEntity)
        {
            if (sqlEntity.PageNumber<0)
            {
                sqlEntity.PageNumber =1;
            }
            if (sqlEntity.PageSize <= 0)
            {
                sqlEntity.PageSize = 100;
            }
            sqlEntity.Sb_Sql.Append(" Limit @SkipNumber,@TakeNumber");
            sqlEntity.DbParams.Add(new MySqlParameter("@SkipNumber",(sqlEntity.PageNumber -1)*sqlEntity.PageSize));
            sqlEntity.DbParams.Add(new MySqlParameter("@TakeNumber",sqlEntity.PageSize));

        }
    }
}
