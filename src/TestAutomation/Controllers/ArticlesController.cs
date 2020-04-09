using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAutomation.Interfaces;
using TestAutomation.Models;

namespace TestAutomation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleServices _articleServices;

        public ArticlesController(IArticleServices articleServices)
        {
            _articleServices = articleServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> Get(int id)
        {
            var article = await _articleServices.GetArticletAsync(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }  

        [HttpGet]
        public async Task<IEnumerable<Article>> List(string q = null) =>
            await _articleServices.ListArticlesAsync(q);

        [HttpPost]
        public async Task<ActionResult<Article>> Add([FromBody] Article article)
        {
            var newArticle = await _articleServices.AddArticleAsync(article);
            if (newArticle == null)
                return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = newArticle.Id }, newArticle);
        }
    }
}
