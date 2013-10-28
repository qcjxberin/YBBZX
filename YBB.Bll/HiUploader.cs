using Ant.Model;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Web;
using YBB.Common;

namespace YBB.Bll
{
    public sealed class HiUploader
    {
        private HiUploader()
        {
        }

        public static bool CheckExt(string string_0, string string_1)
        {
            string_0 = string_0.Substring(string_0.LastIndexOf(".") + 1);
            bool flag = false;
            string[] strArray = string_1.Split(",".ToCharArray());
            for (int i = 0; i < strArray.Length; i++)
            {
                if (string_0.ToLower() == strArray[i])
                {
                    flag = true;
                }
            }
            return flag;
        }

        private static bool CheckPostedFile(HttpPostedFile httpPostedFile_0, string string_0)
        {
            if (string_0 == "0")
            {
                string str = "jpg,bmp,gif,png";
                return ((((httpPostedFile_0 != null) && (httpPostedFile_0.ContentLength != 0)) && httpPostedFile_0.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/")) && CheckExt(httpPostedFile_0.FileName, str));
            }
            if (string_0 == "1")
            {
                string str2 = "jpg,bmp,gif,png,swf";
                if ((httpPostedFile_0 == null) || (httpPostedFile_0.ContentLength == 0))
                {
                    return false;
                }
                if (!httpPostedFile_0.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/"))
                {
                    return CheckExt(httpPostedFile_0.FileName, str2);
                }
                return true;
            }
            if (string_0 == "2")
            {
                string str3 = "csv";
                return (((httpPostedFile_0 != null) && (httpPostedFile_0.ContentLength != 0)) && CheckExt(httpPostedFile_0.FileName, str3));
            }
            if (string_0 == "3")
            {
                string str4 = "xml";
                return (((httpPostedFile_0 != null) && (httpPostedFile_0.ContentLength != 0)) && CheckExt(httpPostedFile_0.FileName, str4));
            }
            if (string_0 == "4")
            {
                string str5 = "csv,txt,xls";
                return (((httpPostedFile_0 != null) && (httpPostedFile_0.ContentLength != 0)) && CheckExt(httpPostedFile_0.FileName, str5));
            }
            if (string_0 == "5")
            {
                string str6 = "flv,swf,wmv,mp4,avi,asf";
                return (((httpPostedFile_0 != null) && (httpPostedFile_0.ContentLength != 0)) && CheckExt(httpPostedFile_0.FileName, str6));
            }
            if (!(string_0 == "6"))
            {
                return false;
            }
            string str7 = "swf";
            return (((httpPostedFile_0 != null) && (httpPostedFile_0.ContentLength != 0)) && CheckExt(httpPostedFile_0.FileName, str7));
        }

        public static void CreateNewsFile(Image image_0, int int_0, int int_1, string string_0, ref string string_1)
        {
            if ((image_0.Width == int_0) && (image_0.Height == int_1))
            {
                image_0.Save(HttpContext.Current.Server.MapPath(string_0));
            }
            else
            {
                ResourcesHelper.MakeThumbnail(image_0, string_0, int_0, int_1, "DB");
            }
            FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(string_0));
            string_1 = info.Length.ToString();
        }

        public static string CreatePathWithDate(string string_0, string string_1)
        {
            string str = "";
            DirectoryInfo info = new DirectoryInfo(HttpContext.Current.Request.MapPath(string_0));
            if (!info.Exists)
            {
                info.Create();
            }
            str = string_1.Replace("YY", DateTime.Now.Year.ToString()).Replace("MM", DateTime.Now.Month.ToString()).Replace("DD", DateTime.Now.Day.ToString());
            DirectoryInfo info2 = new DirectoryInfo(HttpContext.Current.Request.MapPath(string_0 + str));
            if (!info2.Exists)
            {
                info2.Create();
            }
            return (string_0 + str);
        }

        public static void CreatePicFile(Image image_0, int int_0, string string_0, ref string string_1)
        {
            if (image_0.Width == int_0)
            {
                image_0.Save(HttpContext.Current.Server.MapPath(string_0));
            }
            else
            {
                ResourcesHelper.MakeThumbnailOther(image_0, string_0, int_0);
            }
            FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(string_0));
            string_1 = info.Length.ToString();
        }

        public static void DeleteImage(string string_0)
        {
            if (!string.IsNullOrEmpty(string_0))
            {
                try
                {
                    string path = HttpContext.Current.Request.MapPath(Base.ApplicationPath + string_0);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch
                {
                }
            }
        }

        private static string GenerateFilename(string string_0)
        {
            DateTime now = DateTime.Now;
            new Random(DateTime.Now.Millisecond);
            return ((now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + now.Millisecond.ToString() + RandCodeCard(5).ToString()) + string_0);
        }

        public static int GetRandomSeed()
        {
            byte[] data = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        public static string RandCodeCard(int int_0)
        {
            string[] strArray = "0,A,1,B,2,C,3,D,4,E,5,F,6,J,K,7,T,U,V,8,Y,Z,9,G,H,I,L,M,N,O,P,Q,R,S,W,X".Split(new char[] { ',' });
            string str2 = "";
            int num = -1;
            Random random = new Random();
            for (int i = 1; i < (int_0 + 1); i++)
            {
                if (num != -1)
                {
                    random = new Random(GetRandomSeed());
                }
                int index = random.Next(0x1a);
                if ((num != -1) && (num == index))
                {
                    return RandCodeCard(int_0);
                }
                num = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }

        public static void UploadAdvImage(HttpPostedFile httpPostedFile_0, string string_0, int int_0, string string_1, string string_2, ref string string_3, ref string string_4, ref string string_5)
        {
            if (!CheckPostedFile(httpPostedFile_0, string_0))
            {
                string_3 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_3 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Images/", "YY-MM-DD/") + str2;
                string path = str3.Replace(extension, "_" + string_1 + "x" + string_2 + extension);
                string_3 = path.Remove(0, str3.Length - str2.Length);
                if ((extension.ToLower() != ".swf") && ((string_1 != "0") || (string_2 != "0")))
                {
                    if ((string_1 == "100") && (string_2 == "300"))
                    {
                        httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                    }
                    else
                    {
                        ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), path, Convert.ToInt32(string_1), Convert.ToInt32(string_2), "DB");
                    }
                }
                else
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                }
                string_4 = path.Replace("../../", "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_5 = info.Length.ToString();
            }
        }

        public static void UploadBASEImage(HttpPostedFile httpPostedFile_0, int int_0, ref string string_0, ref string string_1, ref string string_2, ref int int_1, ref int int_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string virtualPath = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Images/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(virtualPath));
                string_1 = virtualPath.Replace("../../", "");
                string_0 = str2;
                string_2 = httpPostedFile_0.ContentLength.ToString();
                Image image = Image.FromFile(HttpContext.Current.Server.MapPath(virtualPath));
                int_1 = image.Width;
                int_2 = image.Height;
                image.Dispose();
            }
        }

        public static void UploadChannelImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string_0 = str2;
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Channel/", "YY-MM-DD/") + str2;
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                path = path.Replace(extension, string.Concat(new object[] { "_", image.Width, "x", image.Height, extension }));
                if ((image.Width < int_1) && (image.Height < int_2))
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                }
                else
                {
                    ResourcesHelper.MakeThumbnail(image, path, int_1, int_2, "DB");
                }
                string_1 = path.Replace("../../", "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadCommonFile(string string_0, HttpPostedFile httpPostedFile_0, int int_0, ref string string_1, ref string string_2, ref string string_3)
        {
            if (!CheckPostedFile(httpPostedFile_0, "5"))
            {
                string_1 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > ((int_0 * 0x400) * 0x400))
            {
                string_1 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + string_0 + "/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                string_2 = path.Replace("../../", "");
                string_1 = string_2;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_3 = info.Length.ToString();
            }
        }

        public static void UploadCommonImage(string string_0, HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_1, ref string string_2, ref string string_3)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_1 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_1 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int num = int_1;
                int num2 = int_2;
                int_1 = image.Width;
                int_2 = image.Height;
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + string_0 + "/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image, str3, num, num2, "DB");
                string_2 = str3.Replace("../../", "");
                string_1 = string_2;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_3 = info.Length.ToString();
            }
        }

        public static void UploadCommonImageSwfFile(string string_0, HttpPostedFile httpPostedFile_0, int int_0, ref string string_1, ref string string_2, ref string string_3)
        {
            if (!CheckPostedFile(httpPostedFile_0, "1"))
            {
                string_1 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_1 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + string_0 + "/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                string_2 = path.Replace("../../", "");
                string_1 = string_2;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_3 = info.Length.ToString();
            }
        }

        public static void UploadCommonSwfFile(string string_0, HttpPostedFile httpPostedFile_0, int int_0, ref string string_1, ref string string_2, ref string string_3)
        {
            if (!CheckPostedFile(httpPostedFile_0, "6"))
            {
                string_1 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > ((int_0 * 0x400) * 0x400))
            {
                string_1 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + string_0 + "/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                string_2 = path.Replace("../../", "");
                string_1 = string_2;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_3 = info.Length.ToString();
            }
        }

        public static void UploadCompanyBrandImage(HttpPostedFile httpPostedFile_0, int int_0, ref int int_1, ref int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "1"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                if (extension.ToLower() != ".swf")
                {
                    Image image = Image.FromStream(httpPostedFile_0.InputStream);
                    int_1 = image.Width;
                    int_2 = image.Height;
                }
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Company/", "YY-MM-DD/") + str2;
                path.Replace(extension, string.Concat(new object[] { "_", (int)int_1, "x", (int)int_2, "_", extension }));
                path = path.Replace(extension, string.Concat(new object[] { "_", (int)int_1, "x", (int)int_2, extension }));
                httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                string_1 = path.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadCompanyImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int num = int_1;
                int num2 = int_2;
                int_1 = image.Width;
                int_2 = image.Height;
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Company/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image, str3, num, num2, "DB");
                string_1 = str3.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadCompanyShopImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "1"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                int num = int_1;
                int num2 = int_2;
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Company/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                if ((extension.ToLower() != ".swf") && ((int_1 != 950) || (int_2 != 0)))
                {
                    Image image = Image.FromStream(httpPostedFile_0.InputStream);
                    int_1 = image.Width;
                    int_2 = image.Height;
                    ResourcesHelper.MakeThumbnail(image, str3, num, num2, "DB");
                }
                else
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(str3));
                }
                string_1 = str3.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadContentCompanyImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int_1 = image.Width;
                int_2 = image.Height;
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Company/", "YY-MM-DD/") + str2;
                path.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                path = path.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                string_1 = path.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadCsv(HttpPostedFile httpPostedFile_0, int int_0, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "4"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string virtualPath = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Xml/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(virtualPath));
                string_1 = virtualPath.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(virtualPath));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadCSV(HttpPostedFile httpPostedFile_0, int int_0, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "2"))
            {
                string_1 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_1 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string virtualPath = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "File/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(virtualPath));
                string_1 = virtualPath;
            }
        }

        public static void UploadDzbImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string_0 = str2;
                string str3 = (CreatePathWithDate("../../" + Base.ImageUrlPrefix + "dzb/", "YY-MM-DD/") + str2).Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str3, int_1, int_2, "DB");
                string_1 = str3.Replace("../../", "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadGiftImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, int int_3, int int_4, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref string string_4)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_2 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_2 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("/" + Base.ImageUrlPrefix + "Gift/", "YY-MM-DD/") + str2;
                string_0 = (".." + str3).Replace("../", "");
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                Image image2 = Image.FromStream(httpPostedFile_0.InputStream);
                ResourcesHelper.MakeThumbnail(image, str3, int_3, int_4, "DB");
                string_1 = httpPostedFile_0.ContentLength.ToString();
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image2, str3, int_1, int_2, "DB");
                image2.Dispose();
                image.Dispose();
                string_3 = (".." + str3).Replace("../", "");
                string_2 = string_3;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_4 = info.Length.ToString();
            }
        }

        public static void UploadHouseImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string path = "";
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str3 = GenerateFilename(extension);
                string str4 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "House/", "YY-MM-DD/") + str3;
                path = str4.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str4 = str4.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str4, int_1, int_2, "DB");
                string_1 = str4.Replace("../../", "");
                string_0 = string_1;
                string_0 = string_0.Replace(CreatePathWithDate("../../" + Base.ImageUrlPrefix + "House/", "YY-MM-DD/"), "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str4));
                string_2 = info.Length.ToString();
                Image image2 = Image.FromFile(HttpContext.Current.Server.MapPath(str4));
                SiteWater siteWater = WebInfo.GetSiteWater();
                bool flag = false;
                if ((siteWater.SiteIsImageWaterMark == 1) && (((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))))
                {
                    if ((!string.IsNullOrEmpty(siteWater.SiteWaterMarkFormatString) && (siteWater.SiteWaterBuild == 0)) && ((int_1 == 480) && (int_2 == 300)))
                    {
                        flag = true;
                    }
                }
                else if ((((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))) && ((File.Exists(HttpContext.Current.Server.MapPath("../../" + siteWater.SiteWaterMarkImageUrl)) && (siteWater.SiteWaterBuild == 0)) && ((int_1 == 480) && (int_2 == 300))))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (siteWater.SiteIsImageWaterMark == 1)
                    {
                        ComManagent.AddImageSignText(image2, HttpContext.Current.Server.MapPath(path), siteWater.SiteWaterMarkFormatString, siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkFontName, siteWater.SiteWaterMarkFontSize);
                    }
                    else
                    {
                        ComManagent.AddImageSignPic(image2, HttpContext.Current.Server.MapPath(path), HttpContext.Current.Server.MapPath("../../" + siteWater.SiteWaterMarkImageUrl), siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkTransparency);
                    }
                    try
                    {
                        if (File.Exists(HttpContext.Current.Server.MapPath(str4)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(str4));
                        }
                    }
                    catch
                    {
                    }
                    string_1 = path.Replace("../../", "");
                    string_0 = string_1;
                    string_0 = string_0.Replace(CreatePathWithDate("../../" + Base.ImageUrlPrefix + "House/", "YY-MM-DD/").Replace("../", ""), "");
                }
                else
                {
                    image2.Dispose();
                }
            }
        }

        public static void UploadHouseImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref int int_3, ref int int_4, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref string string_4)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_2 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_2 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "House/", "YY-MM-DD/") + str2;
                string_0 = path.Replace("../../", "");
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int_3 = image.Width;
                int_4 = image.Height;
                Image image2 = Image.FromStream(httpPostedFile_0.InputStream);
                SiteWater siteWater = WebInfo.GetSiteWater();
                bool flag = false;
                if ((siteWater.SiteIsImageWaterMark == 1) && (((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))))
                {
                    if (!string.IsNullOrEmpty(siteWater.SiteWaterMarkFormatString) && (((((siteWater.SiteWaterBuild == 0) && (int_1 == 120)) && (int_2 == 90)) || (((siteWater.SiteWaterBuildHuxing == 0) && (int_1 == 200)) && (int_2 == 200))) || ((((siteWater.SiteWaterCommunity == 0) && (int_1 == 200)) && (int_2 == 200)) || (((siteWater.SiteWaterCommunityHuxing == 0) && (int_1 == 200)) && (int_2 == 200)))))
                    {
                        flag = true;
                    }
                }
                else if ((((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))) && (File.Exists(HttpContext.Current.Server.MapPath("../../" + siteWater.SiteWaterMarkImageUrl)) && (((((siteWater.SiteWaterBuild == 0) && (int_1 == 120)) && (int_2 == 90)) || (((siteWater.SiteWaterBuildHuxing == 0) && (int_1 == 200)) && (int_2 == 200))) || ((((siteWater.SiteWaterCommunity == 0) && (int_1 == 200)) && (int_2 == 200)) || (((siteWater.SiteWaterCommunityHuxing == 0) && (int_1 == 200)) && (int_2 == 200))))))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (siteWater.SiteIsImageWaterMark == 1)
                    {
                        ComManagent.AddImageSignText(image2, HttpContext.Current.Server.MapPath(path), siteWater.SiteWaterMarkFormatString, siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkFontName, siteWater.SiteWaterMarkFontSize);
                    }
                    else
                    {
                        ComManagent.AddImageSignPic(image2, HttpContext.Current.Server.MapPath(path), HttpContext.Current.Server.MapPath("../../" + siteWater.SiteWaterMarkImageUrl), siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkTransparency);
                    }
                }
                else
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(path));
                }
                string_1 = httpPostedFile_0.ContentLength.ToString();
                path = path.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image, path, int_1, int_2, "DB");
                string_3 = path.Replace("../../", "");
                string_2 = string_3;
                string_2 = string_2.Replace(CreatePathWithDate("../../" + Base.ImageUrlPrefix + "House/", "YY-MM-DD/").Replace("../", ""), "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_4 = info.Length.ToString();
            }
        }

        public static void UploadInfoTopImage(string string_0, int int_0, int int_1, HttpPostedFile httpPostedFile_0, int int_2, ref string string_1, ref string string_2, ref string string_3)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_1 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_2 * 0x400))
            {
                string_1 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + string_0 + "/", "YY-MM-DD/") + str2;
                int num = int_0;
                int num2 = int_1;
                string str4 = str3.Replace(extension, string.Concat(new object[] { "_", num, "x", num2, extension }));
                string_1 = str4.Remove(0, str3.Length - str2.Length);
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str4, num, num2, "DB");
                string_2 = str4.Replace("../../", "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str4));
                string_3 = info.Length.ToString();
            }
        }

        public static void UploadJobBrandImage(HttpPostedFile httpPostedFile_0, int int_0, ref int int_1, ref int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "1"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                if (extension.ToLower() != ".swf")
                {
                    Image image = Image.FromStream(httpPostedFile_0.InputStream);
                    int_1 = image.Width;
                    int_2 = image.Height;
                }
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Job/", "YY-MM-DD/") + str2;
                path.Replace(extension, string.Concat(new object[] { "_", (int)int_1, "x", (int)int_2, "_", extension }));
                path = path.Replace(extension, string.Concat(new object[] { "_", (int)int_1, "x", (int)int_2, extension }));
                httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                string_1 = path.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadJobImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Job/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str3, int_1, int_2, "DB");
                string_1 = str3.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadLinkImage(HttpPostedFile httpPostedFile_0, int int_0, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Links/", "YY-MM-DD/") + str2;
                string str4 = str3.Replace(extension, string.Concat(new object[] { "_", 0x58, "x", 0x1f, extension }));
                string_0 = str4.Remove(0, str3.Length - str2.Length);
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str4, 0x58, 0x1f, "DB");
                string_1 = str4.Replace("../../", "");
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str4));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadMemberTuanImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref int int_3, ref int int_4, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref string string_4)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_2 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_2 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string path = CreatePathWithDate("/" + Base.ImageUrlPrefix + "Tuan/", "YY-MM-DD/") + str2;
                string_0 = (".." + path).Replace("../", "");
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int_3 = image.Width;
                int_4 = image.Height;
                Image image2 = Image.FromStream(httpPostedFile_0.InputStream);
                SiteWater siteWater = WebInfo.GetSiteWater();
                bool flag = false;
                if ((siteWater.SiteIsImageWaterMark == 1) && (((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))))
                {
                    if (!string.IsNullOrEmpty(siteWater.SiteWaterMarkFormatString) && (siteWater.SiteWaterTuan == 0))
                    {
                        flag = true;
                    }
                }
                else if ((((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))) && (File.Exists(HttpContext.Current.Server.MapPath("/" + siteWater.SiteWaterMarkImageUrl)) && (siteWater.SiteWaterTuan == 0)))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (siteWater.SiteIsImageWaterMark == 1)
                    {
                        ComManagent.AddImageSignText(image2, HttpContext.Current.Server.MapPath(path), siteWater.SiteWaterMarkFormatString, siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkFontName, siteWater.SiteWaterMarkFontSize);
                    }
                    else
                    {
                        ComManagent.AddImageSignPic(image2, HttpContext.Current.Server.MapPath(path), HttpContext.Current.Server.MapPath("/" + siteWater.SiteWaterMarkImageUrl), siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkTransparency);
                    }
                }
                else
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(path));
                }
                string_1 = httpPostedFile_0.ContentLength.ToString();
                path = path.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image, path, int_1, int_2, "DB");
                string_3 = (".." + path).Replace("../", "");
                string_2 = string_3;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_4 = info.Length.ToString();
            }
        }

        public static void UploadNewsImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "News/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str3, int_1, int_2, "DB");
                string_1 = str3.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadPhotImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Photo/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, "_", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(Image.FromStream(httpPostedFile_0.InputStream), str3, int_1, int_2, "DB");
                string_1 = str3.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadPicImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "News/", "YY-MM-DD/") + str2;
                str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", extension }));
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", extension }));
                ResourcesHelper.MakeThumbnailOther(Image.FromStream(httpPostedFile_0.InputStream), str3, int_1);
                string_1 = str3.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_2 = info.Length.ToString();
            }
        }

        public static void UploadProductImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, int int_3, int int_4, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref string string_4)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_2 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_2 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string str3 = CreatePathWithDate("/" + Base.ImageUrlPrefix + "zxjc/", "YY-MM-DD/") + str2;
                string_0 = (".." + str3).Replace("../", "");
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                Image image2 = Image.FromStream(httpPostedFile_0.InputStream);
                ResourcesHelper.MakeThumbnail(image, str3, int_3, int_4, "DB");
                string_1 = httpPostedFile_0.ContentLength.ToString();
                str3 = str3.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image2, str3, int_1, int_2, "DB");
                image2.Dispose();
                image.Dispose();
                string_3 = (".." + str3).Replace("../", "");
                string_2 = string_3;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(str3));
                string_4 = info.Length.ToString();
            }
        }

        public static void UploadShopCategoryImage(HttpPostedFile httpPostedFile_0, int int_0, ref int int_1, ref int int_2, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int num = int_1;
                int_1 = image.Width;
                int_2 = image.Height;
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Shop/", "YY-MM-DD/") + str2;
                path.Replace(extension, string.Concat(new object[] { "_", (int)int_1, "x", (int)int_2, "_", extension }));
                path = path.Replace(extension, string.Concat(new object[] { "_", (int)int_1, "x", (int)int_2, extension }));
                if (image.Width <= num)
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Server.MapPath(path));
                    string_1 = path.Replace("../../", "");
                    string_0 = string_1;
                    FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                    string_2 = info.Length.ToString();
                }
                else
                {
                    string_0 = "-2";
                }
            }
        }

        public static void UploadShuiYinImage(HttpPostedFile httpPostedFile_0, int int_0, ref string string_0, ref string string_1, ref string string_2, ref int int_1, ref int int_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string virtualPath = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Images/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(virtualPath));
                string_1 = virtualPath.Replace("../../", "");
                string_0 = str2;
                string_2 = httpPostedFile_0.ContentLength.ToString();
                Image image = Image.FromFile(HttpContext.Current.Server.MapPath(virtualPath));
                int_1 = image.Width;
                int_2 = image.Height;
                image.Dispose();
            }
        }

        public static void UploadTuanImage(HttpPostedFile httpPostedFile_0, int int_0, int int_1, int int_2, ref int int_3, ref int int_4, ref string string_0, ref string string_1, ref string string_2, ref string string_3, ref string string_4)
        {
            if (!CheckPostedFile(httpPostedFile_0, "0"))
            {
                string_2 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_2 = "-1";
            }
            else
            {
                string extension = Path.GetExtension(httpPostedFile_0.FileName);
                string str2 = GenerateFilename(extension);
                string path = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Tuan/", "YY-MM-DD/") + str2;
                string_0 = path.Replace("../../", "");
                Image image = Image.FromStream(httpPostedFile_0.InputStream);
                int_3 = image.Width;
                int_4 = image.Height;
                Image image2 = Image.FromStream(httpPostedFile_0.InputStream);
                SiteWater siteWater = WebInfo.GetSiteWater();
                bool flag = false;
                if ((siteWater.SiteIsImageWaterMark == 1) && (((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))))
                {
                    if (!string.IsNullOrEmpty(siteWater.SiteWaterMarkFormatString) && (siteWater.SiteWaterTuan == 0))
                    {
                        flag = true;
                    }
                }
                else if ((((image2.Width >= siteWater.SiteWaterMarkMinWidth) && (image2.Height >= siteWater.SiteWaterMarkMinHeight)) || ((siteWater.SiteWaterMarkMinWidth == 0) || (siteWater.SiteWaterMarkMinHeight == 0))) && (File.Exists(HttpContext.Current.Server.MapPath("../../" + siteWater.SiteWaterMarkImageUrl)) && (siteWater.SiteWaterTuan == 0)))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (siteWater.SiteIsImageWaterMark == 1)
                    {
                        ComManagent.AddImageSignText(image2, HttpContext.Current.Server.MapPath(path), siteWater.SiteWaterMarkFormatString, siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkFontName, siteWater.SiteWaterMarkFontSize);
                    }
                    else
                    {
                        ComManagent.AddImageSignPic(image2, HttpContext.Current.Server.MapPath(path), HttpContext.Current.Server.MapPath("../../" + siteWater.SiteWaterMarkImageUrl), siteWater.SiteWaterMarkPosition, 100, siteWater.SiteWaterMarkTransparency);
                    }
                }
                else
                {
                    httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(path));
                }
                string_1 = httpPostedFile_0.ContentLength.ToString();
                path = path.Replace(extension, string.Concat(new object[] { "_", int_1, "x", int_2, extension }));
                ResourcesHelper.MakeThumbnail(image, path, int_1, int_2, "DB");
                string_3 = path.Replace("../../", "");
                string_2 = string_3;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(path));
                string_4 = info.Length.ToString();
            }
        }

        public static void UploadXML(HttpPostedFile httpPostedFile_0, int int_0, ref string string_0, ref string string_1, ref string string_2)
        {
            if (!CheckPostedFile(httpPostedFile_0, "3"))
            {
                string_0 = string.Empty;
            }
            else if (httpPostedFile_0.ContentLength > (int_0 * 0x400))
            {
                string_0 = "-1";
            }
            else
            {
                string str2 = GenerateFilename(Path.GetExtension(httpPostedFile_0.FileName));
                string virtualPath = CreatePathWithDate("../../" + Base.ImageUrlPrefix + "Xml/", "YY-MM-DD/") + str2;
                httpPostedFile_0.SaveAs(HttpContext.Current.Request.MapPath(virtualPath));
                string_1 = virtualPath.Replace("../../", "");
                string_0 = string_1;
                FileInfo info = new FileInfo(HttpContext.Current.Server.MapPath(virtualPath));
                string_2 = info.Length.ToString();
            }
        }
    }

}
