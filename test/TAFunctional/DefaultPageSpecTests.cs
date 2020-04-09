using Machine.Specifications;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TAFunctional.Tests
{
    public class DefaultPageSpecTests
    {
        [Subject("Search")]
        public class When_searching
        {
            private static IWebDriver subject;
            private static bool has_result;
            private const string q = "Lorem";
            private static int _processId;

            Establish context = () =>
            {
                _processId = Configuration.StartServices();

                subject = new ChromeDriver(Configuration.DriverOptions as ChromeOptions);
                subject.Navigate().GoToUrl(Configuration.GetPageUrl());
                subject.FindElement(By.Id("q")).SendKeys(q);
                subject.FindElement(By.Id("btnSearch")).Click();
            };

            Because of = () =>
                has_result = new WebDriverWait(subject, Configuration.TimeOut)
                    .Until(wd => wd.FindElement(By.Id("table")).Text.Contains("Lorem"));

            It should_have_elements = () =>
                has_result.ShouldBeTrue();

            Cleanup after = () =>
            {
                subject.Quit();
                Configuration.StopServices(_processId);
            };
        }
    }
}
