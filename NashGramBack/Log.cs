using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NashGramBack
{
    public static class Log
    {
        public static string LogText { get; private set; }
        public static void AddLog(string text, bool mode)
        {
            string info = "INFO: ";
            if (mode) info = "ERROR: ";
            LogText += info + text + Environment.NewLine;
        }
        public static void SaveLogFile()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("LogFile.txt", false, Encoding.Default))
            {
                sw.Write(LogText);
            }
        }
    }
}
