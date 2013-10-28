using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace YBB.Bll.ShootSeg
{
    public class Segment
    {
        private ArrayList arrayList_0;
        private ArrayList arrayList_1;
        private ArrayList arrayList_2;
        private ArrayList arrayList_3;
        private double double_0;
        private Hashtable hashtable_0;
        private string string_0;
        private string string_1;
        private string string_2;
        private string string_3;
        private string string_4;
        private string string_5;
        private string string_6;

        public Segment()
        {
            this.string_0 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sDict.dic");
            this.string_1 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sNoise.dic");
            this.string_2 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sNumber.dic");
            this.string_3 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sWord.dic");
            this.string_4 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sPrefix.dic");
            this.string_5 = " ";
            this.string_6 = "[一-龥]";
        }

        public Segment(string string_7, string string_8, string string_9, string string_10)
        {
            this.string_0 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sDict.dic");
            this.string_1 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sNoise.dic");
            this.string_2 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sNumber.dic");
            this.string_3 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sWord.dic");
            this.string_4 = HttpContext.Current.Server.MapPath("~/public/config/ShootSeg/sPrefix.dic");
            this.string_5 = " ";
            this.string_6 = "[一-龥]";
            this.string_3 = string_7;
            this.string_3 = string_8;
            this.string_3 = string_9;
            this.string_3 = string_10;
            this.InitWordDics();
        }

        private static object GetCache(string string_7)
        {
            return HttpContext.Current.Application.Get(string_7);
        }

        public void InitWordDics()
        {
            DateTime now = DateTime.Now;
            if (GetCache("jcms_dict") == null)
            {
                this.hashtable_0 = new Hashtable();
                StreamReader reader = new StreamReader(this.DicPath, Encoding.UTF8);
                string str3 = reader.ReadLine();
                Hashtable hashtable = this.hashtable_0;
                new Hashtable();
                long num = 0L;
                while (str3 != null)
                {
                    SegList list;
                    if (!(str3.Trim() != ""))
                    {
                        break;
                    }
                    num += 1L;
                    string key = str3.Substring(0, 1);
                    string str2 = str3.Substring(1, 1);
                    if (!this.hashtable_0.ContainsKey(key))
                    {
                        hashtable = new Hashtable();
                        this.hashtable_0.Add(key, hashtable);
                    }
                    else
                    {
                        hashtable = (Hashtable)this.hashtable_0[key];
                    }
                    if (!hashtable.ContainsKey(str2))
                    {
                        list = new SegList();
                        if (str3.Length > 2)
                        {
                            list.Add(str3.Substring(2));
                        }
                        else
                        {
                            list.Add("null");
                        }
                        hashtable.Add(str2, list);
                    }
                    else
                    {
                        list = (SegList)hashtable[str2];
                        if (str3.Length > 2)
                        {
                            list.Add(str3.Substring(2));
                        }
                        else
                        {
                            list.Add("null");
                        }
                        hashtable[str2] = list;
                    }
                    this.hashtable_0[key] = hashtable;
                    str3 = reader.ReadLine();
                }
                try
                {
                    reader.Close();
                }
                catch
                {
                }
                SetCache("jcms_dict", this.hashtable_0);
            }
            this.hashtable_0 = (Hashtable)GetCache("jcms_dict");
            this.arrayList_0 = this.LoadWords(this.NoisePath, this.arrayList_0);
            this.arrayList_1 = this.LoadWords(this.NumberPath, this.arrayList_1);
            this.arrayList_2 = this.LoadWords(this.WordPath, this.arrayList_2);
            this.arrayList_3 = this.LoadWords(this.PrefixPath, this.arrayList_3);
            TimeSpan span = (TimeSpan)(DateTime.Now - now);
            this.double_0 = span.TotalMilliseconds;
        }

        public ArrayList LoadWords(string string_7, ArrayList arrayList_4)
        {
            StreamReader reader = new StreamReader(string_7, Encoding.UTF8);
            arrayList_4 = new ArrayList();
            for (string str = reader.ReadLine(); str != null; str = reader.ReadLine())
            {
                arrayList_4.Add(str);
            }
            try
            {
                reader.Close();
            }
            catch
            {
            }
            return arrayList_4;
        }

        private int method_0(string string_7)
        {
            int num = 0;
            if (this.arrayList_1.Contains(string_7))
            {
                num = 1;
            }
            if (this.arrayList_2.Contains(string_7))
            {
                num = 2;
            }
            if (this.hashtable_0.ContainsKey(string_7))
            {
                num += 3;
            }
            return num;
        }

        public int Optimize()
        {
            int num = 0;
            DateTime now = DateTime.Now;
            Hashtable hashtable = new Hashtable();
            StreamReader reader = new StreamReader(this.DicPath, Encoding.UTF8);
            string key = reader.ReadLine();
            while (key != null)
            {
                if (!(key.Trim() != ""))
                {
                    break;
                }
                if (!hashtable.ContainsKey(key))
                {
                    hashtable.Add(key, null);
                }
                else
                {
                    num++;
                }
            }
            Console.WriteLine("ready");
            try
            {
                reader.Close();
            }
            catch
            {
            }
            StreamWriter writer = new StreamWriter(this.DicPath, false, Encoding.UTF8);
            IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                writer.WriteLine(enumerator.Key.ToString());
            }
            try
            {
                writer.Close();
            }
            catch
            {
            }
            TimeSpan span = (TimeSpan)(DateTime.Now - now);
            this.double_0 = span.TotalMilliseconds;
            return num;
        }

        public void OutArrayList(ArrayList arrayList_4)
        {
            if (arrayList_4 != null)
            {
                for (int i = 0; i < arrayList_4.Count; i++)
                {
                    Console.WriteLine(arrayList_4[i].ToString());
                }
            }
        }

        public void OutWords()
        {
            IDictionaryEnumerator enumerator = this.hashtable_0.GetEnumerator();
            while (enumerator.MoveNext())
            {
                IDictionaryEnumerator enumerator2 = ((Hashtable)enumerator.Value).GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    SegList list = (SegList)enumerator2.Value;
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.WriteLine(enumerator.Key.ToString() + enumerator2.Key.ToString() + list.GetElem(i).ToString());
                    }
                }
            }
        }

        public string SegmentText(string string_7)
        {
            TimeSpan span;
            string str = "";
            string_7 = (string_7 + "$").Trim();
            int num = 0;
            if (this.hashtable_0 == null)
            {
                return string_7;
            }
            if (string_7.Length < 3)
            {
                return string_7;
            }
            DateTime now = DateTime.Now;
            bool flag = false;
            bool flag2 = false;
            string separator = this.Separator;
            string str3 = "";
            int num2 = 0;
            string str4 = "";
            for (int i = 0; i < (string_7.Length - 1); i++)
            {
                Hashtable hashtable;
                SegList list;
                bool flag3;
                bool flag4;
                string str7;
                string str8;
                string str9;
                string str5 = string_7.Substring(i, 1);
                string str6 = string_7.Substring(i + 1, 1).Trim();
                if (str.Length > 0)
                {
                    str4 = str.Substring(str.Length - 1);
                }
                if (str5 == " ")
                {
                    if ((flag2 || flag) && (str4 != this.Separator))
                    {
                        str = str + this.Separator;
                    }
                    flag3 = true;
                }
                else
                {
                    flag3 = false;
                }
                int num4 = this.method_0(str5);
                switch (num4)
                {
                    case 1:
                        if (flag)
                        {
                            str = str + this.Separator;
                        }
                        separator = "";
                        flag = false;
                        flag2 = true;
                        goto Label_055B;

                    case 2:
                    case 5:
                        if (!flag2)
                        {
                            goto Label_054F;
                        }
                        separator = this.Separator;
                        goto Label_0556;

                    case 3:
                    case 4:
                        if (flag)
                        {
                            str = str + this.Separator;
                        }
                        if (flag2 && (num4 != 4))
                        {
                            hashtable = (Hashtable)this.hashtable_0["n"];
                            if (!hashtable.ContainsKey(str5))
                            {
                                break;
                            }
                            list = (SegList)hashtable[str5];
                            if (list.Contains(str6))
                            {
                                str = str + str5 + str6 + this.Separator;
                                flag3 = true;
                                i++;
                            }
                            else if (list.Contains("null"))
                            {
                                str = str + str5 + this.Separator;
                                flag3 = true;
                            }
                        }
                        goto Label_01FA;

                    default:
                        if (flag && !flag3)
                        {
                            str = str + this.Separator;
                        }
                        else if (flag2 && !flag3)
                        {
                            str = str + this.Separator;
                        }
                        flag2 = false;
                        flag = false;
                        separator = this.Separator;
                        goto Label_055B;
                }
                str = str + this.Separator;
            Label_01FA:
                if (num4 == 3)
                {
                    flag = false;
                    flag2 = false;
                    separator = this.Separator;
                }
                else
                {
                    separator = "";
                    flag = false;
                    flag2 = true;
                }
                hashtable = (Hashtable)this.hashtable_0[str5];
                if (!hashtable.ContainsKey(str6))
                {
                    goto Label_055B;
                }
                list = (SegList)hashtable[str6];
                for (int j = 0; j < list.Count; j++)
                {
                    flag4 = false;
                    str7 = list.GetElem(j).ToString();
                    if (((str7.Length + i) + 2) < string_7.Length)
                    {
                        str8 = string_7.Substring(i + 2, str7.Length).Trim();
                        if (!(str7 == str8) || flag3)
                        {
                            goto Label_02DB;
                        }
                        goto Label_0321;
                    }
                    if (((str7.Length + i) + 2) == string_7.Length)
                    {
                        str9 = string_7.Substring(i + 2).Trim();
                        if ((str7 == str9) && !flag3)
                        {
                            goto Label_036E;
                        }
                    }
                Label_02DB:
                    if ((!flag4 && (j == (list.Count - 1))) && (list.Contains("null") && !flag3))
                    {
                        goto Label_03BB;
                    }
                    if (flag4)
                    {
                        break;
                    }
                }
                goto Label_044C;
            Label_0321:
                if (str3 != "")
                {
                    str = str + str3 + this.Separator;
                    str3 = "";
                    num2 = 0;
                }
                str = str + str5 + str6 + str8;
                i += str7.Length + 1;
                flag4 = true;
                flag3 = true;
                goto Label_044C;
            Label_036E:
                if (str3 != "")
                {
                    str = str + str3 + this.Separator;
                    str3 = "";
                    num2 = 0;
                }
                str = str + str5 + str6 + str9;
                i += str7.Length + 1;
                flag4 = true;
                flag3 = true;
                goto Label_044C;
            Label_03BB:
                if (num2 == 1)
                {
                    str = str + str3 + str5 + str6;
                    str3 = "";
                    num2 = 0;
                }
                else if (num2 > 1)
                {
                    string str12 = str;
                    str = str12 + str3 + separator + str5 + str6;
                    str3 = "";
                    num2 = 0;
                }
                else
                {
                    if (num4 == 4)
                    {
                        str = str + str5 + str6;
                    }
                    else
                    {
                        str = str + str5 + str6;
                    }
                    separator = this.Separator;
                    flag2 = false;
                }
                i++;
                flag3 = true;
            Label_044C:
                if (!flag3 && list.Contains("null"))
                {
                    if (num2 == 1)
                    {
                        str = str + str3 + str5 + str6;
                        str3 = "";
                        num2 = 0;
                    }
                    else if (num2 > 1)
                    {
                        string str13 = str;
                        str = str13 + str3 + separator + str5 + str6;
                        str3 = "";
                        num2 = 0;
                    }
                    else
                    {
                        if (num4 == 4)
                        {
                            str = str + str5 + str6;
                        }
                        else
                        {
                            str = str + str5 + str6;
                        }
                        separator = this.Separator;
                        flag2 = false;
                    }
                    i++;
                    flag3 = true;
                }
                if (str.Length > 0)
                {
                    str4 = str.Substring(str.Length - 1);
                }
                if ((num4 == 4) && (this.method_0(str4) == 4))
                {
                    flag2 = true;
                }
                else if (str4 != this.Separator)
                {
                    str = str + this.Separator;
                }
                goto Label_055B;
            Label_054F:
                separator = "";
            Label_0556:
                flag = true;
                flag2 = false;
            Label_055B:
                if ((!flag3 && flag2) || (!flag3 && flag))
                {
                    str = str + str5;
                    flag3 = true;
                }
                if (!flag3)
                {
                    if (num2 == 0)
                    {
                        if (this.arrayList_3.Contains(str5 + str6))
                        {
                            i++;
                            str3 = str5 + str6;
                            num2++;
                        }
                        else if (this.arrayList_3.Contains(str5))
                        {
                            if (!flag2)
                            {
                                str3 = str5;
                                num2++;
                            }
                            else
                            {
                                str = str + str5 + separator;
                                flag2 = false;
                                flag = false;
                            }
                        }
                        else if (num2 == 3)
                        {
                            string str14 = str;
                            str = str14 + str3 + this.Separator + str5 + this.Separator;
                            str3 = "";
                            num2 = 0;
                        }
                        else if (num2 > 0)
                        {
                            if (Regex.IsMatch(str5, this.string_6))
                            {
                                str3 = str3 + str5;
                                num2++;
                            }
                            else
                            {
                                string str15 = str;
                                str = str15 + str3 + this.Separator + str5 + this.Separator;
                                str3 = "";
                                num2 = 0;
                            }
                        }
                        else
                        {
                            str = str + str5 + separator;
                            flag2 = false;
                            flag = false;
                        }
                    }
                    else if (num2 == 3)
                    {
                        string str16 = str;
                        str = str16 + str3 + this.Separator + str5 + this.Separator;
                        str3 = "";
                        num2 = 0;
                    }
                    else if (num2 > 0)
                    {
                        if (Regex.IsMatch(str5, this.string_6))
                        {
                            str3 = str3 + str5;
                            num2++;
                        }
                        else
                        {
                            string str17 = str;
                            str = str17 + str3 + this.Separator + str5 + this.Separator;
                            str3 = "";
                            num2 = 0;
                        }
                    }
                    else
                    {
                        str = str + str5 + separator;
                        flag2 = false;
                    }
                }
                num = i;
            }
            if (num >= (string_7.Length - 1))
            {
                goto Label_0911;
            }
            string str10 = string_7.Substring(string_7.Length - 1).Trim();
            string item = string_7.Substring(string_7.Length - 2).Trim();
            if (str.Length > 0)
            {
                str4 = str.Substring(str.Length - 1);
            }
            if (num2 != 0)
            {
                str = str + str3 + str10;
            }
            else
            {
                switch (this.method_0(str10))
                {
                    case 1:
                        if (!(str10 != ".") || !(str10 != "．"))
                        {
                            str = str + this.Separator + str10;
                        }
                        else
                        {
                            str = str + str10;
                        }
                        goto Label_08DC;

                    case 2:
                    case 5:
                        if (this.arrayList_2.Contains(item))
                        {
                            str = str + str10;
                        }
                        goto Label_08DC;

                    case 3:
                    case 4:
                        if ((flag2 || flag) && (str4 != this.Separator))
                        {
                            str = str + this.Separator + str10;
                        }
                        else
                        {
                            str = str + str10;
                        }
                        goto Label_08DC;
                }
                if (str4 != this.Separator)
                {
                    str = str + this.Separator + str10;
                }
                else
                {
                    str = str + str10;
                }
            }
        Label_08DC:
            if (str.Length > 0)
            {
                str4 = str.Substring(str.Length - 1);
            }
            if (str4 != this.Separator)
            {
                str = str + this.Separator;
            }
        Label_0911:
            span = (TimeSpan)(DateTime.Now - now);
            this.double_0 = span.TotalMilliseconds;
            return str.Replace(" $", "");
        }

        public string SegmentText(string string_7, bool bool_0)
        {
            if (!bool_0)
            {
                return this.SegmentText(string_7);
            }
            DateTime now = DateTime.Now;
            string[] strArray = string_7.Split(new char[] { '\n' });
            string str = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                str = str + this.SegmentText(strArray[i]) + "\r\n";
            }
            TimeSpan span = (TimeSpan)(DateTime.Now - now);
            this.double_0 = span.TotalMilliseconds;
            return str;
        }

        private static void SetCache(string string_7, object object_0)
        {
            if (object_0 == null)
            {
                object_0 = " ";
            }
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application.Set(string_7, object_0);
            HttpContext.Current.Application.UnLock();
        }

        public void SortDic()
        {
            this.SortDic(false);
        }

        public void SortDic(bool bool_0)
        {
            DateTime now = DateTime.Now;
            StreamWriter writer = new StreamWriter(this.DicPath, false, Encoding.UTF8);
            IDictionaryEnumerator enumerator = this.hashtable_0.GetEnumerator();
            while (enumerator.MoveNext())
            {
                IDictionaryEnumerator enumerator2 = ((Hashtable)enumerator.Value).GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    SegList list = (SegList)enumerator2.Value;
                    list.Sort();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list.GetElem(i).ToString() == "null")
                        {
                            writer.WriteLine(enumerator.Key.ToString() + enumerator2.Key.ToString());
                        }
                        else
                        {
                            writer.WriteLine(enumerator.Key.ToString() + enumerator2.Key.ToString() + list.GetElem(i).ToString());
                        }
                    }
                }
            }
            writer.Close();
            if (bool_0)
            {
                this.InitWordDics();
            }
            TimeSpan span = (TimeSpan)(DateTime.Now - now);
            this.double_0 = span.TotalMilliseconds;
        }

        public string DicPath
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

        public bool EnablePrefix
        {
            get
            {
                if (this.arrayList_3.Count == 0)
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    this.arrayList_3 = this.LoadWords(this.PrefixPath, this.arrayList_3);
                }
                else
                {
                    this.arrayList_3 = new ArrayList();
                }
            }
        }

        public double EventTime
        {
            get
            {
                return this.double_0;
            }
        }

        public string NoisePath
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

        public string NumberPath
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public string PrefixPath
        {
            get
            {
                return this.string_4;
            }
            set
            {
                this.string_4 = value;
            }
        }

        public string Separator
        {
            get
            {
                return this.string_5;
            }
            set
            {
                if ((value != "") && (value != null))
                {
                    this.string_5 = value;
                }
            }
        }

        public string WordPath
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }
    }

}
