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
    public class TextsControllerServerlessTest
    {
        private readonly string _sqliteConnexionString;
        private const string _text = "Test text.";

        public TextsControllerServerlessTest()
        {
            var sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            _sqliteConnexionString = sqliteConnectionStringBuilder.ToString();
        }

        [Fact]
        public async Task Add_NewArticleCheckResultWithFake_ReturnsArticle()
        {
            using (var connection = new SqliteConnection(_sqliteConnexionString))
            {
                connection.Open();
                using (var context = GetContext(connection))
                {
                    var textController = new TextsController(new TextsServices(new TextRepository(context), new LogFake()));

                    var actionResultArticle = await textController.Add(new Article() { Text = _text });

                    Assert.IsType<CreatedAtActionResult>(actionResultArticle.Result);
                    Assert.IsType<Article>((actionResultArticle.Result as CreatedAtActionResult).Value);
                }
            }
        }

        [Fact]
        public async Task Add_NewArticleCheckResultWithMock_ReturnsArticle()
        {
            using (var connection = new SqliteConnection(_sqliteConnexionString))
            {
                connection.Open();
                using (var context = GetContext(connection))
                {
                    var textController = new TextsController(new TextsServices(new TextRepository(context), LogMockFactory.GetLogMock()));

                    var actionResultArticle = await textController.Add(new Article() { Text = _text });

                    Assert.IsType<CreatedAtActionResult>(actionResultArticle.Result);
                    Assert.IsType<Article>((actionResultArticle.Result as CreatedAtActionResult).Value);
                }
            }
        }

        [Fact]
        public async Task Add_NewArticleCheckLog_ReturnsTrue()
        {
            using (var connection = new SqliteConnection(_sqliteConnexionString))
            {
                connection.Open();
                using (var context = GetContext(connection))
                {
                    var logSpy = new LogSpy(false);
                    var textController = new TextsController(new TextsServices(new TextRepository(context), logSpy));

                    var actionResultArticle = await textController.Add(new Article() { Text = _text });

                    Assert.True(logSpy.LogCalled);
                }
            }
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
