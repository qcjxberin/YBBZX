namespace YBB.Bll
{
    public class ScheduleConfigs
    {
        public static ScheduleConfigInfo GetConfig()
        {
            return ScheduleConfigFileManager.LoadConfig();
        }

        public static bool SaveConfig(ScheduleConfigInfo scheduleConfigInfo_0)
        {
            ScheduleConfigFileManager manager = new ScheduleConfigFileManager();
            ScheduleConfigFileManager.ConfigInfo = scheduleConfigInfo_0;
            return manager.SaveConfig();
        }
    }

}
