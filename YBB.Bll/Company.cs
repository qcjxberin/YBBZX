using Ant.Model;
using YBB.Common;

namespace YBB.Bll
{
    public class Company
    {
        public static CompanyConfig GetConfig()
        {
            AntCache cacheService = AntCache.GetCacheService();
            object config = cacheService.RetrieveObject("/Ant/CompanyConfig");
            if (config == null)
            {
                config = Ant.DAL.Company.GetConfig();
                cacheService.AddObject("/Ant/CompanyConfig", config);
            }
            return (CompanyConfig)config;
        }

    }
}