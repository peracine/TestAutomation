using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TestAutomation.Interfaces;
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
            for (int i = 0; i < 1000; i++)
            {
                articles.Add(new Article() { Id = i + 1, Text = DataMock.GetRandomText() });
            }

            modelBuilder.Entity<Article>().HasData(articles);
        }
    }
}
