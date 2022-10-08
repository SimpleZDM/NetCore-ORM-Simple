using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client.inter
 * 接口名称 ISimpleCommand
 * 开发人员：-nhy
 * 创建时间：2022/9/21 14:15:06
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Client
{
    public interface ISimpleCommand<TEntity>
    {
        public  Task<int> SaveChangeAsync();

        public  Task<TEntity> ReturnEntityAsync();
    }
}
