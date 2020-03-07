using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TAIntegration.Tests.Stubs;
using TestAutomation;
using TestAutomation.Interfaces;

namespace TAIntegration.Tests.Configuration
{
    public class TestAutomationFactory<TStartup> : WebApplicationFactory<Startup> where TStartup : class
    {
        /// <summary>
        /// Customize the Startup class for the test
        /// </summary>
        /// <param name="builder"></param>
        /// <see cref="https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests"/>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var serviceLog = new ServiceDescriptor(typeof(ILog), typeof(LogStub), ServiceLifetime.Transient);
                services.Replace(serviceLog);
            });
        }
    }
}
