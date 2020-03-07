using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Interfaces;
using TestAutomation.Models;

namespace TestAutomation.Services
{
    public class TextServices : ITextServices
    {
        private readonly ITextRepository _textRepository;
        private readonly ILog _log;

        public TextServices(ITextRepository textRepository, ILog log)
        {
            _textRepository = textRepository;
            _log = log;
        }

        public string Uppercase(string text) =>
            text?.Trim().ToUpper() ?? string.Empty;

        public async Task<Article> GetArticletAsync(int id) =>
            await _textRepository.GetArticletAsync(id);

        public async Task<IEnumerable<Article>> ListArticlesAsync(string text = null) =>
            await _textRepository.ListArticlesAsync(text);

        public async Task<Article> AddArticleAsync(Article article)
        {
            await _log.WriteAsync(article.Text);
            return await _textRepository.AddArticleAsync(article);
        }
    }
}


