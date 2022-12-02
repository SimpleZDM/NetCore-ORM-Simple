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
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(), 
                    OrderByInfos.Where(o => o.IsOrderBy).OrderBy(o => o.OrderSoft).
                    Select(o => $"{o.TableName}.{o.ColumnName} {AscendOrDescend(o.OrderType)}")));
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
                entity.StrSqlValue.Append(string.Join(DBMDConst.Comma.ToString(),
                    OrderByInfos.Where(g => g.IsGroupBy).OrderBy(g => g.GroupSoft).
                    Select(g => $"{g.TableName}.{g.ColumnName}")));
                entity.StrSqlValue.Append(" ");
            }

        }

        public override void SetAttr(Type Table = null, Type Column = null)
        {
            base.SetAttr(Table,Column);
        }

        protected override string MapMethod(string methodName, string leftValue, string rightValue, bool IsNot,params ConditionEntity [] conditions)
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

                    value=MysqlConst._Equals(leftValue,rightValue,IsNot);
                    break;
                case MethodConst._IsNull:
                case MethodConst._IsNullOrEmpty:

                    value = MysqlConst._IsNullOrEmpty(leftValue,rightValue,IsNot);
                    break;
                case MethodConst._Sum:

                    value = MysqlConst._Sum(leftValue);
                    break;
                case MethodConst._Min:

                    value =MysqlConst._Min(leftValue);
                    break;

                case MethodConst._Max:
                    value =MysqlConst._Max(leftValue);
                    break;
                case MethodConst._Count:

                    value = MysqlConst._Count(leftValue);
                    break;
                case MethodConst._Average:

                    value = MysqlConst._Average(leftValue);
                    break;
                case MethodConst._FirstOrDefault:

                    value =MysqlConst._FirstOrDefault(leftValue);
                    break;
                case MethodConst._Contains:

                    value = MysqlConst._Contains(leftValue,IsNot,conditions);
                    break;
                case MethodConst._LeftContains:

                    value = MysqlConst._LeftContains(leftValue, IsNot, conditions);
                    break;
                case MethodConst._RightContains:

                    value = MysqlConst._RightContains(leftValue,IsNot,conditions);
                    break;
                case MethodConst._Round:

                    value = MysqlConst._Round(leftValue, conditions); 
                    break;
                case MethodConst._Truncate:

                    value = MysqlConst._Truncate(leftValue,conditions);
                    break;
                case MethodConst._DATEDIFF:

                    value = MysqlConst._DATEDIFF(leftValue,conditions);
                    break;
                case MethodConst._Now:

                    value = MysqlConst._NOW();
                    break;
                case MethodConst._Year:
                    value = MysqlConst._Year(leftValue);
                    break;
                case MethodConst._Month:
                    value = MysqlConst._Month(leftValue);
                    break;
                case MethodConst._Day:
                    value = MysqlConst._Day(leftValue);
                    break;
                default:
                    break;
            }
            return value;
        }
    }
}
