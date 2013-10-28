using Ant.Model;
using System;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class CompanyShow : BasePage
    {
        protected Users AntUser;
        protected CompanyConfig CConfig = YBB.Bll.Company.GetConfig();
        protected company Company = new company();
        protected string Mapstr = "";

        protected override void ShowPage()
        {
            base.CurrentDt = General.GetDataTable(1, "*,case when CompanyQianyuedate>getdate() then '1' else '0' end as qianyue", "Ant_Company", "CompanyID='" + AntRequest.GetInt("ID", 0) + "'", "");
            if (base.CurrentDt.Rows.Count != 1)
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("对不起，此商家不存在！"));
                base.Response.End();
            }
            else if (base.CurrentDt.Rows[0]["CompanyKill"].ToString() != "1")
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("对不起，商家还未通过审核，请稍候访问！"));
                base.Response.End();
            }
            else
            {
                string str = base.Request.Url.ToString().ToLower();
                string url = base.CompanyConfig.ChannelRewriteUrl + "StoreError.aspx";
                if ((((str.IndexOf("scontent") != -1) || (str.IndexOf("sorder") != -1)) || ((str.IndexOf("srenling") != -1) || (str.IndexOf("/storejubao") != -1))) || ((((str.IndexOf("/about") != -1) || (str.IndexOf("/culture") != -1)) || ((str.IndexOf("/hornor") != -1) || (str.IndexOf("/message") != -1))) || ((str.IndexOf("/contactus") != -1) || (str.IndexOf("/shopcontent") != -1))))
                {
                    if (str.IndexOf("shopcontent") != -1)
                    {
                        url = base.ShopConfig.ChannelRewriteUrl + "ShopError.aspx";
                    }
                    if ((base.CurrentDt.Rows[0]["CompanyQianyue"].ToString() != "1") || (Convert.ToDateTime(base.CurrentDt.Rows[0]["CompanyQianyueDate"]) <= DateTime.Now))
                    {
                        base.Response.Redirect(url);
                        base.Response.End();
                    }
                }
                Utils.ShowCompanyDetails(base.CurrentDt, base.CompanyConfig, this.CConfig, base.SiteConfig, ref this.Company);
                this.AntUser = Utils.GetUserLogin(base.SiteConfig);
            }
        }
    }

}
