using Newtonsoft.Json;
using PactNet;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace TAContract.Tests
{
    public class TestContractClassFixture : IDisposable
    {
        private readonly HttpClient _client;
        private readonly Configuration _configuration;
        private readonly Process _applicationProcess;
        private bool _isDisposed;

        public TestContractClassFixture()
        {
            _configuration = new Configuration();
            _applicationProcess = new Process();
            _applicationProcess.StartInfo.FileName = _configuration.ApplicationPath;
            _applicationProcess.Start();
            _client = new HttpClient();
        }

        public IPactBuilder GetPactBuilder(HttpVerb httpVerb, string path)
        {
            string currentProject = Directory.GetCurrentDirectory()
                .Split("bin", StringSplitOptions.RemoveEmptyEntries)
                .First();

            var pactConfig = new PactConfig()
            {
                SpecificationVersion = "2.0.0",
                PactDir = Path.Combine(currentProject, _configuration.PactDir),
                LogDir = Path.Combine(currentProject, _configuration.LogDir)
            };

            var pactBuilder = new PactBuilder(pactConfig);
            pactBuilder.ServiceConsumer(_configuration.ConsumerName).HasPactWith(GetProviderName(httpVerb, path));
            return pactBuilder;
        }

        private string GetProviderName(HttpVerb verb, string path) =>
            $"{path.Replace("/", string.Empty)}_{verb}";

        public async Task<HttpResponseMessage> GetAsync(string endPoint) =>
            await _client.GetAsync(_configuration.MockProviderServerBaseUrl + endPoint);

        public async Task<HttpResponseMessage> PostAsync(string endPoint, object content)
        {
            var httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, MediaTypeNames.Application.Json);
            return await _client.PostAsync(_configuration.MockProviderServerBaseUrl + endPoint, httpContent);
        }

        public void PactVerifier(HttpVerb verb, string path)
        {
            new PactVerifier(new PactVerifierConfig())
                .ServiceProvider(path, _configuration.ApplicationUrl)
                .HonoursPactWith(_configuration.ConsumerName)
                .PactUri(Path.Combine(_configuration.PactDir, $"{_configuration.ConsumerName}-{GetProviderName(verb, path)}.json"))
                .Verify();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_isDisposed)
            {
                if (_client != null)
                    _client.Dispose();

                if (_applicationProcess != null)
                    _applicationProcess.Kill();

                _isDisposed = true;
            }
        }
    }
}
