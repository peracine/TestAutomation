using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAutomation.Interfaces;
using TestAutomation.Models;

namespace TestAutomation.Data
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly TestAutomationContext _context;

        public ArticleRepository(TestAutomationContext context)
        {
            _context = context ?? throw new InvalidOperationException("context cannot be null.");
        }

        public async Task<Article> GetArticletAsync(int id) =>
            await _context.Articles.FindAsync(id).ConfigureAwait(false);

        public async Task<IEnumerable<Article>> ListArticlesAsync(string text = null) =>
            string.IsNullOrEmpty(text) ?
                await _context.Articles.AsNoTracking().ToListAsync().ConfigureAwait(false) :
                await _context.Articles.AsNoTracking().Where(a => a.Text.Contains(text)).ToListAsync().ConfigureAwait(false);

        public async Task<Article> AddArticleAsync(Article article)
        {
            if (string.IsNullOrEmpty(article.Text))
                return null;

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article;
        }
    }
}
