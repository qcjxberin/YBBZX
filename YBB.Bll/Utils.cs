using Ant.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using YBB.Common;

namespace YBB.Bll
{
    public class Utils
    {
        public static string AcceptExt = "";
        public static long MaxSize;
        public static string strFileExt = "";
        public static string strFileSize = "";
        public static string stroldFileName = "";
        public static string strRndName = "";

        public static void ResponseSafe(int int_0)
        {
            if (int_0 == 2)
            {
                HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                HttpContext.Current.Response.Write("<head>");
                HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                HttpContext.Current.Response.Write("<title>域名未授权警告</title>");
                HttpContext.Current.Response.Write("</head>");
                HttpContext.Current.Response.Write("<body>");
                HttpContext.Current.Response.Write("<script> alert('您的域名没有通过授权，暂时不能访问！');</script>");
                HttpContext.Current.Response.Write("</body>");
                HttpContext.Current.Response.Write("</html>");
                HttpContext.Current.Response.End();
            }
            else
            {
                string str = "http://www.xiaomayi.co/Templates/Images/Ant_06.gif";
                string str2 = "http://www.xiaomayi.co/Templates/Images/Ant_09.gif";
                string str3 = "http://www.xiaomayi.co/Templates/Images/AntBg_03.gif";
                HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                HttpContext.Current.Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
                HttpContext.Current.Response.Write("<head>");
                HttpContext.Current.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                HttpContext.Current.Response.Write("<title>域名未授权警告</title>");
                HttpContext.Current.Response.Write("<style type=\"text/css\">");
                HttpContext.Current.Response.Write(".Ant { width:767px; height:248px;  background:url(" + str3 + ") no-repeat; margin: 0 auto;}");
                HttpContext.Current.Response.Write(".Ant * {font-size:12px; font-weight:normal; margin:0; padding:0;}");
                HttpContext.Current.Response.Write(@".Ant h1,.Ant h2 { padding-top:4px; text-indent:2em;font: 12px/1.5 tahoma, arial, \5b8b\4f53, sans-serif;}");
                HttpContext.Current.Response.Write(".Ant h1{ color:#FFFFFF;}");
                HttpContext.Current.Response.Write(".Ant h2 { padding-top:10px;}");
                HttpContext.Current.Response.Write(".AntInfo,.AntInfo h1 { height:160px;}");
                HttpContext.Current.Response.Write(".AntInfo { width:720px;position:relative; overflow:hidden; margin: 0 auto; margin-top:20px;}");
                HttpContext.Current.Response.Write(".AntInfo h1,.AntInfo h1 b{ text-indent:0;;height:auto; padding:0; font-size:24px; font-weight:bold; color:#d20606; font-family:\"微软雅黑\";width:290px; text-align:center; text-align:left;}");
                HttpContext.Current.Response.Write(".AntInfo h1 { padding-bottom:10px;}");
                HttpContext.Current.Response.Write(".AntInfo h1 b { color:#333333; width:500px;}");
                HttpContext.Current.Response.Write(".Picture { width:150px; text-align:center; padding-top:25px;}");
                HttpContext.Current.Response.Write(".Picture,.AntSystem { float:left;}");
                HttpContext.Current.Response.Write(".AntSystem { width:560px;}");
                HttpContext.Current.Response.Write(".AntInfo p { line-height:2em; float:left;}");
                HttpContext.Current.Response.Write(".AntLin { display:block; width:139px; height:40px; background:url(" + str2 + "); clear:both;}");
                HttpContext.Current.Response.Write("</style>");
                HttpContext.Current.Response.Write("</head>");
                HttpContext.Current.Response.Write("");
                HttpContext.Current.Response.Write("<body>");
                HttpContext.Current.Response.Write("</body>");
                HttpContext.Current.Response.Write("<div class=\"Ant\">");
                HttpContext.Current.Response.Write("<h1>一步半地方门户系统</h1>");
                HttpContext.Current.Response.Write("<h2>域名：" + AntRequest.GetDomain() + "</h2>");
                HttpContext.Current.Response.Write("<div class=\"AntInfo\">");
                HttpContext.Current.Response.Write("<div class=\"Picture\"><img src=\"" + str + "\" /></div>");
                HttpContext.Current.Response.Write("<div class=\"AntSystem\">");
                HttpContext.Current.Response.Write("<h1>警告：<b>非授权用户</b></h1>");
                HttpContext.Current.Response.Write("<p>您现在打开的域名未经一步半地方门户授权！如果您是本系统的正版用户，请使用授权的域名访问网站！<br />");
                HttpContext.Current.Response.Write("正版用户请联系系统开发方一步半为您生成域名授权文件，然后将授权文件拷贝到指定的目录中！<br />");
                HttpContext.Current.Response.Write("保护正版、保护你我的合法权益、人人有责，举报的请联系一步半！<br /></p>");
                HttpContext.Current.Response.Write("<a href=\"http://www.0760w.cn/Purchases.html\" class=\"AntLin\"></a>");
                HttpContext.Current.Response.Write("</div>");
                HttpContext.Current.Response.Write("</div>");
                HttpContext.Current.Response.Write("</div>");
                HttpContext.Current.Response.Write("</html>");
                HttpContext.Current.Response.End();
            }
        }

        /// <summary>
        /// 获取用户是否登录
        /// </summary>
        /// <param name="website_0"></param>
        /// <returns></returns>
        public static Users GetUserLogin(website website_0)
        {
            Users users = new Users();
            bool flag = false;
            bool flag2 = false;
            int num = 0;
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            string str6 = "";
            HttpCookie cookie = HttpContext.Current.Request.Cookies["AntLoginUsers"];
            if (cookie == null)
            {
                flag = false;
            }
            else if (cookie.Values.Count == 0)
            {
                flag = false;
            }
            else
            {
                flag2 = true;
                str = cookie["AntLoginKey"];
                str2 = HttpContext.Current.Server.UrlDecode(cookie["AntLoginUserid"]);
                str4 = HttpContext.Current.Server.UrlDecode(cookie["AntLoginUserName"]);
                str5 = HttpContext.Current.Server.UrlDecode(cookie["AntLoginUserEmail"]);
                try
                {
                    num = Convert.ToInt32(cookie["AntLoginCookieday"]);
                }
                catch
                {
                    num = 0;
                }
            }
            if (flag2 && !string.IsNullOrEmpty(str2))
            {
                if (YBB.Common.DES.Decode(YBB.Common.DES.Decode(str, SysConfig.ConfigUserPasswordKey), YBB.Common.MD5.Md5Str(str2, 0x10)) != (str4 + SysConfig.ConfigUserPasswordStr + str4))
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                    if (HttpContext.Current.Request.Url.Host.ToString().ToLower().IndexOf("26.ca") == -1)
                    {
                        if (num > 0)
                        {
                            cookie.Expires = DateTime.Now.AddMinutes((double)num);
                        }
                        cookie["AntLoginUserid"] = str2;
                        cookie["AntLoginUserName"] = HttpContext.Current.Server.UrlEncode(str4.Trim());
                        cookie["AntLoginUserEmail"] = HttpContext.Current.Server.UrlEncode(str5.Trim());
                        string str7 = YBB.Common.DES.Encode(YBB.Common.DES.Encode(str4.Trim() + SysConfig.ConfigUserPasswordStr + str4.Trim(), YBB.Common.MD5.Md5Str(str2, 0x10)), SysConfig.ConfigUserPasswordKey);
                        cookie["AntLoginKey"] = str7;
                        cookie["AntLogindate"] = DateTime.Now.ToString();
                        string cookieDomain = AntRequest.GetCookieDomain(website_0.SiteUrl);
                        if (cookieDomain.Length > 0)
                        {
                            cookie.Domain = cookieDomain;
                        }
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                }
            }
            if (flag)
            {
                users.UserID = Convert.ToInt32(str2);
                users.Username = str4;
                users.Truename = str3;
                users.UserEmail = str5;
                users.UserPwd = AES.Decode(YBB.Common.DES.Decode(str6, SysConfig.ConfigMemberKey), SysConfig.ConfigMemberKey);
                return users;
            }
            users.UserID = -1;
            return users;
        }

        public static DataTable TabConTab(DataTable dataTable_0, string string_0)
        {
            DataView view;
            lock (dataTable_0)
            {
                view = new DataView(dataTable_0, string_0, "", DataViewRowState.CurrentRows);
            }
            return view.ToTable();
        }

        public static string CreateCompanyUrl(bool bool_0, string string_0, string string_1, string string_2, string string_3, website website_0, AttrbuteCompany attrbuteCompany_0, string string_4, string string_5)
        {
            if (string_1 == "0")
            {
                return ComManagent.CreateCompanyShopUrl(bool_0, website_0.SiteAspxRewrite, attrbuteCompany_0.ChannelKill, website_0.SiteTemplateName, attrbuteCompany_0.ChannelRewriteUrl, string_0, website_0.SiteSafeDomain2, AntRequest.StrTrim(string_2), website_0.SiteUrl, string_3);
            }
            if (string_1 == "1")
            {
                return ComManagent.CreateCompanyUrl(bool_0, website_0.SiteAspxRewrite, attrbuteCompany_0.ChannelKill, website_0.SiteTemplateName, attrbuteCompany_0.ChannelRewriteUrl, string_0, website_0.SiteSafeDomain2, AntRequest.StrTrim(string_2), website_0.SiteUrl, string_3, website_0.SiteSafeDomain4, string_4, string_5);
            }
            return ComManagent.CreateStoreUrl(bool_0, website_0.SiteAspxRewrite, attrbuteCompany_0.ChannelKill, website_0.SiteTemplateName, attrbuteCompany_0.ChannelRewriteUrl, string_0, website_0.SiteSafeDomain2, AntRequest.StrTrim(string_2), website_0.SiteUrl, string_3, website_0.SiteSafeDomain4, string_4, string_5);
        }

        public static void ShowCompanyDetails(DataTable dataTable_0, AttrbuteCompany attrbuteCompany_0, CompanyConfig companyConfig_0, website website_0, ref company company_0)
        {
            General.Execute("Update Ant_Company set CompanyViews=CompanyViews+1 where CompanyID='" + dataTable_0.Rows[0]["CompanyID"] + "'");
            company_0.CompanyD = Base.StrToInt(dataTable_0.Rows[0]["CompanyD"], 0);
            company_0.CompanyDText = AntRequest.HtmlDecodeTrim(dataTable_0.Rows[0]["CompanyDText"]);
            company_0.CompanyName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyName"]);
            company_0.CompanyUserName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyUserName"]);
            company_0.CompanyViews = Convert.ToInt32(dataTable_0.Rows[0]["CompanyViews"]);
            if (company_0.CompanyUserName.Length == 0)
            {
                company_0.CompanyUserName = company_0.CompanyName;
            }
            company_0.CompanyUserid = Convert.ToInt32(dataTable_0.Rows[0]["CompanyUserid"]);
            company_0.CompanyMoban = Convert.ToInt32(dataTable_0.Rows[0]["CompanyMoban"]);
            company_0.CompanyID = Convert.ToInt32(dataTable_0.Rows[0]["CompanyID"]);
            company_0.CompanyTypeid = Convert.ToInt32(dataTable_0.Rows[0]["CompanyTypeid"]);
            company_0.CompanyXiangmu = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyXiangmu"]);
            company_0.CompanyDomain = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyDomain"]);
            company_0.CompanyDomainKill = Convert.ToInt32(dataTable_0.Rows[0]["CompanyDomainKill"]);
            company_0.DomainMain = AntRequest.StrTrim(dataTable_0.Rows[0]["DomainMain"]);
            company_0.DomainTypeid = Convert.ToInt32(dataTable_0.Rows[0]["DomainTypeid"]);
            company_0.CompanyAreaID = Convert.ToInt32(dataTable_0.Rows[0]["CompanyAreaID"]);
            company_0.CompanyUrl = CreateCompanyUrl(false, company_0.CompanyID.ToString(), company_0.CompanyTypeid.ToString(), company_0.CompanyDomain, company_0.CompanyDomainKill.ToString(), website_0, attrbuteCompany_0, company_0.DomainMain, company_0.DomainTypeid.ToString());
            company_0.CompanyHaoping = 100.0;
            company_0.CompanyQianyue = Convert.ToInt32(dataTable_0.Rows[0]["qianyue"]);
            company_0.CompanyRenZheng = Convert.ToInt32(dataTable_0.Rows[0]["CompanyRenZheng"]);
            company_0.CompanyDate = Convert.ToDateTime(dataTable_0.Rows[0]["CompanyDate"]);
            company_0.CompanyYingye = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyYingye"]);
            company_0.CompanyMark = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyMark"]);
            company_0.CompanyFilepath = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyFilepath"]);
            company_0.CompanyContent = AntRequest.HtmlDecode(dataTable_0.Rows[0]["CompanyContent"].ToString());
            company_0.CompanyXingyong = Convert.ToInt32(dataTable_0.Rows[0]["CompanyXingyong"]);
            company_0.CompanyQQ1 = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyQQ1"]);
            company_0.CompanyQQ2 = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyQQ2"]);
            company_0.CompanyType1 = Convert.ToInt32(dataTable_0.Rows[0]["CompanyType1"]);
            company_0.CompanyType2 = Convert.ToInt32(dataTable_0.Rows[0]["CompanyType2"]);
            company_0.CompanyType3 = Convert.ToInt32(dataTable_0.Rows[0]["CompanyType3"]);
            company_0.CompanyType4 = Convert.ToInt32(dataTable_0.Rows[0]["CompanyType4"]);
            company_0.CompanyHaopingNum = Convert.ToInt32(dataTable_0.Rows[0]["CompanyHaopingNum"]);
            company_0.CompanyZhongNum = Convert.ToInt32(dataTable_0.Rows[0]["CompanyZhongNum"]);
            company_0.CompanyChaNum = Convert.ToInt32(dataTable_0.Rows[0]["CompanyChaNum"]);
            company_0.CompanyTitle = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyTitle"]);
            company_0.CompanyKeyword = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyKeyword"]);
            company_0.CompanyDescription = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyDescription"]);
            company_0.CompanyQuanClose = Base.StrToInt(dataTable_0.Rows[0]["CompanyQuanClose"], 0);
            company_0.CompanyVideoClose = Base.StrToInt(dataTable_0.Rows[0]["CompanyVideoClose"], 0);
            company_0.CompanyBanner = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyBanner"]);
            company_0.CompanyVideoName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyVideoName"]);
            company_0.CompanyQuanName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyQuanName"]);
            if (((company_0.CompanyBanner.Length > 0) && company_0.CompanyBanner.ToLower().EndsWith(".swf")) && File.Exists(HttpContext.Current.Server.MapPath("/" + company_0.CompanyBanner)))
            {
                FlashInfo info = new FlashInfo(HttpContext.Current.Server.MapPath("/" + company_0.CompanyBanner));
                company_0.CompanyBannerWidth = info.Width;
                company_0.CompanyBannerHeight = info.Height;
            }
            if (string.IsNullOrEmpty(company_0.CompanyTitle))
            {
                company_0.CompanyTitle = company_0.CompanyName;
            }
            if (string.IsNullOrEmpty(company_0.CompanyKeyword))
            {
                company_0.CompanyKeyword = company_0.CompanyName;
            }
            if (string.IsNullOrEmpty(company_0.CompanyDescription))
            {
                company_0.CompanyDescription = company_0.CompanyMark;
            }
            if (company_0.CompanyFilepath.Length == 0)
            {
                if (company_0.CompanyQianyue == 1)
                {
                    company_0.CompanyFilepath = companyConfig_0.CompanyFilepath2;
                }
                else
                {
                    company_0.CompanyFilepath = companyConfig_0.CompanyFilepath1;
                }
            }
            if (company_0.CompanyQianyue == 1)
            {
                if ((company_0.CompanyTypeid != 0) && (company_0.CompanyTypeid != 2))
                {
                    company_0.CompanyFileNum = companyConfig_0.CompanyIndexNum2;
                    company_0.CompanyLogoShow = companyConfig_0.CompanyIndexLogo2;
                }
                else
                {
                    company_0.CompanyFileNum = companyConfig_0.CompanyPicNum2;
                    company_0.CompanyLogoShow = companyConfig_0.CompanyLogo2;
                }
            }
            else if ((company_0.CompanyTypeid != 0) && (company_0.CompanyTypeid != 2))
            {
                company_0.CompanyFileNum = companyConfig_0.CompanyIndexNum1;
                company_0.CompanyLogoShow = companyConfig_0.CompanyIndexLogo1;
            }
            else
            {
                company_0.CompanyLogoShow = companyConfig_0.CompanyLogo1;
                company_0.CompanyFileNum = companyConfig_0.CompanyPicNum1;
            }
            company_0.CompanyComments = General.Count("Ant_CompanyComment", " CommentCompanyID='" + dataTable_0.Rows[0]["CompanyID"] + "' and CommentKill=1 ");
            if (((Convert.ToInt32(dataTable_0.Rows[0]["CompanyHaopingNum"]) + Convert.ToInt32(dataTable_0.Rows[0]["CompanyZhongNum"])) + Convert.ToInt32(dataTable_0.Rows[0]["CompanyChaNum"])) > 0)
            {
                double num = Convert.ToDouble(dataTable_0.Rows[0]["CompanyHaopingNum"]) / ((Convert.ToDouble(dataTable_0.Rows[0]["CompanyHaopingNum"]) + Convert.ToDouble(dataTable_0.Rows[0]["CompanyZhongNum"])) + Convert.ToDouble(dataTable_0.Rows[0]["CompanyChaNum"]));
                company_0.CompanyHaoping = Convert.ToDouble((num * 100.0).ToString("F0"));
            }
            company_0.CompanyLogo = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyLogo"]);
            DataTable table = TabConTab(Area.DataList(), "AreaID='" + dataTable_0.Rows[0]["CompanyAreaID"] + "'");
            if (table.Rows.Count == 1)
            {
                company_0.CompanyArea = "<a href='" + ComManagent.CreateCompanyCommonUrl(false, website_0.SiteAspxRewrite, attrbuteCompany_0.ChannelKill, website_0.SiteTemplateName, attrbuteCompany_0.ChannelRewriteUrl, "Storelist", "id=0&areaid=" + table.Rows[0]["AreaID"].ToString()) + "'>" + AntRequest.StrTrim(table.Rows[0]["AreaName"]) + "</a>";
                if (AntRequest.StrTrim(table.Rows[0]["AreaParent"]) != "0")
                {
                    company_0.CompanyArea = "<a href='" + ComManagent.CreateCompanyCommonUrl(false, website_0.SiteAspxRewrite, attrbuteCompany_0.ChannelKill, website_0.SiteTemplateName, attrbuteCompany_0.ChannelRewriteUrl, "Storelist", "id=0&areaid=" + table.Rows[0]["AreaParent"].ToString()) + "'>" + AntRequest.StrTrim(table.Rows[0]["ParentAreaName"]) + "</a>-" + company_0.CompanyArea;
                }
            }
            table = General.DataList("select case when avg(CAST(CommentNum1 AS numeric)) is null then '5' else avg(CAST(CommentNum1 AS numeric)) end  as CommentNum1,case when avg(CAST(CommentNum2 AS numeric)) is null then '5' else avg(CAST(CommentNum2 AS numeric)) end  as CommentNum2,case when avg(CAST(CommentNum3 AS numeric)) is null then '5' else avg(CAST(CommentNum3 AS numeric)) end as CommentNum3 from Ant_CompanyComment where CommentKill=1 and CommentCompanyID='" + company_0.CompanyID + "' ");
            if (table.Rows.Count == 1)
            {
                company_0.CommentNum1 = Convert.ToDouble(table.Rows[0]["CommentNum1"]);
                company_0.CommentNum2 = Convert.ToDouble(table.Rows[0]["CommentNum2"]);
                company_0.CommentNum3 = Convert.ToDouble(table.Rows[0]["CommentNum3"]);
            }
            if (company_0.CommentNum1 == 0.0)
            {
                company_0.CommentNum1 = 5.0;
            }
            if (company_0.CommentNum2 == 0.0)
            {
                company_0.CommentNum2 = 5.0;
            }
            if (company_0.CommentNum3 == 0.0)
            {
                company_0.CommentNum3 = 5.0;
            }
            company_0.CompanyContentLen = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyContentLen"]);
            company_0.CompanyContent = AntRequest.HtmlDecode(dataTable_0.Rows[0]["CompanyContent"].ToString());
            company_0.CompanyContentName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyContentName"]);
            company_0.CompanyTongji = AntRequest.HtmlDecodeTrim(dataTable_0.Rows[0]["CompanyTongji"]);
            company_0.CompanyProductName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyProductName"]);
            company_0.CompanyNewsName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyNewsName"]);
            company_0.CompanyHuoName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyHuoName"]);
            company_0.CompanyAnliName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyAnliName"]);
            company_0.CompanyTel = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyTel"]);
            company_0.CompanyMan = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyMan"]);
            company_0.CompanyRongyuName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyRongyuName"]);
            company_0.CompanyWenhuaName = AntRequest.StrTrim(dataTable_0.Rows[0]["CompanyWenhuaName"]);
            company_0.CompanyWenhua = AntRequest.HtmlDecode(dataTable_0.Rows[0]["CompanyWenhua"].ToString());
            company_0.CompanyRongyu = AntRequest.HtmlDecode(dataTable_0.Rows[0]["CompanyRongyu"].ToString());
            company_0.CompanyZheKou = Convert.ToDouble(dataTable_0.Rows[0]["CompanyZheKou"].ToString());
            company_0.CompanyZheKouMark = AntRequest.HtmlDecode(dataTable_0.Rows[0]["CompanyZheKouMark"].ToString());
            company_0.DomainBeian = AntRequest.StrTrim(dataTable_0.Rows[0]["DomainBeian"]);
            if (company_0.DomainBeian.Length == 0)
            {
                company_0.DomainBeian = website_0.SiteBeiAn;
            }
            company_0.CompanyCommentKill = 1;
            if (company_0.CompanyQianyue == 0)
            {
                if (companyConfig_0.CompanyRightComment1 == 0)
                {
                    company_0.CompanyCommentKill = Convert.ToInt32(dataTable_0.Rows[0]["CompanyCommentKill"]);
                }
            }
            else if (companyConfig_0.CompanyRightComment2 == 0)
            {
                company_0.CompanyCommentKill = Convert.ToInt32(dataTable_0.Rows[0]["CompanyCommentKill"]);
            }
            company_0.CompanyCommentThemes = Convert.ToInt32(dataTable_0.Rows[0]["CompanyCommentThemes"]);
        }

        public static string RandCode(int int_0)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9".Split(new char[] { ',' });
            string str2 = "";
            int num = -1;
            Random random = new Random();
            for (int i = 1; i < (int_0 + 1); i++)
            {
                if (num != -1)
                {
                    random = new Random(GetRandomSeed());
                }
                int index = random.Next(10);
                if ((num != -1) && (num == index))
                {
                    return RandCode(int_0);
                }
                num = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }

        public static int GetRandomSeed()
        {
            byte[] data = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public static string ShowYp(website website_0, AttrbuteLife attrbuteLife_0, object object_0, int int_0)
        {
            string str = "黄页浏览";
            if (int_0 == 1)
            {
                str = "";
            }
            if (!(object_0.ToString() != "0"))
            {
                return str;
            }
            DataTable table = General.DataList("select top 1 lifeid from Ant_Life where LifeUserID='" + object_0 + "' and LifeKill=1 ");
            if (table.Rows.Count != 1)
            {
                return str;
            }
            string str2 = ComManagent.CreateLifeUrl(false, website_0.SiteAspxRewrite, attrbuteLife_0.ChannelKill, website_0.SiteTemplateName, attrbuteLife_0.ChannelRewriteUrl, table.Rows[0]["LifeID"].ToString());
            if (int_0 == 1)
            {
                return str2;
            }
            return ("<a href='" + str2 + "' target='_blank'>黄页浏览</a>");
        }

        public static string GetLoginOver(string string_0, string string_1)
        {
            AntCache cacheService = AntCache.GetCacheService();
            string str = cacheService.RetrieveObject("/Ant/LoginOver") as string;
            if (str == null)
            {
                str = "";
                if (string_1 == "1")
                {
                    str = YBB.Common.DES.Encode(AES.Encode(DateTime.Now.ToString(), string_0), "bojie");
                    cacheService.AddObject("/Ant/LoginOver", str);
                }
            }
            return str;
        }

        public static void DeleteNewsFile(string string_0, string string_1)
        {
            for (int i = 1; i < 15; i++)
            {
                string str = HttpContext.Current.Server.MapPath(string.Concat(new object[] { "~/Html/Ant/", string_0, string_1, "-", i, ".html" }));
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
            }
            string path = HttpContext.Current.Server.MapPath("~/Html/Ant/News/NewsComment" + string_1 + ".html");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static string Decompress(string string_0)
        {
            byte[] buffer = Convert.FromBase64String(string_0);
            MemoryStream stream = new MemoryStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Position = 0L;
            DeflateStream stream2 = new DeflateStream(stream, CompressionMode.Decompress);
            stream2.Flush();
            byte[] buffer2 = new byte[0x4100];
            int count = stream2.Read(buffer2, 0, 0x4100);
            stream2.Close();
            return Encoding.Unicode.GetString(buffer2, 0, count);
        }

        public static string GetNewsCategory(object object_0)
        {
            string str = "0";
            SqlDataReader reader = General.GetDataReader(0, "TypeID", "Ant_NewsType", "TypeKill=0 and (TypeID='" + object_0.ToString() + "' or TypeParent='" + object_0.ToString() + "')", "");
            while (reader.Read())
            {
                str = str + "," + reader["TypeID"].ToString();
            }
            reader.Close();
            reader.Dispose();
            if (str != "0")
            {
                str = str.Substring(2, str.Length - 2);
            }
            return str;
        }

        public static bool CheckInfoLaji(string string_0, string string_1, string string_2, string string_3, website website_0)
        {
            bool flag = false;
            if (((string_1 != "") && (string_1 != null)) && !flag)
            {
                string siteInfoTEL = website_0.SiteInfoTEL;
                if (siteInfoTEL.Length > 0)
                {
                    string[] strArray = Base.SplitPage(siteInfoTEL, "\r\n");
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string str2 = strArray[i].ToString();
                        if ((str2.Length > 0) && (string_1.Trim().ToLower().IndexOf(str2.Trim()) != -1))
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (((string_0 != "") && (string_0 != null)) && !flag)
            {
                string siteInfoQQ = website_0.SiteInfoQQ;
                if (siteInfoQQ.Length > 0)
                {
                    string[] strArray2 = Base.SplitPage(siteInfoQQ, "\r\n");
                    for (int j = 0; j < strArray2.Length; j++)
                    {
                        string str4 = strArray2[j].ToString();
                        if ((str4.Length > 0) && (string_0.Trim().ToLower().IndexOf(str4.Trim()) != -1))
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (((string_2 != "") && (string_2 != null)) && !flag)
            {
                string siteInfoTitle = website_0.SiteInfoTitle;
                if (siteInfoTitle.Length > 0)
                {
                    string[] strArray3 = Base.SplitPage(siteInfoTitle, "\r\n");
                    for (int k = 0; k < strArray3.Length; k++)
                    {
                        string str6 = strArray3[k].ToString();
                        if ((str6.Length > 0) && (string_2.Trim().ToLower().IndexOf(str6.Trim()) != -1))
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (((string_3 != "") && (string_3 != null)) && !flag)
            {
                string siteInfoContent = website_0.SiteInfoContent;
                if ((siteInfoContent != "") && (siteInfoContent != null))
                {
                    string[] strArray4 = Base.SplitPage(siteInfoContent, "\r\n");
                    for (int m = 0; m < strArray4.Length; m++)
                    {
                        string str8 = strArray4[m].ToString();
                        if ((str8.Length > 0) && (string_3.Trim().ToLower().IndexOf(str8.Trim()) != -1))
                        {
                            flag = true;
                        }
                    }
                }
            }
            if (((website_0.SiteInfoArea == 0) && !flag) && (Base.GetIpAddress().IndexOf(website_0.SiteCity) != -1))
            {
                flag = true;
            }
            if (((website_0.SiteInfoTime.Length > 0) && !flag) && (("," + website_0.SiteInfoTime + ",").IndexOf("," + DateTime.Now.Hour.ToString() + ",") != -1))
            {
                flag = true;
            }
            return flag;
        }

 

    }
}