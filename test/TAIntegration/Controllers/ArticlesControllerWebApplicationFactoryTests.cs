﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestAutomation;
using TestAutomation.Data;
using TestAutomation.Models;
using Xunit;

namespace TAIntegration.Tests
{
    public class ArticlesControllerWebApplicationFactoryTests : IClassFixture<TestAutomationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ArticlesControllerWebApplicationFactoryTests(TestAutomationFactory<Startup> testAutomationFactory)
        {
            _client = testAutomationFactory.CreateClient();
        }

        [Fact]
        public async Task List_SearchArticlesByQuery_ReturnsArticles()
        {
            var allWords = string.Join(" ", DataSeed.ListTexts()).Split(" ", StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
            string text = allWords[new Random().Next(0, allWords.Length - 1)];

            var result = await _client.GetAsync($"Articles?q={text}");
            var content = await result.Content.ReadAsStringAsync();
            var articles = JsonSerializer.Deserialize<IEnumerable<Article>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            Assert.True(result.StatusCode == HttpStatusCode.OK);
            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task Add_NewArticle_ReturnsArticle()
        {            
            string text = "Test text.";
            var httpContent = new StringContent(JsonSerializer.Serialize(new Article() { Text = text }), Encoding.UTF8, MediaTypeNames.Application.Json);

            var result = await _client.PostAsync("Articles", httpContent);
            var content = await result.Content.ReadAsStringAsync();
            var article = JsonSerializer.Deserialize<Article>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            Assert.True(result.StatusCode == HttpStatusCode.Created);
            Assert.Equal(text, article.Text);
        }
    }
}
