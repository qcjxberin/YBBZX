using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using YBB.Common;
using YBB.Config;

namespace YBB.Bll
{
    public class ComManagent
    {
        private static PixelFormat[] indexedPixelFormats;
        protected static string OkKey = "0";
        private static string SelfCheckDomain = "";
        private static bool shouquanflag;
        public delegate int JieMi(string string_0, string string_1);

        static ComManagent()
        {
            PixelFormat[] formatArray = new PixelFormat[6];
            formatArray[2] = PixelFormat.Format16bppArgb1555;
            formatArray[3] = PixelFormat.Format1bppIndexed;
            formatArray[4] = PixelFormat.Format4bppIndexed;
            formatArray[5] = PixelFormat.Format8bppIndexed;
            indexedPixelFormats = formatArray;
        }

        public static bool CheckStatDomain(string string_0)
        {
            return true;
            //bool flag = false;
            //string str = "";
            //if (HttpContext.Current.Request.Cookies["AntXiaouserslogin"] != null)
            //{
            //    str = HttpContext.Current.Request.Cookies["AntXiaouserslogin"].Value;
            //}
            //if (str.Length > 0)
            //{
            //    try
            //    {
            //        str = AES.Decode(DES.Decode(str, "po!@#$cde"), string_0);
            //        if (DateTime.Now.Day == Convert.ToDateTime(str).Day)
            //        {
            //            flag = true;
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            //if (!flag)
            //{
            //    string domain = AntRequest.GetDomain();
            //    AntRequest.CheckDomain(string_0);
            //    SelfCheckDomain = domain;
            //    flag = CheckPower(string_0);
            //}
            //return flag;
        }

        public static bool CheckPower(string string_0)
        {
            string url = AntRequest.GetUrl();
            if (((url.ToLower().IndexOf("/install") == -1) && (url.ToLower().IndexOf("/update") == -1)) && (url.ToLower().IndexOf("?action=test") == -1))
            {
                if (CheckPower_Ant(string_0))
                {
                    shouquanflag = false;
                }
                else
                {
                    shouquanflag = true;
                }
            }
            if (shouquanflag)
            {
                string str2 = DES.Encode(AES.Encode(DateTime.Now.ToString(), string_0), "po!@#$cde");
                HttpCookie cookie = null;
                if (HttpContext.Current.Request.Cookies["AntXiaouserslogin"] == null)
                {
                    cookie = new HttpCookie("AntXiaouserslogin");
                }
                else
                {
                    cookie = HttpContext.Current.Request.Cookies["AntXiaouserslogin"];
                }
                cookie.Value = str2;
                cookie.Expires = DateTime.Now.AddHours(1.0);
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            return shouquanflag;
        }

        public static bool CheckPower_Ant(string string_0)
        {
            string path = null;
            path = HttpContext.Current.Server.MapPath("~/public/config/Ant.lic");
            if (!File.Exists(path))
            {
                return true;
            }
            XmlDocument document = new XmlDocument();
            document.LoadXml(File.ReadAllText(path));
            string innerText = document.DocumentElement.SelectSingleNode("//publickey").InnerText;
            bool flag = true;
            string str3 = HttpContext.Current.Server.MapPath("~/Bin/Ant.BaseComm.dll");
            string str4 = "";
            string str5 = "";
            GetShaMd5(str3, ref str4, ref str5);
            string str6 = HttpContext.Current.Request.Url.Host.ToLower();
            string str7 = string_0;
            if (("." + string_0).ToLower().IndexOf(".www.") != -1)
            {
                str7 = ("." + string_0).ToLower().Replace(".www.", "");
            }
            if (("." + str6).IndexOf("." + str7) != -1)
            {
                str6 = str7;
            }
            if ((str4 == "EDB10DB1F470B5644033CB6124BB1A70393786A5") || (str5 == "875EC65AD165216D4F88DEA5DC9D44B3"))
            {
                string str8 = "";
                if (OkKey != "0")
                {
                    try
                    {
                        string str9 = DES.Decode(OkKey, "%@*&%$#!");
                        str9 = AES.Decode(DES.Decode(OkKey, "%@*&%$#!"), string_0);
                        if ((str9.IndexOf(str6) != -1) && (DateTime.Now.Hour == Convert.ToDateTime(str9.Replace(str6, "")).Hour))
                        {
                            OkKey = "ok";
                        }
                    }
                    catch
                    {
                    }
                }
                if (OkKey == "ok")
                {
                    str8 = "0";
                }
                else
                {
                    DllInvoke invoke = new DllInvoke(str3);
                    JieMi mi = (JieMi)invoke.Invoke("JieMi", typeof(JieMi));
                    str8 = mi(innerText, SelfCheckDomain).ToString();
                    invoke.UnLoadDll();
                }
                if (str8 == "0")
                {
                    OkKey = DES.Encode(AES.Encode(DateTime.Now.ToString() + str6, string_0), "%@*&%$#!");
                    flag = false;
                }
            }
            return flag;
        }

        private static void GetShaMd5(string string_0, ref string string_1, ref string string_2)
        {
            AntCache cacheService = AntCache.GetCacheService();
            string str = cacheService.RetrieveObject("/Ant/FileShaMd5") as string;
            if (str == null)
            {
                MD5andSHA dsha = new MD5andSHA();
                string_1 = dsha.SHA1File(string_0);
                string_2 = dsha.MD5File(string_0);
                cacheService.AddObject("/Ant/FileShaMd5", string_1 + "@" + string_2);
            }
            else
            {
                string[] strArray = AntRequest.SplitPage(str, "@");
                string_1 = strArray[0].ToString();
                string_2 = strArray[1].ToString();
            }
        }

        public static string CreateTuanCommonUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2)
        {
            string text;
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    text = string_2 + ".aspx";
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + ".aspx";
                    if (!bool_0)
                    {
                        text = "../Tuan/" + text;
                    }
                }
            }
            else
            {
                if (int_1 == 0)
                {
                    text = string_2 + string_0;
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + string_0;
                    if (!bool_0)
                    {
                        text = "../Tuan/" + text;
                    }
                }
            }
            return text;
        }

        public static string CreateTuanCommonUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2, string string_3)
        {
            string text;
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    text = string_2 + ".aspx?" + string_3;
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + ".aspx?" + string_3;
                    if (!bool_0)
                    {
                        text = "../Tuan/" + text;
                    }
                }
            }
            else
            {
                string text2 = "";
                for (int i = 0; i < string_3.Split(new char[]
				{
					'&'
				}).Length; i++)
                {
                    text2 += "-";
                    text2 += Regex.Replace(Convert.ToString(string_3.Split(new char[]
					{
						'&'
					})[i]), "[^<>]*=", "");
                }
                if (int_1 == 0)
                {
                    text = string_2 + text2 + string_0;
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + text2 + string_0;
                    if (!bool_0)
                    {
                        text = "../Tuan/" + text;
                    }
                }
            }
            return text;
        }

        public static DataTable GetWebCompanyDomainAttrList(int int_0)
        {
            return YBB.DAL.Company.CompanySelectID(int_0);
        }

        public static string CreateCompanyCommonUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2)
        {
            string text;
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    text = string_2 + ".aspx";
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + ".aspx";
                    if (!bool_0)
                    {
                        text = "../Sj/" + text;
                    }
                }
            }
            else
            {
                if (int_1 == 0)
                {
                    text = string_2 + string_0;
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + string_0;
                    if (!bool_0)
                    {
                        text = "../Sj/" + text;
                    }
                }
            }
            return text;
        }

        public static string CreateCompanyCommonUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2, string string_3)
        {
            string text;
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    text = string_2 + ".aspx?" + string_3;
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + ".aspx?" + string_3;
                    if (!bool_0)
                    {
                        text = "../Sj/" + text;
                    }
                }
            }
            else
            {
                string text2 = "";
                for (int i = 0; i < string_3.Split(new char[]
				{
					'&'
				}).Length; i++)
                {
                    text2 += "-";
                    text2 += Regex.Replace(Convert.ToString(string_3.Split(new char[]
					{
						'&'
					})[i]), "[^<>]*=", "");
                }
                if (int_1 == 0)
                {
                    text = string_2 + text2 + string_0;
                    if (!bool_0)
                    {
                        text = string_1 + text;
                    }
                }
                else
                {
                    text = string_2 + text2 + string_0;
                    if (!bool_0)
                    {
                        text = "../Sj/" + text;
                    }
                }
            }
            return text;
        }

        public static string CreateCompanyShopUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2, int int_2, string string_3, string string_4, string string_5)
        {
            string str = "";
            string_3 = string_3.Trim();
            if (((int_2 == 0) && (string_3.Length > 0)) && (string_5 == "1"))
            {
                if ((string_4.Length > 4) && (string_4.Substring(0, 4).ToLower() == "www."))
                {
                    string_4 = string_4.Substring(4, string_4.Length - 4);
                }
                return ("http://" + string_3 + "." + string_4);
            }
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    str = "ShopView.aspx?ID=" + string_2;
                    if (!bool_0)
                    {
                        str = string_1 + str;
                    }
                    return str;
                }
                str = "ShopView.aspx?ID=" + string_2;
                if (!bool_0)
                {
                    str = "../Sj/" + str;
                }
                return str;
            }
            if (int_1 == 0)
            {
                str = "ShopView-" + string_2 + string_0;
                if (!bool_0)
                {
                    str = string_1 + str;
                }
                return str;
            }
            str = "ShopView-" + string_2 + string_0;
            if (!bool_0)
            {
                str = "../Sj/" + str;
            }
            return str;
        }

        public static string CreateCompanyUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2, int int_2, string string_3, string string_4, string string_5, int int_3, string string_6, string string_7)
        {
            string str = "";
            string_3 = string_3.Trim();
            if (((int_3 == 0) && (string_6.Length > 0)) && (string_7 == "1"))
            {
                return ("http://" + string_6);
            }
            if (((int_2 == 0) && (string_3.Length > 0)) && (string_5 == "1"))
            {
                if ((string_4.Length > 4) && (string_4.Substring(0, 4).ToLower() == "www."))
                {
                    string_4 = string_4.Substring(4, string_4.Length - 4);
                }
                return ("http://" + string_3 + "." + string_4);
            }
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    str = "CompanyShow.aspx?ID=" + string_2;
                    if (!bool_0)
                    {
                        str = string_1 + str;
                    }
                    return str;
                }
                str = "CompanyShow.aspx?ID=" + string_2;
                if (!bool_0)
                {
                    str = "../Sj/" + str;
                }
                return str;
            }
            if (int_1 == 0)
            {
                str = "CompanyShow-" + string_2 + string_0;
                if (!bool_0)
                {
                    str = string_1 + str;
                }
                return str;
            }
            str = "CompanyShow-" + string_2 + string_0;
            if (!bool_0)
            {
                str = "../Sj/" + str;
            }
            return str;
        }

        public static string CreateStoreUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2, int int_2, string string_3, string string_4, string string_5, int int_3, string string_6, string string_7)
        {
            string str = "";
            string_3 = string_3.Trim();
            if (((int_3 == 0) && (string_6.Length > 0)) && (string_7 == "1"))
            {
                return ("http://" + string_6);
            }
            if (((int_2 == 0) && (string_3.Length > 0)) && (string_5 == "1"))
            {
                if ((string_4.Length > 4) && (string_4.Substring(0, 4).ToLower() == "www."))
                {
                    string_4 = string_4.Substring(4, string_4.Length - 4);
                }
                return ("http://" + string_3 + "." + string_4);
            }
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    str = "StoreDetail.aspx?ID=" + string_2;
                    if (!bool_0)
                    {
                        str = string_1 + str;
                    }
                    return str;
                }
                str = "StoreDetail.aspx?ID=" + string_2;
                if (!bool_0)
                {
                    str = "../Sj/" + str;
                }
                return str;
            }
            if (int_1 == 0)
            {
                str = "StoreDetail-" + string_2 + string_0;
                if (!bool_0)
                {
                    str = string_1 + str;
                }
                return str;
            }
            str = "StoreDetail-" + string_2 + string_0;
            if (!bool_0)
            {
                str = "../Sj/" + str;
            }
            return str;
        }

        public static string CreateLifeUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2)
        {
            string str = "";
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    str = "LifeShow.aspx?ID=" + string_2;
                    if (!bool_0)
                    {
                        str = string_1 + str;
                    }
                    return str;
                }
                str = "LifeShow.aspx?ID=" + string_2;
                if (!bool_0)
                {
                    str = "../Life/" + str;
                }
                return str;
            }
            if (int_1 == 0)
            {
                str = "Life-" + string_2 + string_0;
                if (!bool_0)
                {
                    str = string_1 + str;
                }
                return str;
            }
            str = "Life-" + string_2 + string_0;
            if (!bool_0)
            {
                str = "../Life/" + str;
            }
            return str;
        }

        public static void AddImageSignText(Image image_0, string string_0, string string_1, int int_0, int int_1, string string_2, int int_2)
        {
            Graphics graphics;
            string_1 = string_1.Replace("{1}", Base.GetDate());
            string_1 = string_1.Replace("{2}", Base.GetTime());
            Bitmap image = new Bitmap(image_0.Width, image_0.Height, PixelFormat.Format32bppArgb);
            bool flag = false;
            if (IsPixelFormatIndexed(image_0.PixelFormat))
            {
                using (graphics = Graphics.FromImage(image))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(image_0, 0, 0);
                }
                graphics = Graphics.FromImage(image);
                flag = true;
            }
            else
            {
                graphics = Graphics.FromImage(image_0);
            }
            Font font = new Font(string_2, (float)int_2, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF ef = graphics.MeasureString(string_1, font);
            float x = 0f;
            float y = 0f;
            switch (int_0)
            {
                case 1:
                    x = image_0.Width * 0.01f;
                    y = image_0.Height * 0.01f;
                    break;

                case 2:
                    x = (image_0.Width * 0.5f) - (ef.Width / 2f);
                    y = image_0.Height * 0.01f;
                    break;

                case 3:
                    x = (image_0.Width * 0.99f) - ef.Width;
                    y = image_0.Height * 0.01f;
                    break;

                case 4:
                    x = image_0.Width * 0.01f;
                    y = (image_0.Height * 0.5f) - (ef.Height / 2f);
                    break;

                case 5:
                    x = (image_0.Width * 0.5f) - (ef.Width / 2f);
                    y = (image_0.Height * 0.5f) - (ef.Height / 2f);
                    break;

                case 6:
                    x = (image_0.Width * 0.99f) - ef.Width;
                    y = (image_0.Height * 0.5f) - (ef.Height / 2f);
                    break;

                case 7:
                    x = image_0.Width * 0.01f;
                    y = (image_0.Height * 0.99f) - ef.Height;
                    break;

                case 8:
                    x = (image_0.Width * 0.5f) - (ef.Width / 2f);
                    y = (image_0.Height * 0.99f) - ef.Height;
                    break;

                case 9:
                    x = (image_0.Width * 0.99f) - ef.Width;
                    y = (image_0.Height * 0.99f) - ef.Height;
                    break;
            }
            graphics.DrawString(string_1, font, new SolidBrush(Color.White), (float)(x + 1f), (float)(y + 1f));
            graphics.DrawString(string_1, font, new SolidBrush(Color.Black), x, y);
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo encoder = null;
            foreach (ImageCodecInfo info2 in imageEncoders)
            {
                if (info2.MimeType.IndexOf("jpeg") > -1)
                {
                    encoder = info2;
                }
            }
            EncoderParameters encoderParams = new EncoderParameters();
            long[] numArray = new long[1];
            if ((int_1 < 0) || (int_1 > 100))
            {
                int_1 = 80;
            }
            numArray[0] = int_1;
            EncoderParameter parameter = new EncoderParameter(Encoder.Quality, numArray);
            encoderParams.Param[0] = parameter;
            if (flag)
            {
                if (encoder != null)
                {
                    image.Save(string_0, encoder, encoderParams);
                }
                else
                {
                    image.Save(string_0);
                }
            }
            else if (encoder != null)
            {
                image_0.Save(string_0, encoder, encoderParams);
            }
            else
            {
                image_0.Save(string_0);
            }
            image.Dispose();
            graphics.Dispose();
            image_0.Dispose();
        }

        private static bool IsPixelFormatIndexed(PixelFormat pixelFormat_0)
        {
            foreach (PixelFormat format in indexedPixelFormats)
            {
                if (format.Equals(pixelFormat_0))
                {
                    return true;
                }
            }
            return false;
        }

        public static void AddImageSignPic(Image image_0, string string_0, string string_1, int int_0, int int_1, int int_2)
        {
            Graphics graphics;
            Bitmap bitmap = new Bitmap(image_0.Width, image_0.Height, PixelFormat.Format32bppArgb);
            bool flag = false;
            if (IsPixelFormatIndexed(image_0.PixelFormat))
            {
                using (graphics = Graphics.FromImage(bitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.DrawImage(image_0, 0, 0);
                }
                graphics = Graphics.FromImage(bitmap);
                flag = true;
            }
            else
            {
                graphics = Graphics.FromImage(image_0);
            }
            Image image = new Bitmap(string_1);
            if ((image.Height >= image_0.Height) || (image.Width >= image_0.Width))
            {
                image_0.Save(string_0);
                image_0.Dispose();
            }
            else
            {
                ImageAttributes imageAttr = new ImageAttributes();
                ColorMap map = new ColorMap
                {
                    OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                    NewColor = Color.FromArgb(0, 0, 0, 0)
                };
                ColorMap[] mapArray = new ColorMap[] { map };
                imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
                float num = 0.5f;
                if ((int_2 >= 1) && (int_2 <= 10))
                {
                    num = ((float)int_2) / 10f;
                }
                float[][] numArray3 = new float[5][];
                float[] numArray4 = new float[5];
                numArray4[0] = 1f;
                numArray3[0] = numArray4;
                float[] numArray5 = new float[5];
                numArray5[1] = 1f;
                numArray3[1] = numArray5;
                float[] numArray6 = new float[5];
                numArray6[2] = 1f;
                numArray3[2] = numArray6;
                float[] numArray7 = new float[5];
                numArray7[3] = num;
                numArray3[3] = numArray7;
                float[] numArray8 = new float[5];
                numArray8[4] = 1f;
                numArray3[4] = numArray8;
                float[][] newColorMatrix = numArray3;
                ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int x = 0;
                int y = 0;
                switch (int_0)
                {
                    case 1:
                        x = (int)(image_0.Width * 0.01f);
                        y = (int)(image_0.Height * 0.01f);
                        break;

                    case 2:
                        x = ((int)(image_0.Width * 0.5f)) - (image.Width / 2);
                        y = (int)(image_0.Height * 0.01f);
                        break;

                    case 3:
                        x = ((int)(image_0.Width * 0.99f)) - image.Width;
                        y = (int)(image_0.Height * 0.01f);
                        break;

                    case 4:
                        x = (int)(image_0.Width * 0.01f);
                        y = ((int)(image_0.Height * 0.5f)) - (image.Height / 2);
                        break;

                    case 5:
                        x = ((int)(image_0.Width * 0.5f)) - (image.Width / 2);
                        y = ((int)(image_0.Height * 0.5f)) - (image.Height / 2);
                        break;

                    case 6:
                        x = ((int)(image_0.Width * 0.99f)) - image.Width;
                        y = ((int)(image_0.Height * 0.5f)) - (image.Height / 2);
                        break;

                    case 7:
                        x = (int)(image_0.Width * 0.01f);
                        y = ((int)(image_0.Height * 0.99f)) - image.Height;
                        break;

                    case 8:
                        x = ((int)(image_0.Width * 0.5f)) - (image.Width / 2);
                        y = ((int)(image_0.Height * 0.99f)) - image.Height;
                        break;

                    case 9:
                        x = ((int)(image_0.Width * 0.99f)) - image.Width;
                        y = ((int)(image_0.Height * 0.99f)) - image.Height;
                        break;
                }
                graphics.DrawImage(image, new Rectangle(x, y, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo encoder = null;
                foreach (ImageCodecInfo info2 in imageEncoders)
                {
                    if (info2.MimeType.IndexOf("jpeg") > -1)
                    {
                        encoder = info2;
                    }
                }
                EncoderParameters encoderParams = new EncoderParameters();
                long[] numArray2 = new long[1];
                if ((int_1 < 0) || (int_1 > 100))
                {
                    int_1 = 80;
                }
                numArray2[0] = int_1;
                EncoderParameter parameter = new EncoderParameter(Encoder.Quality, numArray2);
                encoderParams.Param[0] = parameter;
                if (flag)
                {
                    if (encoder != null)
                    {
                        bitmap.Save(string_0, encoder, encoderParams);
                    }
                    else
                    {
                        bitmap.Save(string_0);
                    }
                }
                else if (encoder != null)
                {
                    image_0.Save(string_0, encoder, encoderParams);
                }
                else
                {
                    image_0.Save(string_0);
                }
                bitmap.Dispose();
                graphics.Dispose();
                image_0.Dispose();
                image.Dispose();
                imageAttr.Dispose();
            }
        }

        public static string CreateInfoUrl(bool bool_0, int int_0, int int_1, string string_0, string string_1, string string_2)
        {
            string str = "";
            if (int_0 == 1)
            {
                if (int_1 == 0)
                {
                    str = "InfoShow.aspx?ID=" + string_2;
                    if (!bool_0)
                    {
                        str = string_1 + str;
                    }
                    return str;
                }
                str = "InfoShow.aspx?ID=" + string_2;
                if (!bool_0)
                {
                    str = "../Info/" + str;
                }
                return str;
            }
            if (int_1 == 0)
            {
                str = "Info-" + string_2 + string_0;
                if (!bool_0)
                {
                    str = string_1 + str;
                }
                return str;
            }
            str = "Info-" + string_2 + string_0;
            if (!bool_0)
            {
                str = "../Info/" + str;
            }
            return str;
        }



    }
}
