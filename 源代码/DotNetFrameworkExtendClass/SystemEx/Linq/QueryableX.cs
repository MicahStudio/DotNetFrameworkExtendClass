using System;
using System.Linq;
using System.Linq.Expressions;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// ASC方式
        /// </summary>
        OrderBy = 0,
        /// <summary>
        /// Descing方式
        /// </summary>
        OrderByDescending = 1
    }
    /// <summary>
    /// 提供一组用于查询实现 System.Linq.IQueryable&lt;T&gt; 的数据结构的 static（在 Visual Basic 中为 Shared）方法。
    /// </summary>
    public static class QueryableX
    {
        /// <summary>
        /// 根据键对序列的元素排序。
        /// </summary>
        /// <typeparam name="TSource">source 中的元素的类型。</typeparam>
        /// <param name="source">一个要排序的值序列。</param>
        /// <param name="OrderParameter">要进行排序的字段</param>
        /// <param name="OrderDirection">排序方式</param>
        /// <returns>一个 System.Linq.IQueryable&lt;TSource&gt;，将根据某个键按排序方式对其元素进行排序。</returns>
        public static IQueryable<TSource> Sorting<TSource>(this IQueryable<TSource> source, string OrderParameter, SortDirection OrderDirection)
        {
            ParameterExpression param = Expression.Parameter(typeof(TSource), OrderParameter);
            Expression expr = Expression.Call(
                typeof(Queryable),
                Enum.GetName(
                    typeof(SortDirection),
                    OrderDirection),
                new Type[2] { 
                    typeof(TSource), 
                    typeof(TSource).GetProperty(OrderParameter).PropertyType},
                source.Expression,
                Expression.Lambda(
                    Expression.Property(
                        param,
                        OrderParameter),
                    param
                )
            );
            return source.Provider.CreateQuery<TSource>(expr);
        }
    }
}
