using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class SqlCommandX
    {
        /// <summary>
        /// 将指定的 System.Data.SqlClient.SqlParameter 对象添加到 System.Data.SqlClient.SqlParameterCollection 中。
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ParameterName">获取或设置 System.Data.SqlClient.SqlParameter 的名称。</param>
        /// <param name="SqlDbType">获取或设置参数的 System.Data.SqlDbType。</param>
        /// <param name="Value">获取或设置该参数的值。</param>
        public static void AddParameters(this global::System.Data.SqlClient.SqlCommand command, string ParameterName, global::System.Data.SqlDbType SqlDbType, object Value)
        {
            command.Parameters.Add(new global::System.Data.SqlClient.SqlParameter
            {
                ParameterName = ParameterName,
                SqlDbType = SqlDbType,
                Value = Value
            });
        }
        /// <summary>
        /// 将指定的 System.Data.SqlClient.SqlParameter 对象添加到 System.Data.SqlClient.SqlParameterCollection 中。
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ParameterName">获取或设置 System.Data.SqlClient.SqlParameter 的名称。</param>
        /// <param name="Value">获取或设置该参数的值。</param>
        public static void AddParameters(this global::System.Data.SqlClient.SqlCommand command, string ParameterName, object Value)
        {
            command.Parameters.Add(new global::System.Data.SqlClient.SqlParameter(ParameterName, Value));
        }
        /// <summary>
        /// 生成实体类对象集合predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static System.Collections.ObjectModel.ObservableCollection<T> GenericList<T>(this global::System.Data.SqlClient.SqlCommand command)
        {
            System.Collections.ObjectModel.ObservableCollection<T> items = new System.Collections.ObjectModel.ObservableCollection<T>();
            object instance = Activator.CreateInstance(typeof(T));//创建指定类型实例
            System.Reflection.PropertyInfo[] fields = instance.GetType().GetProperties();
            using (System.Data.SqlClient.SqlDataReader sdr = command.ExecuteReader())
            {
                while (sdr.Read())
                {
                    object entity = Activator.CreateInstance(typeof(T));
                    foreach (System.Reflection.PropertyInfo property in fields)
                    {
                        property.SetValue(entity, sdr[property.Name], null);
                    }
                    items.Add((T)entity);
                }
            }
            return items;
        }
        /// <summary>
        /// 生成实体类对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static T GenericObject<T>(this global::System.Data.SqlClient.SqlCommand command)
        {
            object instance = Activator.CreateInstance(typeof(T));
            System.Reflection.PropertyInfo[] fields = instance.GetType().GetProperties();
            using (System.Data.SqlClient.SqlDataReader sdr = command.ExecuteReader(System.Data.CommandBehavior.SingleRow))
            {
                object entity = Activator.CreateInstance(typeof(T));
                if (sdr.Read())
                {
                    foreach (System.Reflection.PropertyInfo property in fields)
                    {
                        property.SetValue(entity, sdr[property.Name], null);
                    }
                }
                return ((T)entity);
            }
        }
    }
}
