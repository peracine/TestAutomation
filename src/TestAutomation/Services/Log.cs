using System;
using System.IO;
using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TestAutomation.Services
{
    public class Log : ILog
    {
        private string LogFile =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{DateTime.Today.ToString("yyyy-MM-dd")}.log");

        public async Task WriteAsync(string text)
        {
            using (var streamWriter = new StreamWriter(LogFile, true))
            {
                await streamWriter.WriteLineAsync($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {text}");
            }
        }
    }
}
