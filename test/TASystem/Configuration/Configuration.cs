using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TASystem.Tests
{
    public static class Configuration
    {
        public static TimeSpan TimeOut { get => TimeSpan.FromSeconds(5); }

        public static RemoteWebDriver WebDriver
        {
            get
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("headless");
                return new ChromeDriver(chromeOptions);
            }
        }

        public static string GetApplicationPath()
        {
            string repoDirectory = Directory.GetCurrentDirectory()
                .Split("test", StringSplitOptions.RemoveEmptyEntries)
                .First();

            string binDirectory = Path.Combine(repoDirectory, @"src\TestAutomation\bin");
            return Directory.GetFiles(binDirectory, "TestAutomation.exe", SearchOption.AllDirectories).FirstOrDefault();
        }

        /// <summary>
        /// This page must be available in all environments requiring test.
        /// </summary>
        /// <returns></returns>
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
    }
}
