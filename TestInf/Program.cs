using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestInf
{
    class Program
    {        
        static Features.DummyConfig MainCfg = new Features.DummyConfig();
        static void Main(string[] args)
        {
            LoadAndRun(args);
        }

        static void LoadAndRun(string[] args)
        {
            var arg = new TestInfArgs(args);
            MainCfg.Load(arg);
            foreach(string taskName in MainCfg.DecomposeTasks())
            {
                TestInfArgs newArg = MainCfg.CreateNewArg(taskName);
                var task = GetTask(taskName);
                task.LoadAndRun(newArg);
            }
        }

        static Features.TestInf GetTask(string taskName)
        {
            switch (taskName.ToLower())
            {
                case "na":
                    return new Features.NA();
                case "helloworld":
                    return new Features.HelloWorld();
                default:
                    throw new TestInfEx($"Invalid task name {taskName}.");
            }
        }
    }
}
