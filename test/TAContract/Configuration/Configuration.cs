using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace TAContract.Tests
{
    public class Configuration
    {
        public string ConsumerName { get; private set; }
        public string LogDir { get; private set; }
        public string PactDir { get; private set; }
        public int MockProviderServerPort { get; private set; }
        public string MockProviderServerBaseUrl { get; private set; }
        public string ApplicationPath { get; private set; }
        public string ApplicationUrl { get; private set; }

        public Configuration()
        {
            string projectDirectory = Directory.GetCurrentDirectory()
                .Split("bin", StringSplitOptions.RemoveEmptyEntries)
                .First();

            var configuration = JObject.Parse(File.ReadAllText(Path.Combine(projectDirectory, @"Configuration\configuration.json")));
            ConsumerName = configuration.SelectToken("consumerName").Value<string>();
            LogDir = Path.Combine(projectDirectory, configuration.SelectToken("logDir").Value<string>());
            PactDir = Path.Combine(projectDirectory, configuration.SelectToken("pactDir").Value<string>());
            MockProviderServerPort = configuration.SelectToken("mockProviderServerPort").Value<int>();
            MockProviderServerBaseUrl = configuration.SelectToken("mockProviderServerBaseUrl").Value<string>();
            ApplicationPath = GetApplicationPath(configuration.SelectToken("applicationPath").Value<string>());
            ApplicationUrl = configuration.SelectToken("applicationUrl").Value<string>();
        }

        private string GetApplicationPath(string configPath)
        {
            if (!string.IsNullOrEmpty(configPath))
                return configPath;

            string repoDirectory = Directory.GetCurrentDirectory()
                .Split("test", StringSplitOptions.RemoveEmptyEntries)
                .First();

            string binDirectory = Path.Combine(repoDirectory, @"src\TestAutomation\bin");
            return Directory.GetFiles(binDirectory, "TestAutomation.exe", SearchOption.AllDirectories).FirstOrDefault();
        }
    }
}
