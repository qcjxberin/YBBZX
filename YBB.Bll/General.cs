using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace YBB.Bll
{
    public class General
    {

        public static DataTable ShowWebDomainSelect(string string_0, int int_0)
        {
            return Ant.DAL.General.ShowWebDomainSelect(string_0, int_0);
        }

        public static DataTable ShowCompanyDomainSelect(string string_0)
        {
            return Ant.DAL.General.ShowCompanyDomainSelect(string_0);
        }

        public static int Count(string string_0, string string_1)
        {
            return Ant.DAL.General.Count(string_0, string_1);
        }

        public static DataTable GetDataTable(int int_0, string string_0, string string_1, string string_2, string string_3)
        {
            if (string_0.Trim().Length == 0)
            {
                string_0 = " * ";
            }
            if ((string_3.Trim().Length > 0) && !string_3.Trim().ToLower().StartsWith("order by"))
            {
                string_3 = " Order By " + string_3;
            }
            return Ant.DAL.General.GetDataTable(int_0, string_0, string_1, string_2, string_3);
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
            return Ant.DAL.General.Page(string_0, string_1, string_2, string_3, int_0, int_1, int_2);
        }

        public static DataTable DataList(string string_0)
        {
            return Ant.DAL.General.DataList(string_0);
        }

        public static int Execute(string string_0)
        {
            return Ant.DAL.General.Execute(string_0);
        }

        public static int InsertFile(string string_0, string string_1, string string_2, string string_3, string string_4, int int_0, int int_1)
        {
            return Ant.DAL.General.InsertFile(string_0, string_1, string_2, string_3, string_4, int_0, int_1);
        }

        public static void DeleteFile(string string_0, string string_1)
        {
            if (!string.IsNullOrEmpty(string_0) && (string_0.Trim().Length > 0))
            {
                try
                {
                    string_0 = string_0.ToLower().Remove(0, string_0.ToLower().IndexOf("upload"));
                    if (File.Exists(HttpContext.Current.Server.MapPath(string_1 + string_0)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(string_1 + string_0));
                    }
                }
                catch (Exception)
                {
                }
                Ant.DAL.General.DeleteFile(string_0);
            }
        }

        public static SqlDataReader GetDataReader(int int_0, string string_0, string string_1, string string_2, string string_3)
        {
            if (string_0.Trim().Length == 0)
            {
                string_0 = " * ";
            }
            if ((string_3.Trim().Length > 0) && !string_3.Trim().ToLower().StartsWith("order by"))
            {
                string_3 = " Order By " + string_3;
            }
            return General.GetDataReader(int_0, string_0, string_1, string_2, string_3);
        }

 

 

    }
}
