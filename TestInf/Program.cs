using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf
{
    class Program
    {
        static TestInfArgs TIArg = new TestInfArgs();
        static void Main(string[] args)
        {
            TIArg = new TestInfArgs(args);
        }


    }
}
