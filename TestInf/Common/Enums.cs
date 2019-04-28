using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf
{
    class Enums
    {
    }

    [Flags]
    public enum TaskMode
    {
        NA = 0,
        Test = 1,
        Config = 2,
        Argument = 4,
    }
}
