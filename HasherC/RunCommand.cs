using System.Diagnostics;
using System.Windows.Input;

namespace HasherC
{
    public class RunCommand : IRunCommand
    {
        public static string GetHashes(string hasherFileName, string arguments)
        {
            var process = new Process
            {
                StartInfo =
                {
                    FileName = hasherFileName,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };
            
            process.ErrorDataReceived += OutputHandler;
            process.Start();
            process.BeginErrorReadLine();

            var output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            process.Close();

            return output;
        }

        private static void OutputHandler(object caller, DataReceivedEventArgs errors)
        {
            //Console.WriteLine(errors.Data);
            //logger.Log(LogLevel.Info, $"Errors encountered: {errors.Data}");
        }

    }

}
