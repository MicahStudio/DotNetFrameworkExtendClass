using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 
    /// </summary>
    public static class SqlConnectionX
    {
        /// <summary>
        /// 使用现成的方式执行Open操作
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Timeout">设置超时的时间，单位为（秒），默认3秒，支持小数位的毫秒。</param>
        [Obsolete("改方法可能会引发内存溢出，不建议使用。", true)]
        public static void Open(this SqlConnection conn, double Timeout = 3.0)
        {
            //conn.OpenDb(Timeout);
        }
        /// <summary>
        /// 用线程的方式执行SqlConnection.Open()
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Timeout">设置超时的时间，单位为（秒），默认3秒，支持小数位的毫秒。</param>
        [Obsolete("改方法可能会引发内存溢出，不建议使用。", true)]
        public static void OpenDb(this SqlConnection conn, double Timeout = 3.0)
        {
            bool successed = false;
            var thread = new Thread(() =>
            {
                try
                {
                    if (ConnectionState.Open == conn.State)
                    {
                        successed = true;
                    }
                    else
                    {
                        conn.Open();
                        successed = true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (ThreadAbortException)
                {
                }
            });
            thread.IsBackground = true;
            Stopwatch sw = Stopwatch.StartNew();
            thread.Start();
            TimeSpan timeout = TimeSpan.FromSeconds(Timeout);
            while (sw.Elapsed < timeout)
            {
                thread.Join(TimeSpan.FromMilliseconds(200));
            }
            sw.Stop();
            if (!successed)
            {
                throw new Exception("超时无法连接.");
            }
        }
        /// <summary>
        /// 如果数据库已经Open那么执行Close
        /// </summary>
        /// <param name="conn"></param>
        public static void CloseDb(this SqlConnection conn)
        {
            if (ConnectionState.Open == conn.State) conn.Close();
        }
        /// <summary>
        /// 将源表中的数据复制到目标表中：SqlBulkCopy方式
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Destination">目标数据库</param>
        /// <param name="SourceTableName">源表表名</param>
        /// <param name="DestinationTableName">目标表表名</param>
        /// <param name="TOP">读取的条数</param>
        public static void CopyTable(this SqlConnection conn, SqlConnection Destination, string SourceTableName, string DestinationTableName, int TOP = 0)
        {
            #region 异常判断
            if (string.IsNullOrWhiteSpace(SourceTableName))
            {
                throw new ArgumentNullException("源表名不能为空。");
            } if (string.IsNullOrWhiteSpace(DestinationTableName))
            {
                throw new ArgumentNullException("目标表名不能为空。");
            }
            #endregion
            #region 获取源表的字段Columns
            string Columns = string.Empty;
            using (SqlCommand columnsCommand = conn.CreateCommand())
            {
                columnsCommand.CommandText = string.Format("SELECT [COLUMN_NAME],[DATA_TYPE],[IS_NULLABLE] FROM INFORMATION_SCHEMA.columns WHERE TABLE_NAME='{0}'", SourceTableName);
                using (SqlDataReader dr = columnsCommand.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            Columns += dr.GetString("COLUMN_NAME");
                            while (dr.Read())
                            {
                                Columns += ",";
                                Columns += dr.GetString("COLUMN_NAME");
                            }
                        }
                    }
                }
            }
            #endregion
            #region 判断是否有可用复制的字段
            if (string.IsNullOrWhiteSpace(Columns))
            {
                throw new ArgumentNullException("表中不包含任何字段或没有此表。");
            }
            #endregion
            using (SqlCommand dataCommand = conn.CreateCommand())
            {
                dataCommand.CommandText = string.Format("SELECT {0}{2} FROM {1} ORDER BY ID DESC", (TOP == 0 ? "" : ("TOP " + TOP + " ")), SourceTableName, Columns);
                using (Destination)
                {
                    Destination.Open();
                    using (SqlDataReader dr = dataCommand.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            using (System.Data.SqlClient.SqlBulkCopy sqlBC = new System.Data.SqlClient.SqlBulkCopy(Destination))
                            {
                                sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler((sender, e) =>
                                {

                                });
                                sqlBC.BatchSize = 300;
                                sqlBC.NotifyAfter = 300;
                                sqlBC.DestinationTableName = DestinationTableName;
                                #region 字段对齐
                                foreach (var t in Columns.Split(','))
                                {
                                    sqlBC.ColumnMappings.Add(t, t);
                                }
                                #endregion
                                sqlBC.WriteToServer(dr);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 将源表中的数据复制到目标表中：SqlBulkCopy方式
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="Destination">目标数据库</param>
        /// <param name="SourceTableName">源表表名</param>
        /// <param name="DestinationTableName">目标表表名</param>
        /// <param name="SourceTableQueryCommand">源表数据的查询语句</param>
        [Obsolete("该方法属于测试用的未完成的实验性方法，请不要使用。", true)]
        public static void CopyTable(this SqlConnection conn, SqlConnection Destination, string SourceTableName, string DestinationTableName, string SourceTableQueryCommand)
        {
            #region 异常判断
            if (string.IsNullOrWhiteSpace(SourceTableName))
            {
                throw new ArgumentNullException("源表名不能为空。");
            } if (string.IsNullOrWhiteSpace(DestinationTableName))
            {
                throw new ArgumentNullException("目标表名不能为空。");
            }
            if (string.IsNullOrWhiteSpace(SourceTableQueryCommand))
            {
                throw new ArgumentNullException("需要制定源表的查询语句。");
            }
            #endregion
            #region 获取源表的字段Columns
            string Columns = string.Empty;
            using (SqlCommand columnsCommand = conn.CreateCommand())
            {
                columnsCommand.CommandText = string.Format("SELECT [COLUMN_NAME],[DATA_TYPE],[IS_NULLABLE] FROM INFORMATION_SCHEMA.columns WHERE TABLE_NAME='{0}'", SourceTableName);
                using (SqlDataReader dr = columnsCommand.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            if (dr.GetString("COLUMN_NAME").Contains(SourceTableQueryCommand))
                                Columns += dr.GetString("COLUMN_NAME");
                            while (dr.Read())
                            {
                                if (dr.GetString("COLUMN_NAME").Contains(SourceTableQueryCommand))
                                {
                                    Columns += ",";
                                    Columns += dr.GetString("COLUMN_NAME");
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region 判断是否有可用复制的字段
            if (string.IsNullOrWhiteSpace(Columns))
            {
                throw new ArgumentNullException("表中不包含任何字段或没有此表。");
            }
            #endregion
            using (SqlCommand dataCommand = conn.CreateCommand())
            {
                dataCommand.CommandText = SourceTableQueryCommand;
                using (Destination)
                {
                    Destination.Open();
                    using (SqlDataReader dr = dataCommand.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            using (System.Data.SqlClient.SqlBulkCopy sqlBC = new System.Data.SqlClient.SqlBulkCopy(Destination))
                            {
                                sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler((sender, e) =>
                                {

                                });
                                sqlBC.BatchSize = 300;
                                sqlBC.NotifyAfter = 300;
                                sqlBC.DestinationTableName = DestinationTableName;
                                #region 字段对齐
                                foreach (var t in Columns.Split(','))
                                {
                                    sqlBC.ColumnMappings.Add(t, t);
                                }
                                #endregion
                                sqlBC.WriteToServer(dr);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 返回所有表名的集合
        /// </summary>
        /// <returns>返回所有表名的集合</returns>
        public static BindingList<string> Tables(this SqlConnection conn)
        {
            BindingList<string> items = new BindingList<string>();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = "SELECT Name FROM SysObjects WHERE XType='U' ORDER BY Name";
            using (SqlDataReader sdr = command.ExecuteReader())
            {
                while (sdr.Read())
                {
                    items.Add(sdr["Name"].ToString());
                }
            }
            return items;
        }

    }
}
