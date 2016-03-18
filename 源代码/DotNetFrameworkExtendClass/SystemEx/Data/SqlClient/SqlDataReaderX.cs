using System;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 提供一种从 SQL Server 数据库读取行的只进流的方式。 此类不能被继承。
    /// </summary>
    public static class SqlDataReaderX
    {
        /// <summary>
        /// 获取指定列的双精度浮点数形式的值
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="ColumnName">列名</param>
        /// <param name="defaultValue">如果为空，返回的默认值</param>
        /// <returns>经过处理后的值，如果Sql数据库中的值为Null，则返回默认值0</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public static double GetDouble(this global::System.Data.SqlClient.SqlDataReader sdr, string ColumnName, double defaultValue = 0.0)
        {
            try
            {
                int ordinal = sdr.GetOrdinal(ColumnName);
                global::System.Data.SqlTypes.SqlDouble sqlValue = sdr.GetDouble(ordinal);
                if (!sqlValue.IsNull) defaultValue = sqlValue.Value;
                return defaultValue;
            }
            catch (IndexOutOfRangeException ioorex)
            {
                throw ioorex;
            }
            catch (Exception)
            {
                return 0.0;
            }
        }
        /// <summary>
        /// 获取指定列的 32 位有符号整数形式的值
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="ColumnName">列名</param>
        /// <param name="defaultValue">如果为空，返回的默认值</param>
        /// <returns>经过处理后的值，如果Sql数据库中的值为Null，则返回默认值0</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public static int GetInt(this global::System.Data.SqlClient.SqlDataReader sdr, string ColumnName, int defaultValue = 0)
        {
            try
            {
                int ordinal = sdr.GetOrdinal(ColumnName);
                global::System.Data.SqlTypes.SqlInt32 sqlValue = sdr.GetInt32(ordinal);
                if (!sqlValue.IsNull) defaultValue = sqlValue.Value;
                return defaultValue;
            }
            catch (IndexOutOfRangeException ioorex)
            {
                throw ioorex;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取指定列的 System.Datetime 对象形式的值
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="ColumnName">列名</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public static DateTime GetDateTime(this global::System.Data.SqlClient.SqlDataReader sdr, string ColumnName, DateTime? defaultValue = null)
        {
            try
            {
                int ordinal = sdr.GetOrdinal(ColumnName);
                global::System.Data.SqlTypes.SqlDateTime sqlValue = sdr.GetDateTime(ordinal);
                if (!sqlValue.IsNull) defaultValue = sqlValue.Value;
                return defaultValue.HasValue ? defaultValue.Value : DateTime.MinValue;
            }
            catch (IndexOutOfRangeException ioorex)
            {
                throw ioorex;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// 获取指定列的单精度浮点形式的值
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="ColumnName">列名</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        public static float GetFloat(this global::System.Data.SqlClient.SqlDataReader sdr, string ColumnName, float defaultValue = 0.0f)
        {
            try
            {
                int ordinal = sdr.GetOrdinal(ColumnName);
                global::System.Data.SqlTypes.SqlDouble sqlValue = sdr.GetFloat(ordinal);
                if (!sqlValue.IsNull) defaultValue = (float)sqlValue.Value;
                return defaultValue;
            }
            catch (IndexOutOfRangeException ioorex)
            {
                throw ioorex;
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }
        /// <summary>
        /// 获取指定列的字符串形式的值。
        /// </summary>
        /// <param name="sdr"></param>
        /// <param name="ColumnName">列名</param>
        /// <param name="defaultValue"></param>
        /// <returns>指定列的值。</returns>
        /// <exception cref="System.IndexOutOfRangeException"></exception>
        /// <exception cref="System.InvalidCastException"></exception>
        public static string GetString(this global::System.Data.SqlClient.SqlDataReader sdr, string ColumnName, string defaultValue = "")
        {
            try
            {
                int ordinal = sdr.GetOrdinal(ColumnName);
                global::System.Data.SqlTypes.SqlString sqlValue = sdr.GetString(ordinal);
                if (!sqlValue.IsNull) defaultValue = Convert.ToString(sqlValue);
                return defaultValue;
            }
            catch (IndexOutOfRangeException ioorex)
            {
                throw ioorex;
            }
            catch (InvalidCastException ice)
            {
                throw ice;
            }
        }
        /// <summary>
        /// 关闭 System.Data.SqlClient.SqlDataReader 对象。
        /// </summary>
        /// <param name="sdr"></param>
        public static void Off(this global::System.Data.SqlClient.SqlDataReader sdr)
        {
            if (!sdr.IsClosed) sdr.Close();
        }
    }
}
