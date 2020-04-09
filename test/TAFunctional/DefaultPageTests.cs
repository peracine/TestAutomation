using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit;

namespace TAFunctional.Tests
{
    public class DefaultPageTests : IDisposable
    {
        private int _processId;

        public DefaultPageTests()
        {
            _processId = Configuration.StartServices();
        }

        [Fact]
        public void DefaultPage_Search_returnsArticles()
        {
            string q = "Lorem";
            using var driver = new ChromeDriver(Configuration.DriverOptions as ChromeOptions);
            driver.Navigate().GoToUrl(Configuration.GetPageUrl());
            driver.FindElement(By.Id("q")).SendKeys(q);
            driver.FindElement(By.Id("btnSearch")).Click();

            var result = new WebDriverWait(driver, Configuration.TimeOut)
                .Until(wd => wd.FindElement(By.Id("table")).Text.Contains(q));

            Assert.True(result);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Configuration.StopServices(_processId);
        }
    }
}
