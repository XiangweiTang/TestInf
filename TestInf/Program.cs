using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf
{
    class Program
    {        
        static Features.DummyConfig MainCfg = new Features.DummyConfig();
        static void Main(string[] args)
        {
            PrimaryLoad(args);
        }

        static void PrimaryLoad(string[] args)
        {
            var arg = new TestInfArgs(args);
            MainCfg.Load(arg);
        }        
    }
}
