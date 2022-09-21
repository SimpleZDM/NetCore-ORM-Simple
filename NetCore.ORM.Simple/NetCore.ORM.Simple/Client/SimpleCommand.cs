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
    public class SimpleCommand<TEntity>:ISimpleCommand<TEntity>
    {
        private List<SqlEntity> sqls;
        private SqlEntity currentSql;
        private Builder builder;

        public SimpleCommand(Builder builder,eDBType dbType,SqlEntity sql,List<SqlEntity>_sqls)
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
        public TEntity ReturnEntity()
        {
            ///执行完了之后
            switch (currentSql.DbCommandType)
            {
                case eDbCommandType.Insert:
                    builder.GetLastInsert<TEntity>(currentSql);
                    break;
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
