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
        public TestInfArgs Arg { get; private set; } = new TestInfArgs();
        private XmlDocument XDoc = new XmlDocument();
        private XmlNode TaskNode = null;
        private XmlNode CommonNode = null;
        public void Load(TestInfArgs arg)
        {
            Arg = arg;
            if ((Arg.Mode & TaskMode.Config) !=0)
                LoadXml(Arg.ConfigPath);
            if ((arg.Mode & TaskMode.Argument) != 0)
                LoadArgs();
        }

        private void LoadXml(string configPath)
        {            
            XDoc.Load(configPath);
            TaskName = XDoc.GetXmlValue("Root", "TaskName");
            TaskNode = XDoc["Root"][TaskName];
            CommonNode = XDoc["Root"]["Common"];
        }
        abstract protected void LoadTaskNode();
        abstract protected void LoadArgs();
        public TestInfArgs CreateNewArg(string taskName)
        {
            if (TaskName != "NA")
            {
                string folderPath = Path.Combine("Tmp", $"{DateTime.Now.ToStringPath()}_{TaskName}");
                Directory.CreateDirectory(folderPath);
                string configPath = Path.Combine(folderPath, "Config.xml");
                var currentNode = XDoc[TaskName];
                Sanity.Requires(currentNode != null, $"The task name {taskName} doesn't exist.");
                XmlDocument xDoc = (XmlDocument)XDoc.Clone();
                xDoc.RemoveAll();
                xDoc.AppendChild(currentNode);
                xDoc.AppendChild(CommonNode);
                xDoc.Save(configPath);
                Arg.ConfigPath = configPath;
            }
            return Arg;
        }
    }

    public class DummyConfig : Config
    {
        public DummyConfig() : base() { }

        protected override void LoadTaskNode()
        {
            // DO NOTHING HERE.
        }
        protected override void LoadArgs()
        {
            // DO NOTHING HERE.
        }

        public IEnumerable<string> DecomposeTasks()
        {
            return TaskName.Split(',').Select(x => x.Trim());
        }
    }
}
