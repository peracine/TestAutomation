using PactNet;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TestAutomation.Data;
using Xunit;

namespace TAContract.Tests
{
    [TestCaseOrderer("TAContract.Tests.AlphabeticalOrderer", "TAContract.Tests")]
    public class Texts_GET : IClassFixture<TestContractClassFixture>, IDisposable
    {
        private TestContractClassFixture _consumerPactClassFixture;
        private const HttpVerb _httpVerb = HttpVerb.Get;
        private string _path = $"/Texts/{DataSeed.GetFirstArticle().Id}";
        private IPactBuilder _pactBuilder;
        private IMockProviderService _mockProviderService;
        private bool _pactFileGenerated;

        public Texts_GET(TestContractClassFixture consumerPactClassFixture)
        {
            _consumerPactClassFixture = consumerPactClassFixture;
            _pactBuilder = _consumerPactClassFixture.GetPactBuilder(_httpVerb, _path);
            _mockProviderService = _consumerPactClassFixture.SetMockService(_pactBuilder);
        }

        [Fact]
        public async Task Consumer()
        {
            var firstArticle = DataSeed.GetFirstArticle();
            _mockProviderService
                .Given("Existing article id")
                .UponReceiving("Valid GET Text")
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
           await _consumerPactClassFixture.GetAsync(_path);
        }

        [Fact]
        public void Provider()
        {
            _consumerPactClassFixture.PactVerifier(_httpVerb, _path);
        }

        public void Dispose()
        {
            if (_pactBuilder != null && !_pactFileGenerated)
            {
                _pactBuilder.Build();
                _pactFileGenerated = true;
            }
        }
    }
}
