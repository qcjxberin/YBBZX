using Ant.Model;
using System;
using System.Data;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class LoginOver : BasePage
    {
        protected string AntRegScript;
        protected string AntRegUrl;
        protected Users AntUser;

        protected override void ShowPage()
        {
            this.AntUser = Utils.GetUserLogin(base.SiteConfig);
            DataTable table = Member.MemberSeleteByUserID(this.AntUser.UserID);
            if (table.Rows.Count == 1)
            {
                this.AntUser.GradeType = Base.StrToInt(table.Rows[0]["styleid"].ToString(), 0);
                this.AntUser.Username = table.Rows[0]["chrname"].ToString();
                this.AntUser.UserPwd = table.Rows[0]["chrpwd"].ToString();
                this.AntUser.UserPwd = DES.Decode(AES.Decode(this.AntUser.UserPwd, SysConfig.ConfigMemberKey), SysConfig.ConfigMemberKey);
                this.AntRegScript = DES.Decode(AntRequest.StrTrim(table.Rows[0]["LoginKey"]), "logingo1");
            }
            if (this.AntUser.UserID == -1)
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("对不起，您没有登陆，请重新登陆，没权进行此步操作。"));
                base.Response.End();
            }
            else
            {
                if (this.AntRegScript == "Ant")
                {
                    this.AntRegScript = "";
                }
                string str = Convert.ToString(this.Session["AntDanLogin"]);
                string siteWebUrl = AntRequest.GetString("from");
                if (str.Length > 0)
                {
                    siteWebUrl = str;
                }
                if (AntRequest.GetString("action") == "forum")
                {
                    string siteForumUrl = base.SiteConfig.SiteForumUrl;
                }
                switch (siteWebUrl)
                {
                    case "":
                    case null:
                        try
                        {
                            siteWebUrl = base.Request.UrlReferrer.ToString();
                        }
                        catch
                        {
                            siteWebUrl = base.SiteConfig.SiteWebUrl;
                        }
                        break;
                }
                if (siteWebUrl.ToLower().IndexOf("memberout.aspx") != -1)
                {
                    siteWebUrl = base.SiteConfig.SiteWebUrl;
                }
                if (siteWebUrl.ToLower().IndexOf("memberreg.aspx") != -1)
                {
                    siteWebUrl = base.SiteConfig.SiteWebUrl + "Account";
                }
                if (siteWebUrl.ToLower().IndexOf("memberregister.aspx") != -1)
                {
                    siteWebUrl = base.SiteConfig.SiteWebUrl + "Account";
                }
                this.AntRegUrl = siteWebUrl;
                if (Convert.ToString(this.Session["LoginQQFflag"]).Length > 0)
                {
                    this.AntRegScript = this.AntRegScript + "<script src='" + Convert.ToString(this.Session["LoginQQFflag"]) + "'></script>";
                    this.Session["LoginQQFflag"] = "";
                }
                string str4 = "";
                if (base.SiteConfig.SiteUrl.ToLower().StartsWith("www."))
                {
                    string[] strArray = base.SiteConfig.SiteUrl.Split(new char[] { '.' });
                    if ((strArray.Length > 2) && (strArray[1].ToString().Length < 3))
                    {
                        str4 = base.SiteConfig.SiteUrl.ToLower().Replace("www.", "");
                    }
                }
                if (str4.Length > 0)
                {
                    string antRegScript = this.AntRegScript;
                    this.AntRegScript = antRegScript + "<script type=\"text/javascript\" src=\"http://" + str4 + "/public/ajax.aspx?action=login&chrname=" + this.AntUser.Username + "&chrpwd=" + this.AntUser.UserPwd + "&script=1&time=" + Utils.RandCode(10) + "\" reload=\"1\"></script>";
                }
            }
        }
    }

}
