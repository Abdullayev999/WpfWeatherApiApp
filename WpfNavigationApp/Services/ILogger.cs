using System;
using System.Collections.Generic;
using System.Text;

namespace WpfDependencyInjectionApp.Services
{
    interface ILogger
    {
        void LogError(string message, Exception exeption);
        void LogInfo(string message);
    }
}
