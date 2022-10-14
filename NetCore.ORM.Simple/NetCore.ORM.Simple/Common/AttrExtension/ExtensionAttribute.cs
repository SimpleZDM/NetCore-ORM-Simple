using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace NetCore.ORM.Simple.Common
{
    public static class ExtensionAttribute
    {
        public static string GetTableName(this Type type,Type AttrTable)
        {
            if (Check.IsNull(type))
            {
                return null;
            }
            if (Check.IsNull(AttrTable))
            {
                AttrTable = typeof(TableNameAttribute);
            }
            if (type.IsDefined(typeof(TableNameAttribute), false))
            {
                IName classNameAttribute = (IName)type.GetCustomAttributes(AttrTable, false)[0];
                string Name = classNameAttribute.GetName();
                if (!Check.IsNullOrEmpty(Name))
                {
                    return Name;
                }
            }
            return type.Name;
        }
        public static string GetColName(this PropertyInfo prop, Type type)
        {
            if (Check.IsNull(prop))
            {
                return null;
            }
            if (Check.IsNull(type))
            {
                type = typeof(ColNameAttribute);
            }
            if (prop.IsDefined(type, false))
            {
                IName classNameAttribute = (IName)prop.GetCustomAttributes(type, false)[0];
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
        public static IEnumerable<PropertyInfo> GetNotKeyAndIgnore(this Type type, Type ColumnType)
        {
            return GetPropertyInfos(type, column =>!column.AutoIncrease && !column.Ignore, ColumnType);
        }


        public static IEnumerable<PropertyInfo> GetNoIgnore(this Type type, Type ColumnType)
        {
            return GetPropertyInfos(type,column=> !column.Ignore, ColumnType);
        }
        public static PropertyInfo GetKey(this Type type,Type ColumnType)
        {
            return GetPropertyInfos(type, column =>column.Key,ColumnType).FirstOrDefault();
        }
        public static PropertyInfo GetAutoKey(this Type type, Type ColumnType)
        {
            return GetPropertyInfos(type, column => column.Key&&column.AutoIncrease, ColumnType).FirstOrDefault();
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

     

        public static void SetPropValue(this PropertyInfo Prop, object data, object vData)
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

        private static IEnumerable<PropertyInfo> GetPropertyInfos(Type type,Func<IColumn,bool> func,Type ColumnType)
        {
            if (Check.IsNull(type))
            {
                throw new ArgumentNullException();
            }
            if (Check.IsNull(ColumnType))
            {
                ColumnType = typeof(ColNameAttribute);
            }
            IEnumerable<PropertyInfo> props = type.GetProperties().
                Where(p => {
                    bool value = false;
                    if (p.IsDefined(ColumnType, false) == false)
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
