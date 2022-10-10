using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common.Extension
 * 接口名称 ArrayExtensions
 * 开发人员：-nhy
 * 创建时间：2022/10/10 9:40:31
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    public class ArrayExtension
    {
        public static string GetValue<T>(T t,object obj)
        {
            string value = string.Empty; ;
            if (t == null) return value;
            foreach (var item in (dynamic)obj)
            {
                if (t.Equals(item.Key))
                {
                    value =$"{item.Value}";
                }
            }
            return value;
        }
        public static string GetValue<T>(T t, object obj,PropertyInfo prop)
        {
            string value = null;
            if (t == null) return null;
            foreach (var item in (dynamic)obj)
            {
                if (t.Equals(item.Key))
                {
                    value = $"{prop.GetValue(item.Value)}";
                }
            }
            return value;
        }
        public static string GetValue<T>(T t, object obj, FieldInfo field)
        {
            string value = string.Empty; ;
            if (t == null) return value;
            foreach (var item in (dynamic)obj)
            {
                if (t.Equals(item.Key))
                {
                    value = $"{field.GetValue(item.Value)}";
                }
            }
            return value;
        }
    }
}
