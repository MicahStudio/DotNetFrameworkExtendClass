using System;
using System.Configuration;
using System.Linq;
namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// [单例模式]读写exe.Config文件
    /// </summary>
    public sealed class ExeConfigContext : Singleton<ExeConfigContext>
    {
        private ExeConfigContext() { }
        private static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        /// <summary>
        /// 设置ConnectionString，并保存
        /// </summary>
        /// <param name="name">节点名</param>
        /// <param name="connectionString">值</param>
        public void SetConnectionString(string name, string connectionString)
        {
            if (config.ConnectionStrings.ConnectionStrings[name] != null)
            {
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(name, connectionString));
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }
        /// <summary>
        /// 获取ConnectionString
        /// </summary>
        /// <param name="name">节点名</param>
        /// <returns></returns>
        public string GetConnectionString(string name)
        {
            ConnectionStringSettings setting = config.ConnectionStrings.ConnectionStrings[name];
            if (setting == null)
            {
                return string.Empty;
            }
            else
            {
                return setting.ConnectionString;
            }
        }
        /// <summary>
        /// 写入配置文件并保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">节点名</param>
        /// <param name="value">值</param>
        public void SetValue<T>(string name, T value) where T : struct
        {
            if (config.AppSettings.Settings.AllKeys.Contains(name))
            {
                config.AppSettings.Settings[name].Value = Convert.ToString(value);
            }
            else
            {
                config.AppSettings.Settings.Add(name, Convert.ToString(value));
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        /// 从节点读取值
        /// </summary>
        /// <param name="name">节点名</param>
        /// <returns></returns>
        public string GetValue(string name)
        {
            if (config.AppSettings.Settings.AllKeys.Contains(name))
            {
                return config.AppSettings.Settings[name].Value;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 从节点读取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">节点名</param>
        /// <returns></returns>
        public T GetValue<T>(string name) where T : struct
        {
            if (config.AppSettings.Settings.AllKeys.Contains(name))
            {
                return (T)Convert.ChangeType(config.AppSettings.Settings[name].Value, typeof(T));
            }
            else
            {
                return default(T);
            }
        }
    }
}
