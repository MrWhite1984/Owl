using ConfHub.Core.Application.News.Interfaces;
using ConfHub.Core.Contracts.Requests.News;
using ConfHub.Core.Contracts.Responses.News;
using ConfHub.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConfHub.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("get-part-news")]
        public async Task<ActionResult<NewsWithAuthorResponse>> GetPartNewsWithAuthorResponse([FromQuery] GetPartNewsRequest getPartNewsRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var currentNews = await _newsService.GetPartOfNewsByDateTimeAsync(getPartNewsRequest.StartDate, getPartNewsRequest.PartSize);

                if (currentNews == null)
                    return NotFound();

                if (!currentNews.NewsWithAuthorDtos.Any())
                    return NotFound();

                var newsItems = currentNews.NewsWithAuthorDtos.Select(news => new Contracts.Responses.News.NewsItem(
                    Id: news.Id,
                    Title: news.Title,
                    Content: news.Content,
                    CreatedAt: news.CreatedAt,
                    Author: new Contracts.Responses.News.AuthorInfo
                    (
                        Surname: news.Author.Surname,
                        Name: news.Author.Name,
                        Patronymic: news.Author.Patronymic,
                        EducationalInstitution: news.Author.EducationalInstitution,
                        JobTitle: news.Author.JobTitle,
                        City: news.Author.City,
                        IsVerified: news.Author.IsVerified
                    ))).ToList();

                var response = new NewsWithAuthorResponse(newsItems, currentNews.NextDateTime);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-news")]
        public async Task<ActionResult<CreateNewsResponse>> CreateNews([FromBody] CreateNewsRequest createNewsRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var roleClaim = User.FindFirst(ClaimTypes.Role);
            if (subClaim == null || roleClaim == null || !Guid.TryParse(subClaim.Value, out var currentPersonId))
            {
                return BadRequest("Невозможно определить пользователя из токена.");
            }
            if (roleClaim.Value.Equals(Roles.User))
                return Forbid();

            try
            {
                await _newsService.AddAsync(createNewsRequest.Title, createNewsRequest.Content, currentPersonId);
                return Ok(new CreateNewsResponse());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("delete-news/{id}")]
        public async Task<ActionResult> DeleteNews(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _newsService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
