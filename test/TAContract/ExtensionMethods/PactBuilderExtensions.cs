using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace TAContract.Tests
{
    public static class PactBuilderExtensions
    {
        public static IMockProviderService GetMockProviderService(this IPactBuilder pactBuilder)
        {
            var configuration = new Configuration();
            var mockService = pactBuilder.MockService(
                    configuration.MockProviderServerPort,
                    new JsonSerializerSettings() { 
                        ContractResolver = new CamelCasePropertyNamesContractResolver() });

            mockService.ClearInteractions();
            return mockService;
        }
    }
}
