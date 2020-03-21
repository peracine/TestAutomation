using Moq;
using System;
using System.Threading.Tasks;
using TestAutomation.Interfaces;

namespace TAIntegration.Tests.TestDoubles
{
    class LogMockFactory
    {
        public static ILog GetLogMock()
        {
            var logMock = new Mock<ILog>(MockBehavior.Strict);
            logMock.Setup(l => l.WriteAsync(null)).Throws<ArgumentNullException>();
            logMock.Setup(l => l.WriteAsync(It.IsNotNull<string>())).Returns(Task.CompletedTask);
            return logMock.Object;
        }
     }
}