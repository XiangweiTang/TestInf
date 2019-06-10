using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf
{
    public class TestInfArgs
    {
        public TaskMode Mode { get; private set; } = TaskMode.NA;
        public string ConfigPath { get; set; } = "";
        public Dictionary<string, string[]> ArgDict { get; private set; } = new Dictionary<string, string[]>();
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
            // FreeArg0 FreeArg1 ... -OPT0 Arg00 Arg01 ... -OPT1 Arg10 Arg11 ...
            /*
             * Key: ""  Value: {FreeArg0, FreeArg1, ...}
             * Key: "opt0"  Value: {Arg00, Arg01, ...}
             * Key: "opt1"  Value: {Arg10, Arg11, ...}
             */

            string key = "";            
            List<string> currentArgList = new List<string>();
            foreach(string arg in args)
            {
                if (arg[0] == '-')
                {
                    ArgDict.Add(key, currentArgList.ToArray());
                    currentArgList.Clear();
                    key = arg.ToLower();
                    Sanity.Requires(!ArgDict.ContainsKey(key), $"Duplication option {arg[0]}.");
                    continue;
                }
                currentArgList.Add(arg);
            }
            ArgDict.Add(key, currentArgList.ToArray());
        }
    }
}
