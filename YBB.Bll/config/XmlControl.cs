using System;
using System.Data;
using System.IO;
using System.Xml;

namespace YBB.Bll.config
{
    public class XmlControl
    {
        private string string_0;
        private XmlDocument xmlDocument_0 = new XmlDocument();

        public XmlControl(string string_1)
        {
            try
            {
                this.xmlDocument_0.Load(string_1);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            this.string_0 = string_1;
        }

        public void Delete(string string_1)
        {
            XmlNode oldChild = this.xmlDocument_0.SelectSingleNode(string_1);
            if (oldChild != null)
            {
                oldChild.ParentNode.RemoveChild(oldChild);
            }
        }

        public void Dispose()
        {
            this.xmlDocument_0 = null;
        }

        public bool ExistNode(string string_1)
        {
            if (this.xmlDocument_0.SelectSingleNode(string_1) == null)
            {
                return false;
            }
            return true;
        }

        public DataView GetData(string string_1)
        {
            DataSet set = new DataSet();
            StringReader reader = new StringReader(this.xmlDocument_0.SelectSingleNode(string_1).OuterXml);
            set.ReadXml(reader);
            return set.Tables[0].DefaultView;
        }

        public XmlNodeList GetList(string string_1)
        {
            return this.xmlDocument_0.SelectNodes(string_1);
        }

        public DataTable GetTable(string string_1)
        {
            try
            {
                DataSet set = new DataSet();
                StringReader reader = new StringReader(this.xmlDocument_0.SelectSingleNode(string_1).OuterXml);
                set.ReadXml(reader);
                return set.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        public string GetText(string string_1)
        {
            return this.GetText(string_1, false);
        }

        public string GetText(string string_1, bool bool_0)
        {
            XmlNode node = this.xmlDocument_0.SelectSingleNode(string_1);
            if (node == null)
            {
                return "";
            }
            if (bool_0)
            {
                return node.FirstChild.InnerText;
            }
            return node.InnerText;
        }

        public void InsertElement(string string_1, string string_2, string string_3, bool bool_0)
        {
            XmlNode node = this.xmlDocument_0.SelectSingleNode(string_1);
            XmlElement newChild = this.xmlDocument_0.CreateElement(string_2);
            if (bool_0)
            {
                newChild.InnerXml = "<![CDATA[" + string_3 + "]]>";
            }
            else
            {
                newChild.InnerText = string_3;
            }
            if (node != null)
            {
                node.AppendChild(newChild);
            }
        }

        public void InsertElement(string string_1, string string_2, string string_3, string string_4, string string_5)
        {
            XmlNode node = this.xmlDocument_0.SelectSingleNode(string_1);
            XmlElement newChild = this.xmlDocument_0.CreateElement(string_2);
            newChild.SetAttribute(string_3, string_4);
            newChild.InnerText = string_5;
            if (node != null)
            {
                node.AppendChild(newChild);
            }
        }

        public void InsertNode(string string_1, string string_2, string string_3, string string_4)
        {
            XmlNode node = this.xmlDocument_0.SelectSingleNode(string_1);
            XmlElement newChild = this.xmlDocument_0.CreateElement(string_2);
            if (node != null)
            {
                node.AppendChild(newChild);
                XmlElement element2 = this.xmlDocument_0.CreateElement(string_3);
                element2.InnerText = string_4;
                newChild.AppendChild(element2);
            }
        }

        public void RemoveAll(string string_1)
        {
            XmlNode node = this.xmlDocument_0.SelectSingleNode(string_1);
            if (node != null)
            {
                node.RemoveAll();
            }
        }

        public void Save()
        {
            try
            {
                this.xmlDocument_0.Save(this.string_0);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void Update(string string_1, string string_2)
        {
            this.Update(string_1, string_2, false);
        }

        public void Update(string string_1, string string_2, bool bool_0)
        {
            if (bool_0)
            {
                this.xmlDocument_0.SelectSingleNode(string_1).FirstChild.InnerText = string_2;
            }
            else
            {
                this.xmlDocument_0.SelectSingleNode(string_1).InnerText = string_2;
            }
        }
    }

}
