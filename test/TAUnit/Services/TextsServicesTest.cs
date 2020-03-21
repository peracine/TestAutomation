using Moq;
using System.Threading.Tasks;
using TestAutomation.Interfaces;
using TestAutomation.Models;
using TestAutomation.Services;
using Xunit;

namespace TAUnit.Tests
{
    public class TextsServicesTest
    {
        private readonly TextsServices _textServices;
        private bool LogCalled { get; set; }
        private const string _testText = "Test text.";

        public TextsServicesTest()
        {
            var textRepository = new Mock<ITextRepository>(MockBehavior.Strict);
            textRepository.Setup(repo => repo.AddArticleAsync(It.IsAny<Article>()))
                .ReturnsAsync(new Article());

            var log = new Mock<ILog>();
            log.Setup(l => l.WriteAsync(It.IsAny<string>()))
                .Callback(() => LogCalled = true)
                .Returns(Task.CompletedTask);

            _textServices = new TextsServices(textRepository.Object, log.Object);
        }

        [Fact]
        public void Uppercase_InputLower_ReturnsUpper()
        {
            var result = _textServices.Uppercase(_testText);

            Assert.Equal(_testText.ToUpper(), result);
        }

        [Fact]
        public async void GetArticle_SetText_ReturnsArticle()
        {
            var result = await _textServices.AddArticleAsync(new Article() { Text = _testText });

            Assert.IsType<Article>(result);
        }

        [Fact]
        public async void GetArticle_CheckIfLogIsCalled_ReturnsVoid()
        {
            LogCalled = false;

            var result = await _textServices.AddArticleAsync(new Article() { Text = _testText });

            Assert.True(LogCalled);
        }

        [Fact]
        public async void ListArticles_NoMockExpectationWithStrictBehavior_ReturnsMockException()
        {
            await Assert.ThrowsAsync<MockException>(() => _textServices.ListArticlesAsync());
        }
    }
}
