using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TAFunctional.Tests
{
    public static class Configuration
    {
        public static TimeSpan TimeOut { get => TimeSpan.FromSeconds(5); }
        public static DriverOptions DriverOptions
        {
            get =>
                new ChromeOptions
                {
                    BinaryLocation = GetWebDriverApplication()
                };
        }

        public static string GetApplicationPath()
        {
            string repoDirectory = Directory.GetCurrentDirectory()
                .Split("test", StringSplitOptions.RemoveEmptyEntries)
                .First();

            string binDirectory = Path.Combine(repoDirectory, @"src\TestAutomation\bin");
            return Directory.GetFiles(binDirectory, "TestAutomation.exe", SearchOption.AllDirectories).FirstOrDefault();
        }

        public static string GetPageUrl() =>
            Path.Combine(Directory.GetCurrentDirectory().Split("test").First(), @"src\TestAutomation\Views\default.html");

        public static int StartServices()
        {
            var applicationProcess = new Process();
            applicationProcess.StartInfo.FileName = GetApplicationPath();
            applicationProcess.Start();
            return applicationProcess.Id;
        }

        public static void StopServices(int processId)
        {
            var applicationProcess = Process.GetProcessById(processId);
            try { applicationProcess.Kill(true); }
            catch { }
        }

        private static string GetWebDriverApplication()
        {
            string projectDirectory = Directory.GetCurrentDirectory()
                .Split("bin", StringSplitOptions.RemoveEmptyEntries)
                .First();
    
            return JObject
                .Parse(File.ReadAllText(Path.Combine(projectDirectory, @"Configuration\configuration.json")))
                .SelectToken("webDriverApplication").Value<string>();
        }
    }
}
