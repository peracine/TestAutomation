using System.Diagnostics;
using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TAIntegration.Tests.TestDoubles
{
    class LogFake : ILog
    {
        public async Task WriteAsync(string text) =>
            await Task.Run(() => Debug.WriteLine($"LogFake.WriteAsync('{text}') called."));
    }
}