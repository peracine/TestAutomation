using Moq;
using System.Threading.Tasks;
using TestAutomation.Interfaces;
using TestAutomation.Models;
using TestAutomation.Services;
using Xunit;

namespace TAUnit.Tests
{
    public class ArticlesServicesTests
    {
        private readonly ArticleServices _articleServices;
        private bool LogCalled { get; set; }
        private const string _testText = "Test text.";

        public ArticlesServicesTests()
        {
            var articleRepository = new Mock<IArticleRepository>(MockBehavior.Strict);
            articleRepository.Setup(repo => repo.AddArticleAsync(It.IsAny<Article>()))
                .ReturnsAsync(new Article());

            var log = new Mock<ILog>();
            log.Setup(l => l.WriteAsync(It.IsAny<string>()))
                .Callback(() => LogCalled = true)
                .Returns(Task.CompletedTask);

            _articleServices = new ArticleServices(articleRepository.Object, log.Object);
        }

        [Fact]
        public void Uppercase_InputLower_ReturnsUpper()
        {
            var result = _articleServices.Uppercase(_testText);

            Assert.Equal(_testText.ToUpper(), result);
        }

        [Fact]
        public async void AddArticle_SetText_ReturnsArticle()
        {
            var result = await _articleServices.AddArticleAsync(new Article() { Text = _testText });

            Assert.IsType<Article>(result);
        }

        [Fact]
        public async void AddArticle_CheckIfLogIsCalled_ReturnsVoid()
        {
            LogCalled = false;

            var result = await _articleServices.AddArticleAsync(new Article() { Text = _testText });

            Assert.True(LogCalled);
        }

        [Fact]
        public async void ListArticles_NoMockExpectationWithStrictBehavior_ReturnsMockException()
        {
            await Assert.ThrowsAsync<MockException>(() => _articleServices.ListArticlesAsync());
        }
    }
}
