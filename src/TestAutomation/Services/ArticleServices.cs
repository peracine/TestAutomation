using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Interfaces;
using TestAutomation.Models;

namespace TestAutomation.Services
{
    public class ArticleServices : IArticleServices
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILog _log;

        public ArticleServices(IArticleRepository articleRepository, ILog log)
        {
            _articleRepository = articleRepository;
            _log = log;
        }

        public string Uppercase(string text) =>
            text?.Trim().ToUpper() ?? string.Empty;

        public async Task<Article> GetArticleAsync(int id) =>
            await _articleRepository.GetArticleAsync(id);

        public async Task<IEnumerable<Article>> ListArticlesAsync(string text = null) =>
            await _articleRepository.ListArticlesAsync(text);

        public async Task<Article> AddArticleAsync(Article article)
        {
            await _log.WriteAsync(article.Text);
            return await _articleRepository.AddArticleAsync(article);
        }
    }
}


