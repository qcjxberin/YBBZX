namespace YBB.Bll
{
    public class GeneralConfigs
    {
        private static object lockHelper = new object();

        public static GeneralConfigInfo Deserialize(string string_0)
        {
            return (GeneralConfigInfo)SerializationHelper.Load(typeof(GeneralConfigInfo), string_0);
        }

        public static GeneralConfigInfo GetConfig()
        {
            return GeneralConfigFileManager.LoadConfig();
        }

        public static bool SaveConfig(GeneralConfigInfo generalConfigInfo_0)
        {
            GeneralConfigFileManager manager = new GeneralConfigFileManager();
            GeneralConfigFileManager.ConfigInfo = generalConfigInfo_0;
            return manager.SaveConfig();
        }

        public static GeneralConfigInfo Serialiaze(GeneralConfigInfo generalConfigInfo_0, string string_0)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(generalConfigInfo_0, string_0);
            }
            return generalConfigInfo_0;
        }
    }

}
