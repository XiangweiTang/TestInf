using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Collections;
using System.Reflection;

namespace TestInf
{
    public static class Common
    {
        #region Collections
        public static IEnumerable<T> ToCollection<T>(this T t)
        {
            yield return t;
        }
        public static IEnumerable<T> ToCollection<T>(params T[] ts)
        {
            return ts;
        }
        public static IEnumerable<T> Concat<T>(this T t, IEnumerable<T> ts)
        {
            return t.ToCollection().Concat(ts);
        }
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> ts, T t)
        {
            return ts.Concat(t.ToCollection());
        }
        #endregion
        #region DateTime
        public static string ToStringLog(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-ss hh:mm:ss");
        }
        public static string ToStringPath(this DateTime dt)
        {
            return dt.ToString("yyyyMMss_hhmmss");
        }
        #endregion
        #region IO
        public static IEnumerable<string> ReadEmbedLines(string embedPath)
        {
            Assembly asmb = Assembly.GetExecutingAssembly();
            using(StreamReader sr=new StreamReader(asmb.GetManifestResourceStream(embedPath)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    yield return line;
            }
        }

        public static string ReadEmbedAllLines(string embedPath)
        {
            Assembly asmb = Assembly.GetExecutingAssembly();
            using(StreamReader sr=new StreamReader(asmb.GetManifestResourceStream(embedPath)))
            {
                return sr.ReadToEnd();
            }
        }
        #endregion
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

    }
}
