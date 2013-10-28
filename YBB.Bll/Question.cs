using System.Data;
using YBB.Common;

namespace YBB.Bll
{
    public class Question
    {
        public static DataTable DataList()
        {
            AntCache cacheService = AntCache.GetCacheService();
            DataTable table = cacheService.RetrieveObject("/Ant/WebQuestion") as DataTable;
            if (table == null)
            {
                table = Ant.DAL.Question.DataList();
                cacheService.AddObject("/Ant/WebQuestion", table);
            }
            return table;
        }

        public static int QuestionAddUpdate(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            return Ant.DAL.Question.QuestionAddUpdate(string_0, string_1, string_2, string_3, string_4, string_5);
        }

        public static void Remove()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/WebQuestion");
        }
    }

}
