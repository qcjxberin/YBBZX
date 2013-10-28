using System;
using System.IO;

namespace YBB.Bll
{
    public class DefaultConfigFileManager
    {
        private static string m_configfilepath;
        private static IConfigInfo m_configinfo = null;
        private static object m_lockHelper = new object();

        public static IConfigInfo DeserializeInfo(string string_0, Type type_0)
        {
            return (IConfigInfo)SerializationHelper.Load(type_0, string_0);
        }

        protected static IConfigInfo LoadConfig(ref DateTime dateTime_0, string string_0, IConfigInfo iconfigInfo_0)
        {
            return LoadConfig(ref dateTime_0, string_0, iconfigInfo_0, true);
        }

        protected static IConfigInfo LoadConfig(ref DateTime dateTime_0, string string_0, IConfigInfo iconfigInfo_0, bool bool_0)
        {
            lock (m_lockHelper)
            {
                m_configfilepath = string_0;
                m_configinfo = iconfigInfo_0;
                if (bool_0)
                {
                    DateTime lastWriteTime = File.GetLastWriteTime(string_0);
                    if (dateTime_0 != lastWriteTime)
                    {
                        dateTime_0 = lastWriteTime;
                        m_configinfo = DeserializeInfo(string_0, iconfigInfo_0.GetType());
                    }
                }
                else
                {
                    m_configinfo = DeserializeInfo(string_0, iconfigInfo_0.GetType());
                }
                return m_configinfo;
            }
        }

        public virtual bool SaveConfig()
        {
            return true;
        }

        public bool SaveConfig(string string_0, IConfigInfo iconfigInfo_0)
        {
            return SerializationHelper.Save(iconfigInfo_0, string_0);
        }

        public static string ConfigFilePath
        {
            get
            {
                return m_configfilepath;
            }
            set
            {
                m_configfilepath = value;
            }
        }

        public static IConfigInfo ConfigInfo
        {
            get
            {
                return m_configinfo;
            }
            set
            {
                m_configinfo = value;
            }
        }
    }

}
