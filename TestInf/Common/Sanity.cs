using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestInf
{
    public static class Sanity
    {
        public static void Requires(bool valid, string message)
        {
            if (!valid)
                throw new TestInfEx(message);
        }

        public static void Requires(bool valid)
        {
            if (!valid)
                throw new TestInfEx();
        }
    }

    public class TestInfEx : Exception
    {
        public TestInfEx(string message) : base(message) { }
        public TestInfEx() : base() { }
    }
}
