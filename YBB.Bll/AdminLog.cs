using System.Data;
namespace YBB.Bll
{
    public class AdminLog
    {
        public static int Add(string string_0, string string_1, string string_2, string string_3)
        {
            return Ant.DAL.AdminLog.Add(string_0, string_1, string_2, string_3);
        }

        public static void Delete(string string_0)
        {
            Ant.DAL.AdminLog.Delete(string_0);
        }

        public static void DeleteEmailLog(string string_0)
        {
            Ant.DAL.AdminLog.DeleteEmailLog(string_0);
        }

        public static DataTable SelectEmailLog(string string_0)
        {
            return Ant.DAL.AdminLog.SelectEmailLog(string_0);
        }
    }

}
