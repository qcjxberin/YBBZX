using Ant.Model;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;
using YBB.Bll.ScheduledEvents;
using YBB.Common;

namespace YBB.Bll
{
    public class YBBHttpModule : IHttpModule
    {
        private static Timer eventTimer;

        public void Dispose()
        {
            eventTimer = null;
        }

        public void Application_OnError(object sender, EventArgs e)
        {
            string url = AntRequest.GetUrl();
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            if ((GeneralConfigs.GetConfig().Installation == 0) && (url.IndexOf("install") == -1))
            {
                context.Server.ClearError();
                HttpContext.Current.Response.Redirect("/install/default.aspx");
            }
        }

        public void Init(HttpApplication context)
        {
            EventLogs.LogFileName = HttpContext.Current.Server.MapPath("/Upload/EventFaildlog.config");
            context.BeginRequest += new EventHandler(this.method_1);
            if (eventTimer == null)
            {
                eventTimer = new Timer(new TimerCallback(this.method_0), context.Context, 0x927c0, EventManager.TimerMinutesInterval * 0xea60);
            }
            context.Error += new EventHandler(this.Application_OnError);
        }

        private void method_0(object object_0)
        {
            try
            {
                EventManager.Execute();
            }
            catch (Exception exception)
            {
                EventLogs.WriteFailedLog("启用定时任务失败：" + exception.ToString());
            }
        }

        private bool VerifyUrl(string url)
        {
            var bol = true;
            if (HttpContext.Current.Request.Url.IsFile)
            {
                return bol;
            }
            if (url.IndexOf("/upgrade/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/html/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/install/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/verifycode/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/favicon.ico") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/upload/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/bankpay/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/editor/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/anteditor/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/ditu/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/map/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/swfupload/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/public/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/api/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/template/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/admin/") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/webform1.aspx") != -1)
            {
                return bol;
            }
            if (url.IndexOf(".axd") != -1)
            {
                return bol;
            }
            if (url.IndexOf("/banner/") != -1)
            {
                return bol;
            }
            if (url.IndexOf(".txt") != -1)
            {
                return bol;
            }
            if (url.IndexOf(".xml") != -1)
            {
                return bol;
            }
            return false;
        }

        private void method_1(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            string str = context.Request.Url.Host.ToString().ToLower();
            if (str.StartsWith("www."))
            {
                str = str.Substring(4, str.Length - 4);
            }
            string str2 = context.Request.Path.ToLower();
            if (!this.VerifyUrl(str2))
            {
                websitemodule module = WebInfo.GetModule();
                if (str2.IndexOf("/" + module.SiteAdmin + "/") < 0)
                {
                    string str3 = module.SiteUrl.ToLower();
                    if (str3.StartsWith("www."))
                    {
                        str3 = str3.Substring(4, str3.Length - 4);
                    }
                    bool flag = false;
                    string str4 = "";
                    string str5 = "";
                    bool flag2 = true;
                    string domainOther = AntRequest.GetDomainOther();
                    string siteTemplate = module.SiteTemplate;
                    string str7 = request.Url.AbsoluteUri.ToLower().Replace("./", "/");
                    if (str7.EndsWith("/"))
                    {
                        str7 = (str7 + "antantant").Replace("/antantant", "");
                    }
                    string str8 = "";
                    string str9 = "";
                    string str10 = str7.Replace(domainOther, "/");
                    bool flag3 = true;
                    if ((str2.IndexOf("/account/") == -1) && (str2.IndexOf("showajaxpage.aspx") == -1))
                    {
                        string input = str10.ToLower().Replace("/tuan", "");
                        #region deal.aspx
                        if ((str7.IndexOf("/deal.aspx") != -1) || Regex.IsMatch(input, @"^/r(\d+).aspx", RegexOptions.IgnoreCase))
                        {
                            flag3 = false;
                            str7 = str7.Replace("deal.aspx", "default.aspx").Replace("r.aspx", "default.aspx");
                            if (Regex.IsMatch(input, @"^/r(\d+).aspx", RegexOptions.IgnoreCase))
                            {
                                string str12 = Regex.Replace(input, @"/R(\d+).aspx", "r=$1", AntRequest.GetRegexCompiledOptions() | RegexOptions.IgnoreCase);
                                str7 = str7.Substring(0, str7.LastIndexOf("/")) + "/default.aspx?" + str12;
                            }
                        }
                        #endregion
                        Regex regex = new Regex(@"^http://(\w+)." + str3 + "/$", RegexOptions.IgnoreCase);
                        if (regex.IsMatch("http://" + str + "/"))
                        {
                            #region region
                            flag = true;
                            str9 = regex.Matches("http://" + str + "/")[0].Groups[1].Value;
                            DataTable table = General.ShowWebDomainSelect(str9, 0);
                            if (table.Rows.Count != 1)
                            {
                                context.RewritePath("~/aspx/" + siteTemplate + "/NoStore.aspx", false);
                                return;
                            }
                            if (table.Rows[0]["DomainTypeid"].ToString() == "1")
                            {
                                str9 = AntRequest.StrTrim(table.Rows[0]["DomainMain"]);
                                if (str9 == "Sj")
                                {
                                    this.method_2(module, ref str2, ref str9, ref str4, ref str8, ref flag2, ref str7, context);
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "2")
                            {
                                str9 = "House";
                                if (((str7.IndexOf("agentsale.aspx") < 0) && (str7.IndexOf("agentrent") < 0)) && (str7.IndexOf("agentdi") < 0))
                                {
                                    str7 = "default.aspx";
                                }
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    str7 = "AgentShop.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "3")
                            {
                                str9 = "Sj";
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    DataTable table2 = General.ShowCompanyDomainSelect(table.Rows[0]["DomainCompanyID"].ToString());
                                    if (table2.Rows.Count == 1)
                                    {
                                        if (table2.Rows[0]["CompanyTypeid"].ToString() == "0")
                                        {
                                            if ((table2.Rows[0]["CompanyQianyue"].ToString() == "1") && (Convert.ToDateTime(table2.Rows[0]["CompanyQianyueDate"]) > DateTime.Now))
                                            {
                                                str7 = "ShopView.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                                str9 = "Sj/ShopTemplate/" + table2.Rows[0]["MobanPath"].ToString();
                                            }
                                            else
                                            {
                                                str9 = "shop";
                                                str7 = "comshop.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                            }
                                        }
                                        else if (table2.Rows[0]["CompanyTypeid"].ToString() == "2")
                                        {
                                            if ((table2.Rows[0]["CompanyQianyue"].ToString() == "1") && (Convert.ToDateTime(table2.Rows[0]["CompanyQianyueDate"]) > DateTime.Now))
                                            {
                                                str7 = "StoreDetail.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                                str9 = "Sj/StoreTemplate/" + table2.Rows[0]["MobanPath"].ToString();
                                            }
                                            else
                                            {
                                                str7 = "comstore.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                            }
                                        }
                                        else if ((table2.Rows[0]["CompanyQianyue"].ToString() == "1") && (Convert.ToDateTime(table2.Rows[0]["CompanyQianyueDate"]) > DateTime.Now))
                                        {
                                            str7 = "CompanyShow.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                            str9 = "Sj/CompanyTemplate/" + table2.Rows[0]["MobanPath"].ToString();
                                        }
                                        else
                                        {
                                            str7 = "comcompany.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    this.method_2(module, ref str2, ref str9, ref str4, ref str8, ref flag2, ref str7, context);
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "5")
                            {
                                str9 = "Dzb";
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    str7 = "DzbList.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "6")
                            {
                                str9 = "Vote";
                                if ((str7.IndexOf("default.aspx") != -1) && (str7.IndexOf("?") == -1))
                                {
                                    str7 = "default.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "7")
                            {
                                str9 = "House";
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    str7 = "Store.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "8")
                            {
                                str9 = "House";
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    str7 = "BuildView.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "9")
                            {
                                str9 = "zxjc";
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    str7 = "zxweb.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            else if (table.Rows[0]["DomainTypeid"].ToString() == "10")
                            {
                                str9 = "zxjc";
                                if (str7.IndexOf("default.aspx") != -1)
                                {
                                    str7 = "jcweb.aspx?id=" + table.Rows[0]["DomainCompanyID"].ToString();
                                }
                            }
                            #endregion
                        }
                        if (!flag && (("." + HttpContext.Current.Request.Url.Host.ToLower().Replace("http://", "") + "/").IndexOf(("." + module.SiteUrl.ToLower().Replace("http://", "") + "/").Replace(".www.", "")) == -1))
                        {
                            #region region
                            bool flag4 = false;
                            string pattern = @"^http://(www\.)?[^\.]+(\.com|\.com\.cn|\.cn|\.net|\.net\.cn|\.org|\.org\.cn|\.cc|\.co|\.in)/$";
                            regex = new Regex(pattern, RegexOptions.IgnoreCase);
                            if (regex.IsMatch("http://" + str + "/"))
                            {
                                flag4 = true;
                            }
                            else
                            {
                                pattern = @"^http://((.*)\.)?[^\.]+(\.com|\.com\.cn|\.cn|\.net|\.net\.cn|\.org|\.org\.cn|\.cc|\.co|\.in)/$";
                                regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                if (regex.IsMatch("http://" + str + "/"))
                                {
                                    flag4 = true;
                                }
                            }
                            if (flag4)
                            {
                                str9 = str;
                                DataTable table3 = General.ShowWebDomainSelect(str9, 4);
                                if (table3.Rows.Count == 1)
                                {
                                    flag = true;
                                    if (table3.Rows[0]["DomainTypeid"].ToString() == "4")
                                    {
                                        str9 = "Sj";
                                        if (str7.IndexOf("default.aspx") != -1)
                                        {
                                            DataTable table4 = General.ShowCompanyDomainSelect(table3.Rows[0]["DomainCompanyID"].ToString());
                                            if (table4.Rows[0]["CompanyTypeid"].ToString() == "0")
                                            {
                                                str7 = "ShopView.aspx?id=" + table3.Rows[0]["DomainCompanyID"].ToString();
                                            }
                                            else if ((table4.Rows[0]["CompanyQianyue"].ToString() == "1") && (Convert.ToDateTime(table4.Rows[0]["CompanyQianyueDate"]) > DateTime.Now))
                                            {
                                                str7 = "CompanyShow.aspx?id=" + table3.Rows[0]["DomainCompanyID"].ToString();
                                                str9 = "Sj/CompanyTemplate/" + table4.Rows[0]["MobanPath"].ToString();
                                            }
                                            else
                                            {
                                                str7 = "comcompany.aspx?id=" + table3.Rows[0]["DomainCompanyID"].ToString();
                                            }
                                            table4.Dispose();
                                        }
                                        else
                                        {
                                            this.method_2(module, ref str2, ref str9, ref str4, ref str8, ref flag2, ref str7, context);
                                        }
                                        bool flag5 = true;
                                        if (HttpContext.Current.Request.Cookies["AntXiaouserslogin"] != null)
                                        {
                                            string str14 = HttpContext.Current.Request.Cookies["AntXiaouserslogin"].Value;
                                            if (str14.Length > 0)
                                            {
                                                str14 = AES.Decode(DES.Decode(str14, "po!@#$cde"), module.SiteUrl);
                                                try
                                                {
                                                    if (DateTime.Now.Day == Convert.ToDateTime(str14).Day)
                                                    {
                                                        flag5 = false;
                                                    }
                                                }
                                                catch
                                                {
                                                }
                                            }
                                        }
                                        if (flag5)
                                        {
                                            string str15 = DES.Encode(AES.Encode(DateTime.Now.ToString(), module.SiteUrl), "po!@#$cde");
                                            HttpCookie cookie = null;
                                            if (HttpContext.Current.Request.Cookies["AntXiaouserslogin"] == null)
                                            {
                                                cookie = new HttpCookie("AntXiaouserslogin");
                                            }
                                            else
                                            {
                                                cookie = HttpContext.Current.Request.Cookies["AntXiaouserslogin"];
                                            }
                                            cookie.Value = str15;
                                            cookie.Expires = DateTime.Now.AddHours(1.0);
                                            HttpContext.Current.Response.AppendCookie(cookie);
                                        }
                                    }
                                }
                                table3.Dispose();
                            }
                            #endregion
                        }
                        if (!flag)
                        {
                            #region region
                            string str16 = HttpContext.Current.Request.Url.Host.ToLower().ToString();
                            string[] strArray = ("http://" + module.SiteUrl).Replace("http://www.", "").Split(new char[] { '.' });
                            string[] strArray2 = ("http://" + str16).Replace("http://www.", "").Split(new char[] { '.' });
                            if ((((("." + str16.ToLower().Replace("http://", "") + "/").IndexOf(("." + module.SiteUrl.ToLower().Replace("http://", "") + "/").Replace(".www.", "")) == -1) && (str16.IndexOf("localhost") == -1)) || (Base.IsIP(str16) || (strArray2.Length > (strArray.Length + 1)))) && (module.DomainKill == 1))
                            {
                                response.Redirect("http://" + module.SiteUrl);
                                response.End();
                            }
                            #endregion
                        }
                        if (flag)
                        {
                            #region region
                            if (module.SiteAspxRewrite == 1)
                            {
                                context.RewritePath("~/aspx/" + siteTemplate + "/" + str9 + "/" + str7.Replace(domainOther, "/"), false);
                                return;
                            }
                            if (!str2.ToLower().EndsWith(".aspx"))
                            {
                                str2 = str2.ToLower().Replace(".shtml", ".aspx").Replace(".html", ".aspx").Replace(".htm", ".aspx");
                            }
                            if (module.SiteTemplateName == ".aspx")
                            {
                                foreach (SiteUrls.URLRewrite url in SiteUrls.GetSiteUrls().Urls)
                                {
                                    if (Regex.IsMatch(str2, url.Pattern, AntRequest.GetRegexCompiledOptions() | RegexOptions.IgnoreCase))
                                    {
                                        str5 = Regex.Replace(str2, url.Pattern, url.QueryString, AntRequest.GetRegexCompiledOptions() | RegexOptions.IgnoreCase);
                                        string str16 = url.Page;
                                        if (((((str7.ToLower().IndexOf("companyshow") != -1) || (str7.ToLower().IndexOf("about-") != -1)) || ((str7.ToLower().IndexOf("culture-") != -1) || (str7.ToLower().IndexOf("hornor-") != -1))) || (str7.ToLower().IndexOf("messages-") != -1)) || ((((str7.ToLower().IndexOf("contactus-") != -1) || (str7.ToLower().IndexOf("achievement-") != -1)) || (str7.ToLower().IndexOf("actives-") != -1)) || ((((str7.ToLower().IndexOf("comnews-") != -1) || (str7.ToLower().IndexOf("products-") != -1)) || ((str7.ToLower().IndexOf("comnewsviews-") != -1) || (str7.ToLower().IndexOf("activesshow-") != -1))) || (((str7.ToLower().IndexOf("achievementview-") != -1) || (str7.ToLower().IndexOf("showproducts-") != -1)) || ((str7.ToLower().IndexOf("comvideo-") != -1) || (str7.ToLower().IndexOf("comquan-") != -1))))))
                                        {
                                            string str13 = AntRequest.GetBody(str5, "id=", "&");
                                            if (str5.IndexOf("&") == -1)
                                            {
                                                str13 = str5.Replace("id=", "");
                                            }
                                            DataTable table11 = ComManagent.GetWebCompanyDomainAttrList(Convert.ToInt32(str13));
                                            if (table11.Rows.Count == 1)
                                            {
                                                str16 = str16.ToLower().Replace("sj/", "Sj/CompanyTemplate/" + table11.Rows[0]["MobanPath"].ToString() + "/");
                                                if ((str7.ToLower().IndexOf("/companyshow") != -1) && ((table11.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table11.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now)))
                                                {
                                                    str16 = "sj/comcompany.aspx";
                                                    str5 = "id=" + str13;
                                                }
                                            }
                                        }
                                        if (((str7.ToLower().IndexOf("shopview-") != -1) || (str7.ToLower().IndexOf("shopcontent-") != -1)) || (((((str7.ToLower().IndexOf("/activity-") != -1) || (str7.ToLower().IndexOf("/activityitem") != -1)) || ((str7.ToLower().IndexOf("/storeshow") != -1) || (str7.ToLower().IndexOf("/photoviews") != -1))) || (((str7.ToLower().IndexOf("/shoprate") != -1) || (str7.ToLower().IndexOf("/itemshopsearch") != -1)) || ((str7.ToLower().IndexOf("/items") != -1) && (str7.ToLower().IndexOf("/itemsearch") == -1)))) || ((str7.ToLower().IndexOf("/shopvideo") != -1) || (str7.ToLower().IndexOf("/shopquan-") != -1))))
                                        {
                                            string str14 = AntRequest.GetBody(str5, "id=", "&");
                                            if (str5.IndexOf("&") == -1)
                                            {
                                                str14 = str5.Replace("id=", "");
                                            }
                                            DataTable table12 = ComManagent.GetWebCompanyDomainAttrList(Convert.ToInt32(str14));
                                            if (table12.Rows.Count == 1)
                                            {
                                                str16 = str16.ToLower().Replace("sj/", "Sj/ShopTemplate/" + table12.Rows[0]["MobanPath"].ToString() + "/");
                                                if (str7.ToLower().IndexOf("/shopview") == -1)
                                                {
                                                    if ((str7.ToLower().IndexOf("/items") != -1) && ((table12.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table12.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now)))
                                                    {
                                                        str16 = "shop/comitem.aspx";
                                                    }
                                                }
                                                else if ((table12.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table12.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now))
                                                {
                                                    str16 = "shop/comshop.aspx";
                                                }
                                            }
                                        }
                                        if ((((str7.ToLower().IndexOf("/storedetail") != -1) || (str7.ToLower().IndexOf("/sshow") != -1)) || ((str7.ToLower().IndexOf("/srenling") != -1) || (str7.ToLower().IndexOf("/srate") != -1))) || ((((str7.ToLower().IndexOf("/squan") != -1) || (str7.ToLower().IndexOf("/spviews") != -1)) || ((str7.ToLower().IndexOf("/sorder") != -1) || (str7.ToLower().IndexOf("/scontent") != -1))) || (((str7.ToLower().IndexOf("/sactivityitem") != -1) || (str7.ToLower().IndexOf("/sactivity") != -1)) || ((str7.ToLower().IndexOf("/svideo") != -1) || (str7.ToLower().IndexOf("/squan") != -1)))))
                                        {
                                            string str15 = AntRequest.GetBody(str5, "id=", "&");
                                            if (str5.IndexOf("&") == -1)
                                            {
                                                str15 = str5.Replace("id=", "");
                                            }
                                            DataTable table13 = ComManagent.GetWebCompanyDomainAttrList(Convert.ToInt32(str15));
                                            if (table13.Rows.Count == 1)
                                            {
                                                str16 = str16.ToLower().Replace("sj/", "Sj/StoreTemplate/" + table13.Rows[0]["MobanPath"].ToString() + "/");
                                                if ((str7.ToLower().IndexOf("/storedetail") != -1) && ((table13.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table13.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now)))
                                                {
                                                    str16 = "Sj/comstore.aspx";
                                                    str5 = "id=" + str15;
                                                }
                                            }
                                        }
                                        context.RewritePath("~/aspx/" + siteTemplate + "/" + str16, string.Empty, str5);
                                        return;
                                    }
                                }
                            }
                            str2 = "/" + str9 + str2;
                            if (!flag2)
                            {
                                context.RewritePath("~/aspx/" + siteTemplate + "/" + str4, string.Empty, str8);
                                return;
                            }
                            context.RewritePath("~/aspx/" + siteTemplate + "/" + str9 + "/" + str7.Replace(domainOther, ""), false);
                            return;
                            #endregion
                        }
                    }
                    if (!flag)
                    {
                        #region region
                        if (str10.IndexOf("?") > -1)
                        {
                            str10 = str10.Substring(0, str10.IndexOf("?"));
                        }
                        if (((domainOther.Contains(str7) || str10.ToLower() == "/default.aspx") && !flag) && flag3)
                        {
                            if (module.SiteDefaultMoban == 1)
                            {
                                context.RewritePath("~/aspx/" + siteTemplate + "/default.aspx?gg=1&CreateIndex=" + AntRequest.GetString("CreateIndex"), false);
                            }
                            else
                            {
                                context.RewritePath(string.Concat(new object[] { "~/aspx/", siteTemplate, "/default", module.SiteDefaultMoban, ".aspx?gg=1&CreateIndex=", AntRequest.GetString("CreateIndex") }), false);
                            }
                        }
                        else
                        {
                            if (!str2.ToLower().EndsWith(".aspx"))
                            {
                                str2 = str2.ToLower().Replace(".shtml", ".aspx").Replace(".html", ".aspx").Replace(".htm", ".aspx");
                            }
                            if (module.SiteTemplateName == ".aspx")
                            {
                                foreach (SiteUrls.URLRewrite url in SiteUrls.GetSiteUrls().Urls)
                                {
                                    if (Regex.IsMatch(str2, url.Pattern, AntRequest.GetRegexCompiledOptions() | RegexOptions.IgnoreCase))
                                    {
                                        str5 = Regex.Replace(str2, url.Pattern, url.QueryString, AntRequest.GetRegexCompiledOptions() | RegexOptions.IgnoreCase);
                                        string str16 = url.Page;
                                        if (((((str7.ToLower().IndexOf("companyshow") != -1) || (str7.ToLower().IndexOf("about-") != -1)) || ((str7.ToLower().IndexOf("culture-") != -1) || (str7.ToLower().IndexOf("hornor-") != -1))) || (str7.ToLower().IndexOf("messages-") != -1)) || (((str7.ToLower().IndexOf("contactus-") != -1) || (str7.ToLower().IndexOf("achievement-") != -1)) || (((((str7.ToLower().IndexOf("actives-") != -1) || (str7.ToLower().IndexOf("comnews-") != -1)) || ((str7.ToLower().IndexOf("products-") != -1) || (str7.ToLower().IndexOf("comnewsviews-") != -1))) || (((str7.ToLower().IndexOf("activesshow-") != -1) || (str7.ToLower().IndexOf("achievementview-") != -1)) || ((str7.ToLower().IndexOf("showproducts-") != -1) || (str7.ToLower().IndexOf("comvideo-") != -1)))) || (str7.ToLower().IndexOf("comquan-") != -1))))
                                        {
                                            string str17 = AntRequest.GetBody(str5, "id=", "&");
                                            if (str5.IndexOf("&") == -1)
                                            {
                                                str17 = str5.Replace("id=", "");
                                            }
                                            DataTable table17 = ComManagent.GetWebCompanyDomainAttrList(Convert.ToInt32(str17));
                                            if (table17.Rows.Count == 1)
                                            {
                                                str16 = str16.ToLower().Replace("sj/", "Sj/CompanyTemplate/" + table17.Rows[0]["MobanPath"].ToString() + "/");
                                                if ((str7.ToLower().IndexOf("/companyshow") != -1) && ((table17.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table17.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now)))
                                                {
                                                    str16 = "sj/comcompany.aspx";
                                                }
                                            }
                                        }
                                        if ((((((str7.ToLower().IndexOf("shopview-") != -1) || (str7.ToLower().IndexOf("shopcontent-") != -1)) || ((str7.ToLower().IndexOf("/activity-") != -1) || (str7.ToLower().IndexOf("/activityitem") != -1))) || (((str7.ToLower().IndexOf("/storeshow") != -1) || (str7.ToLower().IndexOf("/photoviews") != -1)) || ((str7.ToLower().IndexOf("/shoprate") != -1) || (str7.ToLower().IndexOf("/itemshopsearch") != -1)))) || ((str7.ToLower().IndexOf("/items") != -1) && (str7.ToLower().IndexOf("/itemsearch") == -1))) || ((str7.ToLower().IndexOf("/shopvideo") != -1) || (str7.ToLower().IndexOf("/shopquan-") != -1)))
                                        {
                                            string str18 = AntRequest.GetBody(str5, "id=", "&");
                                            if (str5.IndexOf("&") == -1)
                                            {
                                                str18 = str5.Replace("id=", "");
                                            }
                                            DataTable table18 = ComManagent.GetWebCompanyDomainAttrList(Convert.ToInt32(str18));
                                            if (table18.Rows.Count == 1)
                                            {
                                                str16 = str16.ToLower().Replace("sj/", "Sj/ShopTemplate/" + table18.Rows[0]["MobanPath"].ToString() + "/");
                                                if (str7.ToLower().IndexOf("/shopview") == -1)
                                                {
                                                    if ((str7.ToLower().IndexOf("/items") != -1) && ((table18.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table18.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now)))
                                                    {
                                                        str16 = "shop/comitem.aspx";
                                                    }
                                                }
                                                else if ((table18.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table18.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now))
                                                {
                                                    str16 = "shop/comshop.aspx";
                                                }
                                            }
                                        }
                                        if ((((str7.ToLower().IndexOf("/storedetail") != -1) || (str7.ToLower().IndexOf("/sshow") != -1)) || ((str7.ToLower().IndexOf("/srenling") != -1) || (str7.ToLower().IndexOf("/srate") != -1))) || ((str7.ToLower().IndexOf("/squan") != -1) || ((((str7.ToLower().IndexOf("/spviews") != -1) || (str7.ToLower().IndexOf("/sorder") != -1)) || ((str7.ToLower().IndexOf("/scontent") != -1) || (str7.ToLower().IndexOf("/sactivityitem") != -1))) || (((str7.ToLower().IndexOf("/sactivity") != -1) || (str7.ToLower().IndexOf("/svideo") != -1)) || (str7.ToLower().IndexOf("/squan") != -1)))))
                                        {
                                            string str19 = AntRequest.GetBody(str5, "id=", "&");
                                            if (str5.IndexOf("&") == -1)
                                            {
                                                str19 = str5.Replace("id=", "");
                                            }
                                            DataTable table19 = ComManagent.GetWebCompanyDomainAttrList(Convert.ToInt32(str19));
                                            if (table19.Rows.Count == 1)
                                            {
                                                str16 = str16.ToLower().Replace("sj/", "Sj/StoreTemplate/" + table19.Rows[0]["MobanPath"].ToString() + "/");
                                                if ((str7.ToLower().IndexOf("/storedetail") != -1) && ((table19.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(table19.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now)))
                                                {
                                                    str16 = "sj/comstore.aspx";
                                                }
                                            }
                                        }
                                        context.RewritePath("~/aspx/" + siteTemplate + "/" + str16, string.Empty, str5);
                                        return;
                                    }
                                }
                            }
                            if (str2.IndexOf("/sj/") != -1)
                            {
                                str9 = "sj";
                                this.method_2(module, ref str2, ref str9, ref str4, ref str8, ref flag2, ref str7, context);
                                if ((domainOther + "sj/").Contains(str7))
                                {
                                    context.RewritePath("~/aspx/" + siteTemplate + "/" + str9 + "/Default.aspx", false);
                                }
                                else
                                {
                                    var re1 = str7.Replace(domainOther + "sj/", "");
                                    if (re1.Contains("."))
                                    {
                                        context.RewritePath("~/aspx/" + siteTemplate + "/" + str9 + "/" + re1, false);
                                    }
                                    else
                                    {
                                        context.RewritePath("~/aspx/" + siteTemplate + "/" + str9 + "/" + re1 + "/Default.aspx", false);
                                    }
                                }
                            }
                            else
                            {
                                if (domainOther.Contains(str7))
                                {
                                    context.RewritePath("~/aspx/" + siteTemplate + "/Default.aspx", false);
                                }
                                else
                                {
                                    var re1 = str7.Replace(domainOther, "");
                                    if (re1.Contains("."))
                                    {
                                        context.RewritePath("~/aspx/" + siteTemplate + "/" + re1, false);
                                    }
                                    else
                                    {
                                        context.RewritePath("~/aspx/" + siteTemplate + "/" + re1 + "/Default.aspx", false);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        private void method_2(websitemodule websitemodule_0, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref bool bool_0, ref string string_4, HttpContext httpContext_0)
        {
            string a = Path.GetFileName(httpContext_0.Request.Path).ToString().ToLower();
            bool flag = true;
            foreach (YBBHttpModule.SiteCompanyUrl.UrlSetRewrite urlSetRewrite in YBBHttpModule.SiteCompanyUrl.GetSiteCompanyUrls().ComUrls)
            {
                if (a == urlSetRewrite.Name)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                int @int = AntRequest.GetInt("id", 0);
                DataTable dataTable = General.ShowCompanyDomainSelect(@int.ToString());
                if (dataTable.Rows.Count == 1)
                {
                    if (dataTable.Rows[0]["CompanyTypeid"].ToString() == "1")
                    {
                        string_1 = "Sj/CompanyTemplate/" + dataTable.Rows[0]["MobanPath"].ToString();
                        if (string_4.ToLower().IndexOf("/companyshow") != -1 && (!(dataTable.Rows[0]["CompanyQianyue"].ToString() == "1") || !(Convert.ToDateTime(dataTable.Rows[0]["CompanyQianyueDate"]) > DateTime.Now)))
                        {
                            string_1 = "Sj";
                            string_4 = "ComCompany.aspx?id=" + @int;
                            if (websitemodule_0.SiteTemplateName == ".aspx")
                            {
                                string_2 = "sj/comcompany.aspx";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (dataTable.Rows[0]["CompanyTypeid"].ToString() == "0")
                        {
                            string_1 = "Sj/ShopTemplate/" + dataTable.Rows[0]["MobanPath"].ToString();
                            if (string_4.ToLower().IndexOf("/shopview") != -1)
                            {
                                if (!(dataTable.Rows[0]["CompanyQianyue"].ToString() == "1") || !(Convert.ToDateTime(dataTable.Rows[0]["CompanyQianyueDate"]) > DateTime.Now))
                                {
                                    string_1 = "shop";
                                    string_4 = "ComShop.aspx?" + httpContext_0.Request.QueryString.ToString();
                                    if (websitemodule_0.SiteTemplateName == ".aspx")
                                    {
                                        string_2 = "sj/ComShop.aspx";
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (string_4.ToLower().IndexOf("/items") != -1 && (!(dataTable.Rows[0]["CompanyQianyue"].ToString() == "1") || !(Convert.ToDateTime(dataTable.Rows[0]["CompanyQianyueDate"]) > DateTime.Now)))
                                {
                                    string_1 = "shop";
                                    string_4 = "comitem.aspx?" + httpContext_0.Request.QueryString.ToString();
                                    if (websitemodule_0.SiteTemplateName == ".aspx")
                                    {
                                        string_2 = "sj/comitem.aspx";
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (dataTable.Rows[0]["CompanyTypeid"].ToString() == "2")
                            {
                                string_1 = "Sj/StoreTemplate/" + dataTable.Rows[0]["MobanPath"].ToString();
                                if (string_4.ToLower().IndexOf("/storedetail") != -1 && (!(dataTable.Rows[0]["CompanyQianyue"].ToString() == "1") || !(Convert.ToDateTime(dataTable.Rows[0]["CompanyQianyueDate"]) > DateTime.Now)))
                                {
                                    string_1 = "Sj";
                                    string_4 = "comstore.aspx?id=" + @int;
                                    if (websitemodule_0.SiteTemplateName == ".aspx")
                                    {
                                        string_2 = "sj/comstore.aspx";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public class SiteCompanyUrl
        {
            private ArrayList arrayList_0 = new ArrayList();
            private static volatile YBBHttpModule.SiteCompanyUrl instance = null;
            private static object lockHelper = new object();

            private SiteCompanyUrl()
            {
                string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath("/aspx/ant/sj"));
                for (int i = 0; i < files.Length; i++)
                {
                    FileInfo info = new FileInfo(files[i]);
                    if (info.Extension.ToString().ToLower() == ".aspx")
                    {
                        this.arrayList_0.Add(new UrlSetRewrite(info.Name.ToLower()));
                    }
                }
            }

            public static YBBHttpModule.SiteCompanyUrl GetSiteCompanyUrls()
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new YBBHttpModule.SiteCompanyUrl();
                        }
                    }
                }
                return instance;
            }

            public static void SetInstance()
            {
                SetInstance(new YBBHttpModule.SiteCompanyUrl());
            }

            public static void SetInstance(YBBHttpModule.SiteCompanyUrl siteCompanyUrl_0)
            {
                if (siteCompanyUrl_0 != null)
                {
                    instance = siteCompanyUrl_0;
                }
            }

            public ArrayList ComUrls
            {
                get
                {
                    return this.arrayList_0;
                }
                set
                {
                    this.arrayList_0 = value;
                }
            }

            public class UrlSetRewrite
            {
                private string string_0;

                public UrlSetRewrite(string string_1)
                {
                    this.string_0 = string_1;
                }

                public string Name
                {
                    get
                    {
                        return this.string_0;
                    }
                    set
                    {
                        this.string_0 = value;
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 站点伪Url信息类
        /// </summary>
        public class SiteUrls
        {
            #region 内部属性和方法
            private static object lockHelper = new object();
            private static volatile SiteUrls instance = null;

            string SiteUrlsFile = HttpContext.Current.Server.MapPath("/public/config/url.config");
            private System.Collections.ArrayList _Urls;
            public System.Collections.ArrayList Urls
            {
                get
                {
                    return _Urls;
                }
                set
                {
                    _Urls = value;
                }
            }

            private System.Collections.Specialized.NameValueCollection _Paths;
            public System.Collections.Specialized.NameValueCollection Paths
            {
                get
                {
                    return _Paths;
                }
                set
                {
                    _Paths = value;
                }
            }

            private SiteUrls()
            {
                Urls = new System.Collections.ArrayList();
                Paths = new System.Collections.Specialized.NameValueCollection();

                XmlDocument xml = new XmlDocument();

                xml.Load(SiteUrlsFile);

                XmlNode root = xml.SelectSingleNode("urls");
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
                    {
                        XmlAttribute name = n.Attributes["name"];
                        XmlAttribute path = n.Attributes["path"];
                        XmlAttribute page = n.Attributes["page"];
                        XmlAttribute querystring = n.Attributes["querystring"];
                        XmlAttribute pattern = n.Attributes["pattern"];

                        if (name != null && path != null && page != null && querystring != null && pattern != null)
                        {
                            Paths.Add(name.Value, path.Value);
                            Urls.Add(new URLRewrite(name.Value, pattern.Value, page.Value.Replace("^", "&"), querystring.Value.Replace("^", "&")));
                        }
                    }
                }
            }
            #endregion

            public static SiteUrls GetSiteUrls()
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new SiteUrls();
                        }
                    }
                }
                return instance;

            }

            public static void SetInstance(SiteUrls anInstance)
            {
                if (anInstance != null)
                    instance = anInstance;
            }

            public static void SetInstance()
            {
                SetInstance(new SiteUrls());
            }


            /// <summary>
            /// 重写伪地址
            /// </summary>
            public class URLRewrite
            {
                #region 成员变量
                private string _Name;
                public string Name
                {
                    get
                    {
                        return _Name;
                    }
                    set
                    {
                        _Name = value;
                    }
                }

                private string _Pattern;
                public string Pattern
                {
                    get
                    {
                        return _Pattern;
                    }
                    set
                    {
                        _Pattern = value;
                    }
                }

                private string _Page;
                public string Page
                {
                    get
                    {
                        return _Page;
                    }
                    set
                    {
                        _Page = value;
                    }
                }

                private string _QueryString;
                public string QueryString
                {
                    get
                    {
                        return _QueryString;
                    }
                    set
                    {
                        _QueryString = value;
                    }
                }
                #endregion

                #region 构造函数
                public URLRewrite(string name, string pattern, string page, string querystring)
                {
                    _Name = name;
                    _Pattern = pattern;
                    _Page = page;
                    _QueryString = querystring;
                }
                #endregion
            }

        }

    }
}
