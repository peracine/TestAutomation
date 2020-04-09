using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TAIntegration.Tests.TestDoubles;
using TestAutomation.Controllers;
using TestAutomation.Data;
using TestAutomation.Models;
using TestAutomation.Services;
using Xunit;

namespace TAIntegration.Tests
{
    public class ArticlesControllerServerlessTests
    {
        private readonly string _sqliteConnexionString;
        private const string _text = "Test text.";

        public ArticlesControllerServerlessTests()
        {
            var sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            _sqliteConnexionString = sqliteConnectionStringBuilder.ToString();
        }

        [Fact]
        public async Task Add_NewArticleCheckResultWithFake_ReturnsArticle()
        {
            using var connection = new SqliteConnection(_sqliteConnexionString);
            connection.Open();
            using var context = GetContext(connection);
            var articleController = new ArticlesController(new ArticleServices(new ArticleRepository(context), new LogFake()));

            var actionResultArticle = await articleController.Add(new Article() { Text = _text });

            Assert.IsType<CreatedAtActionResult>(actionResultArticle.Result);
            Assert.IsType<Article>((actionResultArticle.Result as CreatedAtActionResult).Value);
        }

        [Fact]
        public async Task Add_NewArticleCheckResultWithMock_ReturnsArticle()
        {
            using var connection = new SqliteConnection(_sqliteConnexionString);
            connection.Open();
            using var context = GetContext(connection);
            var articleController = new ArticlesController(new ArticleServices(new ArticleRepository(context), LogMockFactory.GetLogMock()));

            var actionResultArticle = await articleController.Add(new Article() { Text = _text });

            Assert.IsType<CreatedAtActionResult>(actionResultArticle.Result);
            Assert.IsType<Article>((actionResultArticle.Result as CreatedAtActionResult).Value);
        }

        [Fact]
        public async Task Add_NewArticleCheckLog_ReturnsTrue()
        {
            using var connection = new SqliteConnection(_sqliteConnexionString);
            connection.Open();
            using var context = GetContext(connection);
            var logSpy = new LogSpy(false);
            var articleController = new ArticlesController(new ArticleServices(new ArticleRepository(context), logSpy));

            var actionResultArticle = await articleController.Add(new Article() { Text = _text });

            Assert.True(logSpy.LogCalled);
        }

        private TestAutomationContext GetContext(SqliteConnection sqliteConnection)
        {
            var options = new DbContextOptionsBuilder<TestAutomationContext>()
                    .UseSqlite(sqliteConnection)
                    .Options;
            return new TestAutomationContext(options);
        }
    }
}
