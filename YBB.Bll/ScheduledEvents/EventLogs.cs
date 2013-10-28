using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace YBB.Bll.ScheduledEvents
{
    public sealed class EventLogs
    {
        public static string LogFileName = string.Empty;

        public static void WriteFailedLog(string string_0)
        {
            if (string.IsNullOrEmpty(LogFileName))
            {
                LogFileName = HttpContext.Current.Server.MapPath("/Upload/EventFaildlog.config");
            }
            StringBuilder builder = new StringBuilder();
            builder.Append(DateTime.Now);
            builder.Append("\t");
            builder.Append(Environment.MachineName);
            builder.Append("\t");
            builder.Append(string_0);
            builder.Append("\r\n");
            using (FileStream stream = new FileStream(LogFileName, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
        }
    }

}
