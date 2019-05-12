using System.Diagnostics;

namespace HasherC
{
    public static class CpvCommand
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
                    //StandardOutputEncoding = Encoding.GetEncoding(1251)
                }
            };
            
            process.ErrorDataReceived += OutputHandler;
            process.Start();
            process.BeginErrorReadLine();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();

            return output.Trim('\n');
        }

        private static void OutputHandler(object caller, DataReceivedEventArgs errors)
        {
            //Console.WriteLine(errors.Data);
            //logger.Log(LogLevel.Info, $"Errors encountered: {errors.Data}");
        }

    }

}
