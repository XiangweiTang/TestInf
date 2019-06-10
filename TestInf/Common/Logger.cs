using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestInf
{
    public static class Logger
    {
        public static string Path { get; set; } = "Log.txt";
        public static void WriteLine(string content, bool writeToFile = true)
        {
            string line = $"{DateTime.Now.ToStringLog()}\t{content}";
            Console.WriteLine(line);
            if (writeToFile)
                File.AppendAllText(Path, line);
        }
    }
}
