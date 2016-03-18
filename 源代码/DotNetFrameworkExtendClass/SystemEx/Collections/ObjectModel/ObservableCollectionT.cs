using System.Linq;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class ObservableCollectionT
    {
        /// <summary>
        /// 取得分页的总页数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Source"></param>
        /// <param name="Number">每页显示条数</param>
        /// <returns>总页数</returns>
        public static int PageSize<T>(this global::System.Collections.ObjectModel.ObservableCollection<T> Source, int Number)
        {
            try
            {
                int Count = Source.Count;
                int PageSize = Count % Number == 0 ? (Count / Number) : (Count / Number + 1);
                return PageSize;
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取某页的数据
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="Number">每页显示的条数</param>
        /// <param name="CurrentSize">页码</param>
        /// <returns>数据集</returns>
        public static global::System.Collections.ObjectModel.ObservableCollection<T> PageCollection<T>(this  global::System.Collections.ObjectModel.ObservableCollection<T> source, int Number, int CurrentSize)
        {
            return new global::System.Collections.ObjectModel.ObservableCollection<T>(source.Take(Number * CurrentSize).Skip(Number * (CurrentSize - 1)));
        }
    }
}
