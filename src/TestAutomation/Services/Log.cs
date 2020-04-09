using System;
using System.IO;
using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TestAutomation.Services
{
    public class Log : ILog
    {
        private string LogFile =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{DateTime.Today:yyyy-MM-dd}.log");

        public async Task WriteAsync(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("'text' parameter cannot be null or empty.");

            using var streamWriter = new StreamWriter(LogFile, true);
            await streamWriter.WriteLineAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {text}");
        }
    }
}
