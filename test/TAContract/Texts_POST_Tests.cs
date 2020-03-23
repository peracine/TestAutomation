using PactNet;
using PactNet.Matchers;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TestAutomation.Models;
using Xunit;

namespace TAContract.Tests
{
    [TestCaseOrderer("TAContract.Tests.AlphabeticalOrderer", "TAContract.Tests")]
    public class Texts_POST_Tests : IClassFixture<TestContractClassFixture>, IDisposable, IContractTest
    {
        private readonly TestContractClassFixture _consumerPactClassFixture;
        private const HttpVerb _httpVerb = HttpVerb.Post;
        private const string _path = "/Texts";
        private readonly IPactBuilder _pactBuilder;
        private readonly IMockProviderService _mockProviderService;
        private bool _pactFileGenerated;

        public Texts_POST_Tests(TestContractClassFixture consumerPactClassFixture)
        {
            _consumerPactClassFixture = consumerPactClassFixture;
            _pactBuilder = _consumerPactClassFixture.GetPactBuilder(_httpVerb, _path);
            _mockProviderService = _pactBuilder.GetMockProviderService();
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
                    Body = new { id = Match.Type(1), creationDate = Match.Type(DateTime.Now), article.text }
                });

            var result = await _consumerPactClassFixture.PostAsync(_path, article);

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            JsonSerializer.Deserialize<Article>(await result.Content.ReadAsStringAsync());
        }

        [Fact]
        public void Provider() =>
            _consumerPactClassFixture.PactVerifier(_httpVerb, _path);

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
