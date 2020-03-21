using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TAIntegration.Tests.TestDoubles
{
    class LogSpy : ILog
    {
        public bool LogCalled { get; set; }

        public LogSpy(bool logCalled)
        {
            LogCalled = logCalled;
        }

        public async Task WriteAsync(string text)
        {
            LogCalled = true;
            await Task.CompletedTask;
        }
    }
}