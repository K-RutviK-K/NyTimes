using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NyTimes.Application.Services;
using NyTimes.Domain.Models;
using NyTimes.Domain.ViewModels;
namespace NyTimes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController : ControllerBase
{
    private readonly ArticleService _articleService;

    private readonly HttpClient _httpClient;


    public ArticleController(ArticleService articleService, HttpClient httpClient)
    {
        _articleService = articleService;
        _httpClient = httpClient;
    }


    [HttpPost("PostByKey")]
    public async Task<IActionResult> AddArticleByKey([FromBody] KeyViewModel keyViewModel)
    {
        if (string.IsNullOrEmpty(keyViewModel.Key))
        {
            return BadRequest("API key is required.");
        }

        try
        {
            var content = await _articleService.FetchArticlesByKeyAsync(keyViewModel.Key);
            return Content(content, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetArticles()
    {
        return Ok(await _articleService.GetArticlesAsync());
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetArticle(int id)
    //{
    //    return Ok(await _articleService.GetArticleByIdAsync(id));
    //}

    //[HttpPost]
    //public async Task<IActionResult> AddArticle([FromBody] Articles article)
    //{
    //    await _articleService.AddArticleAsync(article);
    //    return Ok();
    //}


    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateArticle(int id, [FromBody] Articles article)
    //{
    //    if (id != article.Id) return BadRequest();
    //    await _articleService.UpdateArticleAsync(article);
    //    return Ok();
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteArticle(int id)
    //{
    //    await _articleService.DeleteArticleAsync(id);
    //    return Ok();
    //}
}
