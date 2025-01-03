using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PoViEmu.Base
{
    public static class HtmlHelper
    {
        public static XDocument CreateDoc(string title)
        {
            var doc = XDocument.Parse("<html lang='en'>" +
                                      "<head>" +
                                      "<meta charset='UTF-8' />" +
                                      $"<title>{title}</title>" +
                                      "</head>" +
                                      "<body>" +
                                      $"<h1 id='title'>{title}</h1>" +
                                      "</body>" +
                                      "</html>");
            return doc;
        }

        public static XText ToText(params string[] lines)
        {
            var txt = string.Join(Environment.NewLine, lines);
            var item = new XText(txt);
            return item;
        }

        public static XElement CreateTable()
        {
            var item = XElement.Parse("<table><thead></thead><tbody></tbody></table>");
            return item;
        }

        public static XElement[] Repeat(string tag, params string[] texts)
        {
            return texts.Select(txt => new XElement(tag, new XText(txt.Trim()))).ToArray();
        }
        
        public static XElement ColSpan(int span, string txt)
        {
            return new XElement("td", new XAttribute("colspan", span), new XText(txt));
        }
        
        public static byte[] AsBytes(XDocument doc)
        {
            using var mem = new MemoryStream();
            using var writer = new XmlTextWriter(mem, Encoding.UTF8);
            doc.WriteTo(writer);
            writer.Flush();
            return mem.ToArray();
        }
    }
}