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
            return base.GetInsert(datas,offset);
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

            entity.StrSqlValue.Append(DBMDConst.Semicolon);
        }

        public override void GetCount(SelectEntity select, QueryEntity entity)
        {
             base.GetCount(select,entity);

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
                entity.StrSqlValue.Append($" {DBMDConst.Order} {DBMDConst.By} ");
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma, 
                    OrderByInfos.Where(o => o.IsOrderBy).OrderBy(o => o.OrderSoft).
                    Select(o => $"{o.TableName}.{o.ColumnName} {MysqlConst.AscendOrDescend(o.OrderType)}")));
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

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table,Column);
        }

        protected override string MapMethod(string methodName, string leftValue, string rightValue, ConditionEntity condition, bool IsNot)
        {
            string value = DBMDConst.Equal.ToString();
            if (Check.IsNullOrEmpty(methodName))
            {
                return value;
            }
            switch (methodName)
            {
                case MethodConst._ToString:
                    break;
                case MethodConst._Equals:
                    if (IsNot)
                    {
                        value = $"{leftValue}{DBMDConst.LessThan}{DBMDConst.GreaterThan}{rightValue}";
                    }
                    else
                    {
                        value = $"{leftValue}{DBMDConst.Equal}{rightValue}";
                    }
                    break;
                case MethodConst._IsNull:
                case MethodConst._IsNullOrEmpty:
                    if (IsNot)
                    {
                        if (!Check.IsNullOrEmpty(leftValue))
                        {
                            value = $"{leftValue} {DBMDConst.Is} {DBMDConst.Not} {DBMDConst.StrNULL}";
                        }
                        else if (!Check.IsNullOrEmpty(rightValue))
                        {
                            value = $"{rightValue} {DBMDConst.Is} {DBMDConst.Not} {DBMDConst.StrNULL}";
                        }
                    }
                    else
                    {
                        if (!Check.IsNullOrEmpty(leftValue))
                        {
                            value = $"{leftValue} {DBMDConst.Is} {DBMDConst.StrNULL}";
                        }
                        else if (!Check.IsNullOrEmpty(rightValue))
                        {
                            value = $"{rightValue} {DBMDConst.Is}  {DBMDConst.StrNULL}";
                        }
                    }

                    break;
                case MethodConst._Sum:
                    value = $" SUM{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";
                    break;
                case MethodConst._Min:
                    value = $" Min{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";
                    break;
                case MethodConst._Max:
                    value = $" Max{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket}";
                    break;
                case MethodConst._Count:
                    leftValue = Check.IsNullOrEmpty(leftValue) ? DBMDConst.Asterisk.ToString() : leftValue;
                    value = $" COUNT{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket}";
                    break;
                case MethodConst._Average:
                    value = $" AVG{DBMDConst.LeftBracket}{leftValue}{DBMDConst.RightBracket} ";
                    break;
                case MethodConst._FirstOrDefault:
                    value = $" {leftValue}";
                    break;
                case MethodConst._Contains:
                    if (eDataType.SimpleString == condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                        }
                        else
                        {
                            value = $"{leftValue}  {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";

                        }
                    }
                    else if ((int)eDataType.SimpleArrayInt <= (int)condition.DataType
                        && (int)eDataType.SimpleListDecimal >= (int)condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} {DBMDConst.Not} {DBMDConst.In} {DBMDConst.LeftBracket}{condition.DisplayName}{DBMDConst.RightBracket} ";
                        }
                        else
                        {
                            value = $"{leftValue}  {DBMDConst.In} {DBMDConst.LeftBracket}{condition.DisplayName}{DBMDConst.RightBracket}";
                        }
                    }
                    break;
                case MethodConst._LeftContains:
                    if (eDataType.SimpleString == condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.SingleQuotes} ";
                        }
                        else
                        {
                            value = $"{leftValue} {DBMDConst.Like} {DBMDConst.SingleQuotes}{DBMDConst.Percent}{condition.DisplayName}{DBMDConst.SingleQuotes} ";
                        }

                    }
                    break;
                case MethodConst._RightContains:
                    if (eDataType.SimpleString == condition.DataType)
                    {
                        if (IsNot)
                        {
                            value = $"{leftValue} {DBMDConst.Not} {DBMDConst.Like} {DBMDConst.SingleQuotes}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
                        }
                        else
                        {
                            value = $"{leftValue} {DBMDConst.Like} {DBMDConst.SingleQuotes}{condition.DisplayName}{DBMDConst.Percent}{DBMDConst.SingleQuotes} ";
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
