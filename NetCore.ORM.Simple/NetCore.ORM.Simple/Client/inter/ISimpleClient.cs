using NetCore.ORM.Simple.Queryable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Client.inter
 * 接口名称 ISimpleClient
 * 开发人员：-nhy
 * 创建时间：2022/9/20 17:40:52
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Client
{
    public interface ISimpleClient
    {
        public ISimpleQueryable<T1, T2> Queryable<T1, T2>();
    }
}
