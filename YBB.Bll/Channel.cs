using Ant.Model;
using System;
using System.Data;
using YBB.Common;

namespace YBB.Bll
{
    public class Channel
    {
        public static int ChannelInsertUpdateDelete(int int_0, string string_0, string string_1, int int_1, int int_2, int int_3, int int_4, string string_2, string string_3, string string_4)
        {
            return Channel.ChannelInsertUpdateDelete(int_0, string_0, string_1, int_1, int_2, int_3, int_4, string_2, string_3, string_4);
        }

        public static DataTable GetChannel()
        {
            AntCache cacheService = AntCache.GetCacheService();
            object obj2 = cacheService.RetrieveObject("/Ant/Channel");
            if (obj2 == null)
            {
                obj2 = General.GetDataTable(0, "[Channelid],[ChannelLogo],[ChannelName],[ChannelUrl],[ChannelRewriteUrl],[ChannelOpen],[ChannelTitle],[ChannelKeyword],[ChannelDescription],[ChannelTodayNum],[ChannelYestodayNum],[ChannelToday],[ChannelWidth],[ChannelHeight],[Channel1],[Channel2],[Channel3],[Channel4],[Channel5],[ChannelSiteName],[ChannelImage],[ChannelClose]", "ant_Channel", "ChannelID<100", "");
                cacheService.AddObject("/Ant/Channel", obj2);
            }
            return (DataTable)obj2;
        }

        public static AttrbuteCompany GetCompany()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteCompany company = cacheService.RetrieveObject("/Ant/CompanyChannel") as AttrbuteCompany;
            if (company == null)
            {
                company = new AttrbuteCompany();
                DataRow[] rowArray = GetChannel().Select("Channelid =6");
                company.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                company.ChannelName = rowArray[0]["ChannelName"].ToString();
                company.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                company.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                company.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                company.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                company.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                company.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                company.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                company.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                company.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                company.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                company.ChannelToday = DateTime.Now;
                company.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                company.Channel1 = rowArray[0]["Channel1"].ToString();
                company.Channel2 = rowArray[0]["Channel2"].ToString();
                company.Channel3 = rowArray[0]["Channel3"].ToString();
                company.Channel4 = rowArray[0]["Channel4"].ToString();
                string str = rowArray[0]["Channel5"].ToString();
                try
                {
                    company.Channel5 = str.Split(new char[] { ',' })[0];
                    company.Channel6 = str.Split(new char[] { ',' })[1];
                }
                catch
                {
                    company.Channel5 = "430";
                    company.Channel6 = "250";
                }
                cacheService.AddObject("/Ant/CompanyChannel", company);
                rowArray = null;
            }
            return company;
        }

        public static AttrbuteDaohang GetDaohang()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteDaohang daohang = cacheService.RetrieveObject("/Ant/DaohangChannel") as AttrbuteDaohang;
            if (daohang == null)
            {
                daohang = new AttrbuteDaohang();
                DataRow[] rowArray = GetChannel().Select("Channelid =15");
                if (rowArray.Length != 1)
                {
                    return daohang;
                }
                daohang.ChannelName = rowArray[0]["ChannelName"].ToString();
                daohang.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                daohang.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                daohang.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                daohang.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                daohang.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                daohang.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                daohang.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                daohang.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                daohang.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                daohang.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                if (daohang.ChannelClose == 0)
                {
                    daohang.ChannelClose = 1;
                }
                cacheService.AddObject("/Ant/DaohangChannel", daohang);
                rowArray = null;
            }
            return daohang;
        }

        public static DataTable GetData()
        {
            AntCache cacheService = AntCache.GetCacheService();
            object obj2 = cacheService.RetrieveObject("/Ant/ChannelData");
            if (obj2 == null)
            {
                obj2 = General.GetDataTable(0, "ChannelRewriteUrl,ChannelOrder,ChannelTarget,ChannelColor,ChannelFont,case when ChannelSiteName is null or ChannelSiteName=''  then ChannelName else ChannelSiteName end as ChannelSiteName,ChannelImage", "ant_Channel", "ChannelOpen=0 ", "ChannelOrder asc");
                cacheService.AddObject("/Ant/ChannelData", obj2);
            }
            return (DataTable)obj2;
        }

        public static AttrbuteDzb GetDzb()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteDzb dzb = cacheService.RetrieveObject("/Ant/DzbChannel") as AttrbuteDzb;
            if (dzb == null)
            {
                dzb = new AttrbuteDzb();
                DataRow[] rowArray = GetChannel().Select("Channelid =13");
                dzb.ChannelName = rowArray[0]["ChannelName"].ToString();
                dzb.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                dzb.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                dzb.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                dzb.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                dzb.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                dzb.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                dzb.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                dzb.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                dzb.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                dzb.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                if (dzb.ChannelClose == 0)
                {
                    dzb.ChannelClose = 1;
                }
                cacheService.AddObject("/Ant/DzbChannel", dzb);
                rowArray = null;
            }
            return dzb;
        }

        public static AttrbuteGift GetGift()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteGift gift = cacheService.RetrieveObject("/Ant/GiftChannel") as AttrbuteGift;
            if (gift == null)
            {
                gift = new AttrbuteGift();
                DataRow[] rowArray = GetChannel().Select("Channelid =9");
                gift.ChannelName = rowArray[0]["ChannelName"].ToString();
                gift.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                gift.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                gift.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                gift.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                gift.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                gift.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                gift.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                gift.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                gift.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/GiftChannel", gift);
                rowArray = null;
            }
            return gift;
        }

        public static AttrbuteHouse GetHouse()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteHouse house = cacheService.RetrieveObject("/Ant/HouseChannel") as AttrbuteHouse;
            if (house == null)
            {
                house = new AttrbuteHouse();
                DataRow[] rowArray = GetChannel().Select("Channelid =3");
                house.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                house.ChannelName = rowArray[0]["ChannelName"].ToString();
                house.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                house.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                house.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                house.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                house.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                house.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                house.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                house.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                house.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                house.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                house.ChannelToday = DateTime.Now;
                house.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                house.Channel1 = rowArray[0]["Channel1"].ToString();
                house.Channel2 = rowArray[0]["Channel2"].ToString();
                house.Channel5 = rowArray[0]["Channel5"].ToString();
                cacheService.AddObject("/Ant/HouseChannel", house);
                rowArray = null;
            }
            return house;
        }

        public static AttrbuteInfo GetInfo()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteInfo info = cacheService.RetrieveObject("/Ant/InfoChannel") as AttrbuteInfo;
            if (info == null)
            {
                info = new AttrbuteInfo();
                DataRow[] rowArray = GetChannel().Select("Channelid =1");
                info.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                info.ChannelName = rowArray[0]["ChannelName"].ToString();
                info.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                info.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                info.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                info.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                info.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                info.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                info.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                info.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                info.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                info.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                info.ChannelToday = DateTime.Now;
                info.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                info.Channel1 = Base.StrToInt(rowArray[0]["Channel1"], 1);
                cacheService.AddObject("/Ant/InfoChannel", info);
                rowArray = null;
            }
            return info;
        }

        public static AttrbuteJob GetJob()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteJob job = cacheService.RetrieveObject("/Ant/JobChannel") as AttrbuteJob;
            if (job == null)
            {
                job = new AttrbuteJob();
                DataRow[] rowArray = GetChannel().Select("Channelid =5");
                job.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                job.ChannelName = rowArray[0]["ChannelName"].ToString();
                job.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                job.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                job.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                job.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                job.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                job.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                job.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                job.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                job.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                job.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                job.ChannelToday = DateTime.Now;
                job.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                job.Channel1 = rowArray[0]["Channel1"].ToString();
                job.Channel2 = rowArray[0]["Channel2"].ToString();
                job.Channel3 = rowArray[0]["Channel3"].ToString();
                job.Channel4 = rowArray[0]["Channel4"].ToString();
                job.Channel5 = rowArray[0]["Channel5"].ToString();
                cacheService.AddObject("/Ant/JobChannel", job);
                rowArray = null;
            }
            return job;
        }

        public static AttrbuteLife GetLife()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteLife life = cacheService.RetrieveObject("/Ant/LifeChannel") as AttrbuteLife;
            if (life == null)
            {
                life = new AttrbuteLife();
                DataRow[] rowArray = GetChannel().Select("Channelid =8");
                life.ChannelName = rowArray[0]["ChannelName"].ToString();
                life.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                life.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                life.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                life.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                life.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                life.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                life.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                life.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                life.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                life.Channel1 = rowArray[0]["Channel1"].ToString();
                life.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/LifeChannel", life);
                rowArray = null;
            }
            return life;
        }

        public static AttrbuteMovie GetMovie()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteMovie movie = cacheService.RetrieveObject("/Ant/MovieChannel") as AttrbuteMovie;
            if (movie == null)
            {
                movie = new AttrbuteMovie();
                DataRow[] rowArray = GetChannel().Select("Channelid =14");
                if (rowArray.Length == 1)
                {
                    movie.ChannelName = rowArray[0]["ChannelName"].ToString();
                    movie.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                    movie.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                    movie.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                    movie.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                    movie.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                    movie.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                    movie.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                    movie.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                    movie.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                    cacheService.AddObject("/Ant/MovieChannel", movie);
                    rowArray = null;
                }
            }
            return movie;
        }

        public static AttrbuteMp3 GetMp3()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteMp3 mp = cacheService.RetrieveObject("/Ant/Mp3Channel") as AttrbuteMp3;
            if (mp == null)
            {
                mp = new AttrbuteMp3();
                DataRow[] rowArray = GetChannel().Select("Channelid =11");
                mp.ChannelName = rowArray[0]["ChannelName"].ToString();
                mp.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                mp.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                mp.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                mp.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                mp.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                mp.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                mp.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                mp.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                mp.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/Mp3Channel", mp);
                rowArray = null;
            }
            return mp;
        }

        public static AttrbuteNews GetNews()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteNews news = cacheService.RetrieveObject("/Ant/NewsChannel") as AttrbuteNews;
            if (news == null)
            {
                news = new AttrbuteNews();
                DataRow[] rowArray = GetChannel().Select("Channelid =2");
                news.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                news.ChannelName = rowArray[0]["ChannelName"].ToString();
                news.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                news.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                news.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                news.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                news.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                news.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                news.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                news.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                news.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                news.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                news.ChannelToday = DateTime.Now;
                news.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/NewsChannel", news);
                rowArray = null;
            }
            return news;
        }

        public static AttrbutePic GetPic()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbutePic pic = cacheService.RetrieveObject("/Ant/PicChannel") as AttrbutePic;
            if (pic == null)
            {
                pic = new AttrbutePic();
                DataRow[] rowArray = GetChannel().Select("Channelid =20");
                pic.ChannelName = rowArray[0]["ChannelName"].ToString();
                pic.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                pic.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                pic.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                pic.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                pic.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                pic.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                pic.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                pic.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                cacheService.AddObject("/Ant/PicChannel", pic);
                rowArray = null;
            }
            return pic;
        }

        public static AttrbuteQuan GetQuan()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteQuan quan = cacheService.RetrieveObject("/Ant/WishChannel") as AttrbuteQuan;
            if (quan == null)
            {
                quan = new AttrbuteQuan();
                DataRow[] rowArray = GetChannel().Select("Channelid =18");
                quan.ChannelName = rowArray[0]["ChannelName"].ToString();
                quan.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                quan.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                quan.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                quan.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                quan.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                quan.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                quan.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                quan.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                quan.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                cacheService.AddObject("/Ant/WishChannel", quan);
                rowArray = null;
            }
            return quan;
        }

        public static AttrbuteShop GetShop()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteShop shop = cacheService.RetrieveObject("/Ant/ShopChannel") as AttrbuteShop;
            if (shop == null)
            {
                shop = new AttrbuteShop();
                DataRow[] rowArray = GetChannel().Select("Channelid =7");
                shop.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                shop.ChannelName = rowArray[0]["ChannelName"].ToString();
                shop.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                shop.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                shop.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                shop.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                shop.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                shop.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                shop.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                shop.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                shop.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                shop.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                shop.ChannelToday = DateTime.Now;
                shop.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/ShopChannel", shop);
                rowArray = null;
            }
            return shop;
        }

        public static AttrbuteTools GetTools()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteTools tools = cacheService.RetrieveObject("/Ant/ToolsChannel") as AttrbuteTools;
            if (tools == null)
            {
                tools = new AttrbuteTools();
                DataRow[] rowArray = GetChannel().Select("Channelid =12");
                tools.ChannelName = rowArray[0]["ChannelName"].ToString();
                tools.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                tools.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                tools.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                tools.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                tools.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                tools.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                tools.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                tools.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                tools.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/ToolsChannel", tools);
                rowArray = null;
            }
            return tools;
        }

        public static AttrbuteTuan GetTuan()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteTuan tuan = cacheService.RetrieveObject("/Ant/TuanChannel") as AttrbuteTuan;
            if (tuan == null)
            {
                tuan = new AttrbuteTuan();
                DataRow[] rowArray = GetChannel().Select("Channelid =4");
                tuan.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                tuan.ChannelName = rowArray[0]["ChannelName"].ToString();
                tuan.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                tuan.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                tuan.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                tuan.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                tuan.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                tuan.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                tuan.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                tuan.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                tuan.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                tuan.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                tuan.ChannelToday = DateTime.Now;
                tuan.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                tuan.ChannelQuanName = rowArray[0]["Channel1"].ToString();
                tuan.ChannelMoney = Convert.ToDouble(rowArray[0]["Channel2"].ToString());
                cacheService.AddObject("/Ant/TuanChannel", tuan);
                rowArray = null;
            }
            return tuan;
        }

        public static AttrbuteTv GetTv()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteTv tv = cacheService.RetrieveObject("/Ant/TvChannel") as AttrbuteTv;
            if (tv == null)
            {
                tv = new AttrbuteTv();
                DataRow[] rowArray = GetChannel().Select("Channelid =10");
                tv.ChannelName = rowArray[0]["ChannelName"].ToString();
                tv.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                tv.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                tv.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                tv.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                tv.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                tv.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                tv.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                tv.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                tv.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                cacheService.AddObject("/Ant/TvChannel", tv);
                rowArray = null;
            }
            return tv;
        }

        public static AttrbuteVideo GetVideo()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteVideo video = cacheService.RetrieveObject("/Ant/VideoChannel") as AttrbuteVideo;
            if (video == null)
            {
                video = new AttrbuteVideo();
                DataRow[] rowArray = GetChannel().Select("Channelid =16");
                if (rowArray.Length == 1)
                {
                    video.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                    video.ChannelName = rowArray[0]["ChannelName"].ToString();
                    video.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                    video.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                    video.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                    video.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                    video.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                    video.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                    video.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                    video.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                    video.ChannelTodayNum = int.Parse(rowArray[0]["ChannelTodayNum"].ToString());
                    video.ChannelYestodayNum = int.Parse(rowArray[0]["ChannelYestodayNum"].ToString());
                    video.ChannelToday = DateTime.Now;
                    video.ChannelClose = int.Parse(rowArray[0]["ChannelClose"].ToString());
                    rowArray = null;
                }
                cacheService.AddObject("/Ant/VideoChannel", video);
                rowArray = null;
            }
            return video;
        }

        public static AttrbuteWish GetWish()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteWish wish = cacheService.RetrieveObject("/Ant/WishChannel") as AttrbuteWish;
            if (wish == null)
            {
                wish = new AttrbuteWish();
                DataRow[] rowArray = GetChannel().Select("Channelid =17");
                wish.ChannelName = rowArray[0]["ChannelName"].ToString();
                wish.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                wish.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                wish.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                wish.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                wish.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                wish.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                wish.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                wish.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                wish.Channel1 = rowArray[0]["Channel1"].ToString();
                cacheService.AddObject("/Ant/WishChannel", wish);
                rowArray = null;
            }
            return wish;
        }

        public static AttrbuteZxjc GetZxjc()
        {
            AntCache cacheService = AntCache.GetCacheService();
            AttrbuteZxjc zxjc = cacheService.RetrieveObject("/Ant/ZxjcChannel") as AttrbuteZxjc;
            if (zxjc == null)
            {
                zxjc = new AttrbuteZxjc();
                DataRow[] rowArray = GetChannel().Select("Channelid =19");
                zxjc.ChannelName = rowArray[0]["ChannelName"].ToString();
                zxjc.ChannelLogo = rowArray[0]["ChannelLogo"].ToString();
                zxjc.ChannelUrl = rowArray[0]["ChannelUrl"].ToString();
                zxjc.ChannelRewriteUrl = rowArray[0]["ChannelRewriteUrl"].ToString();
                zxjc.ChannelOpen = int.Parse(rowArray[0]["ChannelOpen"].ToString());
                zxjc.ChannelHeight = int.Parse(rowArray[0]["ChannelHeight"].ToString());
                zxjc.ChannelWidth = int.Parse(rowArray[0]["ChannelWidth"].ToString());
                zxjc.ChannelTitle = rowArray[0]["ChannelTitle"].ToString();
                zxjc.ChannelKeyword = rowArray[0]["ChannelKeyword"].ToString();
                zxjc.ChannelDescription = rowArray[0]["ChannelDescription"].ToString();
                zxjc.Channel1 = Base.StrToInt(rowArray[0]["Channel1"].ToString(), 0);
                zxjc.Channel2 = int.Parse(rowArray[0]["Channel2"].ToString());
                cacheService.AddObject("/Ant/ZxjcChannel", zxjc);
                rowArray = null;
            }
            return zxjc;
        }

        public static void RemoveChannel()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
        }

        public static void RemoveCompany()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/CompanyChannel");
        }

        public static void RemoveDaohang()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/DaohangChannel");
        }

        public static void RemoveData()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/ChannelData");
        }

        public static void RemoveDzb()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/DzbChannel");
        }

        public static void RemoveGift()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/GiftChannel");
        }

        public static void RemoveHouse()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/HouseChannel");
        }

        public static void RemoveInfo()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/InfoChannel");
        }

        public static void RemoveJob()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/JobChannel");
        }

        public static void RemoveLife()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/LifeChannel");
        }

        public static void RemoveMovie()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/MovieChannel");
        }

        public static void RemoveMp3()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/Mp3Channel");
        }

        public static void RemoveNews()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/NewsChannel");
        }

        public static void RemovePic()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/PicChannel");
        }

        public static void RemoveQuan()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/WishChannel");
        }

        public static void RemoveShop()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/ShopChannel");
        }

        public static void RemoveTools()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/ToolsChannel");
        }

        public static void RemoveTuan()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/TuanChannel");
        }

        public static void RemoveTv()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/TvChannel");
        }

        public static void RemoveVideo()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/VideoChannel");
        }

        public static void RemoveWish()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/WishChannel");
        }

        public static void RemoveZxjc()
        {
            AntCache.GetCacheService().RemoveObject("/Ant/Channel");
            AntCache.GetCacheService().RemoveObject("/Ant/ZxjcChannel");
        }

        public static int UpdateChannel(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5, string string_6, string string_7, string string_8, string string_9, string string_10, string string_11, string string_12, string string_13, string string_14)
        {
            return Channel.UpdateChannel(string_0, string_1, string_2, string_3, string_4, string_5, string_6, string_7, string_8, string_9, string_10, string_11, string_12, string_13, string_14);
        }
    }

}
