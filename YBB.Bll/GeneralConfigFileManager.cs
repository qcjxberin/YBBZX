using System;
using System.IO;

namespace YBB.Bll
{
    internal class GeneralConfigFileManager : DefaultConfigFileManager
    {
        public static string filename = null;
        private static GeneralConfigInfo m_configinfo = ((GeneralConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(GeneralConfigInfo)));
        private static DateTime m_fileoldchange = File.GetLastWriteTime(ConfigFilePath);

        public static GeneralConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            return (ConfigInfo as GeneralConfigInfo);
        }

        public override bool SaveConfig()
        {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }

        public new static string ConfigFilePath
        {
            get
            {
                if (filename == null)
                {
                    filename = DefaultConfig.GetMapPath("/Public/config/general.config");
                }
                return filename;
            }
        }

        public new static IConfigInfo ConfigInfo
        {
            get
            {
                return m_configinfo;
            }
            set
            {
                m_configinfo = (GeneralConfigInfo)value;
            }
        }
    }

}
