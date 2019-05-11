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
        public Dictionary<string, string> ArgDict { get; private set; } = new Dictionary<string, string>();
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
                    SetArgDict(args.Skip(1));
                    break;
                case "magicschedule":
                    Mode |= TaskMode.Config;
                    Mode |= TaskMode.Argument;
                    Sanity.Requires(args.Length >= 2, "Schedule mode requires at least two args.");
                    ConfigPath = args[1];
                    SetArgDict(args.Skip(2));
                    break;
                case "magicarg":
                    Mode |= TaskMode.Argument;
                    SetArgDict(args.Skip(1));
                    break;
                default:
                    Mode |= TaskMode.Config;
                    ConfigPath = args[0];
                    break;
            }
        }

        private void SetArgDict(IEnumerable<string> args)
        {
            // -OPT0 ARG00 ARG01 ... -OPT1 ARG10 ARG11 ...
            string key = "";
            List<string> values = new List<string>();
            foreach(string arg in args)
            {
                if (arg[0] == '-')
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        Sanity.Requires(!ArgDict.ContainsKey(key), $"Duplicate in options: -{key}.");
                        string value = string.Join(" ", values);                        
                        ArgDict.Add(key, value);
                        values.Clear();
                    }
                    key = arg.Substring(1);
                }
                else
                {
                    values.Add(arg);
                }
            }
            if (!string.IsNullOrEmpty(key))
            {
                Sanity.Requires(!ArgDict.ContainsKey(key), $"Duplicate in options: -{key}.");
                string value = string.Join(" ", values);
                ArgDict.Add(key, value);
            }
        }
    }
}
