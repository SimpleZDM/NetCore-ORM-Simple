using NetCore.ORM.Simple.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.ORM.Simple
{
    public  class Simple
    {
        public static bool Contains<T>(T[]datas,T targe)
        {
            return default(bool);
        }
        /// <summary>
        /// 左边模糊查询
        /// </summary>
        /// <param name="value"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool LeftContains(string value, string left)
        {
            return default(bool);
        }
        /// <summary>
        /// 右边模糊查询
        /// </summary>
        /// <param name="value"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool RightContains(string value, string right)
        {
            return default(bool);
        }
        /// <summary>
        /// 日期函数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int DateDiff(DateTime start,DateTime end,eDateType type)
        {
            return default(int);
        }
        /// <summary>
        /// 日期函数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int DateDiff(TimeSpan start, TimeSpan end, eDateType type)
        {
            return default(int);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int Year(DateTime time)
        {
            return default(int);
        }
        public static int Month(DateTime time)
        {
            return default(int);
        }
        public static int Day(DateTime time)
        {
            return default(int);
        }
        public static int Hour(DateTime time)
        {
            return default(int);
        }
        public static int Second(DateTime time)
        {
            return default(int);
        }
        public static DateTime Now()
        {
            return default(DateTime);
        }
        /// <summary>
        /// 向上取整数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static float Round(float value,int len)
        {
            return default(float);
        }
        public static double Round(double value, int len)
        {
            return default(double);
        }
        public static decimal Round( decimal value, int len)
        {
            return default(decimal);
        }
        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static float Truncate( float value, int len)
        {
            return default(float);
        }
        public static double Truncate( double value, int len)
        {
            return default(double);
        }
        public static decimal Truncate( decimal value, int len)
        {
            return default(decimal);
        }

        public static bool IsNullOrEmpty( string strValue)
        {
            return default(bool);
        }

        public static bool IsNull<T>(T t)
        {
            return default(bool);
        }
        /// <summary>
        /// 满足条件返回t1 不满足条件返回第二个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static T IF<T>(bool condition,T t1,T t2)
        {
            return default(T);
        }

        public  Simple ElseIF<T>(bool condition, T t1)
        {
            return this;
        }
        public T End<T>(T t1)
        {
            return default(T);
        }
        public static Simple IF<T>(bool condition, T t1)
        {
            return new Simple();
        }

    }
}
