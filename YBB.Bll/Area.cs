using System.Data;
using YBB.Common;

namespace YBB.Bll
{
    public class Area
    {
        public static int AddUpdate(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8)
        {
            return Ant.DAL.Area.AddUpdate(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8);
        }

        public static DataTable DataList()
        {
            AntCache cacheService = AntCache.GetCacheService();
            DataTable table = cacheService.RetrieveObject("/Ant/AreaList") as DataTable;
            if (table == null)
            {
                table = new DataTable();
                table.Columns.Add("AreaID");
                table.Columns.Add("AreaName");
                table.Columns.Add("AreaParent");
                table.Columns.Add("ParentAreaName");
                table.Columns.Add("AreaX");
                table.Columns.Add("AreaY");
                DataTable table2 = Ant.DAL.Area.DataList();
                DataRow[] rowArray = table2.Select("  AreaParent=0 and areakill= 0 ", "AreaOrder asc");
                if (rowArray.Length > 0)
                {
                    for (int i = 0; i < rowArray.Length; i++)
                    {
                        DataRow row = table.NewRow();
                        row[0] = rowArray[i]["AreaID"].ToString();
                        row[1] = AntRequest.StrTrim(rowArray[i]["AreaName"].ToString());
                        row[2] = "0";
                        row[3] = "0";
                        row[4] = rowArray[i]["AreaX"].ToString();
                        row[5] = rowArray[i]["AreaY"].ToString();
                        table.Rows.Add(row);
                        DataRow[] rowArray2 = table2.Select("  AreaParent>0 and areakill= 0 and AreaParent='" + rowArray[i]["AreaID"].ToString() + "'", "AreaOrder asc");
                        if (rowArray2.Length > 0)
                        {
                            for (int j = 0; j < rowArray2.Length; j++)
                            {
                                row = table.NewRow();
                                row[0] = rowArray2[j]["AreaID"].ToString();
                                row[1] = AntRequest.StrTrim(rowArray2[j]["AreaName"].ToString());
                                row[2] = rowArray2[j]["AreaParent"].ToString();
                                row[3] = AntRequest.StrTrim(rowArray2[j]["ParentAreaName"].ToString());
                                row[4] = rowArray2[j]["AreaX"].ToString();
                                row[5] = rowArray2[j]["AreaY"].ToString();
                                table.Rows.Add(row);
                            }
                        }
                        rowArray2 = null;
                    }
                }
                rowArray = null;
                cacheService.AddObject("/Ant/AreaList", table);
                table2.Dispose();
            }
            return table;
        }

        public static void Remove()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/AreaList");
        }
    }

}
