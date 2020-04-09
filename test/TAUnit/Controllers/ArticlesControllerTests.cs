using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Controllers;
using TestAutomation.Interfaces;
using TestAutomation.Models;
using Xunit;

namespace TAUnit.Tests
{
    public class ArticlesControllerTests
    {
        [Fact]
        public async Task List_TestByUsingMoq_ReturnsArticles()
        {
            //No need to mock other dependencies (IArticleRepository and ILog)
            var articleServices = new Mock<IArticleServices>();
            articleServices.Setup(repo => repo.ListArticlesAsync("Test")).ReturnsAsync(new List<Article>());
            var articleController = new ArticlesController(articleServices.Object);

            var result = await articleController.List();

            Assert.IsAssignableFrom<IEnumerable<Article>>(result);
        }
    }
}
