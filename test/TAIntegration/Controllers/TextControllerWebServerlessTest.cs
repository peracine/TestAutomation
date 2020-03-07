using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TAIntegration.Tests.Stubs;
using TestAutomation.Controllers;
using TestAutomation.Data;
using TestAutomation.Models;
using TestAutomation.Services;
using Xunit;

namespace TAIntegration.Tests
{
    public class TextControllerWebServerlessTest
    {
        private readonly string _sqliteConnexionString;
        public TextControllerWebServerlessTest()
        {
            var sqliteConnectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            _sqliteConnexionString = sqliteConnectionStringBuilder.ToString();
        }

        [Fact]
        public async Task Add_NewArticle_ReturnsArticle()
        {
            using (var connection = new SqliteConnection(_sqliteConnexionString))
            {
                connection.Open();
                using (var context = GetContext(connection))
                {
                    var textController = new TextController(new TextServices(new TextRepository(context), new LogStub()));

                    var actionResultArticle = await textController.Add(new Article() { Text = "Test text." });

                    Assert.IsType<CreatedAtActionResult>(actionResultArticle.Result);
                    Assert.IsType<Article>((actionResultArticle.Result as CreatedAtActionResult).Value);
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
