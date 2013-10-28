using System.Web;

namespace YBB.Bll
{
    public class DefaultConfig
    {
        public static string GetMapPath(string string_0)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(string_0);
            }
            if (string_0.StartsWith("/"))
            {
                string_0 = string_0.Substring(1, string_0.Length - 1);
            }
            return (HttpRuntime.AppDomainAppPath + string_0);
        }
    }

}
