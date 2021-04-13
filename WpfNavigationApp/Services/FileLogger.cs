using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfDependencyInjectionApp.Services
{
    class FileLogger : ILogger
    {
        private const string LofFileName = "log.txt";
        public void LogError(string message, Exception exepction)
        {
            string log = $"[{DateTime.Now.ToString()}]:  {message}   Exeption: {exepction.Message}\n";
            File.AppendAllText(LofFileName, log);
        }

        public void LogInfo(string message)
        {
            string log = $"[{DateTime.Now.ToString()}]:  {message} ";
            File.AppendAllText(LofFileName, log);
        }
    }
}
