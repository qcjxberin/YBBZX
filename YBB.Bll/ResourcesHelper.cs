using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace YBB.Bll
{
    public sealed class ResourcesHelper
    {
        private ResourcesHelper()
        {
        }

        public static void MakeThumbnail(Image image_0, string string_0, int int_0, int int_1, string string_1)
        {
            if (WebInfo.GetModule().SiteImageCut == 1)
            {
                Image image = image_0;
                string_0 = HttpContext.Current.Request.MapPath(string_0);
                if (((int_0 == 0) && (int_1 == 0)) || ((image_0.Width == int_0) && (image_0.Height == int_1)))
                {
                    image.Save(string_0);
                    image.Dispose();
                    return;
                }
                int num = int_0;
                int num2 = int_1;
                int num3 = 0;
                int num4 = 0;
                int num5 = image.Width;
                int num6 = image.Height;
                int num7 = 0;
                int num8 = 0;
                int num9 = num;
                int num10 = num2;
                double num11 = 0.0;
                if (image.Width >= image.Height)
                {
                    num11 = ((double)image.Width) / ((double)int_0);
                }
                else
                {
                    num11 = ((double)image.Height) / ((double)int_1);
                }
                if ((num5 <= int_0) && (num6 <= int_1))
                {
                    num9 = image.Width;
                    num10 = image.Height;
                    num7 = Convert.ToInt32((double)((num - num5) / 2.0));
                    num8 = Convert.ToInt32((double)((num2 - num6) / 2.0));
                }
                else
                {
                    num9 = Convert.ToInt32((double)(((double)image.Width) / num11));
                    num10 = Convert.ToInt32((double)(((double)image.Height) / num11));
                    num8 = Convert.ToInt32((double)((int_1 - num10) / 2.0));
                    num7 = Convert.ToInt32((double)((int_0 - num9) / 2.0));
                }
                Image image2 = new Bitmap(num, num2);
                Graphics graphics = Graphics.FromImage(image2);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(ColorTranslator.FromHtml("#ffffff"));
                graphics.DrawImage(image, new Rectangle(num7, num8, num9, num10), new Rectangle(num3, num4, num5, num6), GraphicsUnit.Pixel);
                try
                {
                    try
                    {
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
                        long[] numArray = new long[] { 100L };
                        EncoderParameter parameter = new EncoderParameter(Encoder.Quality, numArray);
                        encoderParams.Param[0] = parameter;
                        if (encoder != null)
                        {
                            image2.Save(string_0, encoder, encoderParams);
                        }
                        else
                        {
                            image2.Save(string_0);
                        }
                    }
                    catch (Exception exception)
                    {
                        throw exception;
                    }
                    return;
                }
                finally
                {
                    image.Dispose();
                    image2.Dispose();
                    graphics.Dispose();
                }
            }
            string_0 = HttpContext.Current.Request.MapPath(string_0);
            Image image3 = image_0;
            int width = int_0;
            int height = int_1;
            int x = 0;
            int y = 0;
            int num16 = image3.Width;
            int num17 = image3.Height;
            if (((num16 > width) || (num17 > height)) && ((width != 0) && (height != 0)))
            {
                string str2;
                string_1 = "Cut";
                string text1 = str2 = string_1;
                switch (str2)
                {
                    case "W":
                        height = (image3.Height * int_0) / image3.Width;
                        break;

                    case "H":
                        width = (image3.Width * int_1) / image3.Height;
                        break;

                    case "Cut":
                        if ((((double)image3.Width) / ((double)image3.Height)) > (((double)width) / ((double)height)))
                        {
                            num17 = image3.Height;
                            num16 = (image3.Height * width) / height;
                            y = 0;
                            x = (image3.Width - num16) / 2;
                        }
                        else
                        {
                            num16 = image3.Width;
                            num17 = (image3.Width * int_1) / width;
                            x = 0;
                            y = (image3.Height - num17) / 2;
                        }
                        break;

                    case "DB":
                        if ((((double)image3.Width) / ((double)width)) < (((double)image3.Height) / ((double)height)))
                        {
                            height = int_1;
                            width = (image3.Width * int_1) / image3.Height;
                        }
                        else
                        {
                            width = int_0;
                            height = (image3.Height * int_0) / image3.Width;
                        }
                        break;
                }
                Image image4 = new Bitmap(width, height);
                Graphics graphics2 = Graphics.FromImage(image4);
                graphics2.InterpolationMode = InterpolationMode.High;
                graphics2.SmoothingMode = SmoothingMode.HighQuality;
                graphics2.Clear(Color.Transparent);
                graphics2.DrawImage(image3, new Rectangle(0, 0, width, height), new Rectangle(x, y, num16, num17), GraphicsUnit.Pixel);
                try
                {
                    switch (string_0.Substring(string_0.LastIndexOf(".") + 1).ToUpper())
                    {
                        case "JPG":
                        case "JPEG":
                            image4.Save(string_0, ImageFormat.Jpeg);
                            break;

                        case "BMP":
                            image4.Save(string_0, ImageFormat.Bmp);
                            break;

                        case "GIF":
                            image4.Save(string_0, ImageFormat.Jpeg);
                            break;

                        case "PNG":
                            image4.Save(string_0, ImageFormat.Png);
                            break;

                        case "ICO":
                            image4.Save(string_0, ImageFormat.Icon);
                            break;
                    }
                }
                catch (Exception exception2)
                {
                    throw exception2;
                }
                finally
                {
                    image4.Dispose();
                    graphics2.Dispose();
                }
            }
            else
            {
                image3.Save(string_0);
            }
            image3.Dispose();
        }

        public static void MakeThumbnail(string string_0, string string_1, int int_0, int int_1, string string_2)
        {
            if (WebInfo.GetModule().SiteImageCut == 1)
            {
                string_0 = HttpContext.Current.Request.MapPath(string_0);
                string_1 = HttpContext.Current.Request.MapPath(string_1);
                Image image = Image.FromFile(string_0);
                int width = int_0;
                int height = int_1;
                int x = 0;
                int y = 0;
                int num5 = image.Width;
                int num6 = image.Height;
                int num7 = 0;
                int num8 = 0;
                int num9 = width;
                int num10 = height;
                double num11 = 0.0;
                if (image.Width >= image.Height)
                {
                    num11 = ((double)image.Width) / ((double)int_0);
                }
                else
                {
                    num11 = ((double)image.Height) / ((double)int_1);
                }
                if ((num5 <= int_0) && (num6 <= int_1))
                {
                    num9 = image.Width;
                    num10 = image.Height;
                    num7 = Convert.ToInt32((double)((width - num5) / 2.0));
                    num8 = Convert.ToInt32((double)((height - num6) / 2.0));
                }
                else
                {
                    num9 = Convert.ToInt32((double)(((double)image.Width) / num11));
                    num10 = Convert.ToInt32((double)(((double)image.Height) / num11));
                    num8 = Convert.ToInt32((double)((int_1 - num10) / 2.0));
                    num7 = Convert.ToInt32((double)((int_0 - num9) / 2.0));
                }
                Image image2 = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(image2);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(ColorTranslator.FromHtml("#ffffff"));
                graphics.DrawImage(image, new Rectangle(num7, num8, num9, num10), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
                try
                {
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
                    long[] numArray = new long[] { 100L };
                    EncoderParameter parameter = new EncoderParameter(Encoder.Quality, numArray);
                    encoderParams.Param[0] = parameter;
                    if (encoder != null)
                    {
                        image2.Save(string_1, encoder, encoderParams);
                    }
                    else
                    {
                        image2.Save(string_1);
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    image.Dispose();
                    image2.Dispose();
                    graphics.Dispose();
                }
            }
            else
            {
                string_0 = HttpContext.Current.Request.MapPath(string_0);
                string_1 = HttpContext.Current.Request.MapPath(string_1);
                Image image3 = Image.FromFile(string_0);
                int num12 = int_0;
                int num13 = int_1;
                int num14 = 0;
                int num15 = 0;
                int num16 = image3.Width;
                int num17 = image3.Height;
                if ((num16 <= num12) && (num17 <= num13))
                {
                    File.Copy(string_0, string_1);
                }
                else
                {
                    string str2;
                    if (((str2 = string_2) != null) && (str2 != "HW"))
                    {
                        if (str2 == "W")
                        {
                            num13 = (image3.Height * int_0) / image3.Width;
                        }
                        else if (str2 == "H")
                        {
                            num12 = (image3.Width * int_1) / image3.Height;
                        }
                        else if (str2 == "Cut")
                        {
                            if ((((double)image3.Width) / ((double)image3.Height)) > (((double)num12) / ((double)num13)))
                            {
                                num17 = image3.Height;
                                num16 = (image3.Height * num12) / num13;
                                num15 = 0;
                                num14 = (image3.Width - num16) / 2;
                            }
                            else
                            {
                                num16 = image3.Width;
                                num17 = (image3.Width * int_1) / num12;
                                num14 = 0;
                                num15 = (image3.Height - num17) / 2;
                            }
                        }
                        else if (str2 == "DB")
                        {
                            if ((((double)image3.Width) / ((double)num12)) < (((double)image3.Height) / ((double)num13)))
                            {
                                num13 = int_1;
                                num12 = (image3.Width * int_1) / image3.Height;
                            }
                            else
                            {
                                num12 = int_0;
                                num13 = (image3.Height * int_0) / image3.Width;
                            }
                        }
                    }
                    Image image4 = new Bitmap(num12, num13);
                    Graphics graphics2 = Graphics.FromImage(image4);
                    graphics2.InterpolationMode = InterpolationMode.High;
                    graphics2.SmoothingMode = SmoothingMode.HighQuality;
                    graphics2.Clear(Color.Transparent);
                    graphics2.DrawImage(image3, new Rectangle(0, 0, num12, num13), new Rectangle(num14, num15, num16, num17), GraphicsUnit.Pixel);
                    try
                    {
                        switch (string_1.Substring(string_1.LastIndexOf(".") + 1).ToUpper())
                        {
                            case "JPG":
                            case "JPEG":
                                image4.Save(string_1, ImageFormat.Jpeg);
                                break;

                            case "BMP":
                                image4.Save(string_1, ImageFormat.Bmp);
                                break;

                            case "GIF":
                                image4.Save(string_1, ImageFormat.Jpeg);
                                break;

                            case "PNG":
                                image4.Save(string_1, ImageFormat.Png);
                                break;

                            case "ICO":
                                image4.Save(string_1, ImageFormat.Icon);
                                break;
                        }
                    }
                    catch (Exception exception2)
                    {
                        throw exception2;
                    }
                    finally
                    {
                        image4.Dispose();
                        graphics2.Dispose();
                    }
                }
                image3.Dispose();
            }
        }

        public static void MakeThumbnailOther(Image image_0, string string_0, int int_0)
        {
            string_0 = HttpContext.Current.Request.MapPath(string_0);
            Image image = image_0;
            int width = int_0;
            int x = 0;
            int y = 0;
            int num4 = image.Width;
            int height = image.Height;
            if ((num4 > width) && (width != 0))
            {
                int num6 = (image.Height * int_0) / image.Width;
                Image image2 = new Bitmap(width, num6);
                Graphics graphics = Graphics.FromImage(image2);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(image, new Rectangle(0, 0, width, num6), new Rectangle(x, y, num4, height), GraphicsUnit.Pixel);
                try
                {
                    switch (string_0.Substring(string_0.LastIndexOf(".") + 1).ToUpper())
                    {
                        case "JPG":
                        case "JPEG":
                            image2.Save(string_0, ImageFormat.Jpeg);
                            break;

                        case "BMP":
                            image2.Save(string_0, ImageFormat.Bmp);
                            break;

                        case "GIF":
                            image2.Save(string_0, ImageFormat.Jpeg);
                            break;

                        case "PNG":
                            image2.Save(string_0, ImageFormat.Png);
                            break;

                        case "ICO":
                            image2.Save(string_0, ImageFormat.Icon);
                            break;
                    }
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                finally
                {
                    image2.Dispose();
                    graphics.Dispose();
                }
            }
            else
            {
                image.Save(string_0);
            }
            image.Dispose();
        }

        public static Stream ResizeImage(Stream stream_0, int int_0, int int_1, ImageFormat imageFormat_0)
        {
            Image image = Image.FromStream(stream_0);
            if ((image.Width <= int_1) && (image.Height <= int_0))
            {
                return stream_0;
            }
            int width = image.Width;
            int height = image.Height;
            float num3 = ((float)int_0) / ((float)height);
            if ((((float)int_1) / ((float)width)) < num3)
            {
                num3 = ((float)int_1) / ((float)width);
            }
            int_1 = (int)(width * num3);
            int_0 = (int)(height * num3);
            Image image2 = new Bitmap(int_1, int_0);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.Clear(Color.White);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(image, new Rectangle(0, 0, int_1, int_0), new Rectangle(0, 0, width, height), GraphicsUnit.Pixel);
            EncoderParameters encoderParams = new EncoderParameters();
            EncoderParameter parameter = new EncoderParameter(Encoder.Quality, 100L);
            encoderParams.Param[0] = parameter;
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo encoder = null;
            for (int i = 0; i < imageEncoders.Length; i++)
            {
                if (imageEncoders[i].FormatDescription.Equals("JPEG"))
                {
                    encoder = imageEncoders[i];
                    break;
                }
            }
            MemoryStream stream = new MemoryStream();
            image2.Save(stream, encoder, encoderParams);
            image.Dispose();
            image2.Dispose();
            graphics.Dispose();
            return stream;
        }
    }

}
