using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TestInf
{
    public static class Common
    {
        public static string GetXmlValue(this XmlNode node, string xpath, string attribute = "")
        {
            Sanity.Requires(node != null, "Root node is null.");
            var currentNode = string.IsNullOrEmpty(xpath) ? node : node.SelectSingleNode(xpath);
            Sanity.Requires(currentNode != null, $"The xpath {xpath} doesn't exist.");
            if (!string.IsNullOrWhiteSpace(attribute))
                return currentNode.InnerText;
            var attrib = currentNode.Attributes[attribute];
            Sanity.Requires(attrib != null, $"The attribute {attribute} doesn't exist.");
            return attrib.Value;
        }

        public static string ToStringLog(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-ss hh:mm:ss");
        }
        public static string ToStringPath(this DateTime dt)
        {
            return dt.ToString("yyyyMMss_hhmmss");
        }
    }
}
