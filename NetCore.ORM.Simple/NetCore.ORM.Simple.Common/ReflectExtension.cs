using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 NetCore.ORM.Simple.Common
 * 接口名称 ReflectExtension
 * 开发人员：-nhy
 * 创建时间：2022/9/21 16:22:36
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace NetCore.ORM.Simple.Common
{
    public static class ReflectExtension
    {
        public static string GetTypeName<T>()
        {
            Type type = typeof(T);
            return type.GetClassName();
        }
        public static string[] GetTypeName<T,T2>() 
        {
            string[] names = new string[2];
            names[0] = GetTypeName<T>();
            names[2] = GetTypeName<T2>();
            return names;
        }
        public static string[] GetTypeName<T,T2,T3>()
        {
            string[] names=new string[3];
            names[0]=GetTypeName<T>();
            names[1]=GetTypeName<T2>();
            names[2]=GetTypeName<T3>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3,T4>()
        {
            string[] names = new string[4];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            return names;
        }

        public static Type GetType<T>()
        {
            Type type = typeof(T);
            return type;
        }
        public static Type[] GetType<T, T2>()
        {
            Type[] types = new Type[2];
            types[0] = GetType<T>();
            types[2] = GetType<T2>();
            return types;
        }
        public static Type[] GetType<T, T2, T3>()
        {
            Type[] types = new Type[3];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            return types;
        }
        public static Type[] GetType<T, T2, T3, T4>()
        {
            Type[] types = new Type[4];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            return types;
        }
    }
}
