using Ant.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using YBB.Bll;
using YBB.Common;
using YBB.TemplateEngine;

namespace YBB.BaseData
{
    public class BasePage : Page, ISDEHandler
    {
        private static List<WeakReference> __ENCList = new List<WeakReference>();
        public readonly RegexOptions _RegexOptions = (RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public string BeginDocument = string.Empty;
        public AttrbuteCompany CompanyConfig;
        public DataTable CurDataTable;
        public DataTable CurrentDt;
        public int CurrentPageIndex;
        public string CurrentSearchUrl;
        public string CurrentSplitStr;
        public string CurrentUrl;
        public AttrbuteDaohang DaohangConfig;
        public StringBuilder document;
        public AttrbuteDzb DzbConfig;
        private Encoding encoding_0;
        public AttrbuteGift GiftConfig;
        public AttrbuteHouse HouseConfig;
        public AttrbuteInfo InfoConfig;
        public int ItemCountPerPage;
        public int ItemPageSize = 5;
        public AttrbuteJob JobConfig;
        public AttrbuteLife LifeConfig;
        public AttrbuteMovie MovieConfig;
        public AttrbuteMp3 Mp3Config;
        public AttrbuteNews NewsConfig;
        public int Num;
        public AttrbutePic PicConfig;
        public AttrbuteQuan QuanConfig;
        public AttrbuteShop ShopConfig;
        public website SiteConfig;
        public DateTime startdate = DateTime.Now;
        private string string_0;
        private string string_1;
        private TimeSpan timeSpan_0;
        public AttrbuteTools ToolsConfig;
        public int TotalItemCount;
        public int TotalPageCount;
        public AttrbuteTuan TuanConfig;
        public AttrbuteTv TvConfig;
        public AttrbuteVideo VideoConfig;
        public AttrbuteWish WishConfig;
        public AttrbuteZxjc ZxjcConfig;

        public BasePage()
        {
            __ENCAddToList(this);
            this.document = new StringBuilder();
            this.document.Capacity = 220000;
            this.SiteConfig = WebInfo.Get();
            this.InfoConfig = Channel.GetInfo();
            this.JobConfig = Channel.GetJob();
            this.HouseConfig = Channel.GetHouse();
            this.CompanyConfig = Channel.GetCompany();
            this.NewsConfig = Channel.GetNews();
            this.ShopConfig = Channel.GetShop();
            this.TuanConfig = Channel.GetTuan();
            this.ToolsConfig = Channel.GetTools();
            this.Mp3Config = Channel.GetMp3();
            this.GiftConfig = Channel.GetGift();
            this.TvConfig = Channel.GetTv();
            this.MovieConfig = Channel.GetMovie();
            this.VideoConfig = Channel.GetVideo();
            this.QuanConfig = Channel.GetQuan();
            this.LifeConfig = Channel.GetLife();
            this.DzbConfig = Channel.GetDzb();
            this.DaohangConfig = Channel.GetDaohang();
            this.ZxjcConfig = Channel.GetZxjc();
            this.PicConfig = Channel.GetPic();
            this.WishConfig = Channel.GetWish();
            this.ItemCountPerPage = SysConfig.IntPageSize;
            if (AntRequest.GetInt("gg", 0) == 1)
            {
                this.ExpirationTime = TimeSpan.FromSeconds((double)(this.SiteConfig.SiteCreateTime * 60));
            }
            else
            {
                this.ExpirationTime = TimeSpan.FromSeconds((double)(this.SiteConfig.SiteOtherTime * 60));
            }
            if (!ComManagent.CheckStatDomain(this.SiteConfig.SiteUrl))
            {
                Utils.ResponseSafe(this.SiteConfig.DomainKill);
            }
            if (this.SiteConfig.WebSiteClose == 1)
            {
                HttpContext.Current.Response.Write(this.SiteConfig.WebSiteCloseName);
                HttpContext.Current.Response.End();
            }
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

        public void BeginCreateDocument(ref StringBuilder stringBuilder_0)
        {
            this.BeginDocument = "<span style=\"color:red;\">BeginCreateDocument</span>";
        }

        public void EndCreateDocument(ref StringBuilder stringBuilder_0)
        {
            stringBuilder_0.Replace("在创建页面代码文档之后运行的内容：(这些文本将被替换)", "在创建页面代码文档之后运行的内容：<span style=\"color:red;\">EndCreateDocument</span>");
        }

        public bool ExecuteSDE()
        {
            if (AntRequest.GetString("CreateIndex") == "true")
            {
                return false;
            }
            if (this.ExpirationTime == TimeSpan.Zero)
            {
                return false;
            }
            FileInfo info = new FileInfo(base.Server.MapPath(this.StaticFileName));
            if (!info.Exists)
            {
                return false;
            }
            if ((DateTime.Now - info.LastWriteTime) > this.ExpirationTime)
            {
                return false;
            }
            base.Server.Transfer(this.StaticFileName, true);
            return true;
        }

        protected override void OnInit(EventArgs e)
        {
            this.TextEncoding = Encoding.UTF8;
            base.OnInit(e);
            this.ShowPage();
        }

        protected virtual void ShowPage()
        {
        }

        public void UpdateStaticFile(StringBuilder document)
        {
            if ((this.ExpirationTime != TimeSpan.Zero) || (AntRequest.GetString("CreateIndex") == "true"))
            {
                string path = base.Server.MapPath(this.StaticFileName.Substring(0, this.StaticFileName.LastIndexOf("/")));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                StreamWriter writer = null;
                writer = new StreamWriter(base.Server.MapPath(this.StaticFileName), false, Encoding.GetEncoding("UTF-8"));
                writer.Write(document.ToString());
                writer.Flush();
                writer.Close();
            }
        }

        public string DynamicFileName
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public TimeSpan ExpirationTime
        {
            get
            {
                return this.timeSpan_0;
            }
            set
            {
                this.timeSpan_0 = value;
            }
        }

        public string StaticFileName
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

        public Encoding TextEncoding
        {
            get
            {
                return this.encoding_0;
            }
            set
            {
                this.encoding_0 = value;
            }
        }

    }
}
