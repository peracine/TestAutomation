using PactNet;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace TAContract.Tests
{
    [TestCaseOrderer("TAContract.Tests.AlphabeticalOrderer", "TAContract.Tests")]
    public class Texts_POST : IClassFixture<TestContractClassFixture>, IDisposable
    {
        private TestContractClassFixture _consumerPactClassFixture;
        private const HttpVerb _httpVerb = HttpVerb.Post;
        private const string _path = "/Texts";
        private IPactBuilder _pactBuilder;
        private IMockProviderService _mockProviderService;
        private bool _pactFileGenerated;

        public Texts_POST(TestContractClassFixture consumerPactClassFixture)
        {
            _consumerPactClassFixture = consumerPactClassFixture;
            _pactBuilder = _consumerPactClassFixture.GetPactBuilder(_httpVerb, _path);
            _mockProviderService = _consumerPactClassFixture.SetMockService(_pactBuilder);
        }

        [Fact]
        public async Task Consumer()
        {
            var article = new { text = "Test pact" };
            _mockProviderService
                .Given("New article")
                .UponReceiving("Valid POST Article object")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = _path,
                    Body = article,
                    Headers = new Dictionary<string, object> { { "Content-Type", "application/json; charset=utf-8" } },
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = (int)HttpStatusCode.Created,
                    Headers = new Dictionary<string, object> { { "Content-Type", "application/json; charset=utf-8" } },
                    Body = new { id = Match.Type(1), creationDate = Match.Type(DateTime.Now), text = article.text }
                });

            await _consumerPactClassFixture.PostAsync(_path, article);
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
