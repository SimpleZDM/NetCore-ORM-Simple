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
 * 接口名称 SqlServiceBuilder
 * 开发人员：-nhy
 * 创建时间：2022/9/20 14:59:18
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.SqlBuilder
{
    public class SqlServiceBuilder : BaseBuilder, ISqlBuilder
    {
        public SqlServiceBuilder(eDBType dbType) : base(dbType)
        {

        }
        public override SqlCommandEntity GetInsert<TData>(TData data, int random)
        {
            SqlCommandEntity sql = new SqlCommandEntity();
            Type type = typeof(TData);
            var Props = GetNotKeyAndIgnore(type).ToArray();

            sql.StrSqlValue.Append($"{DBMDConst.Insert} {DBMDConst.Into} {GetTableName(type)} ");
            sql.StrSqlValue.Append(DBMDConst.LeftBracket);
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma, Props.Select(p => $"{GetColName(p)}")));
            sql.StrSqlValue.Append(DBMDConst.RightBracket);
            sql.StrSqlValue.Append($" {DBMDConst.Values}{DBMDConst.LeftBracket}");
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

        public override SqlCommandEntity GetUpdate<TData>(TData data, int random)
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
        public override SqlCommandEntity GetUpdate<TData>(List<TData> datas, int offset)
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
            sql.StrSqlValue.Append(DBMDConst.Semicolon);
            return sql;
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
                    sql.StrSqlValue.Append($"{DBMDConst.Insert} {DBMDConst.Into} {GetTableName(type)} ");
                    sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                    sql.StrSqlValue.Append(string.Join(DBMDConst.Comma, Props.Select(p => $" {GetColName(p)} ")));
                    sql.StrSqlValue.Append(DBMDConst.RightBracket);
                    sql.StrSqlValue.Append($" {DBMDConst.Values}");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(DBMDConst.LeftBracket);
                sql.StrSqlValue.Append(string.Join(DBMDConst.Comma,
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
            var Key = GetKey(type);
            sql.StrSqlValue.Append($" {DBMDConst.Where} {GetColName(Key)}={DBMDConst.Scope_identity};");
            //SELECT scope_identity()
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


            if (IsPage(entity))
            {
                var key = select.MapInfos.Where(m => m.IsKey).FirstOrDefault();
                if (Check.IsNull(key))
                {
                    throw new Exception("请配置主键!");
                }

                entity.StrSqlValue.Append($"{DBMDConst.Select} {DBMDConst.Asterisk} {DBMDConst.From} {DBMDConst.LeftBracket}{DBMDConst.Select} {DBMDConst.ROW_NUMBER} {DBMDConst.Over}{DBMDConst.LeftBracket}");
                if (!IsGroup(select)&&IsOrder(select))
                {
                    OrderBy(select.OrderInfos,entity,o=>o.IsOrderBy);
                }else if (IsGroup(select))
                {
                    OrderBy(select.OrderInfos, entity, o => o.IsGroupBy);
                }
                else
                {
                    entity.StrSqlValue.Append($"{DBMDConst.Order} {DBMDConst.By} {key.TableName}.{key.ColumnName}");
                }
               
                entity.StrSqlValue.Append($"{DBMDConst.RightBracket} {DBMDConst.As} {DBMDConst.NoIndex}{DBMDConst.Comma}");

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

                GroupBy(select.OrderInfos, entity);

               // OrderBy(select.OrderInfos, entity);
                entity.StrSqlValue.Append($" {DBMDConst.RightBracket} {DBMDConst.SimpleTable} ");
                SetPageList(entity);
            }
            else
            {
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

                GroupBy(select.OrderInfos, entity);

                base.OrderBy(select.OrderInfos, entity);
            }

            entity.StrSqlValue.Append(DBMDConst.Semicolon);
        }

        public override void GetCount(SelectEntity select, QueryEntity entity)
        {
            if (Check.IsNull(entity))
            {
                entity = new QueryEntity();
            }
            if (Check.IsNull(select))
            {
                throw new ArgumentNullException(nameof(select));
            }
            if (IsPage(entity))
            {
                var key = select.MapInfos.Where(m => m.IsKey).FirstOrDefault();
                MapEntity map=null;
                if (Check.IsNull(key))
                {
                    throw new Exception("请配置主键!");
                }

                entity.StrSqlValue.Append($"{DBMDConst.Select} {DBMDConst.Count}{DBMDConst.LeftBracket}{DBMDConst.Asterisk}{DBMDConst.RightBracket} {DBMDConst.As} {DBMDConst.SimpleNumber} {DBMDConst.From} {DBMDConst.LeftBracket}{DBMDConst.Select} {DBMDConst.ROW_NUMBER} {DBMDConst.Over}{DBMDConst.LeftBracket}");


                if (!IsGroup(select) && IsOrder(select))
                {
                    OrderBy(select.OrderInfos, entity, o => o.IsOrderBy);
                   
                }
                else if (IsGroup(select))
                {
                    OrderBy(select.OrderInfos, entity, o => o.IsGroupBy); 
                    var order = select.OrderInfos.Where(o => o.IsGroupBy).FirstOrDefault();
                    map = new MapEntity()
                    {
                        ColumnName = order.ColumnName,
                        TableName = order.TableName,
                    };
                }
                else
                {
                    entity.StrSqlValue.Append($"{DBMDConst.Order} {DBMDConst.By} {key.TableName}.{key.ColumnName}");
                    map = key;
                }

                entity.StrSqlValue.Append($"{DBMDConst.RightBracket} {DBMDConst.As} {DBMDConst.NoIndex}");


                //连接
                LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
                //条件
                if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
                {
                    entity.StrSqlValue.Append($" {DBMDConst.Where}");
                    LinkConditions(select.Conditions, select.TreeConditions, entity);
                }

                GroupBy(select.OrderInfos, entity);

                entity.StrSqlValue.Append($" {DBMDConst.RightBracket} {DBMDConst.SimpleTable} ");
                SetPageList(entity);
            }
            else
            {
                entity.StrSqlValue.Append($"{DBMDConst.Select} {DBMDConst.Count}{DBMDConst.LeftBracket}{DBMDConst.Asterisk}{DBMDConst.RightBracket} {DBMDConst.As} {DBMDConst.SimpleNumber}");

                //连接
                LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
                //条件
                if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
                {
                    entity.StrSqlValue.Append($" {DBMDConst.Where}");
                    LinkConditions(select.Conditions, select.TreeConditions, entity);
                }
                OrderBy(select.OrderInfos,entity);

                GroupBy(select.OrderInfos,entity);
            }
            //视图
            entity.StrSqlValue.Append(DBMDConst.Semicolon);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDate"></typeparam>
        /// <param name="entity"></param>
        public override SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions)
        {
            SqlCommandEntity sqlCommand = new SqlCommandEntity();
            sqlCommand.StrSqlValue.Append($"{DBMDConst.Delete}  {DBMDConst.From} {GetTableName(type)} ");
            if (Check.IsNull(treeConditions) || treeConditions.Count.Equals(CommonConst.ZeroOrNull))
            {
                throw new Exception(ErrorType.DeleteNotMatch.GetErrorInfo());
            }
            sqlCommand.StrSqlValue.Append($" {DBMDConst.Where} ");
            LinkConditions(conditions, treeConditions, sqlCommand);
            return sqlCommand;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <exception cref="Exception"></exception>
        public override SqlCommandEntity GetDelete<TData>(TData data, int random)
        {
            Type type = typeof(TData);
            var PropKey = GetKey(type);
            if (Check.IsNull(PropKey))
            {
                throw new Exception(ErrorType.NotKey.GetErrorInfo());
            }
            SqlCommandEntity sqlCommand = new SqlCommandEntity();
            var key = $"{DBMDConst.AT}{GetColName(PropKey)}{random}";
            sqlCommand.StrSqlValue.Append($"{DBMDConst.Delete} {DBMDConst.From} {GetTableName(type)} {DBMDConst.Where} {GetColName(PropKey)}{DBMDConst.Equal}{key}");
            sqlCommand.AddParameter(DbType, key, PropKey.GetValue(data));
            return sqlCommand;
        }



        /// <summary>
        /// 拼接分页
        /// </summary>
        /// <param name="sqlEntity"></param>
        protected override void SetPageList(QueryEntity sqlEntity)
        {
            sqlEntity.StrSqlValue.Append($" {DBMDConst.Where} {DBMDConst.NoIndex}>{DBMDConst.AT}{DBMDConst.SkipNumber} and {DBMDConst.NoIndex}<={DBMDConst.AT}{DBMDConst.TakeNumber}");
            sqlEntity.AddParameter(DbType, $"{DBMDConst.AT}{DBMDConst.SkipNumber}", (sqlEntity.PageNumber - 1) * sqlEntity.PageSize);
            sqlEntity.AddParameter(DbType, $"{DBMDConst.AT}{DBMDConst.TakeNumber}", sqlEntity.PageSize*sqlEntity.PageNumber);

        }


        protected override void LinkConditions(List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions, QueryEntity sqlEntity)
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
                ConditionEntity currentConditon = null ;
                switch (treeConditions[i].LeftCondition.ConditionType)
                {
                    case eConditionType.ColumnName:
                        leftValue = $" {treeConditions[i].LeftCondition.DisplayName} ";
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            base.GetConditionValue(treeConditions[i].RightCondition,sqlEntity,rightValue);
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
                            leftValue = $"{treeConditions[i].LeftCondition.DisplayName}";
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
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName} ";
                            rightValue = $"{DBMDConst.AT}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            base.GetConditionValue(treeConditions[i].LeftCondition, sqlEntity, rightValue);
                            currentConditon = treeConditions[i].LeftCondition;
                        }
                        break;
                    default:
                        break;
                }
                if (Check.IsNull(treeConditions[i].RelationCondition))
                {
                    
                    if (leftValue.ToLower().Contains("true"))
                    {
                        StrValue.Append($" {DBMDConst.LeftBracket}1{DBMDConst.Equal}1{DBMDConst.RightBracket} ");
                    }
                    else
                    {
                        StrValue.Append($" {DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ");
                    }
                }
                else if (treeConditions[i].RelationCondition.ConditionType.Equals(eConditionType.Method))
                {
                    StrValue.Append(MapMethod(treeConditions[i].RelationCondition.DisplayName, leftValue,rightValue,currentConditon,treeConditions[i].IsNot));
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
           
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsGroupBy||o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append($" {DBMDConst.Order} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma, 
                    OrderByInfos.Where(o => o.IsOrderBy&&o.IsGroupBy).
                    OrderBy(o => o.OrderSoft).
                    Select(o => $"{o.TableName}.{o.ColumnName} {MysqlConst.AscendOrDescend(o.OrderType)}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected  void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity,Func<OrderByEntity,bool>func)
        {

            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsGroupBy || o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append($" {DBMDConst.Order} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma, OrderByInfos.Where(o => 
                   {   bool value = true;
                       value = o.IsOrderBy;
                       if (!Check.IsNull(func))
                       {
                           value=func.Invoke(o);
                       }
                       return value;
                    }).OrderBy(o => o.OrderSoft).Select(o => $"{o.TableName}.{o.ColumnName} {MysqlConst.AscendOrDescend(o.OrderType)}")));
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
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma, OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected override void Update<TEntity>(SqlCommandEntity sql, string keyName, string tableName, PropertyInfo pKey, TEntity data, IEnumerable<PropertyInfo> Props, int index)
        {
            sql.AddParameter(DbType, $"{keyName}{index}", pKey.GetValue(data));
            sql.StrSqlValue.Append($"{DBMDConst.Update} {tableName} {DBMDConst.Set} ");
            sql.StrSqlValue.Append(string.Join(DBMDConst.Comma,
            Props.Select(p =>
            {
                string colName = $"{GetColName(p)}";
                sql.AddParameter(DbType, $"{DBMDConst.AT}{colName}{DBMDConst.DownLine}{index}", p.GetValue(data));
                return $"{colName}{DBMDConst.Equal}{DBMDConst.AT}{colName}{DBMDConst.DownLine}{index}";
            })));
            sql.StrSqlValue.Append($" {DBMDConst.Where} ");
            sql.StrSqlValue.Append($"{GetColName(pKey)}{DBMDConst.Equal}{keyName}{index}");
            sql.StrSqlValue.Append(DBMDConst.Semicolon);
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }
    }
}
