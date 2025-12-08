using ConfHub.Core.Application.Common.Interfaces;
using ConfHub.Core.Application.News.DTO;
using ConfHub.Core.Application.News.Interfaces;
using ConfHub.Core.Application.Persons.Interfaces;

namespace ConfHub.Core.Application.News.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(INewsRepository newsRepository, IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _newsRepository = newsRepository;
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(string title, string content, Guid authorId)
        {
            Domain.Entities.News news = new Domain.Entities.News(Guid.NewGuid(), title, content, authorId, DateTime.UtcNow);
            await _newsRepository.AddAsync(news);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _newsRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Domain.Entities.News?> GetNewsByIdAsync(Guid id)
        {
            var currentNews = await _newsRepository.GetNewsByIdAsync(id);
            return currentNews;
        }

        public async Task<NewsWithAuthorListDto> GetPartOfNewsByDateTimeAsync(DateTime startDateTime, int partSize)
        {
            var currentNews = await _newsRepository.GetPartOfNewsByDateTimeAsync(startDateTime, partSize);
            if (!currentNews.News.Any())
                return new NewsWithAuthorListDto(Enumerable.Empty<NewsWithAuthorDto>(), startDateTime);

            var authorIds = currentNews.News.Select(i => i.AuthorPersonId).Distinct().ToList();
            var authors = await _personRepository.GetPersonsByIdsAsync(authorIds);
            var authorsDict = authors.ToDictionary(o => o.Id);

            return new NewsWithAuthorListDto(
                currentNews.News.Select(news => new NewsWithAuthorDto(
                Id: news.Id,
                Title: news.Title,
                Content: news.Content,
                CreatedAt: news.PublishedAt,
                Author: new AuthorDto(
                    Id: authorsDict[news.AuthorPersonId].Id,
                    Surname: authorsDict[news.AuthorPersonId].Surname,
                    Name: authorsDict[news.AuthorPersonId].Name,
                    Patronymic: authorsDict[news.AuthorPersonId].Patronymic,
                    EducationalInstitution: authorsDict[news.AuthorPersonId].EducationalInstitution,
                    JobTitle: authorsDict[news.AuthorPersonId].JobTitle,
                    City: authorsDict[news.AuthorPersonId].City,
                    IsVerified: authorsDict[news.AuthorPersonId].IsVerified
                    ))),
                currentNews.NextDateTime);
        }

        public async Task UpdateAsync(Domain.Entities.News entity)
        {
            _newsRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
