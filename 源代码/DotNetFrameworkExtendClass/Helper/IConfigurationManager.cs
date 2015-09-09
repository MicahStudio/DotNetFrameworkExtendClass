using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DotNetFrameworkExtendClass
{
    /// <summary>
    /// 桌面程序操作app.config的辅助类
    /// </summary>
    public abstract class IConfigurationManager
    {
        private bool _isSecret = false;
        /// <summary>
        /// 是否对字段的值进行加密（默认为不加密）
        /// </summary>
        public bool IsSecret
        {
            set
            {
                if (value != _isSecret) _isSecret = value;
            }
            get
            {
                return _isSecret;
            }
        }
        private string _secretKey = "";
        /// <summary>
        /// 加＼解密的Key（需要将IsSecret设置为Ture才会生效）
        /// </summary>
        public string SecretKey
        {
            set
            {
                if (value != _secretKey) _secretKey = value;
            }
            get
            {
                return _secretKey;
            }
        }
        /// <summary>
        /// config文件
        /// </summary>
        private static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        /// <summary>
        /// 更新或添加节点
        /// </summary>
        /// <param name="SectionType">节点类型</param>
        /// <param name="Key">节点名</param>
        /// <param name="Value">节点值</param>
        public void UpdateOnInsert(ConfigurationManagerSectionType SectionType, string Key, string Value)
        {
            if (_isSecret)
            {
                Value = Value.Encrypt(_secretKey);
            }
            switch (SectionType)
            {
                case ConfigurationManagerSectionType.AppSettings:
                    {
                        if (config.AppSettings.Settings.AllKeys.Contains(Key))
                        {
                            config.AppSettings.Settings[Key].Value = Value;
                        }
                        else
                        {
                            config.AppSettings.Settings.Add(Key, Value);
                        }
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                        break;
                    }
                case ConfigurationManagerSectionType.ConnectionStrings:
                    {
                        if (config.ConnectionStrings.ConnectionStrings[Key] != null)
                        {
                            config.ConnectionStrings.ConnectionStrings[Key].ConnectionString = Value;
                        }
                        else
                        {
                            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(Key, Value));
                        }
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("ConnectionStrings");
                        break;
                    }
            }
        }
        /// <summary>
        /// 获取节点的值
        /// </summary>
        /// <param name="SectionType">节点类型</param>
        /// <param name="Key">节点名</param>
        /// <param name="defaultValue">如果没查找到节点那么返回给定的默认值</param>
        /// <returns></returns>
        public string GetValue(ConfigurationManagerSectionType SectionType, string Key, string defaultValue = "")
        {
            string Value = defaultValue;
            switch (SectionType)
            {
                case ConfigurationManagerSectionType.AppSettings:
                    {
                        if (config.AppSettings.Settings.AllKeys.Contains(Key))
                        {
                            Value = config.AppSettings.Settings[Key].Value;
                        }
                        else
                        {
                            Value = defaultValue;
                        }
                        break;
                    }
                case ConfigurationManagerSectionType.ConnectionStrings:
                    {
                        ConnectionStringSettings setting = config.ConnectionStrings.ConnectionStrings[Key];
                        if (setting != null)
                        {
                            Value = setting.ConnectionString;
                        }
                        else
                        {
                            Value = defaultValue;
                        }
                        break;
                    }
            }
            if (_isSecret)
            {
                Value = Value.Decrypt(_secretKey);
            }
            return Value;
        }
    }
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum ConfigurationManagerSectionType
    {
        /// <summary>
        /// AppSettings
        /// </summary>
        AppSettings,
        /// <summary>
        /// ConnectionStrings
        /// </summary>
        ConnectionStrings
    }
}
