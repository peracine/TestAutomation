using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Models;

namespace TestAutomation.Interfaces
{
    public interface IArticleServices
    {
        string Uppercase(string text);
        Task<Article> GetArticleAsync(int id);
        Task<IEnumerable<Article>> ListArticlesAsync(string text = null);
        Task<Article> AddArticleAsync(Article article);
    }
}