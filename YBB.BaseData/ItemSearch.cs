using Ant.Model;
using System.Data;
using System.Data.SqlClient;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class ItemSearch : BasePage
    {
        protected Users AntUser;
        protected int BrandID;
        protected int ClassID;
        protected int CurrentID;
        protected SqlDataReader dr;
        protected int o1;
        protected int P1;
        protected int P2;
        protected int P3;
        protected int P4;
        protected string PageDescription = "";
        protected string PageKeyword = "";
        protected string PageTitle = "";
        protected double R1;
        protected double R2;
        protected string SearchKeyword;
        protected string SearchR1 = "";
        protected string SearchR2 = "";
        protected DataTable ShopClassDt;

        protected override void ShowPage()
        {
            if (base.ShopConfig.ChannelClose == 1)
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=88");
                base.Response.End();
            }
            this.AntUser = Utils.GetUserLogin(base.SiteConfig);
            int @int = AntRequest.GetInt("p", 1);
            this.SearchKeyword = AntRequest.GetString("SearchKeyword");
            this.BrandID = AntRequest.GetInt("brandid", 0);
            this.ClassID = AntRequest.GetInt("id", 0);
            this.ShopClassDt = General.DataList("select top 1 ClassID,ClassName,ClassLevel from Ant_ShopClass where ClassID =" + this.ClassID.ToString());
            this.P1 = AntRequest.GetInt("P1", 0);
            this.P2 = AntRequest.GetInt("P2", 0);
            this.P3 = AntRequest.GetInt("P3", 0);
            this.P4 = AntRequest.GetInt("P4", 0);
            this.o1 = AntRequest.GetInt("o1", 0);
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
            if (@int < 1)
            {
                @int = 1;
            }
            if (base.SiteConfig.SiteAspxRewrite == 1)
            {
                this.SearchKeyword = this.SearchKeyword.Replace("ss", "");
                base.CurrentSearchUrl = "ItemSearch.aspx?id={ID}&BrandID={BrandID}&P1={P1}&P2={P2}&P3={P3}&P4={P4}&o1={o1}&R1={R1}&R2={R2}&SearchKeyword={SearchKeyword}";
                base.CurrentUrl = string.Concat(new object[] { 
                "ItemSearch.aspx?ID=", this.ClassID, "&BrandID=", this.BrandID, "&P1=", this.P1, "&P2=", this.P2, "&P3=", this.P3, "&P4 = ", this.P4, "&o1= ", this.o1, "&R1= ", this.R1, 
                "&R2= ", this.R2, "&SearchKeyword=", this.SearchKeyword, "&p={PageIndex}"
             });
            }
            else
            {
                base.CurrentSearchUrl = "ItemSearch-{ID}-{BrandID}-{P1}-{P2}-{P3}-{P4}-{o1}-{R1}-{R2}-S{SearchKeyword}S-p0" + base.SiteConfig.SiteTemplateName;
                base.CurrentUrl = string.Concat(new object[] { 
                "ItemSearch-", this.ClassID, "-", this.BrandID, "-", this.P1, "-", this.P2, "-", this.P3, "-", this.P4, "-", this.o1, "-", this.R1, 
                "-", this.R2, "-S", this.SearchKeyword, "S-p{PageIndex}", base.SiteConfig.SiteTemplateName
             });
            }
            base.CurrentUrl = base.CurrentUrl.Replace(" ", "");
            base.CurrentPageIndex = @int;
            string str = " ShopKill= 1 and CompanyKill=1  ";
            string str2 = "ShopOrder desc,ShopDate desc,ShopID desc";
            if (this.o1 == 1)
            {
                str2 = "ShopMoney  desc,ShopDate desc,ShopID desc";
            }
            else if (this.o1 == 2)
            {
                str2 = "CompanyXingyong desc,ShopDate desc,ShopID desc";
            }
            else if (this.o1 == 3)
            {
                str2 = "ShopOrder asc,ShopDate desc,ShopID desc";
            }
            else if (this.o1 == 4)
            {
                str2 = "ShopMoney asc,ShopDate desc,ShopID desc";
            }
            else if (this.o1 == 5)
            {
                str2 = "CompanyXingyong asc,ShopDate desc,ShopID desc";
            }
            else if (this.o1 == 6)
            {
                str2 = "ShopIndexMoney desc,ShopDate desc,ShopID desc";
            }
            if (this.BrandID > 0)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, " and  ShopPinpaiID='", this.BrandID, "'   " });
            }
            if (this.ClassID > 0)
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, " and ShopClassID in (select ClassID from Ant_SelectShopTClass where bigClassParent='", this.ClassID, "' or ClassParent='", this.ClassID, "'  or ClassID='", this.ClassID, "' )  " });
            }
            if (this.P1 == 1)
            {
                this.CurrentID = 1;
                str = str + " and ShopType1=1 ";
            }
            if (this.P2 == 1)
            {
                this.CurrentID = 2;
                str = str + " and ShopType2=1 ";
            }
            if (this.P3 == 1)
            {
                this.CurrentID = 3;
                str = str + " and ShopType3=1 ";
            }
            if (this.P4 == 1)
            {
                this.CurrentID = 4;
                str = str + " and ShopType4=1 ";
            }
            if ((this.R1 > 0.0) && (this.R2 > 0.0))
            {
                object obj4 = str;
                str = string.Concat(new object[] { obj4, " and ShopMoney>=cast(", this.R1, " as money) and ShopMoney<=cast(", this.R2, " as money) " });
            }
            if (this.BrandID > 0)
            {
                object obj5 = str;
                str = string.Concat(new object[] { obj5, " and ShopPinpaiid='", this.BrandID, "'" });
            }
            if ((this.SearchKeyword.Length > 0) && (this.SearchKeyword != "easy"))
            {
                string[] strArray = this.SearchKeyword.Split(new char[] { ' ' });
                string str3 = "";
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (AntRequest.StrTrim(strArray[i].ToString()).Length > 0)
                    {
                        string str6 = str3;
                        str3 = str6 + Base.iif(str3.Length > 0, " or ", "") + "  ( ShopName  like '%" + Base.Chk39(strArray[i].ToString()) + "%' or ShopCode  like '%" + Base.Chk39(strArray[i].ToString()) + "%'  or CompanyName  like '%" + Base.Chk39(strArray[i].ToString()) + "%' )";
                    }
                }
                str = str + " and ( " + str3 + ")";
            }
            if (this.ClassID > 0)
            {
                DataTable table = General.GetDataTable(1, "ClassTitle,ClassKeyword,ClassDescription", "Ant_ShopClass", "ClassID='" + this.ClassID + "'", "");
                if (table.Rows.Count == 1)
                {
                    this.PageTitle = table.Rows[0]["ClassTitle"].ToString();
                    this.PageKeyword = table.Rows[0]["ClassKeyword"].ToString();
                    this.PageDescription = table.Rows[0]["ClassDescription"].ToString();
                }
            }
            if (this.BrandID > 0)
            {
                string str4 = "";
                DataTable table2 = General.GetDataTable(1, "ClassName", "Ant_ShopPinpai", "Classid='" + this.BrandID + "'", "");
                if (table2.Rows.Count == 1)
                {
                    str4 = str4 + AntRequest.HtmlDecodeTrim(table2.Rows[0]["ClassName"]);
                }
                this.PageTitle = this.PageTitle + Base.iif(this.PageTitle.Length > 0, "-", "") + str4 + "品牌";
            }
            if (this.PageTitle == "")
            {
                if (this.P1 == 1)
                {
                    this.PageTitle = "新品上架";
                }
                if (this.P2 == 1)
                {
                    this.PageTitle = "特价促销";
                }
                if (this.P3 == 1)
                {
                    this.PageTitle = "热卖商品";
                }
                if (this.P4 == 1)
                {
                    this.PageTitle = "推荐商品";
                }
            }
            if ((this.PageTitle == "") && (this.SearchKeyword != ""))
            {
                this.PageTitle = "搜索“" + this.SearchKeyword + "”";
            }
            if (this.PageTitle == "")
            {
                this.PageTitle = "全部商品";
            }
            this.SearchKeyword = base.Server.UrlEncode(this.SearchKeyword);
            base.TotalItemCount = General.Count("ant_shop a inner join ant_company b on a.shopcompanyid=b.companyid and b.CompanyTypeid=0", str);
            string str5 = "case when ShopIndexDate>getdate() then ShopIndexMoney else 0 end as ShopIndexMoney,ShopBuyNum,ShopViews,CompanyXingyong,ShopFilepath,ShopMoney,ShopYunfei,CompanyType2,CompanyType1,CompanyType3,CompanyType4,ShopCompanyID,CompanyDomain,CompanyDomainKill,'' as ShopUrl,ShopIndexDate,ShopOrder,ShopName,ShopId,ShopCode,ShopKill,ShopDate,ShopIp,ShopIpAddress,ShopType1,ShopType2,ShopType3,ShopType4,CASE when ShopIndexDate>getdate() then '1' else '0' end as ShopIndex";
            this.dr = General.Page(str5, "ant_shop a inner join ant_company b on a.shopcompanyid=b.companyid and b.CompanyTypeid=0", str, str2, base.ItemCountPerPage, base.CurrentPageIndex, base.TotalItemCount);
            if ((base.TotalItemCount % base.ItemCountPerPage) == 0)
            {
                base.TotalPageCount = base.TotalItemCount / base.ItemCountPerPage;
            }
            else
            {
                base.TotalPageCount = (base.TotalItemCount / base.ItemCountPerPage) + 1;
            }
            base.CurrentPageIndex = (@int > base.TotalPageCount) ? base.TotalPageCount : @int;
        }
    }

}
