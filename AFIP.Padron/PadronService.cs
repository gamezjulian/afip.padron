using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AFIP.Padron
{
    public class PadronService
    {
        public List<string> ReadPadron()
        {
            var originalFileName = ConfigurationService.GetConfigValue(Constants.OriginalFileName);
            var filePath = Directory.GetCurrentDirectory().TrimEnd('\\') + "\\" + originalFileName;

            var lines = new List<string>();

            Logger.WriteInfo("Reading file: " + filePath);

            if (!string.IsNullOrEmpty(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        lines.Add(line);
                    }
                }
            }
            else
            {
                Logger.WriteError("File path is null");
            }

            Logger.WriteInfo("Reading file finished.");

            return lines;
        }

        public List<string> ProcessPadron(List<string> lines)
        {
            Logger.WriteInfo("Starting processing the padron");

            var result = new List<string>();
            var regexValue = ConfigurationService.GetConfigValue(Constants.Expression);
            var replacedFor = ConfigurationService.GetConfigValue(Constants.ReplaceFor);
            var replacedChars = new List<string>();

            var regex = new Regex(regexValue);

            lines.ForEach(l =>
            {
                var matches = regex.Matches(l);
                var replacedValue = string.Empty;
                if (matches.Count > 0)
                {
                    foreach (var m in matches.Cast<Match>())
                    {
                        if (m.Success)
                        {
                            replacedChars.Add(m.Value);

                            replacedValue = l.Replace(m.Value, replacedFor);
                            l = replacedValue;
                        }

                    }

                    result.Add(replacedValue);
                }
                else
                {
                    result.Add(l);
                }
            });

            Logger.WriteInfo("Replaced values: " + String.Join(" ", replacedChars.Distinct().ToArray()));

            Logger.WriteInfo("Ending processing the padron");

            return result;
        }

        public bool CreateResultsFile(List<string> lines)
        {
            var path = Directory.GetCurrentDirectory().TrimEnd('\\') + "\\" + ConfigurationService.GetConfigValue(Constants.TargetFileName);
            var ok = true;

            Logger.WriteInfo("Creating target file: " + path);

            try
            {
                System.IO.File.WriteAllLines(path, lines.ToArray());
            }
            catch (Exception ex)
            {
                Logger.WriteError("Failed while creating the file: " + ex.ToString());
                ok = false;

                throw;
            }

            return ok;
        }
    }
}
