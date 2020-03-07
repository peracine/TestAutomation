﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Models;

namespace TestAutomation.Interfaces
{
    public interface ITextRepository
    {
        Task<Article> GetArticletAsync(int id);
        Task<IEnumerable<Article>> ListArticlesAsync(string text = null);
        Task<Article> AddArticleAsync(Article article);
    }
}