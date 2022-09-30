using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Entity
 * 接口名称 QueryEntity
 * 开发人员：-nhy
 * 创建时间：2022/9/28 14:03:04
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Entity
{
    public class QueryEntity:SqlBase
    {
        public QueryEntity()
        {
            StrSqlValue = new StringBuilder();
            DbParams = new List<DbParameter>(10);
            DyToMap = new List<dynamic>();
            PageSize = -1;
            PageNumber = -1;
        }
     
        /// <summary>
        /// 取出的数据
        /// </summary>
        public int PageNumber { get { return pageNumber; } set { pageNumber = value; } }
        /// <summary>
        /// 跳过的数据
        /// </summary>
        public int PageSize { get { return pageSize; } set { pageSize = value; } }


        /// <summary>
        ///映射信息
        /// </summary>
        public MapEntity[] MapInfos { get { return mapInfos; } set { mapInfos = value; } }
        /// <summary>
        ///映射
        /// </summary>
        public List<dynamic>DyToMap { get { return dyToMap; } set { dyToMap = value; } }

        public List<Type> LastType { get { return lastType; } set { lastType = value; } }

       

        //public Type 
        /// <summary>
        /// 最后一个是否是匿名对象
        /// </summary>
        public bool LastAnonymity { get { return lastAnonymity; } set { lastAnonymity = value; } }

        public TResult GetResult<TResult>(params object [] objs)
        {
            object obj = GetResult<TResult>(DyToMap[0], objs);

            for (int i = 1; i < DyToMap.Count; i++)
            {
               
                obj=DyToMap[i].Invoke(obj);
            }
            return (TResult)obj;
        }

        public object GetResult<TResult>(dynamic func,object[] objs)
        {
            if (lastType.Count>objs.Count())
            {
                throw new Exception();
            }
            switch (LastType.Count)
            {
                case 1:
                    var a = func.Invoke((dynamic)objs[0]);
                    return a;
                case 2:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1]);
                case 3:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2]);
                case 4:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3]);
                case 5:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3],(dynamic)objs[4]);
                case 6:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3],(dynamic)objs[4],(dynamic)objs[5]);
                case 7:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3],(dynamic)objs[4],(dynamic)objs[5],(dynamic)objs[6]);
                case 8:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3],(dynamic)objs[4],(dynamic)objs[5],(dynamic)objs[6],(dynamic)objs[7]);
                case 9:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3],(dynamic)objs[4],(dynamic)objs[5],(dynamic)objs[6],(dynamic)objs[7],(dynamic)objs[8]);
                case 10:
                    return func.Invoke((dynamic)objs[0],(dynamic)objs[1],(dynamic)objs[2],(dynamic)objs[3],(dynamic)objs[4],(dynamic)objs[5],(dynamic)objs[6],(dynamic)objs[7],(dynamic)objs[8],(dynamic)objs[9]);
                default:
                    break;
            }
            return null;
            
        }
        

        private MapEntity[] mapInfos;
        private int pageNumber;
        private int pageSize;
        private List<dynamic> dyToMap;
        private bool lastAnonymity;
        private List<Type> lastType;


    }
}
