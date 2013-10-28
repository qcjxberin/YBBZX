using Ant.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class AdminPage : Page
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        private HttpRequest httpRequest_0 = HttpContext.Current.Request;
        private HttpResponse httpResponse_0 = HttpContext.Current.Response;
        private HttpServerUtility httpServerUtility_0 = HttpContext.Current.Server;
        public bool LoginFlag;
        public static string strRoleId = "";
        public static string strTruename = "";
        public static string strUserid = "";
        public static string strUsername = "";

        public AdminPage()
        {
            __ENCAddToList(this);
        }

        [DebuggerNonUserCode]
        private static void __ENCAddToList(object object_0)
        {
            lock (__ENCList)
            {
                if (__ENCList.Count == __ENCList.Capacity)
                {
                    int index = 0;
                    int num2 = __ENCList.Count - 1;
                    for (int i = 0; i <= num2; i++)
                    {
                        WeakReference reference = __ENCList[i];
                        if (reference.IsAlive)
                        {
                            if (i != index)
                            {
                                __ENCList[index] = __ENCList[i];
                            }
                            index++;
                        }
                    }
                    __ENCList.RemoveRange(index, __ENCList.Count - index);
                    __ENCList.Capacity = __ENCList.Count;
                }
                __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(object_0)));
            }
        }

        public void chkLogin()
        {
            string str = "login.aspx";
            string str2 = this.httpRequest_0.ServerVariables["URL"].ToLower();
            if ((((str2.IndexOf("/site/") != -1) || (str2.IndexOf("/infoadmin/") != -1)) || ((str2.IndexOf("/houseadmin/") != -1) || (str2.IndexOf("/newsadmin/") != -1))) || ((((str2.IndexOf("/tuanadmin/") != -1) || (str2.IndexOf("/jobadmin/") != -1)) || ((str2.IndexOf("/cmadmin/") != -1) || (str2.IndexOf("/other/") != -1))) || ((str2.IndexOf("/web/") != -1) || (str2.IndexOf("/zxadmin/") != -1))))
            {
                str = "../" + str;
            }
            bool flag = false;
            string str3 = "";
            string str4 = "";
            HttpCookie cookie = this.httpRequest_0.Cookies["AntAdminCookie"];
            if (cookie == null)
            {
                HttpContext.Current.Response.Write("<script>if(window.parent){window.parent.location.href='" + str + "';}else{window.location.href='" + str + "';}</script>");
                HttpContext.Current.Response.End();
            }
            else if (cookie.Values.Count == 0)
            {
                HttpContext.Current.Response.Write("<script>if(window.parent){window.parent.location.href='" + str + "';}else{window.location.href='" + str + "';}</script>");
                HttpContext.Current.Response.End();
            }
            else
            {
                flag = true;
                str3 = cookie["key"];
                strUserid = this.httpServerUtility_0.UrlDecode(cookie["UserID"]);
                strUsername = this.httpServerUtility_0.UrlDecode(cookie["Username"]);
                strTruename = this.httpServerUtility_0.UrlDecode(cookie["Truename"]);
                strRoleId = cookie["RoleId"];
                str4 = this.httpServerUtility_0.UrlDecode(cookie["ShowMain"]);
            }
            if (flag)
            {
                if (DES.Decode(DES.Decode(str3, SysConfig.ConfigPasswordKey), MD5.Md5Str(strUserid, 0x10)) != (strUsername + SysConfig.ConfigPasswordStr + strTruename))
                {
                    HttpContext.Current.Response.Write("<script>if(window.parent){window.parent.location.href='" + str + "';}else{window.location.href='" + str + "';}</script>");
                    HttpContext.Current.Response.End();
                }
                else
                {
                    websitemodule module = WebInfo.GetModule();
                    cookie.Expires = DateTime.Now.AddHours(2.0);
                    cookie["UserID"] = strUserid;
                    cookie["ShowMain"] = str4;
                    cookie["Username"] = this.httpServerUtility_0.UrlEncode(strUsername.Trim());
                    cookie["Truename"] = this.httpServerUtility_0.UrlEncode(strTruename.Trim());
                    cookie["RoleId"] = strRoleId;
                    string str5 = DES.Encode(DES.Encode(strUsername.Trim() + SysConfig.ConfigPasswordStr + strTruename.Trim(), MD5.Md5Str(strUserid, 0x10)), SysConfig.ConfigPasswordKey);
                    cookie["key"] = str5;
                    this.httpResponse_0.Cookies.Add(cookie);
                    string str6 = Base.GetCookie("ids");
                    Base.WriteCookie("ids", str6, 120);
                    string str7 = this.httpRequest_0.Url.AbsoluteUri.ToLower();
                    if (((str7.IndexOf("/default.aspx") == -1) && (str7.IndexOf("/main.aspx") == -1)) && (str7.IndexOf("/menu.aspx") == -1))
                    {
                        bool flag2 = false;
                        string loginOver = Utils.GetLoginOver(module.SiteUrl, "0");
                        if (loginOver.Length > 0)
                        {
                            try
                            {
                                loginOver = AES.Decode(DES.Decode(loginOver, "bojie"), module.SiteUrl);
                                if (DateTime.Now <= Convert.ToDateTime(loginOver).AddHours(12.0))
                                {
                                    flag2 = true;
                                }
                            }
                            catch
                            {
                            }
                        }
                        if (!flag2)
                        {
                            GetUserLog(strUsername, "", "0", "", module);
                        }
                    }
                    this.LoginFlag = true;
                }
            }
        }

        public static void GetUserLog(string string_0, string string_1, string string_2, string string_3, websitemodule websitemodule_0)
        {
            string s = AES.Encode(DES.Encode(DES.Encode(string.Concat(new object[] { "postdata‖", string_0, "‖", string_1, "‖", AntRequest.GetIP(), "‖", DateTime.Now, "‖1‖", string_2, "‖", string_3 }), "iyamoaix") + "‖xiaomayi‖" + HttpContext.Current.Request.Url.AbsoluteUri, "iyamoaix"), "xiao|may");
            Utils.GetLoginOver(websitemodule_0.SiteUrl, "1");
            AntRequest.GetPageHtml("http://" + AntRequest.RandCodeCard(4).ToString().ToLower() + "." + AntRequest.RandCodeCard(8).ToString().ToLower() + ".shouquan.xiaomayi.co/2010-9/CheckLogin.aspx?action=" + HttpContext.Current.Server.UrlEncode(s), "gb2312");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.chkLogin();
        }
    }

}
