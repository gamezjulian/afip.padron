using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AFIP.Padron
{
    public static class Logger
    {
        public enum Level
        {
            WARNING,
            INFO,
            ERROR
        }

        public static void WriteLog(string line, Level level)
        {
            var path = Directory.GetCurrentDirectory().TrimEnd('\\') + "\\" + ConfigurationService.GetConfigValue(Constants.LogFileName);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, true))
            {
                file.WriteLine(level.ToString() + ": " + line);
            }
        }

        public static void WriteError(string line)
        {
            WriteLog(line, Level.ERROR);
        }

        public static void WriteInfo(string line)
        {
            WriteLog(line, Level.INFO);
        }

        public static void WriteWarning(string line)
        {
            WriteLog(line, Level.WARNING);
        }
    }
}
