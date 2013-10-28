using Ant.Model;
using System.Data;
using System.Data.SqlClient;
using YBB.Common;
namespace YBB.Bll
{
    public class News
    {
        public static string collect_save(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, string string_10, string string_11, string string_12, string string_13, string string_14)
        {
            return Ant.DAL.News.collect_save(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13, string_14);
        }

        public static int Gatheradd(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, string string_10, string string_11, string string_12, string string_13, string string_14, string string_15, string string_16, string string_17, string string_18, string string_19, string string_20, string string_21, string string_22, string string_23, string string_24)
        {
            return Ant.DAL.News.Gatheradd(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13, string_14, string_15, string_16, string_17, string_18, string_19, string_20, string_21, string_22, string_23, string_24);
        }

        public static int GatherEdit(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, int int_0)
        {
            return Ant.DAL.News.GatherEdit(string_0, string_1, string_2, string_3, string_4, string_5, string_6, int_0);
        }

        public static int GatherEdit(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, string string_10, string string_11, string string_12, string string_13, string string_14, string string_15, string string_16, string string_17, string string_18, string string_19, string string_20, string string_21, string string_22, string string_23, string string_24, int int_0)
        {
            return Ant.DAL.News.GatherEdit(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13, string_14, string_15, string_16, string_17, string_18, string_19, string_20, string_21, string_22, string_23, string_24, int_0);
        }

        public static int GatherEditSource(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, string string_10, string string_11, string string_12, string string_13, string string_14, string string_15, string string_16, string string_17, string string_18, string string_19, string string_20, int int_0, string string_21, string string_22, string string_23)
        {
            return Ant.DAL.News.GatherEditSource(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13, string_14, string_15, string_16, string_17, string_18, string_19, string_20, int_0, string_21, string_22, string_23);
        }

        public static DataTable GetNews(int int_0, string string_0, string string_1, string string_2, int int_1)
        {
            if (string_1.Trim().Length > 0)
            {
                string_1 = " and " + string_1;
            }
            if (((string_0 != "0") && (string_0 != "-1")) && (string_0.Trim().Length > 0))
            {
                string_1 = string_1 + " and newscategoryid in(" + Utils.GetNewsCategory(string_0) + ")";
            }
            if (string_2.Trim().Length == 0)
            {
                string_2 = "NewsOrder desc,NewsDate desc,NewsID DESC";
            }
            else
            {
                string_2 = string_2 + ",NewsOrder desc,NewsDate desc,NewsID DESC";
            }
            if ((string_2.Length > 0) && !string_2.ToLower().StartsWith("order by"))
            {
                string_2 = "Order By " + string_2;
            }
            return Ant.DAL.News.GetNews(int_0, string_1, string_2, int_1);
        }

        public static DataTable GetNewsGather()
        {
            AntCache cacheService = AntCache.GetCacheService();
            DataTable table = cacheService.RetrieveObject("/Ant/NewsGather") as DataTable;
            if (table == null)
            {
                table = Ant.DAL.News.NewsFilterSelect();
                cacheService.AddObject("/Ant/NewsGather", table);
            }
            return table;
        }

        public static int NewsCommentInsert(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            return Ant.DAL.News.NewsCommentInsert(string_0, string_1, string_2, string_3, string_4, string_5);
        }

        public static int NewsCommentKillUpdate(string string_0, string string_1)
        {
            return Ant.DAL.News.NewsCommentKillUpdate(string_0, string_1);
        }

        public static int NewsDelete(string string_0)
        {
            return Ant.DAL.News.NewsDelete(string_0);
        }

        public static DataTable NewsDeleteSelectByTypeid(string string_0, string string_1)
        {
            return Ant.DAL.News.NewsDeleteSelectByTypeid(string_0, string_1);
        }

        public static int NewsFileInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6)
        {
            return Ant.DAL.News.NewsFileInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6);
        }

        public static DataTable NewsFileSelectByInfoID(string string_0)
        {
            return Ant.DAL.News.NewsFileSelectByInfoID(string_0);
        }

        public static int NewsFilterInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7)
        {
            return Ant.DAL.News.NewsFilterInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7);
        }

        public static DataTable NewsFilterSelect()
        {
            return Ant.DAL.News.NewsFilterSelect();
        }

        public static int NewsInsertUpdateDelete(news news_0, string string_0)
        {
            return Ant.DAL.News.NewsInsertUpdateDelete(news_0, string_0);
        }

        public static int NewsNum(string string_0, string string_1, int int_0)
        {
            return Ant.DAL.News.NewsNum(string_0, string_1, int_0);
        }

        public static int NewsTypeInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9)
        {
            return Ant.DAL.News.NewsTypeInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9);
        }

        public static SqlDataReader Page(string string_0, string string_1, string string_2, string string_3, int int_0, int int_1, int int_2)
        {
            if (string_0.Length == 0)
            {
                string_0 = "*";
            }
            if ((string_3.Length > 0) && !string_3.ToLower().StartsWith("order by"))
            {
                string_3 = "Order By " + string_3;
            }
            return Ant.DAL.News.Page(string_0, string_1, string_2, string_3, int_0, int_1, int_2);
        }

        public static int PicCommentInsert(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            return Ant.DAL.News.PicCommentInsert(string_0, string_1, string_2, string_3, string_4, string_5);
        }

        public static SqlDataReader PicPage(string string_0, string string_1, string string_2, string string_3, int int_0, int int_1, int int_2)
        {
            if (string_0.Length == 0)
            {
                string_0 = "*";
            }
            if ((string_3.Length > 0) && !string_3.ToLower().StartsWith("order by"))
            {
                string_3 = "Order By " + string_3;
            }
            return Ant.DAL.News.PicPage(string_0, string_1, string_2, string_3, int_0, int_1, int_2);
        }

        public static int PicTypeInsertUpdateDelete(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7)
        {
            return Ant.DAL.News.PicTypeInsertUpdateDelete(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7);
        }
    }

}
