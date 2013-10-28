using System;
using System.Data;
using System.Data.SqlClient;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class Storelist : BasePage
    {
        protected int AreaID;
        protected int ClassID;
        protected int CurrentID = 1;
        protected SqlDataReader dr;
        protected int o1;
        protected string Mapstr = "";
        protected int P1;
        protected int P2;
        protected int P3;
        protected int P4;
        protected int P5;
        protected string PageDescription = "";
        protected string PageKeyword = "";
        protected string PageTitle = "";
        protected string SearchKeyword;
        protected int t;
        protected int TopAreaID;
        protected int TopClassID;
        protected DataRow[] WebAreaList = Area.DataList().Select(" AreaParent=0 ");
        protected DataRow[] WebSmallAreaList;

        protected string ShowComUrl(string string_2)
        {
            if (string_2.ToString().ToLower().IndexOf("storedetail") != -1)
            {
                return base.CompanyConfig.ChannelRewriteUrl;
            }
            if (string_2.ToString().ToLower().IndexOf("companyshow") != -1)
            {
                return base.CompanyConfig.ChannelRewriteUrl;
            }
            return (string_2.ToString() + "/");
        }

        protected string ShowHaoping(object object_0, object object_1, object object_2)
        {
            string str = "100";
            if (((Convert.ToInt32(object_0) + Convert.ToInt32(object_1)) + Convert.ToInt32(object_2)) > 0)
            {
                double num = Convert.ToDouble(object_0) / ((Convert.ToDouble(object_0) + Convert.ToDouble(object_1)) + Convert.ToDouble(object_2));
                str = (num * 100.0).ToString("F0");
            }
            return str;
        }

        protected override void ShowPage()
        {
            this.t = AntRequest.GetInt("t", 0);
            this.AreaID = AntRequest.GetInt("AreaID", 0);
            this.ClassID = AntRequest.GetInt("id", 0);
            this.P1 = AntRequest.GetInt("P1", 0);
            this.P2 = AntRequest.GetInt("P2", 0);
            this.P3 = AntRequest.GetInt("P3", 0);
            this.P4 = AntRequest.GetInt("P4", 0);
            this.P5 = AntRequest.GetInt("P5", 0);
            int @int = AntRequest.GetInt("p", 1);
            this.SearchKeyword = AntRequest.QueryStringDecode("SearchKeyword");
            try
            {
                int length = this.SearchKeyword.Length;
            }
            catch
            {
                this.SearchKeyword = AntRequest.GetString("SearchKeyword");
            }
            this.o1 = AntRequest.GetInt("o1", 0);
            if (@int < 1)
            {
                @int = 1;
            }
            if (base.SiteConfig.SiteAspxRewrite == 1)
            {
                base.CurrentSearchUrl = "StoreList.aspx?id={ID}&AreaID={AreaID}&P1={P1}&P2={P2}&P3={P3}&P4={P4}&P5={P5}&t={t}&o1={o1}&SearchKeyword={SearchKeyword}";
                base.CurrentUrl = string.Concat(new object[] { 
                "StoreList.aspx?ID=", this.ClassID, "&AreaID=", this.AreaID, "&P1=", this.P1, "&P2=", this.P2, "&P3=", this.P3, "&P4 = ", this.P4, "&P5 = ", this.P5, "&t = ", this.t, 
                "&o1= ", this.o1, "&SearchKeyword=", this.SearchKeyword, "&p={PageIndex}"
             });
            }
            else
            {
                base.CurrentSearchUrl = "StoreList-{ID}-{AreaID}-{P1}-{P2}-{P3}-{P4}-{P5}-{t}-{o1}-S{SearchKeyword}S-p0" + base.SiteConfig.SiteTemplateName;
                base.CurrentUrl = string.Concat(new object[] { 
                "StoreList-", this.ClassID, "-", this.AreaID, "-", this.P1, "-", this.P2, "-", this.P3, "-", this.P4, "-", this.P5, "-", this.t, 
                "-", this.o1, "-S", this.SearchKeyword, "S-p{PageIndex}", base.SiteConfig.SiteTemplateName
             });
            }
            if (base.Request.Url.ToString().ToLower().IndexOf("tmall") != -1)
            {
                if (base.ShopConfig.ChannelClose == 1)
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=88");
                    base.Response.End();
                }
                this.CurrentID = 5;
                this.t = 3;
                base.CurrentSearchUrl = base.CurrentSearchUrl.Replace("StoreList", "tmall");
                base.CurrentUrl = base.CurrentUrl.Replace("StoreList", "tmall");
            }
            base.CurrentPageIndex = @int;
            string str = " CompanyKill= 1  ";
            string str2 = "CompanyOrder desc,CompanyDate desc,CompanyID DESC";
            string str3 = "";
            if (this.o1 == 1)
            {
                str2 = "CompanyXingyong desc,CompanyOrder desc,CompanyDate desc,CompanyID DESC";
            }
            else if (this.o1 == 2)
            {
                str2 = "CompanyViews desc,CompanyOrder desc,CompanyDate desc,CompanyID DESC";
            }
            if (this.AreaID > 0)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, " and  CompanyAreaID in (select areaid from Ant_Area where AreaID ='", this.AreaID, "' or AreaParent='", this.AreaID, "') " });
            }
            if (this.ClassID > 0)
            {
                object obj3 = str;
                str = string.Concat(new object[] { obj3, " and  CompanyClassID in (select ClassID from Ant_CompanyClass where ClassID ='", this.ClassID, "' or ClassParent='", this.ClassID, "') " });
            }
            if (this.t == 1)
            {
                str3 = "店铺";
                str = str + " and CompanyTypeid=2 ";
            }
            else if (this.t == 2)
            {
                str3 = "企业";
                str = str + " and CompanyTypeid=1 ";
            }
            else if (this.t == 3)
            {
                str3 = "商城";
                str = str + " and CompanyTypeid=0 ";
            }
            if (this.P1 == 1)
            {
                str = str + " and CompanyID in(select YouhuiCompanyID from Ant_CompanyYouhui where YouhuiKill=1)";
            }
            if (this.P2 == 1)
            {
                str = str + " and CompanyZheKou > 0 ";
            }
            if (this.P3 == 1)
            {
                str = str + " and CompanyID in(select NewsCompanyID from Ant_CompanyNews where NewsKill=1 and (NewsTypeid=0 or NewsTypeid=5))";
            }
            if (this.P4 == 1)
            {
                str = str + " and CompanyRenZheng=1 ";
            }
            if (this.P5 == 1)
            {
                str = str + " and CompanyQianyue=1 ";
            }
            if (this.SearchKeyword.Length > 0)
            {
                string[] strArray = this.SearchKeyword.Split(new char[] { ' ' });
                string str4 = "";
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (AntRequest.StrTrim(strArray[i].ToString()).Length > 0)
                    {
                        string str8 = str4;
                        str4 = str8 + Base.iif(str4.Length > 0, " or ", "") + "  ( CompanyName  like '%" + Base.Chk39(strArray[i].ToString()) + "%' or CompanyXiangmu  like '%" + Base.Chk39(strArray[i].ToString()) + "%' )";
                    }
                }
                str = str + " and ( " + str4 + ")";
            }
            this.TopClassID = this.ClassID;
            if (this.ClassID > 0)
            {
                DataTable table = General.GetDataTable(1, "classid,classparent,ClassTitle,ClassKeyword,ClassDescription", "Ant_CompanyClass", "Classid='" + this.ClassID + "'", "");
                if (table.Rows.Count == 1)
                {
                    if (Convert.ToInt32(table.Rows[0]["ClassID"]) == this.ClassID)
                    {
                        this.TopClassID = Convert.ToInt32(table.Rows[0]["ClassParent"]);
                    }
                    this.PageTitle = table.Rows[0]["ClassTitle"].ToString();
                    this.PageKeyword = table.Rows[0]["ClassKeyword"].ToString();
                    this.PageDescription = table.Rows[0]["ClassDescription"].ToString();
                }
            }
            if (this.TopClassID == 0)
            {
                this.TopClassID = this.ClassID;
                this.ClassID = 0;
            }
            this.TopAreaID = this.AreaID;
            if (this.AreaID > 0)
            {
                string str5 = "";
                DataRow[] rowArray = Area.DataList().Select(" AreaID in(" + this.AreaID + ") ");
                if (rowArray.Length > 0)
                {
                    for (int j = 0; j < rowArray.Length; j++)
                    {
                        if (j > 0)
                        {
                            str5 = str5 + "+";
                        }
                        string str6 = Base.iif(Convert.ToInt32(rowArray[j]["AreaParent"]) > 0, AntRequest.HtmlDecodeTrim(rowArray[j]["ParentAreaName"]) + "-", "") + AntRequest.HtmlDecodeTrim(rowArray[j]["AreaName"]);
                        str5 = str5 + str6;
                        if (Convert.ToInt32(rowArray[j]["AreaID"]) == this.AreaID)
                        {
                            this.TopAreaID = Convert.ToInt32(rowArray[j]["AreaParent"]);
                        }
                    }
                }
                string pageTitle = this.PageTitle;
                this.PageTitle = pageTitle + Base.iif(this.PageTitle.Length > 0, "-", "") + base.SiteConfig.SiteCity + str5 + str3;
            }
            if (this.TopAreaID == 0)
            {
                this.TopAreaID = this.AreaID;
                this.AreaID = 0;
            }
            this.WebSmallAreaList = Area.DataList().Select(" AreaParent ='" + this.TopAreaID + "' ");
            if ((this.PageTitle == "") && (this.SearchKeyword != ""))
            {
                this.PageTitle = "搜索“" + this.SearchKeyword + "”";
            }
            if (this.PageTitle == "")
            {
                this.PageTitle = base.SiteConfig.SiteCity + str3;
            }
            this.SearchKeyword = base.Server.UrlEncode(this.SearchKeyword);
            base.TotalItemCount = General.Count("Ant_Company", str);
            string str7 = "CompanyVideoClose,CompanyVideoName,CompanyQuanClose,CompanyQuanName,CompanyZheKou,CompanyZheKouMark,(SELECT COUNT(CommentID) FROM Ant_CompanyComment ee WHERE ee.CommentCompanyID = CompanyID AND ee.CommentKill = 1) AS RevertNum,CompanyHaopingNum,CompanyZhongNum,CompanyChaNum,case when CompanyQianyueDate>=getdate() then '1' else '0' end as CompanyQianyue ,CompanyQianyueDate,CompanyXingyong,CompanyViews,CompanyRenZheng,CompanyMark,CompanyXiangmu,CompanyLogo,CompanyTypeid,CompanyName,CompanyID,CompanyUserid,CompanyDate,CompanyDomain,CompanyDomainKill,CompanyKill,CompanyTel,CompanyOrder,DomainMain,DomainTypeid";
            this.dr = General.Page(str7, "Ant_Company", str, str2, base.ItemCountPerPage, base.CurrentPageIndex, base.TotalItemCount);
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

        protected string ShowQuanurl(string string_2, object object_0, object object_1, object object_2)
        {
            if ((object_0.ToString() == "2") && (object_1.ToString() == "1"))
            {
                string str = ComManagent.CreateCompanyCommonUrl(true, base.SiteConfig.SiteAspxRewrite, base.CompanyConfig.ChannelKill, base.SiteConfig.SiteTemplateName, base.CompanyConfig.ChannelRewriteUrl, "SQuan", "id=" + object_2);
                return (string_2 + str);
            }
            if ((object_0.ToString() == "1") && (object_1.ToString() == "1"))
            {
                string str2 = ComManagent.CreateCompanyCommonUrl(true, base.SiteConfig.SiteAspxRewrite, base.CompanyConfig.ChannelKill, base.SiteConfig.SiteTemplateName, base.CompanyConfig.ChannelRewriteUrl, "ComQuan", "id=" + object_2);
                return (string_2 + str2);
            }
            return "";
        }

        protected string ShowVideoUrl(string string_2, object object_0, object object_1, object object_2)
        {
            if ((object_0.ToString() == "1") && (object_1.ToString() == "1"))
            {
                string str = ComManagent.CreateCompanyCommonUrl(true, base.SiteConfig.SiteAspxRewrite, base.CompanyConfig.ChannelKill, base.SiteConfig.SiteTemplateName, base.CompanyConfig.ChannelRewriteUrl, "ComVideo", "id=" + object_2);
                return (string_2 + str);
            }
            return "";
        }
    }

}
