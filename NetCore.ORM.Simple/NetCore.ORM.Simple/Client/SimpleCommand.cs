using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCore.ORM.Simple.Entity;
using NetCore.ORM.Simple.SqlBuilder;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client
 * 接口名称 SimpleCommand
 * 开发人员：-nhy
 * 创建时间：2022/9/21 14:14:19
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Client
{
    public class SimpleCommand<TEntity>:ISimpleCommand<TEntity>where TEntity : class
    {
        private List<SqlEntity> sqls;
        private SqlEntity currentSql;
        private Builder builder;
        private DBDrive dbDrive;

        public SimpleCommand(Builder builder,eDBType dbType,SqlEntity sql,List<SqlEntity>_sqls,DBDrive dBDrive)
        {
            sqls = _sqls;
            currentSql= sql;
            this.builder = builder;
        }
        public bool GetResult()
        {
            ///执行完了之后
            sqls.Remove(currentSql);
            return true;
        }
        public async Task<TEntity> ReturnEntityAsync()
        {

            ///执行完了之后
            switch (currentSql.DbCommandType)
            {
                case eDbCommandType.Insert:
                     SqlEntity GetInsertSql=new SqlEntity();
                     builder.GetLastInsert<TEntity>(GetInsertSql);
                     return await dbDrive.ExcuteAsync<TEntity>(currentSql,GetInsertSql.StrSqlValue.ToString());
                case eDbCommandType.Update:
                    break;
                default:
                    break;
            }
            sqls.Remove(currentSql);

            return default(TEntity);
        }
    }
}
