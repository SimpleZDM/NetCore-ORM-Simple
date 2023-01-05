using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Common
 * 接口名称 IExpressionConvert
 * 开发人员：-nhy
 * 创建时间：2022/7/12 14:39:33
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Common
{
    public interface IExpressionConvert
    {
        public TTarget Map<TSource, TTarget>(TSource source) where TTarget : class;
        public void Map<TSource, TTarget>(TSource source, TTarget target);
    }
}
