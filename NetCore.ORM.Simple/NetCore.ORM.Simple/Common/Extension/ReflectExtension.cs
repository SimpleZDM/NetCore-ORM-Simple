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
            return type.GetTableName();
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
        public static string[] GetTypeName<T, T2, T3, T4,T5>()
        {
            string[] names = new string[5];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5,T6>()
        {
            string[] names = new string[6];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>( );
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5, T6,T7>()
        {
            string[] names = new string[7];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            names[6] = GetTypeName<T7>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5, T6, T7,T8>()
        {
            string[] names = new string[8];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            names[6] = GetTypeName<T7>();
            names[7] = GetTypeName<T8>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5, T6, T7, T8,T9>()
        {
            string[] names = new string[9];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            names[6] = GetTypeName<T7>();
            names[7] = GetTypeName<T8>();
            names[8] = GetTypeName<T9>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5, T6, T7, T8, T9,T10>()
        {
            string[] names = new string[10];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            names[6] = GetTypeName<T7>();
            names[7] = GetTypeName<T8>();
            names[8] = GetTypeName<T9>();
            names[9] = GetTypeName<T10>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5, T6, T7, T8, T9,T10,T11>()
        {
            string[] names = new string[11];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            names[6] = GetTypeName<T7>();
            names[7] = GetTypeName<T8>();
            names[8] = GetTypeName<T9>();
            names[9] = GetTypeName<T10>();
            names[10] = GetTypeName<T11>();
            return names;
        }
        public static string[] GetTypeName<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12>()
        {
            string[] names = new string[12];
            names[0] = GetTypeName<T>();
            names[1] = GetTypeName<T2>();
            names[2] = GetTypeName<T3>();
            names[3] = GetTypeName<T4>();
            names[4] = GetTypeName<T5>();
            names[5] = GetTypeName<T6>();
            names[6] = GetTypeName<T7>();
            names[7] = GetTypeName<T8>();
            names[8] = GetTypeName<T9>();
            names[9] = GetTypeName<T10>();
            names[10] = GetTypeName<T11>();
            names[11] = GetTypeName<T12>();
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
            types[1] = GetType<T2>();
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
        public static Type[] GetType<T, T2, T3, T4,T5>()
        {
            Type[] types = new Type[5];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            return types;
        }
        public static Type[] GetType<T,T2, T3, T4, T5,T6>()
        {
            Type[] types = new Type[6];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            return types;
        }
        public static Type[] GetType<T, T2, T3, T4, T5, T6,T7>()
        {
            Type[] types = new Type[7];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            types[6] = GetType<T7>();
            return types;
        }

        public static Type[] GetType<T, T2, T3, T4, T5, T6, T7,T8>()
        {
            Type[] types = new Type[8];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            types[6] = GetType<T7>();
            types[7] = GetType<T8>();
            return types;
        }
        public static Type[] GetType<T, T2, T3, T4, T5, T6, T7, T8,T9>()
        {
            Type[] types = new Type[9];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            types[6] = GetType<T7>();
            types[7] = GetType<T8>();
            types[8] = GetType<T9>();
            return types;
        }
        public static Type[] GetType<T, T2, T3, T4, T5, T6, T7, T8, T9,T10>()
        {
            Type[] types = new Type[10];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            types[6] = GetType<T7>();
            types[7] = GetType<T8>();
            types[8] = GetType<T9>();
            types[9] = GetType<T10>();
            return types;
        }
        public static Type[] GetType<T, T2, T3, T4, T5, T6, T7, T8, T9, T10,T11>()
        {
            Type[] types = new Type[11];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            types[6] = GetType<T7>();
            types[7] = GetType<T8>();
            types[8] = GetType<T9>();
            types[9] = GetType<T10>();
            types[10] = GetType<T11>();
            return types;
        }
        public static Type[] GetType<T, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11,T12>()
        {
            Type[] types = new Type[12];
            types[0] = GetType<T>();
            types[1] = GetType<T2>();
            types[2] = GetType<T3>();
            types[3] = GetType<T4>();
            types[4] = GetType<T5>();
            types[5] = GetType<T6>();
            types[6] = GetType<T7>();
            types[7] = GetType<T8>();
            types[8] = GetType<T9>();
            types[9] = GetType<T10>();
            types[10] = GetType<T11>();
            types[11] = GetType<T12>();
            return types;
        }
    }
}
