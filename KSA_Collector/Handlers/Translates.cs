using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KSA_Collector.Handlers
{
    internal class Translates
    {
        XDocument doc;
        private string PATH = $"{AppDomain.CurrentDomain.BaseDirectory}Resource.";
        public void LoadPhrase(string key, string value)
        {
            string pathRes="";
            if (!Regex.IsMatch(value, @"\P{IsCyrillic}"))
            {
                pathRes = PATH + "ru.xml";
            }
            else
            {
                pathRes = PATH + "en.xml";
            }


            doc = new XDocument();
            if (File.Exists(pathRes))
            {
                doc = XDocument.Load(pathRes);
            }
            else
            {
                doc.Add(new XElement("translates"));
            }
            XElement xdata = null;

            try
            {
                doc.Root.XPathSelectElement($"data[@name = '{key}']").Remove();
            }
            catch { }

            xdata = new XElement("data",
                new XAttribute("name", key),
                new XAttribute(XNamespace.Xml + "space", "preserve"));

            XElement xvalue = new XElement("value");
            xvalue.SetValue(value);
            xdata.Add(xvalue);

            doc.Root.Add(xdata);
            doc.Save(pathRes);
        }
    }
}
