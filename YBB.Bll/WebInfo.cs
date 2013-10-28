using Ant.Model;
using YBB.Common;

namespace YBB.Bll
{
    public class WebInfo
    {
        public static websitemodule GetModule()
        {
            AntCache cacheService = AntCache.GetCacheService();
            websitemodule websitemodule = cacheService.RetrieveObject("/Ant/WebSiteModule") as websitemodule;
            if (websitemodule == null)
            {
                websitemodule = Ant.DAL.WebInfo.GetModule();
                cacheService.AddObject("/Ant/WebSiteModule", websitemodule);
            }
            return websitemodule;
        }

        public static website Get()
        {
            AntCache cacheService = AntCache.GetCacheService();
            website website = cacheService.RetrieveObject("/Ant/WebSiteMain") as website;
            if (website == null)
            {
                website = Ant.DAL.WebInfo.Get();
                cacheService.AddObject("/Ant/WebSiteMain", website);
            }
            return website;
        }

        public static SiteWater GetSiteWater()
        {
            AntCache cacheService = AntCache.GetCacheService();
            SiteWater siteWater = cacheService.RetrieveObject("/Ant/SiteWater") as SiteWater;
            if (siteWater == null)
            {
                siteWater = WebInfo.GetSiteWater();
                cacheService.AddObject("/Ant/SiteWater", siteWater);
            }
            return siteWater;
        }


    }
}
