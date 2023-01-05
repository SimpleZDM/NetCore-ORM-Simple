using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NetCore.ORM.Simple.Common
{
    public static class ExtensionAttribute
    {
        private static Type ColumnType { get; set; }
        private static Type TableType {get; set;}
        static ExtensionAttribute(){

            TableType = typeof(TableNameAttribute);
            ColumnType = typeof(ColNameAttribute);
        }

        public static void SetAttr(Type tableType,Type columnType)
        {
            if (!Check.IsNull(tableType))
            {
                TableType = tableType;
            }

            if (!Check.IsNull(columnType))
            {
                ColumnType = columnType;
            }
          
        }
        public static string GetTableName(this Type type)
        {
            if (Check.IsNull(type))
            {
                return null;
            }
            if (type.IsDefined(TableType, false))
            {
                IName classNameAttribute = (IName)type.GetCustomAttributes(TableType, false)[0];
                string Name = classNameAttribute.GetName();
                if (!Check.IsNullOrEmpty(Name))
                {
                    return Name;
                }
            }
            return type.Name;
        }
        public static string GetColName(this PropertyInfo prop)
        {
            if (Check.IsNull(prop))
            {
                return null;
            }
           
            if (prop.IsDefined(ColumnType, false))
            {
                IName classNameAttribute = (IName)prop.GetCustomAttributes(ColumnType, false)[0];
                string Name = classNameAttribute.GetName();
                if (!Check.IsNullOrEmpty(Name))
                {
                    return Name;
                }
            }
            return prop.Name;
        }

        /// <summary>
        /// 获取不包含自增主键，和忽略的列
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<PropertyInfo> GetNotKeyAndIgnore(this Type type)
        {
            return GetPropertyInfos(type, column =>!column.AutoIncrease && !column.Ignore);
        }


        public static IEnumerable<PropertyInfo> GetNoIgnore(this Type type)
        {
            return GetPropertyInfos(type,column=> !column.Ignore);
        }
        public static PropertyInfo GetKey(this Type type)
        {
            return GetPropertyInfos(type, column =>column.Key,true).FirstOrDefault();
        }
        public static PropertyInfo GetAutoKey(this Type type)
        {
            return GetPropertyInfos(type, column => column.Key&&column.AutoIncrease,true).FirstOrDefault();
        }
        public static IEnumerable<PropertyInfo> GetNoIgnore(this Type type, Func<PropertyInfo, bool> func)
        {
            IEnumerable<PropertyInfo> props = type.GetProperties().
                Where(t => func.Invoke(t));
            if (props == null)
            {
                return null;
            }
            return props;
        }

     

        public static void SetPropValue(this PropertyInfo Prop,object data, object vData)
        {
            if (Check.IsNull(Prop) || Check.IsNull(data))
            {
                return;
            }
            if (Prop.PropertyType == typeof(int))
            {
                int.TryParse(vData.ToString(), out int value);
                Prop.SetValue(data, value);
            }
            else if (Prop.PropertyType == typeof(float))
            {
                float.TryParse(vData.ToString(), out float value);
                Prop.SetValue(data, value);
            }
            else if (Prop.PropertyType == typeof(double))
            {
                double.TryParse(vData.ToString(), out double value);
                Prop.SetValue(data, value);
            }
            else if (Prop.PropertyType == typeof(decimal))
            {
                decimal.TryParse(vData.ToString(), out decimal value);
                Prop.SetValue(data, value);
            }
            else if (Prop.PropertyType == typeof(string))
            {
                Prop.SetValue(data, vData.ToString());
            }
            else if (Prop.PropertyType == typeof(DateTime))
            {
                DateTime.TryParse(vData.ToString(), out DateTime value);
                Prop.SetValue(data, value);
            }
            else if (Prop.PropertyType == typeof(TimeSpan))
            {
                TimeSpan.TryParse(vData.ToString(), out TimeSpan value);
                Prop.SetValue(data, value);
            }
            else if (Prop.PropertyType == typeof(Guid))
            {
                Guid.TryParse(vData.ToString(), out Guid value);
                Prop.SetValue(data, value);
            }
        }

        public static object SetPropValue(this Type type,object vData)
        {
            if (Check.IsNull(type))
            {
                return null;
            }
            if (type == typeof(int))
            {
                int.TryParse(vData.ToString(), out int value);
                return value;
            }
            else if (type == typeof(float))
            {
                float.TryParse(vData.ToString(), out float value);
                return value;
            }
            else if (type == typeof(decimal))
            {
                decimal.TryParse(vData.ToString(), out decimal value);
                return value;
            }
            else if (type == typeof(string))
            {
                return vData.ToString();
            }
            else if (type == typeof(DateTime))
            {
                DateTime.TryParse(vData.ToString(), out DateTime value);
                return value;
            }
            else if (type == typeof(TimeSpan))
            {
                TimeSpan.TryParse(vData.ToString(), out TimeSpan value);
                return value;
            }
            else if (type == typeof(Guid))
            {
                Guid.TryParse(vData.ToString(), out Guid value);
                return value;
            }
            return null;
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfos(Type type,Func<IColumn,bool> func,bool declare=false)
        {
            if (Check.IsNull(type))
            {
                throw new ArgumentNullException();
            }
           
            IEnumerable<PropertyInfo> props = type.GetProperties().
                Where(p => {
                    bool value = false;
                    if (p.IsDefined(ColumnType, false) == false&&declare==false)
                    {
                        return true;
                    }
                    if (p.GetCustomAttribute(ColumnType, false) is IColumn column)
                    {
                        if (Check.IsNull(column))
                        {
                            throw new Exception("使用自定义特性请继承IColumn!");
                        }
                        if (!Check.IsNull(func))
                        {
                            value = func.Invoke(column);
                        }
                    }
                    return value;
                });
            return props;
        }

    }
}
