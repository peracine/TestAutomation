using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Controllers;
using TestAutomation.Interfaces;
using TestAutomation.Models;
using Xunit;

namespace TAUnit.Tests
{
    public class TextsControllerTest
    {
        [Fact]
        public async Task List_TestByUsingMoq_ReturnsArticles()
        {
            //No need to mock other dependencies (ITextRepository and ILog)
            var textServices = new Mock<ITextServices>();
            textServices.Setup(repo => repo.ListArticlesAsync("Test")).ReturnsAsync(new List<Article>());
            var textController = new TextsController(textServices.Object);

            var result = await textController.List();

            Assert.IsAssignableFrom<IEnumerable<Article>>(result);
        }
    }
}
