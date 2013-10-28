using Ant.Model;
using System;
using System.Data;
using YBB.Bll;
using YBB.Common;

namespace YBB.BaseData
{
    public class InfoFabuOver : BasePage
    {
        protected Users AntUser;
        protected string CurrentInfoJifen;
        protected int CurrentInfoKill;

        private void method_0()
        {
            UserPowers userPower = Member.GetUserPower();
            Users userLogin = Utils.GetUserLogin(base.SiteConfig);
            if ((userPower.InfoYouke != 0) && (userLogin.UserID == -1))  //判断是否允许游客发布
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Account/MemberLogin.aspx?from=" + base.Server.UrlEncode(AntRequest.GetString("from")));
                base.Response.End();
            }
            int @int = AntRequest.GetInt("thisInfoClassID", 0);
            int num2 = AntRequest.GetInt("txtInfoAreaID", 0);
            string str = AntRequest.HtmlEncodeTrim(AntRequest.GetString("txtInfoTitle"));
            string str2 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("txtInfoMark"));
            string str3 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("txtInfoMan"));
            string str4 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("txtInfoTel"));
            string str5 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("txtInfoQQ"));
            string str6 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("txtInfoPassword"));
            int num3 = 1;
            int num4 = 1;
            string str7 = AntRequest.GetString("AntAllImages");
            int num5 = AntRequest.GetInt("txtInfoYouxiao", 0);
            string str8 = "0";
            string str9 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("AntRegQuestion"));
            string str10 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("AntRegUserCode"));
            string str12 = AES.Decode(Convert.ToString(this.Session["AntCodeCookie"]), "Ant1Cookie");
            string ipAddress = Base.GetIpAddress();
            bool flag = true;
            if (base.SiteConfig.SiteCityInfo.Length > 0)
            {
                string str14 = ipAddress;
                if (str14.IndexOf(base.SiteConfig.SiteCityInfo) == -1)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                int num6 = 0;
                int num7 = 0;
                if (userLogin.UserID > 0)
                {
                    DataTable table = Member.MemberSeleteByUserID(userLogin.UserID);
                    if (table.Rows.Count == 1)
                    {
                        num6 = userPower.InfoKill1;
                        num7 = userPower.Infotype1;
                        userLogin.Userjifen = Convert.ToInt32(table.Rows[0]["chrjifen"]);
                    }
                    else
                    {
                        userLogin.UserID = -1;
                    }
                }
                if (userLogin.UserID > 0)
                {
                    str8 = userLogin.UserID.ToString();
                }
                if ((userLogin.UserID > 0) && (num7 == 1))
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，您没有权限发布分类信息，如有疑问请联系网站客服。"));
                    base.Response.End();
                }
                if (((userLogin.Userjifen + base.SiteConfig.SiteJifenInfoFabu) < 0) && (userLogin.UserID > 0))
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("11"));
                    base.Response.End();
                }
                if (Utils.CheckInfoLaji(str5, str4, str, str2, base.SiteConfig))
                {
                    if (base.SiteConfig.SiteInfoClose == 0)
                    {
                        num6 = 0;
                    }
                    else
                    {
                        base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + base.Server.UrlEncode("对不起,您的信息中含有非法内容，或者您所在地区不对，已经被阻止发布！如有异议，请联系本站客服!"));
                        base.Response.End();
                    }
                }
                if (@int < 1)
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，请选择所属类目。"));
                }
                else if (num2 < 1)
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，请选择地区。"));
                }
                else if (string.IsNullOrEmpty(str))
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，请输入信息标题。"));
                }
                else if ((str2.Length < 1) || (str2.Length > 0x7d0))
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，补充说明限制在3-2000字符内。"));
                }
                else if ((str3.Length < 1) || (str3.Length > 20))
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，联系人控制在2-20字符内。"));
                }
                else if (str10.ToLower() != str12.ToLower())
                {
                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + AntRequest.HtmlEncodeTrim("对不起，验证码不正确。"));
                }
                else
                {
                    bool flag2 = true;
                    string str15 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("chrquestionid"));
                    str15 = AES.Decode(DES.Decode(Convert.ToString(this.Session["AntQ"]), "itnewuoyow"), "woyouwenti");
                    if (base.SiteConfig.SiteQuestionInfo == 0)
                    {
                        if (!Base.IsNumeric(str15))
                        {
                            flag2 = false;
                            base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + base.Server.UrlEncode("对不起，验证答案错误，请重新输入，或者联系管理员。"));
                        }
                        else
                        {
                            DataRow[] rowArray = Question.DataList().Select("QuestionID='" + str15 + "'");
                            if (rowArray.Length == 1)
                            {
                                if (str9 != rowArray[0]["QuestionMark"].ToString())
                                {
                                    flag2 = false;
                                    base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + base.Server.UrlEncode("对不起，验证答案错误，请重新输入，或者联系管理员。"));
                                }
                            }
                            else
                            {
                                flag2 = false;
                                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + base.Server.UrlEncode("对不起，验证答案错误，请重新输入，或者联系管理员。"));
                            }
                        }
                    }
                    if (flag2)
                    {
                        if (((userLogin.Userjifen + base.SiteConfig.SiteJifenInfoFabu) < 0) && (userLogin.UserID > 0))
                        {
                            base.Response.Redirect(base.SiteConfig.SiteWebUrl + "error.aspx?info=" + AntRequest.HtmlEncodeTrim("11"));
                        }
                        else
                        {
                            string str17 = AntRequest.GetString("AntFirstImages");
                            if (str17.Length > 5)
                            {
                                str17 = str17.ToLower().Remove(0, str17.ToLower().IndexOf("upload"));
                            }
                            string str18 = "";
                            str7 = str7.ToLower().Replace(Base.GetRootUrl(""), "");
                            string[] strArray = str7.Split(new char[] { ',' });
                            int length = strArray.Length;
                            if (str7.Length == 0)
                            {
                                length = 0;
                            }
                            int num9 = Info.InfoAddInsertUpdateDelete(str, num2.ToString(), @int.ToString(), "", "1", str3, str4, str5, "0", str2, str17, length.ToString(), "0", "", "0", "0", "", num4.ToString(), DateTime.Now.ToString(), str8, Base.GetRealIP(), num3.ToString(), ipAddress, str18, num5.ToString(), num6.ToString(), "0", "0", MD5.Md5Str(str6, 0x20));
                            if (strArray.Length > 0)
                            {
                                for (int i = 0; i < strArray.Length; i++)
                                {
                                    string str19 = Convert.ToString(strArray[i]);
                                    if (str19.Length > 5)
                                    {
                                        str19 = str19.ToLower().Remove(0, str19.ToLower().IndexOf("upload"));
                                    }
                                    if (str19.Length > 0)
                                    {
                                        string str20 = str19.Replace("_120x120", "");
                                        Info.InfoFileInsertUpdateDelete("0", str19, str20, num9.ToString(), (i + 1).ToString(), "0");
                                    }
                                }
                            }
                            DataTable table2 = Info.InfoAttrTypeSelectByClassid(@int.ToString());
                            if (table2.Rows.Count > 0)
                            {
                                for (int j = 0; j < table2.Rows.Count; j++)
                                {
                                    string str21 = AntRequest.HtmlEncodeTrim(AntRequest.GetString("AttrID" + table2.Rows[j]["AttrID"].ToString()));
                                    if (str21.Length > 0)
                                    {
                                        Info.InfoAttrInsertUpdateDelete(table2.Rows[j]["AttrID"].ToString(), str21, num9.ToString(), "0");
                                    }
                                }
                            }
                            if (num6 == 1)
                            {
                                Admin.ActionInsertUpdateDelete("0", "发布分类信息", userLogin.UserID.ToString(), base.SiteConfig.SiteJifenInfoFabu.ToString(), "1", "0", num9.ToString(), "0");
                            }
                            string s = ComManagent.CreateInfoUrl(false, base.SiteConfig.SiteAspxRewrite, base.InfoConfig.ChannelKill, base.SiteConfig.SiteTemplateName, base.InfoConfig.ChannelRewriteUrl, num9.ToString());
                            this.Session["AntInfoOver"] = MD5.Md5Str("AntInfoOver", 0x10);
                            if (AntRequest.GetDomain().ToLower() == base.InfoConfig.ChannelRewriteUrl.ToLower())
                            {
                                base.Response.Redirect(AntRequest.GetDomain() + "UserInfoAddOver.aspx?InfoTitle=" + base.Server.UrlEncode(str) + "&url=" + base.Server.UrlEncode(s));
                            }
                            else
                            {
                                base.Response.Redirect(AntRequest.GetDomain() + "Info/UserInfoAddOver.aspx?InfoTitle=" + base.Server.UrlEncode(str) + "&url=" + base.Server.UrlEncode(s));
                            }
                        }
                    }
                }
            }
            else
            {
                base.Response.Redirect(base.SiteConfig.SiteWebUrl + "Error.aspx?info=" + base.Server.UrlEncode("对不起，您所在地区已限制发布信息，如有特殊需要请联系网站客服！\r\n客服电话：" + base.SiteConfig.SiteTel + "  客服QQ：" + base.SiteConfig.SiteQQ1));
            }
        }

        protected override void ShowPage()
        {
            if (AntRequest.GetString("Action").ToLower() == "addinfo")
            {
                this.method_0();
            }
            else
            {
                this.AntUser = Utils.GetUserLogin(base.SiteConfig);
                UserPowers userPower = Member.GetUserPower();
                this.Session["AntInfoOver"] = string.Empty;
                if ((this.AntUser.UserID > 0) && (Member.MemberSeleteByUserID(this.AntUser.UserID).Rows.Count == 1))
                {
                    this.CurrentInfoKill = userPower.InfoKill1;
                }
                if (base.SiteConfig.SiteJifenInfoFabu > 0)
                {
                    this.CurrentInfoJifen = "+" + base.SiteConfig.SiteJifenInfoFabu;
                }
                else
                {
                    this.CurrentInfoJifen = base.SiteConfig.SiteJifenInfoFabu.ToString();
                }
            }
        }
    }

}
