using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*********************************************************
 * 命名空间 MDT.VirtualSoftPlatform.Common
 * 接口名称 ExpressionConvert
 * 开发人员：-nhy
 * 创建时间：2022/7/12 14:01:21
 * 描述说明：
 * 更改历史：
 * 
 * *******************************************************/
namespace MDT.VirtualSoftPlatform.Common
{
    public class ExpressionConvert:IExpressionConvert
    {

        public ExpressionConvert()
        {

        }
        /// <summary>
        /// 原类型映射成指定类型
        /// </summary>
        /// <typeparam name="TSource">原类型</typeparam>
        /// <typeparam name="TTarget">映射之后的目标类型</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TTarget Map<TSource, TTarget>(TSource source) where TTarget : class
        {

            Func<TSource, TTarget> _func = null;
            try
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "s");
                List<MemberBinding> memberBindings = new List<MemberBinding>();
                foreach (var item in typeof(TTarget).GetProperties())
                {
                    var sProp = typeof(TSource).GetProperty(item.Name);
                    if (sProp != null && sProp.PropertyType == item.PropertyType && sProp.CanWrite && item.CanWrite&& IsNull(source,sProp))
                    {
                        MemberExpression property = Expression.Property(parameterExpression, sProp);
                        MemberBinding memberBinding = Expression.Bind(item, property);
                        memberBindings.Add(memberBinding);
                    }
                }
                foreach (var item in typeof(TTarget).GetFields())
                {
                    var sField = typeof(TSource).GetField(item.Name);
                    if (sField != null && sField.FieldType == item.FieldType && IsNull(source,sField))
                    {
                        MemberExpression property = Expression.Field(parameterExpression, sField);
                        MemberBinding memberBinding = Expression.Bind(item, property);
                        memberBindings.Add(memberBinding);
                    }
                }
                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TTarget)), memberBindings);

                Expression<Func<TSource, TTarget>> lamada = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, new ParameterExpression[] {
            parameterExpression
            });
                //_func

               _func= lamada.Compile();
            }
            catch (Exception ex)
            {
                // LoggerHelper.Log4netHelper.Error($"{typeof(TSource).Name} 转 {typeof(TTarget).Name} 出错：{ex.Message}");
            }
            if (_func == null)
            {
                return null;
            }
            return _func.Invoke(source);
        }


        public void Map<TSource, TTarget>(TSource source,TTarget target)
        {
            Type tSource=typeof(TSource);
            Type tTarget=typeof(TTarget);
            foreach (var item in tSource.GetProperties())
            {
                var sProp= tTarget.GetProperty(item.Name);
                if (sProp!=null && sProp.PropertyType == item.PropertyType && IsNull(source,item))
                {
                    sProp.SetValue(target,item.GetValue(source));
                }
            }

            foreach (var item in tSource.GetFields())
            {
                var sField = tTarget.GetField(item.Name);
                if (sField != null && sField.FieldType == item.FieldType && IsNull(source, item))
                {
                    sField.SetValue(target, item.GetValue(source));
                }
            }
        }


        private bool IsNull<TSource>(TSource source, PropertyInfo member)
        {
            try
            {
                
                if (member.PropertyType.Equals(typeof(int)))
                {
                    object obj = member.GetValue(source);
                    int res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.PropertyType.Equals(typeof(float)))
                {
                    object obj = member.GetValue(source);
                    float res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.PropertyType.Equals(typeof(double)))
                {
                    object obj = member.GetValue(source);
                    double res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.PropertyType.Equals(typeof(decimal)))
                {
                    object obj = member.GetValue(source);
                    decimal res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.PropertyType.Equals(typeof(DateTime)))
                {
                    object obj = member.GetValue(source);
                    DateTime res = (DateTime)obj;
                    if (res == DateTime.MinValue)
                    {
                        return false;
                    }
                    return true;
                }

                else if (member.PropertyType.Equals(typeof(Guid)))
                {
                    object obj = member.GetValue(source);
                    Guid res = (Guid)obj;
                    if (res == Guid.Empty)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.PropertyType.Equals(typeof(string)))
                {
                    object obj = member.GetValue(source);
                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch(Exception e)
            {

            }
            return false;
        }

        private bool IsNull<TSource>(TSource source, FieldInfo member)
        {
            try
            {
                if (member.FieldType.Equals(typeof(int)))
                {
                    object obj = member.GetValue(source);
                    int res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.FieldType.Equals(typeof(float)))
                {
                    object obj = member.GetValue(source);
                    float res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.FieldType.Equals(typeof(double)))
                {
                    object obj = member.GetValue(source);
                    double res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.FieldType.Equals(typeof(decimal)))
                {
                    object obj = member.GetValue(source);
                    decimal res = (int)obj;
                    if (res == -1)
                    {
                        return false;
                    }
                    return true;
                }
                else if (member.FieldType.Equals(typeof(DateTime)))
                {
                    object obj = member.GetValue(source);
                    DateTime res = (DateTime)obj;
                    if (res == DateTime.MaxValue)
                    {
                        return false;
                    }
                    return true;
                }

                else if (member.FieldType.Equals(typeof(Guid)))
                {
                    object obj = member.GetValue(source);
                    Guid res = (Guid)obj;
                    if (res == Guid.Empty)
                    {
                        return false;
                    }
                    return true;
                }

                else if (member.FieldType.Equals(typeof(Guid)))
                {
                    object obj = member.GetValue(source);
                    Guid res = (Guid)obj;
                    if (res == Guid.Empty)
                    {
                        return false;
                    }
                    return true;
                }

                else if (member.FieldType.Equals(typeof(string)))
                {
                    object obj = member.GetValue(source);
                    if (obj == null || string.IsNullOrEmpty(obj.ToString()))
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }


    }
}
