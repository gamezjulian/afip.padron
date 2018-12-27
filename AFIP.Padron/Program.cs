using System;

namespace AFIP.Padron
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Logger.WriteInfo("------------ Starting ------------");

                Console.WriteLine("Welcome to the AFIP parser application. If you want to contribute, please ask me for the CBU");
                Console.WriteLine("");
                Console.WriteLine("Press a key to start...");
                Console.ReadKey();

                var padronService = new PadronService();

                var lines = padronService.ReadPadron();
                var processedLines = padronService.ProcessPadron(lines);
                var result = padronService.CreateResultsFile(processedLines);

                Logger.WriteInfo("The result was: " + result);

                Logger.WriteInfo("------------ Finished ------------");

                Console.WriteLine("Processed: " + processedLines.Count + " lines");
                Console.WriteLine("Finished successfully: " + result + ".");
                Console.WriteLine("Press a key to close the windows.");
                Console.ReadKey();
            }
            catch (System.Exception ex)
            {
                Logger.WriteError(ex.ToString());
                Console.Write(ex.ToString());
                Console.ReadKey();
            }
        }
    }
}
