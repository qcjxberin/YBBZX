using Ant.Model;
using System.Data;
using System.Data.SqlClient;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class ComShop : BasePage
    {
        protected Users AntUser;
        protected CompanyConfig CConfig = YBB.Bll.Company.GetConfig();
        protected int ClassID;
        protected company Company = new company();
        protected int CurrentID = 5;
        protected SqlDataReader dr;
        protected int o1;
        protected string Mapstr = "";
        protected int P1;
        protected int P2;
        protected int P3;
        protected int P4;
        protected double R1;
        protected double R2;
        protected string SearchKeyword = "";
        protected string SearchR1 = "";
        protected string SearchR2 = "";
        protected string tt = "";

        protected override void ShowPage()
        {
            int @int = AntRequest.GetInt("ID", 0);
            base.CurrentDt = General.GetDataTable(1, "*,case when CompanyQianyuedate>getdate() then '1' else '0' end as qianyue", "Ant_Company", "CompanyID='" + @int + "'", "");
            if (base.CurrentDt.Rows.Count != 1)
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("对不起，此店铺不存在！"));
                base.Response.End();
            }
            else if (base.CurrentDt.Rows[0]["CompanyKill"].ToString() != "1")
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("对不起，店铺还未通过审核，请稍候访问！"));
                base.Response.End();
            }
            else
            {
                this.AntUser = Utils.GetUserLogin(base.SiteConfig);
                Utils.ShowCompanyDetails(base.CurrentDt, base.CompanyConfig, this.CConfig, base.SiteConfig, ref this.Company);
                int num2 = AntRequest.GetInt("p", 1);
                this.SearchKeyword = AntRequest.GetString("SearchKeyword");
                this.tt = AntRequest.GetString("tt");
                this.ClassID = AntRequest.GetInt("scid", 0);
                this.P1 = AntRequest.GetInt("P1", 0);
                this.P2 = AntRequest.GetInt("P2", 0);
                this.P3 = AntRequest.GetInt("P3", 0);
                this.P4 = AntRequest.GetInt("P4", 0);
                this.o1 = AntRequest.GetInt("o1", 1);
                this.R1 = AntRequest.GetFloat("R1", 0f);
                this.R2 = AntRequest.GetFloat("R2", 0f);
                if (this.R1 > 0.0)
                {
                    this.SearchR1 = this.R1.ToString();
                }
                if (this.R2 > 0.0)
                {
                    this.SearchR2 = this.R2.ToString();
                }
                if (num2 < 1)
                {
                    num2 = 1;
                }
                if (base.SiteConfig.SiteAspxRewrite == 1)
                {
                    this.SearchKeyword = this.SearchKeyword.Replace("ss", "");
                    base.CurrentSearchUrl = "ItemShopSearch.aspx?id={CompanyID}&o1={o1}&scid={ClassID}&P1={P1}&P2={P2}&P3={P3}&P4={P4}&R1={R1}&R2={R2}&SearchKeyword={SearchKeyword}";
                    base.CurrentUrl = string.Concat(new object[] { 
                    "ItemShopSearch.aspx?ID=", @int, "&o1= ", this.o1, "&scid=", this.ClassID, "&P1=", this.P1, "&P2=", this.P2, "&P3=", this.P3, "&P4 = ", this.P4, "&R1= ", this.R1, 
                    "&R2= ", this.R2, "&SearchKeyword=", base.Server.UrlEncode(this.SearchKeyword), "&p={PageIndex}"
                 });
                }
                else
                {
                    base.CurrentSearchUrl = "ItemShopSearch-{CompanyID}-{o1}-{ClassID}-{P1}-{P2}-{P3}-{P4}-{R1}-{R2}-S{SearchKeyword}S-p0" + base.SiteConfig.SiteTemplateName;
                    base.CurrentUrl = string.Concat(new object[] { 
                    "ItemShopSearch-", @int, "-", this.o1, "-", this.ClassID, "-", this.P1, "-", this.P2, "-", this.P3, "-", this.P4, "-", this.R1, 
                    "-", this.R2, "-S", base.Server.UrlEncode(this.SearchKeyword), "S-p{PageIndex}", base.SiteConfig.SiteTemplateName
                 });
                }
                base.CurrentUrl = base.CurrentUrl.Replace(" ", "");
                if (base.Request.Url.ToString().ToLower().IndexOf("comshop") != -1)
                {
                    base.CurrentSearchUrl = base.CurrentSearchUrl.Replace("ItemShopSearch", "shopview");
                    base.CurrentUrl = base.CurrentUrl.Replace("ItemShopSearch", "shopview");
                }
                base.CurrentPageIndex = num2;
                string str = " ShopKill= 1 and ShopCompanyID='" + this.Company.CompanyID + "'";
                string str2 = "ShopOrder desc,ShopDate desc,ShopID desc";
                if (this.o1 == 1)
                {
                    str2 = "ShopBuyNum desc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 2)
                {
                    str2 = "ShopBuyNum asc,ShopOrder desc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 3)
                {
                    str2 = "ShopOrder desc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 4)
                {
                    str2 = "ShopOrder asc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 5)
                {
                    str2 = "ShopMoney desc,ShopOrder desc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 6)
                {
                    str2 = "ShopMoney asc,ShopOrder desc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 7)
                {
                    str2 = "ShopViews desc,ShopOrder desc,ShopDate desc,ShopID desc";
                }
                else if (this.o1 == 8)
                {
                    str2 = "ShopViews asc,ShopOrder desc,ShopDate desc,ShopID desc";
                }
                if (this.ClassID > 0)
                {
                    DataTable table = General.DataList("select ClassID from Ant_ShopCategory where ClassParent ='" + Base.StrToInt(this.ClassID, 0) + "'");
                    if (table.Rows.Count > 0)
                    {
                        str = str + " and (  ";
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (i > 0)
                            {
                                str = str + " or ";
                            }
                            str = str + "   ','+ShopCategoryID+',' like '%," + table.Rows[i]["ClassID"].ToString() + ",%' ";
                        }
                        str = str + " ) ";
                    }
                    else
                    {
                        object obj2 = str;
                        str = string.Concat(new object[] { obj2, " and ','+ShopCategoryID+',' like '%,", this.ClassID, ",%' " });
                    }
                }
                if (this.P1 == 1)
                {
                    str = str + " and ShopType1=1 ";
                }
                if (this.P2 == 1)
                {
                    str = str + " and ShopType2=1 ";
                }
                if (this.P3 == 1)
                {
                    str = str + " and ShopType3=1 ";
                }
                if (this.P4 == 1)
                {
                    str = str + " and ShopType4=1 ";
                }
                if ((this.R1 > 0.0) && (this.R2 > 0.0))
                {
                    object obj3 = str;
                    str = string.Concat(new object[] { obj3, " and ShopMoney>=cast(", this.R1, " as money) and ShopMoney<=cast(", this.R2, " as money) " });
                }
                if (this.SearchKeyword.Length > 0)
                {
                    string[] strArray = this.SearchKeyword.Split(new char[] { ' ' });
                    string str3 = "";
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        string str4 = str3;
                        str3 = str4 + Base.iif(str3.Length > 0, " or ", "") + "  ( ShopName  like '%" + Base.Chk39(strArray[j].ToString()) + "%' or ShopCode  like '%" + Base.Chk39(strArray[j].ToString()) + "%'  )";
                    }
                    str = str + " and ( " + str3 + ")";
                }
                base.TotalItemCount = General.Count("Ant_Shop", str);
                this.dr = General.Page("ShopID,ShopName,ShopClassID,ShopBuyNum,ShopCompanyID,ShopCategoryID,ShopKill,ShopDate,ShopFilepath,ShopOrder,ShopViews,ShopImage,ShopMoney", "Ant_Shop", str, str2, base.ItemCountPerPage, base.CurrentPageIndex, base.TotalItemCount);
                if ((base.TotalItemCount % base.ItemCountPerPage) == 0)
                {
                    base.TotalPageCount = base.TotalItemCount / base.ItemCountPerPage;
                }
                else
                {
                    base.TotalPageCount = (base.TotalItemCount / base.ItemCountPerPage) + 1;
                }
                base.CurrentPageIndex = (num2 > base.TotalPageCount) ? base.TotalPageCount : num2;
            }
        }
    }

}
