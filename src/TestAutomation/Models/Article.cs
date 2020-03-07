using System;

namespace TestAutomation.Models
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Text { get; set; }
    }
}
