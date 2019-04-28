using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace TestInf.Features
{
    public abstract class Config
    {
        public Config() { }
        public string TaskName { get; private set; } = "NA";
        private XmlDocument XDoc = new XmlDocument();
        private XmlNode TaskNode = null;
        private XmlNode CommonNode = null;
        public void Load(TestInfArgs arg)
        {

        }

        private void LoadXml(string configPath)
        {            
            XDoc.Load(configPath);
            TaskName = XDoc.GetXmlValue("Root", "TaskName");
            TaskNode = XDoc["Root"][TaskName];
            CommonNode = XDoc["Root"]["Common"];
        }
        abstract protected void LoadTaskNode();
        virtual protected void LoadArg(string[] args) { }

        public void CreateConfig(string taskName, string path)
        {
            var currentNode = XDoc[TaskName];
            Sanity.Requires(currentNode != null, $"The task name {taskName} doesn't exist.");
            XmlDocument xDoc = (XmlDocument)XDoc.Clone();
            xDoc.RemoveAll();
            xDoc.AppendChild(currentNode);
            xDoc.AppendChild(CommonNode);
            xDoc.Save(path);
        }
    }

    public class DummyConfig : Config
    {
        public DummyConfig() : base() { }

        protected override void LoadTaskNode()
        {
            // DO NOTHING HERE.
        }
    }
}
