using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TAIntegration.Tests.TestDoubles
{
    class LogStub : ILog
    {
        public async Task WriteAsync(string text) =>
            await Task.CompletedTask;
    }
}