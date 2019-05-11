using System;
using System.Diagnostics;

namespace HasherC
{
    public class RunCommand
    {
        private static Process _process;

        public RunCommand()
        {
            var hasherFilename = @"d:\CRC\cpverify.exe";
            var process = new Process
            {
                StartInfo =
                {
                    FileName = hasherFilename,
                    Arguments = "-mk " + @"d:\CRC\dtcl.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.ErrorDataReceived += OutputHandler;
            process.Start();
            process.BeginErrorReadLine();
            var output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            //logger.Log(LogLevel.Info, $"Hash created: {output}");
            process.WaitForExit();
        }

        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //logger.Log(LogLevel.Info, $"Hash created: {outLine.Data}");
        }

    }
    
}
