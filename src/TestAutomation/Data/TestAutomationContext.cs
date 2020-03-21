using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestAutomation.Models;

namespace TestAutomation.Data
{
    public class TestAutomationContext : DbContext
    {
        public TestAutomationContext(DbContextOptions<TestAutomationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed
            var articles = new List<Article>();
            articles.Add(DataSeed.GetFirstArticle());
            for (int i = 2; i <= 1000; i++)
            {
                articles.Add(new Article() { Id = i, Text = DataSeed.GetRandomText() });
            }

            modelBuilder.Entity<Article>().HasData(articles);
        }
    }
}
