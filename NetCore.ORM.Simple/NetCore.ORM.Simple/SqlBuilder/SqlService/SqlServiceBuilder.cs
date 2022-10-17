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

            sql.StrSqlValue.Append($"{MainWordType.Insert.GetMainWordStr()} {MainWordType.Into.GetMainWordStr()} {GetTableName(type)} ");
            sql.StrSqlValue.Append("(");
            sql.StrSqlValue.Append(string.Join(',', Props.Select(p => $"{GetColName(p)}")));
            sql.StrSqlValue.Append(") ");
            sql.StrSqlValue.Append($" {MainWordType.Values.GetMainWordStr()}(");
            sql.StrSqlValue.Append(string.Join(',',
                Props.Select(p =>
                {
                    string key = $"{MainWordType.AT.GetMainWordStr()}{random}{GetColName(p)}";

                    sql.AddParameter(DbType, key, p.GetValue(data));
                    return key;
                })));
            sql.StrSqlValue.Append(");");

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
            string keyName = $"{MainWordType.AT.GetMainWordStr()}{GetColName(pKey)}";
            Update(sql, keyName, GetTableName(type), pKey, data, Props, random);
            sql.DbCommandType = eDbCommandType.Update;
            sql.StrSqlValue.Append(" ;");
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
            string keyName = $"{MainWordType.AT.GetMainWordStr()}{GetColName(pKey)}";
            foreach (var data in datas)
            {
                Update(sql, keyName, tableName, pKey, data, Props, Index);
                Index++;
            }
            sql.DbCommandType = eDbCommandType.Update;
            sql.StrSqlValue.Append(" ;");
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
                    sql.StrSqlValue.Append($"{MainWordType.Insert.GetMainWordStr()} {MainWordType.Into.GetMainWordStr()} {GetTableName(type)} ");
                    sql.StrSqlValue.Append("(");
                    sql.StrSqlValue.Append(string.Join(',', Props.Select(p => $" {GetColName(p)} ")));
                    sql.StrSqlValue.Append(") ");
                    sql.StrSqlValue.Append($" {MainWordType.Values.GetMainWordStr()}");
                }
                Index++;
                count++;
                sql.StrSqlValue.Append(" (");
                sql.StrSqlValue.Append(string.Join(',',
                  Props.Select(p =>
                  {
                      string key = $"{MainWordType.AT.GetMainWordStr()}{Index + offset}{charConnectSign}{GetColName(p)}";
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
            sql.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()} {GetColName(Key)}=Scope_identity();");
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

                entity.StrSqlValue.Append($"{MainWordType.Select.GetMainWordStr()} * {MainWordType.From.GetMainWordStr()} ({MainWordType.Select.GetMainWordStr()} ROW_NUMBER() over(");
                if (!IsGroup(select)&&IsOrder(select))
                {
                    OrderBy(select.OrderInfos,entity,o=>o.IsOrderBy);
                }else if (IsGroup(select))
                {
                    OrderBy(select.OrderInfos, entity, o => o.IsGroupBy);
                }
                else
                {
                    entity.StrSqlValue.Append($"{MainWordType.Order.GetMainWordStr()} {MainWordType.By.GetMainWordStr()} {key.TableName}.{key.ColumnName}");
                }
               
                entity.StrSqlValue.Append($") {MainWordType.As.GetMainWordStr()} {MainWordType.NoIndex.GetMainWordStr()},");

                entity.MapInfos = select.MapInfos.ToArray();

                LinkMapInfos(select.MapInfos, entity);

                //连接
                LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
                //条件
                if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
                {
                    entity.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()}");
                    LinkConditions(select.Conditions, select.TreeConditions, entity);
                }

                GroupBy(select.OrderInfos, entity);

               // OrderBy(select.OrderInfos, entity);
                entity.StrSqlValue.Append($" ) {MainWordType.SimpleTable.GetMainWordStr()} ");
                SetPageList(entity);
            }
            else
            {
                entity.StrSqlValue.Append($"{MainWordType.Select.GetMainWordStr()} ");

                entity.MapInfos = select.MapInfos.ToArray();

                LinkMapInfos(select.MapInfos, entity);

                //连接
                LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
                //条件
                if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
                {
                    entity.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()}");
                    LinkConditions(select.Conditions, select.TreeConditions, entity);
                }

                GroupBy(select.OrderInfos, entity);

                base.OrderBy(select.OrderInfos, entity);
            }
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
            //entity.StrSqlValue.Append($" {key.TableName}.{key.ColumnName} ");
            if (IsPage(entity))
            {
                var key = select.MapInfos.Where(m => m.IsKey).FirstOrDefault();
                MapEntity map=null;
                if (Check.IsNull(key))
                {
                    throw new Exception("请配置主键!");
                }

                entity.StrSqlValue.Append($"{MainWordType.Select.GetMainWordStr()} {MainWordType.Count}(*) {MainWordType.As} {MainWordType.SimpleNumber} {MainWordType.From.GetMainWordStr()} ({MainWordType.Select.GetMainWordStr()} ROW_NUMBER() over(");


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
                    entity.StrSqlValue.Append($"{MainWordType.Order.GetMainWordStr()} {MainWordType.By.GetMainWordStr()} {key.TableName}.{key.ColumnName}");
                    map = key;
                }

                entity.StrSqlValue.Append($") {MainWordType.As.GetMainWordStr()} {MainWordType.NoIndex.GetMainWordStr()}");

                //entity.StrSqlValue.Append($",{MainWordType.Count}(*) {MainWordType.As.GetMainWordStr()} {MainWordType.SimpleNumber.GetMainWordStr()}");

                //entity.StrSqlValue.Append($" {map.TableName}.{map.ColumnName}");
                //entity.MapInfos = select.MapInfos.ToArray();

                //LinkMapInfos(select.MapInfos, entity);

                //连接
                LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
                //条件
                if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
                {
                    entity.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()}");
                    LinkConditions(select.Conditions, select.TreeConditions, entity);
                }

                GroupBy(select.OrderInfos, entity);

                // OrderBy(select.OrderInfos, entity);
                entity.StrSqlValue.Append($" ) {MainWordType.SimpleTable.GetMainWordStr()} ");
                SetPageList(entity);
            }
            else
            {
                entity.StrSqlValue.Append($"{MainWordType.Select.GetMainWordStr()} {MainWordType.Count.GetMainWordStr()}(*) {MainWordType.As.GetMainWordStr()} {MainWordType.SimpleNumber.GetMainWordStr()}");

                //连接
                LinkJoinInfos(select.JoinInfos.Values.ToArray(), entity);
                //条件
                if (!Check.IsNull(select.TreeConditions) && select.TreeConditions.Count > CommonConst.ZeroOrNull)
                {
                    entity.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()}");
                    LinkConditions(select.Conditions, select.TreeConditions, entity);
                }
                OrderBy(select.OrderInfos,entity);

                GroupBy(select.OrderInfos,entity);
            }
            //视图

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDate"></typeparam>
        /// <param name="entity"></param>
        public override SqlCommandEntity GetDelete(Type type, List<ConditionEntity> conditions, List<TreeConditionEntity> treeConditions)
        {
            SqlCommandEntity sqlCommand = new SqlCommandEntity();
            sqlCommand.StrSqlValue.Append($"{MainWordType.Delete.GetMainWordStr()}  {MainWordType.From} {GetTableName(type)} ");
            if (Check.IsNull(treeConditions) || treeConditions.Count.Equals(CommonConst.ZeroOrNull))
            {
                throw new Exception(ErrorType.DeleteNotMatch.GetErrorInfo());
            }
            sqlCommand.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()} ");
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
            var key = $"{MainWordType.AT.GetMainWordStr()}{GetColName(PropKey)}{random}";
            sqlCommand.StrSqlValue.Append($"{MainWordType.Delete.GetMainWordStr()} {MainWordType.From.GetMainWordStr()} {GetTableName(type)} {MainWordType.Where.GetMainWordStr()} {GetColName(PropKey)}={key}");
            sqlCommand.AddParameter(DbType, key, PropKey.GetValue(data));
            return sqlCommand;
        }



        /// <summary>
        /// 拼接分页
        /// </summary>
        /// <param name="sqlEntity"></param>
        protected override void SetPageList(QueryEntity sqlEntity)
        {
            sqlEntity.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()} {MainWordType.NoIndex.GetMainWordStr()}>{MainWordType.AT.GetMainWordStr()}{MainWordType.SkipNumber.GetMainWordStr()} and {MainWordType.NoIndex.GetMainWordStr()}<={MainWordType.AT.GetMainWordStr()}{MainWordType.TakeNumber.GetMainWordStr()}");
            sqlEntity.AddParameter(DbType, $"{MainWordType.AT.GetMainWordStr()}{MainWordType.SkipNumber.GetMainWordStr()}", (sqlEntity.PageNumber - 1) * sqlEntity.PageSize);
            sqlEntity.AddParameter(DbType, $"{MainWordType.AT.GetMainWordStr()}{MainWordType.TakeNumber.GetMainWordStr()}", sqlEntity.PageSize*sqlEntity.PageNumber);

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

                switch (treeConditions[i].LeftCondition.ConditionType)
                {
                    case eConditionType.ColumnName:
                        leftValue = $" {treeConditions[i].LeftCondition.DisplayName} ";
                        if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.Constant))
                        {
                            rightValue = $"{MainWordType.AT.GetMainWordStr()}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
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
                            leftValue = $"{MainWordType.AT.GetMainWordStr()}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, leftValue, treeConditions[i].LeftCondition.DisplayName);

                            rightValue = $"{MainWordType.AT.GetMainWordStr()}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].RightCondition.DisplayName);
                        }
                        else if (treeConditions[i].RightCondition.ConditionType.Equals(eConditionType.ColumnName))
                        {
                            leftValue = $" {treeConditions[i].RightCondition.DisplayName} ";
                            rightValue = $"{MainWordType.AT.GetMainWordStr()}{MD5Encrypt.Encrypt(DateTime.Now.ToString(), 8)}{i}";
                            sqlEntity.AddParameter(DbType, rightValue, treeConditions[i].LeftCondition.DisplayName);
                        }
                        break;
                    default:
                        break;
                }
                if (Check.IsNull(treeConditions[i].RelationCondition))
                {
                    
                    if (leftValue.ToLower().Contains("true"))
                    {
                        StrValue.Append($" (1=1) ");
                    }
                    else
                    {
                        StrValue.Append($" ({leftValue}) ");
                    }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity)
        {
           
            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsGroupBy||o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append($" {MainWordType.Order.GetMainWordStr()} {MainWordType.By.GetMainWordStr()} ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(o => o.IsOrderBy&&o.IsGroupBy).OrderBy(o => o.OrderSoft).Select(o => $"{o.TableName}.{o.ColumnName} {MysqlConst.AscendOrDescend(o.OrderType)}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected  void OrderBy(List<OrderByEntity> OrderByInfos, QueryEntity entity,Func<OrderByEntity,bool>func)
        {

            if (!Check.IsNull(OrderByInfos) && OrderByInfos.Where(o => o.IsGroupBy || o.IsOrderBy).Any())
            {
                entity.StrSqlValue.Append($" {MainWordType.Order.GetMainWordStr()} {MainWordType.By.GetMainWordStr()} ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(o => 
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
                entity.StrSqlValue.Append($" {MainWordType.Group.GetMainWordStr()} {MainWordType.By.GetMainWordStr()} ");
                entity.StrSqlValue.Append(string.Join(',', OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        protected override void Update<TEntity>(SqlCommandEntity sql, string keyName, string tableName, PropertyInfo pKey, TEntity data, IEnumerable<PropertyInfo> Props, int index)
        {
            sql.AddParameter(DbType, $"{keyName}{index}", pKey.GetValue(data));
            sql.StrSqlValue.Append($"{MainWordType.Update.GetMainWordStr()} {tableName} {MainWordType.Set.GetMainWordStr()} ");
            sql.StrSqlValue.Append(string.Join(',',
            Props.Select(p =>
            {
                string colName = $"{GetColName(p)}";
                sql.AddParameter(DbType, $"{MainWordType.AT.GetMainWordStr()}{colName}{index}", p.GetValue(data));
                return $"{colName}={MainWordType.AT.GetMainWordStr()}{colName}{index}";
            })));
            sql.StrSqlValue.Append($" {MainWordType.Where.GetMainWordStr()} ");
            sql.StrSqlValue.Append($"{GetColName(pKey)}={keyName}{index}");
            sql.StrSqlValue.Append(";");
        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table, Column);
        }
    }
}
