using MySql.Data.MySqlClient;
using NetCore.ORM.Simple.Common;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class MysqlBuilder: ISqlBuilder
    {

        private const int INSERTMAX = 800;
        private const char charConnectSign = '_';
        
        //private JoinVisitor joinVisitor;
        //private ConditionVisitor conditionVisitor;
        //private MapVisitor mapVisitor;
        private string[] strJoins = new string[] {"INNER JOIN","LEFT JOIN","RIGHT JOIN"};
        public MysqlBuilder()
        {
           
            //joinVisitor=new JoinVisitor(); 
            //conditionVisitor = new ConditionVisitor();
            //mapVisitor = new MapVisitor();
        }
        /// <summary>
        /// 单挑sql语句生成
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        public SqlEntity GetInsert<TData>(TData data, int random = 0)
        {
            SqlEntity sql = new SqlEntity();
            Type type = typeof(TData);
            var Props = type.GetNotKeyAndIgnore();

            sql.Sb_Sql.Append($"INSERT INTO {type.GetClassName()} ");
            sql.Sb_Sql.Append("(");
            sql.Sb_Sql.Append(string.Join(',', Props.Select(p => p.GetColName())));
            sql.Sb_Sql.Append(") ");
            sql.Sb_Sql.Append(" VALUE(");
            sql.Sb_Sql.Append(string.Join(',',
                Props.Select(p => {
                    string key = $"@{random}{p.GetColName()}";
                    sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                    return key;
                })));
            sql.Sb_Sql.Append(");");
            return sql;
        }
        /// <summary>
        /// 更新单挑sql语句
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public SqlEntity GetUpdate<TData>(TData data)
        {
            SqlEntity sql = new SqlEntity();
            Type type = typeof(TData);
            var Props = type.GetNoIgnore();
            var pKey = type.GetKey();
            string keyName = $"@{pKey.GetColName()}";
            sql.DbParams.Add(new MySqlParameter(keyName, pKey.GetValue(data)));


            sql.Sb_Sql.Append($"UPDATE {type.GetClassName()} SET ");

            sql.Sb_Sql.Append(string.Join(',',
                Props.Select(p => {
                    string key = $"@{p.GetColName()}";
                    sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                    return key;
                })));
            if (Check.IsNull(pKey))
            {
                throw new Exception("没有主键请为实体设置主键!");
            }
            sql.Sb_Sql.Append(" Where ");

            sql.Sb_Sql.Append($"{pKey.GetColName()}={keyName}");
            sql.Sb_Sql.Append(";");
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
                  Props.Select(p => {
                      string key = $"@{Index}{charConnectSign}{p.GetColName()}";
                      sql.DbParams.Add(new MySqlParameter(key, p.GetValue(data)));
                      return key;
                  })));
                sql.Sb_Sql.Append(" (");
                if (count == INSERTMAX)
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
        public SqlEntity GetSelect<TData>()
        {
            SqlEntity sql = new SqlEntity();
            Type type = typeof(TData);
            sql.Sb_Sql.Append($"SELECT " +
                $"{string.Join(',', type.GetNoIgnore())} " +
                $"FROM {type.GetClassName()} ");
            return sql;
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
        public SqlEntity GetSelect(List<MapEntity>mapInfos,List<JoinTableEntity>joinInfos,string condition)
        {
            SqlEntity sql = new SqlEntity();
            //视图
            sql.Sb_Sql.Append("SELECT ");
            for (int i = 0; i <mapInfos.Count; i++)
            {
                mapInfos[i].AsColumnName= $"{mapInfos[i].TableName}{charConnectSign}{mapInfos[i].ColumnName}";
                sql.Sb_Sql.Append($" {mapInfos[i].TableName}.{mapInfos[i].ColumnName} AS {mapInfos[i].AsColumnName} ");

            }
           
            //连接
            foreach (var join in joinInfos)
            {
                if (join.TableType.Equals(eTableType.Master))
                {
                    sql.Sb_Sql.Append($" FROM {join.DisplayName} ");
                }
                else
                {
                    sql.Sb_Sql.Append($" {strJoins[(int)join.JoinType]} {join.DisplayName} ON ");
                    int length = join.QValue.Count;
                    for (int i = 0; i < length; i++)
                    {
                        sql.Sb_Sql.Append($" {join.QValue.Dequeue()} ");
                    }
                }
            }
            //条件
            sql.Sb_Sql.Append($" Where {condition} ");

            return sql;
        }
    }
}
