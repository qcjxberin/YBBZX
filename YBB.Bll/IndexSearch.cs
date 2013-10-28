using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using YBB.Bll.config;
using YBB.Common;

namespace YBB.Bll
{
    public class IndexSearch
    {

        public static IndexWriter CreateSearchIndex(int int_0, string string_0, string string_1, int int_1, bool bool_0, ref bool bool_1)
        {
            XmlControl control = new XmlControl(HttpContext.Current.Server.MapPath("~/public/config/SearchIndex.config"));
            string text = control.GetText("Module/" + string_0 + "/lastid");
            control.Dispose();
            string str3 = HttpContext.Current.Server.MapPath("~/public/config/index/" + string_0 + "/");
            IndexWriter writer = null;
            if (!bool_0)
            {
                try
                {
                    writer = new IndexWriter(str3, new StandardAnalyzer(), false);
                }
                catch (Exception)
                {
                    text = "0";
                    writer = new IndexWriter(str3, new StandardAnalyzer(), true);
                }
            }
            else
            {
                writer = new IndexWriter(str3, new StandardAnalyzer(), true);
                text = "0";
            }
            writer.SetMergeFactor(100);
            string str4 = " and [" + string_1 + "]>" + text;
            string str5 = "";
            if (int_0 == 1)
            {
                str5 = string.Concat(new object[] { "select top ", int_1, " NewsID as ID,NewsTitle as Title,NewsMark as Mark,NewsContent as Content,NewsDate as  Date,'' as SaleTypeID,NewsCategoryID as typeid from Ant_News where newskill=1 and NewsTypeID =2 ", str4, " order by NewsID asc" });
            }
            else if (int_0 == 2)
            {
                str5 = string.Concat(new object[] { "select top ", int_1, " NewsID as ID,NewsTitle as Title,NewsMark as Mark,NewsContent as Content,NewsDate as  Date,'' as SaleTypeID,NewsCategoryID as typeid from Ant_News where newskill=1 and NewsTypeID =4 ", str4, " order by NewsID asc" });
            }
            else if (int_0 == 3)
            {
                str5 = string.Concat(new object[] { "select top ", int_1, " InfoID as ID,InfoTitle as Title,'' as Mark,InfoMark as Content,InfoDate as  Date,'' as SaleTypeID,infoclassid as typeid from Ant_Info where Infokill=1 ", str4, " order by InfoID asc" });
            }
            else if (int_0 == 4)
            {
                str5 = string.Concat(new object[] { "select top ", int_1, " SaleID as ID,SaleTitle as Title,'' as Mark,SaleMark as Content,SaleDate as  Date,SaleTypeID from Ant_HouseSales where SaleKill=1 ", str4, " order by SaleID asc" });
            }
            else if (int_0 == 5)
            {
                str5 = string.Concat(new object[] { "select top ", int_1, " VideoID as ID,VideoTitle as Title,VideoMark as Mark,'' as Content,VideoDate as  Date,'' as SaleTypeID,videoclassid as typeid from Ant_Video where VideoKill=1 ", str4, " order by VideoID asc" });
            }
            else if (int_0 == 6)
            {
                str5 = string.Concat(new object[] { "select top ", int_1, " InfoID as ID,InfoName as Title,'' as Mark,InfoContent as Content,InfoDate as  Date,'' as SaleTypeID from Ant_JobInfo where InfoKill=1 ", str4, " order by InfoID asc" });
            }
            DataTable table = General.DataList(str5);
            int count = table.Rows.Count;
            if (count > 0)
            {
                bool_1 = true;
                string str6 = table.Rows[count - 1]["ID"].ToString();
                string str7 = "";
                string str8 = "";
                string str9 = "0";
                string str10 = "0";
                DataTable table2 = new DataTable();
                if (int_0 == 1)
                {
                    table2 = General.GetDataTable(0, "a.TypeID,a.TypeParent,a.TypeName,B.TypeName AS ParentTypeName", "Ant_NewsType A LEFT OUTER JOIN Ant_NewsType B ON A.TypeParent = B.TypeID", "a.TypeKill=0", "");
                }
                else if (int_0 == 2)
                {
                    table2 = General.GetDataTable(0, "Classid,ClassName", "Ant_NewsPicType", "ClassKill=0", "");
                }
                else if (int_0 == 3)
                {
                    table2 = General.GetDataTable(0, "a.Classid,a.ClassParent,a.ClassName,B.ClassName AS ParentClassName", "Ant_InfoClass A LEFT OUTER JOIN Ant_InfoClass B ON A.ClassParent = B.Classid", "a.ClassKill=0", "");
                }
                else if (int_0 == 5)
                {
                    table2 = General.GetDataTable(0, "ClassID,ClassName", "Ant_VideoClass", "ClassKill=0", "");
                }
                int num2 = 0;
                while (true)
                {
                    if (num2 >= table.Rows.Count)
                    {
                        break;
                    }
                    try
                    {
                        if (int_0 == 1)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["TypeID"] };
                            }
                            DataRow row = table2.Rows.Find(table.Rows[num2]["typeid"]);
                            if (row != null)
                            {
                                str7 = AntRequest.HtmlDecodeTrim(row["ParentTypeName"]);
                                str8 = AntRequest.HtmlDecodeTrim(row["TypeName"]);
                                str9 = row["TypeParent"].ToString();
                                str10 = row["TypeID"].ToString();
                                if (str9 == "0")
                                {
                                    str9 = row["TypeID"].ToString();
                                    str7 = AntRequest.HtmlDecodeTrim(row["TypeName"]);
                                }
                            }
                        }
                        else if (int_0 == 2)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["Classid"] };
                            }
                            DataRow row2 = table2.Rows.Find(table.Rows[num2]["typeid"]);
                            if (row2 != null)
                            {
                                str7 = AntRequest.HtmlDecodeTrim(row2["ClassName"]);
                                str9 = row2["Classid"].ToString();
                            }
                        }
                        else if (int_0 == 3)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["Classid"] };
                            }
                            DataRow row3 = table2.Rows.Find(table.Rows[num2]["typeid"]);
                            if (row3 != null)
                            {
                                str7 = AntRequest.HtmlDecodeTrim(row3["ParentClassName"]);
                                str8 = AntRequest.HtmlDecodeTrim(row3["ClassName"]);
                                str9 = row3["ClassParent"].ToString();
                                str10 = row3["Classid"].ToString();
                            }
                        }
                        else if (int_0 == 5)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["Classid"] };
                            }
                            DataRow row4 = table2.Rows.Find(table.Rows[num2]["typeid"]);
                            if (row4 != null)
                            {
                                str7 = AntRequest.HtmlDecodeTrim(row4["ClassName"]);
                                str9 = row4["Classid"].ToString();
                            }
                        }
                        Document document = new Document();
                        document.Add(new Field("id", table.Rows[num2]["ID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("saletypeid", table.Rows[num2]["SaleTypeID"].ToString(), Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("channelid", int_0.ToString(), Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("title", AntRequest.HtmlDecodeTrim(table.Rows[num2]["Title"]), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("content", AntRequest.LostHTML(AntRequest.HtmlDecode(table.Rows[num2]["content"].ToString())), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("adddate", Convert.ToDateTime(table.Rows[num2]["date"]).ToString("yyyyMMddHHmmss"), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("mark", AntRequest.HtmlDecodeTrim(table.Rows[num2]["Mark"]), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("bigcategory", str7, Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("smallcategory", str8, Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("bigid", str9.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("smallid", str10.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        writer.AddDocument(document);
                    }
                    catch (Exception)
                    {
                    }
                    num2++;
                }
                table2.Clear();
                table2.Dispose();
                table.Clear();
                table.Dispose();
                writer.SetUseCompoundFile(true);
                writer.Optimize();
                writer.Close();
                control = new XmlControl(HttpContext.Current.Server.MapPath("~/public/config/SearchIndex.config"));
                control.Update("Module/" + string_0 + "/lastid", str6);
                control.Update("Module/" + string_0 + "/lasttime", DateTime.Now.ToString(), true);
                control.Save();
                control.Dispose();
            }
            return writer;
        }

        public static void DeleteSearchIndex(string string_0, string string_1)
        {
            string str = HttpContext.Current.Server.MapPath("~/public/config/index/" + string_0 + "/");
            if (File.Exists(str + @"\segments.gen"))
            {
                FSDirectory directory = FSDirectory.GetDirectory(str, false);
                if (IndexReader.IsLocked(directory))
                {
                    IndexReader.Unlock(directory);
                }
                IndexReader reader = IndexReader.Open(directory);
                string[] strArray = string_1.Split(new char[] { ',' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    Term term = new Term("id", strArray[i].ToString());
                    reader.DeleteDocuments(term);
                }
                reader.Close();
                directory.Close();
            }
        }

        public static string HighLightKeyWord(string string_0, string string_1)
        {
            string input = string_0;
            string[] strArray = string_1.Split(new string[] { " " }, StringSplitOptions.None);
            if (strArray.Length < 1)
            {
                return input;
            }
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Trim().Length > 0)
                {
                    MatchCollection matchs = Regex.Matches(input, strArray[i].Trim(), RegexOptions.IgnoreCase);
                    for (int j = 0; j < matchs.Count; j++)
                    {
                        try
                        {
                            input = input.Insert((matchs[j].Index + strArray[i].Trim().Length) + (j * 0x1f), "</font>");
                            input = input.Insert(matchs[j].Index + (j * 0x1f), "<font style=\"color:red\">");
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return input.Replace(@"\", @"\\").Replace("'", @"\'").Replace("\t", " ").Replace("\r", " ").Replace("\n", " ");
        }

        public static void UpdateSearchIndex(int int_0, string string_0, string string_1, string string_2)
        {
            string str = HttpContext.Current.Server.MapPath("~/public/config/index/" + string_1 + "/");
            if (File.Exists(str + @"\segments.gen"))
            {
                string str2 = "and " + string_0 + " in(" + string_2 + ")";
                string str3 = "";
                if (int_0 == 1)
                {
                    str3 = "select NewsID as ID,NewsTitle as Title,NewsMark as Mark,NewsContent as Content,NewsDate as  Date,'' as SaleTypeID,NewsCategoryID as typeid from Ant_News where newskill=1 and NewsTypeID =2 " + str2 + " order by NewsID asc";
                }
                else if (int_0 == 2)
                {
                    str3 = "select NewsID as ID,NewsTitle as Title,NewsMark as Mark,NewsContent as Content,NewsDate as  Date,'' as SaleTypeID,NewsCategoryID as typeid from Ant_News where newskill=1 and NewsTypeID =4 " + str2 + " order by NewsID asc";
                }
                else if (int_0 == 3)
                {
                    str3 = "select InfoID as ID,InfoTitle as Title,'' as Mark,InfoMark as Content,InfoDate as  Date,'' as SaleTypeID,infoclassid as typeid from Ant_Info where Infokill=1 " + str2 + " order by InfoID asc";
                }
                else if (int_0 == 4)
                {
                    str3 = "select SaleID as ID,SaleTitle as Title,'' as Mark,SaleMark as Content,SaleDate as  Date,SaleTypeID from Ant_HouseSales where SaleKill=1 " + str2 + " order by SaleID asc";
                }
                else if (int_0 == 5)
                {
                    str3 = "select VideoID as ID,VideoTitle as Title,VideoMark as Mark,'' as Content,VideoDate as  Date,'' as SaleTypeID,videoclassid as typeid from Ant_Video where VideoKill=1 " + str2 + " order by VideoID asc";
                }
                else if (int_0 == 6)
                {
                    str3 = "select InfoID as ID,InfoName as Title,'' as Mark,InfoContent as Content,InfoDate as  Date,'' as SaleTypeID from Ant_JobInfo where InfoKill=1 " + str2 + " order by InfoID asc";
                }
                DataTable table = General.DataList(str3);
                if (table.Rows.Count > 0)
                {
                    FSDirectory directory = FSDirectory.GetDirectory(str, false);
                    if (IndexReader.IsLocked(directory))
                    {
                        IndexReader.Unlock(directory);
                    }
                    IndexWriter writer = new IndexWriter(str, new StandardAnalyzer(), false);
                    writer.SetMaxMergeDocs(2);
                    string str4 = "";
                    string str5 = "";
                    string str6 = "0";
                    string str7 = "0";
                    DataTable table2 = new DataTable();
                    if (int_0 == 1)
                    {
                        table2 = General.GetDataTable(0, "a.TypeID,a.TypeParent,a.TypeName,B.TypeName AS ParentTypeName", "Ant_NewsType A LEFT OUTER JOIN Ant_NewsType B ON A.TypeParent = B.TypeID", "a.TypeKill=0", "");
                    }
                    else if (int_0 == 2)
                    {
                        table2 = General.GetDataTable(0, "Classid,ClassName", "Ant_NewsPicType", "ClassKill=0", "");
                    }
                    else if (int_0 == 3)
                    {
                        table2 = General.GetDataTable(0, "a.Classid,a.ClassParent,a.ClassName,B.ClassName AS ParentClassName", "Ant_InfoClass A inner JOIN Ant_InfoClass B ON A.ClassParent = B.Classid", "a.ClassKill=0", "");
                    }
                    else if (int_0 == 5)
                    {
                        table2 = General.GetDataTable(0, "ClassID,ClassName", "Ant_VideoClass", "ClassKill=0", "");
                    }
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (int_0 == 1)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["TypeID"] };
                            }
                            DataRow row = table2.Rows.Find(table.Rows[i]["typeid"]);
                            if (row != null)
                            {
                                str4 = AntRequest.HtmlDecodeTrim(row["ParentTypeName"]);
                                str5 = AntRequest.HtmlDecodeTrim(row["TypeName"]);
                                str6 = row["TypeParent"].ToString();
                                str7 = row["TypeID"].ToString();
                                if (str6 == "0")
                                {
                                    str6 = row["TypeID"].ToString();
                                    str4 = AntRequest.HtmlDecodeTrim(row["TypeName"]);
                                }
                            }
                        }
                        else if (int_0 == 2)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["Classid"] };
                            }
                            DataRow row2 = table2.Rows.Find(table.Rows[i]["typeid"]);
                            if (row2 != null)
                            {
                                str4 = AntRequest.HtmlDecodeTrim(row2["ClassName"]);
                                str6 = row2["Classid"].ToString();
                            }
                        }
                        else if (int_0 == 3)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["Classid"] };
                            }
                            DataRow row3 = table2.Rows.Find(table.Rows[i]["typeid"]);
                            if (row3 != null)
                            {
                                str4 = AntRequest.HtmlDecodeTrim(row3["ParentClassName"]);
                                str5 = AntRequest.HtmlDecodeTrim(row3["ClassName"]);
                                str6 = row3["ClassParent"].ToString();
                                str7 = row3["Classid"].ToString();
                            }
                        }
                        else if (int_0 == 5)
                        {
                            if (table2.PrimaryKey.Length == 0)
                            {
                                table2.PrimaryKey = new DataColumn[] { table2.Columns["Classid"] };
                            }
                            DataRow row4 = table2.Rows.Find(table.Rows[i]["typeid"]);
                            if (row4 != null)
                            {
                                str4 = AntRequest.HtmlDecodeTrim(row4["ClassName"]);
                                str6 = row4["Classid"].ToString();
                            }
                        }
                        Term term = new Term("id", table.Rows[i]["ID"].ToString());
                        Document document = new Document();
                        document.Add(new Field("id", table.Rows[i]["ID"].ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("saletypeid", table.Rows[i]["SaleTypeID"].ToString(), Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("channelid", int_0.ToString(), Field.Store.YES, Field.Index.NO));
                        document.Add(new Field("title", AntRequest.HtmlDecodeTrim(table.Rows[i]["Title"]), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("content", AntRequest.LostHTML(AntRequest.HtmlDecode(table.Rows[i]["content"].ToString())), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("adddate", Convert.ToDateTime(table.Rows[i]["date"]).ToString("yyyyMMddHHmmss"), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("mark", AntRequest.HtmlDecodeTrim(table.Rows[i]["Mark"]), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("bigcategory", str4, Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("smallcategory", str5, Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("bigid", str6.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        document.Add(new Field("smallid", str7.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                        writer.UpdateDocument(term, document);
                    }
                    writer.SetUseCompoundFile(true);
                    writer.Close();
                    directory.Close();
                }
            }
        }
    }

}
