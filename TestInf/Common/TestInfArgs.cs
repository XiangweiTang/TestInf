using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf
{
    public class TestInfArgs
    {
        public TaskMode Mode { get; private set; } = TaskMode.NA;
        public string ConfigPath { get; private set; } = "";
        public string[] Arguments { get; private set; } = new string[0];
        public TestInfArgs() { }
        public TestInfArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Mode |= TaskMode.Config;
                ConfigPath = "Config.xml";
                return;
            }

            switch (args[0].ToLower())
            {
                case "magictest":
                    Mode |= TaskMode.Test;
                    Mode |= TaskMode.Argument;
                    Arguments = args.Skip(1).ToArray();
                    break;
                case "magicschedule":
                    Mode |= TaskMode.Config;
                    Mode |= TaskMode.Argument;
                    Sanity.Requires(args.Length >= 2, "Schedule mode requires at least two args.");
                    ConfigPath = args[1];
                    Arguments = args.Skip(2).ToArray();
                    break;
                case "magicarg":
                    Mode |= TaskMode.Argument;
                    Arguments = args.Skip(1).ToArray();
                    break;
                default:
                    Mode |= TaskMode.Config;
                    ConfigPath = args[0];
                    break;
            }
        }
    }
}
