using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TestAutomation.Data;
using TestAutomation.Models;
using Xunit;

namespace TAContract.Tests
{
    [TestCaseOrderer("TAContract.Tests.AlphabeticalOrderer", "TAContract.Tests"), Collection("ContractTest")]
    public class Articles_GET_Tests : IClassFixture<TestContractClassFixture>, IDisposable, IContractTest
    {
        private readonly TestContractClassFixture _consumerPactClassFixture;
        private const HttpVerb _httpVerb = HttpVerb.Get;
        private readonly string _path = $"/Articles/{DataSeed.GetFirstArticle().Id}";
        private readonly IPactBuilder _pactBuilder;
        private readonly IMockProviderService _mockProviderService;
        private bool _isDisposed;

        public Articles_GET_Tests(TestContractClassFixture consumerPactClassFixture)
        {
            _consumerPactClassFixture = consumerPactClassFixture;
            _pactBuilder = _consumerPactClassFixture.GetPactBuilder(_httpVerb, _path);
            _mockProviderService = _pactBuilder.GetMockProviderService();
        }

        [Fact]
        public async Task Consumer()
        {
            var firstArticle = DataSeed.GetFirstArticle();
            _mockProviderService
                .Given("Existing article id")
                .UponReceiving("Valid GET Article")
                .With(new ProviderServiceRequest
                {
                   Method = HttpVerb.Get,
                   Path = _path,
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = (int)HttpStatusCode.OK,
                    Headers = new Dictionary<string, object> { { "Content-Type", "application/json; charset=utf-8" } },
                    Body = firstArticle
                });
            
            var result = await _consumerPactClassFixture.GetAsync(_path);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            JsonSerializer.Deserialize<Article>(await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public void Provider() =>
            _consumerPactClassFixture.PactVerifier(_httpVerb, _path);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_isDisposed)
            { 
                _pactBuilder.Build();
                _isDisposed = true;
            }
        }
    }
}
