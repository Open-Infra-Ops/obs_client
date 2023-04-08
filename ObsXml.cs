using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace obs_client
{
    class ObsXml
    {
        private static Dictionary<string, string> ObsConfig = new Dictionary<string, string>();
        private static string FileName = @"ObsClient.xml";

        public static Dictionary<string,string> ReadXml()
        {
            if (ObsConfig.Count == 0)
            {
                var configDict = new Dictionary<string, string>();
                if (File.Exists(FileName))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FileName);
                    XmlNodeList xnl = doc.DocumentElement.ChildNodes;
                    foreach (XmlNode item in xnl)
                    {
                        string name = item.Name.ToString();
                        string value = item.InnerText.ToString();
                        configDict.Add(name, value);
                    }
                    ObsConfig = configDict;
                }
            }
            return ObsConfig;
        }
    }
}
