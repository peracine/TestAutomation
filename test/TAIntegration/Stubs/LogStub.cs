using System.Diagnostics;
using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TAIntegration.Tests.Stubs
{
    class LogStub : ILog
    {
        public async Task WriteAsync(string text) =>
            await Task.Run(() => Debug.WriteLine($"LogStub.WriteAsync('{text}') called."));
    }
}