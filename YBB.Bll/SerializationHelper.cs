using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace YBB.Bll
{
    public class SerializationHelper
    {
        private static Dictionary<int, XmlSerializer> serializer_dict = new Dictionary<int, XmlSerializer>();

        public static object DeSerialize(Type type_0, string string_0)
        {
            object obj2;
            byte[] bytes = Encoding.UTF8.GetBytes(string_0);
            try
            {
                obj2 = GetSerializer(type_0).Deserialize(new MemoryStream(bytes));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return obj2;
        }

        public static XmlSerializer GetSerializer(Type type_0)
        {
            int hashCode = type_0.GetHashCode();
            if (!serializer_dict.ContainsKey(hashCode))
            {
                serializer_dict.Add(hashCode, new XmlSerializer(type_0));
            }
            return serializer_dict[hashCode];
        }

        public static object Load(Type type_0, string string_0)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlSerializer serializer = new XmlSerializer(type_0);
                return serializer.Deserialize(stream);
            }
            catch
            {
                stream.Close();
                HttpContext.Current.Response.Write(@"<font color=red style='font-size:14px;font-weight:bold;'>看到此错误，请将 C:\WINDOWS\Temp 这个目录加上Users用户的读取、运行、列出权限，不需要写入和修改权限（否则将对系统造成不安全等因素），然后再重启IIS！<font>");
                HttpContext.Current.Response.End();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return null;
        }

        public static bool Save(object object_0, string string_0)
        {
            bool flag = false;
            FileStream stream = null;
            try
            {
                stream = new FileStream(string_0, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                new XmlSerializer(object_0.GetType()).Serialize((Stream)stream, object_0);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return flag;
        }

        public static string Serialize(object object_0)
        {
            string str = "";
            XmlSerializer serializer = GetSerializer(object_0.GetType());
            MemoryStream w = new MemoryStream();
            XmlTextWriter writer = null;
            StreamReader reader = null;
            try
            {
                writer = new XmlTextWriter(w, Encoding.UTF8)
                {
                    Formatting = Formatting.Indented
                };
                serializer.Serialize((XmlWriter)writer, object_0);
                w.Seek(0L, SeekOrigin.Begin);
                reader = new StreamReader(w);
                str = reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
                w.Close();
            }
            return str;
        }
    }

}
