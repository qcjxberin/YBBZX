using System.Web;
using YBB.Bll;
using YBB.Common;
using YBB.Controls;

namespace YBB.BaseData
{
    public class AdminUtils
    {

        public static void CheckMenuPower(int int_0)
        {
            if (!string.IsNullOrEmpty(int_0.ToString()))
            {
                string url = "showerr.aspx?msg=" + HttpContext.Current.Server.UrlEncode("对不起，您没有权限操作！");
                if ((((HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/site/") != -1) || (HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/infoadmin/") != -1)) || ((HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/houseadmin/") != -1) || (HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/newsadmin/") != -1))) || (((HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/tuanadmin/") != -1) || (HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/jobadmin/") != -1)) || (((HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/cmadmin/") != -1) || (HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/other/") != -1)) || (HttpContext.Current.Request.ServerVariables["URL"].ToLower().IndexOf("/web/") != -1))))
                {
                    url = "../" + url;
                }
                bool flag = false;
                try
                {
                    string str2 = Utils.Decompress(DES.Decode(Base.GetCookie("ids"), SysConfig.ConfigPasswordKey));
                    if (((str2 != "") && (str2 != ",")) && (Admin.GetAdminRoles().Select(string.Concat(new object[] { " RoleId in (", str2, ") and ','+AdminMenuId+',' like '%,", int_0, ",%' " })).Length > 0))
                    {
                        flag = true;
                    }
                }
                catch
                {
                }
                if (!flag)
                {
                    HttpContext.Current.Response.Redirect(url);
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static bool CheckMenuPowerMenu(int int_0)
        {
            bool flag = false;
            try
            {
                string str = Utils.Decompress(DES.Decode(Base.GetCookie("ids"), SysConfig.ConfigPasswordKey));
                if (((str != "") && (str != ",")) && (Admin.GetAdminRoles().Select(string.Concat(new object[] { " RoleId in (", str, ") and ','+AdminMenuId+',' like '%,", int_0, ",%' " })).Length > 0))
                {
                    flag = true;
                }
            }
            catch
            {
            }
            return flag;
        }

        public static void ShowMessage(string string_0, bool bool_0, SmallStatusMessage smallStatusMessage_0)
        {
            if (smallStatusMessage_0 != null)
            {
                smallStatusMessage_0.Success = bool_0;
                smallStatusMessage_0.Text = string_0;
                smallStatusMessage_0.Visible = true;
            }
        }

        public static void ShowMsg(string string_0, bool bool_0, StatusMessage statusMessage_0)
        {
            ShowMsg(string_0, bool_0, false, statusMessage_0);
        }

        public static void ShowMsg(string string_0, bool bool_0, bool bool_1, StatusMessage statusMessage_0)
        {
            if (statusMessage_0 != null)
            {
                statusMessage_0.Success = bool_0;
                statusMessage_0.IsWarning = bool_1;
                statusMessage_0.Text = string_0;
                statusMessage_0.Visible = true;
            }
        }
    }

}
