using System;
using System.IO;

namespace YBB.Bll
{
    internal class ScheduleConfigFileManager : DefaultConfigFileManager
    {
        public static string filename = null;
        private static ScheduleConfigInfo m_configinfo = ((ScheduleConfigInfo)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(ScheduleConfigInfo)));
        private static DateTime m_fileoldchange = File.GetLastWriteTime(ConfigFilePath);

        public static ScheduleConfigInfo LoadConfig()
        {
            ConfigInfo = DefaultConfigFileManager.LoadConfig(ref m_fileoldchange, ConfigFilePath, ConfigInfo, true);
            return (ConfigInfo as ScheduleConfigInfo);
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
                    filename = DefaultConfig.GetMapPath("/Public/config/task.config");
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
                m_configinfo = (ScheduleConfigInfo)value;
            }
        }
    }

}
