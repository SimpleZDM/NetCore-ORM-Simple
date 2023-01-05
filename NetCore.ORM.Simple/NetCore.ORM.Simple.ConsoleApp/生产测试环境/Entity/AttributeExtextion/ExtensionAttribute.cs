using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace MDT.VirtualSoftPlatform.Common
{
    public static class ExtensionAttribute
    {
        /// <summary>
        /// 校准接口参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        //public static bool Verify<T>(this T t, out string messgae) where T : IModeVerify
        //{
        //    bool IsResualt = true;
        //    Type type = typeof(T);
        //    messgae = "";
        //    foreach (var prop in type.GetProperties().Where(prop => prop.IsDefined(typeof(ParaVerifyAttribute), false)))
        //    {
        //        ParaVerifyAttribute VerifyAttr = (ParaVerifyAttribute)prop.GetCustomAttributes(typeof(ParaVerifyAttribute),false).ToArray()[0];
        //        if (prop.PropertyType == typeof(string)||prop.PropertyType==typeof(Guid))
        //        {
        //            string val = prop.GetValue(t)==null ? string.Empty : prop.GetValue(t).ToString();
        //            if (VerifyAttr.GetIsEmpty()== false && 
        //                (string.IsNullOrEmpty(val)|| 
        //                val.Length < VerifyAttr.GetLength()||
        //                val==Guid.Empty.ToString()))
        //            {  //string 类型验证失败
        //                messgae = VerifyAttr.GetDesc();
        //                IsResualt = false;
        //                break;
        //            }
        //        }
        //        else if (prop.PropertyType == typeof(Int16) || prop.PropertyType == typeof(Int32) || prop.PropertyType == typeof(Int64))
        //        {
                    
        //            int val;
        //            int.TryParse(prop.GetValue(t).ToString(), out val);
        //            if (val > VerifyAttr.GetMax() || val < VerifyAttr.GetMin())
        //            {
        //                IsResualt = false;
        //                messgae = VerifyAttr.GetDesc();
        //                break;
        //            }
        //        }else if (typeof(IFormFile)==prop.PropertyType)
        //        {
        //            if (prop.GetValue(t) == null)
        //            {
        //                messgae = VerifyAttr.GetDesc();
        //                IsResualt = false;
        //                break;
        //            }
        //        }
        //    }
        //    return IsResualt;
        //}

        public static string GetClassName(this Type type)
        {
            if (type.IsDefined(typeof(ClassNameAttribute), false))
            {
                ClassNameAttribute classNameAttribute = (ClassNameAttribute)type.GetCustomAttributes(typeof(ClassNameAttribute),false)[0];
                return classNameAttribute.GetName();
            }
            else
            {
                return type.Name;
            }
        }

        public static string GetColName(this PropertyInfo prop)
        {
            if (prop.IsDefined(typeof(ColNameAttribute), false))
            {
                ColNameAttribute classNameAttribute = (ColNameAttribute)prop.GetCustomAttributes(typeof(ColNameAttribute),false)[0];
                return classNameAttribute.GetName();
            }
            else
            {
                return prop.Name;
            }
        }


        public static IEnumerable<PropertyInfo> GetNoIgnore(this Type type)
        {
            IEnumerable<PropertyInfo> props=type.GetProperties().
                Where(t=>t.IsDefined(typeof(IgnoreAttribute),false)==false);
            if (props==null )
            {
                return null;
            }
            return props;
        }
        
        public static IEnumerable<PropertyInfo> GetNoIgnore(this Type type,Func<PropertyInfo,bool> func)
        {
            IEnumerable<PropertyInfo> props = type.GetProperties().
                Where(t=>func.Invoke(t));
            if (props == null)
            {
                return null;
            }
            return props;
        }

        public static T DicMapEntity<T>(this Dictionary<string,int> dic)
        {
            Type type = typeof(T);
            object obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                if (dic.ContainsKey(prop.GetColName()))
                {
                    prop.SetValue(obj,dic[prop.GetColName()]);
                }
            }

            return (T)obj;
        }
        public static T DicMapEntity<T,TValue>(this Dictionary<string,TValue> dic)
        {
            Type type = typeof(T);
            object obj = Activator.CreateInstance(type);

            foreach (var prop in type.GetProperties())
            {
                if (dic.ContainsKey(prop.GetColName()))
                {
                    prop.SetValue(obj, dic[prop.GetColName()]);
                }
            }

            return (T)obj;
        }
    }
}
