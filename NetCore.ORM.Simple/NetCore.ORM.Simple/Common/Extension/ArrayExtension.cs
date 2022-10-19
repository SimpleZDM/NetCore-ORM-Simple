using NetCore.ORM.Simple.Entity;
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
        public static string GetValue<T>(T t, object obj)
        {
            string value = string.Empty; ;
            if (t == null) return value;
            switch (CommonConst.GetType(obj.GetType()))
            {
                case eDataType.SimpleString:
                    break;
                case eDataType.SimpleInt:
                    break;
                case eDataType.SimpleGuid:
                    break;
                case eDataType.SimpleTime:
                    break;
                case eDataType.SimpleFloat:
                    break;
                case eDataType.SimpleDouble:
                    break;
                case eDataType.SimpleDecimal:
                case eDataType.SimpleArrayInt:
                case eDataType.SimpleArrayString:
                case eDataType.SimpleArrayGuid:
                case eDataType.SimpleArrayDouble:
                case eDataType.SimpleArrayFloat:
                case eDataType.SimpleArrayDecimal:
                case eDataType.SimpleListInt:
                case eDataType.SimpleListString:
                case eDataType.SimpleListGuid:
                case eDataType.SimpleListFloat:
                case eDataType.SimpleListDouble:
                case eDataType.SimpleListDecimal:
                case eDataType.SimpleList:
                case eDataType.SimpleArray:
                    dynamic dy = (dynamic)obj;
                    value = $"{dy[t]}";
                    break;
                case eDataType.NuKnow:
                default:
                    foreach (var item in (dynamic)obj)
                    {
                        if (t.Equals(item.Key))
                        {
                            value = $"{item.Value}";
                        }
                    }
                    break;
            }
            return value;
        }
        public static string GetValue<T>(T t, object obj, PropertyInfo prop)
        {
            string value = null;
            if (t == null) return null;

            switch (CommonConst.GetType(obj.GetType()))
            {
                case eDataType.SimpleString:
                    break;
                case eDataType.SimpleInt:
                    break;
                case eDataType.SimpleGuid:
                    break;
                case eDataType.SimpleTime:
                    break;
                case eDataType.SimpleFloat:
                    break;
                case eDataType.SimpleDouble:
                    break;
                case eDataType.SimpleDecimal:
                case eDataType.SimpleArrayInt:
                case eDataType.SimpleArrayString:
                case eDataType.SimpleArrayGuid:
                case eDataType.SimpleArrayDouble:
                case eDataType.SimpleArrayFloat:
                case eDataType.SimpleArrayDecimal:
                case eDataType.SimpleListInt:
                case eDataType.SimpleListString:
                case eDataType.SimpleListGuid:
                case eDataType.SimpleListFloat:
                case eDataType.SimpleListDouble:
                case eDataType.SimpleListDecimal:
                case eDataType.SimpleList:
                case eDataType.SimpleArray:
                    dynamic dy = (dynamic)obj;
                    var o = dy[t].GetType().GetProperty(prop.Name);
                    if (o==null)
                    {
                        value = $"{dy[t]}";
                    }
                    else
                    {
                        value = $"{prop.GetValue(dy[t])}";
                    }
                    break;
                case eDataType.NuKnow:
                default:
                    foreach (var item in (dynamic)obj)
                    {
                        if (t.Equals(item.Key))
                        {
                             o = item.Value.GetType().GetProperty(prop.Name);
                            if (o!=null)
                            {
                                value = $"{prop.GetValue(item.Value)}";
                            }
                            else
                            {
                                value = $"{item.Value}";
                            }
                        }
                    }
                    break;
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
